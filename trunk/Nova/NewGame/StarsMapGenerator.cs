#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This class is used to generate stars map. The approach is based on emulation 
// of two-dimensional random value with the specified probability density 
// function. After each star is placed the density function is changed 
// accordingly to reduce function.
// ===========================================================================
#endregion

namespace Nova.NewGame
{
    using System;
    using System.Collections.Generic;

    #region StarMap

    /// <summary>
    /// This class is used to generate stars map.
    /// </summary>
    /// <remarks>
    /// The approach is based on emulation 
    /// of two-dimensional random value with the specified probability density 
    /// function. After each star is placed the density function is changed 
    /// accordingly to reduce function.
    /// </remarks>
    public class StarsMapGenerator 
    {
        // the number of failed attempts to stop after
        private const int FailuresThreshold = 5000;

        // map settings
        private readonly int mapWidth;
        private readonly int mapHeight;
        private readonly int starSeparation;
        private readonly int starDensity;
        private readonly int starUniformity;
        private readonly int numPlayers;

        // non-normalized probability density function
        // values are between 0 and 1
        private readonly double[,] density;

        private readonly Random random = new Random();

        // List of stars positions int[2]; int[0] - x, int[1] - y
        private readonly List<int[]> stars = new List<int[]>();
        private readonly List<int[]> homeworlds = new List<int[]>();

        // the width and height of the frame where the density values will be updated after placing the star
        private int updateFrameSize;

        // values calculated from map parameters, which define the shape of reduce function
        private double baseDensity;
        private double maxRadius;
        private int minSeparation;

        /// <summary>
        /// Initializes a new instance of the StarsMapGenerator class.
        /// </summary>
        /// <param name="mapWidth">Width of the map in ly.</param>
        /// <param name="mapHeight">Height of the map in ly.</param>
        public StarsMapGenerator(int mapWidth, int mapHeight, int starSeparation, int starDensity, int starUniformity, int numPlayers)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            this.starSeparation = starSeparation;
            this.starDensity = starDensity;
            this.starUniformity = starUniformity;
            
            this.numPlayers = numPlayers;
            
            this.density = new double[mapWidth, mapHeight];
        }
        
        /// <summary>
        /// Generate stars and homeworlds.
        /// Note that the number of generated stars will be a random value.
        /// </summary>
        public void Generate()
        {
            // Initial uniform density
            this.SetStandardDensity();
            this.SetHomeworldReducer();
            this.PlaceHomeworlds();
            // Reset the density for Star generation
            this.SetStandardDensity();
            this.SetStandardReducer();
            // Account for already placed homeworlds in the density function.
            this.AccountHomeworlds();
            this.PlaceStars();
        }
            
        /// <summary>
        /// Sets an uniform density of 1 across the universe
        /// </summary>
        private void SetStandardDensity()
        {
            // initialize density function
            // the initial values can influence the shape of generated map
            for (int i = 0; i < this.mapWidth; ++i)
            {
                for (int j = 0; j < this.mapHeight; ++j)
                {
                    this.density[i, j] = 1.0;
                }
            }
        }
        
        /// <summary>
        /// Sets the parameteres for the density reduce function, for
        /// generating a standard StarMap.
        /// </summary>
        private void SetStandardReducer()
        {
            this.baseDensity = ((2.0 * (this.starUniformity - 1)) + (0.11 * (100 - this.starUniformity))) / 99.0;
            this.maxRadius = ((100.0 * (this.starUniformity - 1)) + (400 * (100 - this.starUniformity))) / 99.0;

            // middle value of uniformity produces low density (~ x0.63), so equalizing this a bit with border values
            double densityBalancer = ((1 * Math.Abs(50.0 - this.starUniformity)) / 50.0) + (0.63 * (1 - (Math.Abs(50.0 - this.starUniformity) / 50.0)));

            this.baseDensity *= densityBalancer;
            this.maxRadius *= densityBalancer;

            double densityApplied = (0.5 * ((this.starDensity - 1) / 99.0)) + ((2.0 * (100 - this.starDensity)) / 99.0);

            this.baseDensity *= densityApplied;
            this.maxRadius *= densityApplied;

            this.updateFrameSize = (int)Math.Ceiling(this.maxRadius);
            
            this.minSeparation = this.starSeparation;
        }
  
        /// <summary>
        /// Sets the parameters for the density reduce function, for
        /// generating homeworlds.
        /// </summary>
        private void SetHomeworldReducer()
        {   
            this.baseDensity = 1.0;
            
            this.minSeparation = (int)Math.Min(this.mapWidth, this.mapHeight) /
                (2 * ((int)Math.Floor(Math.Sqrt(this.numPlayers)) + 1));
            
            this.maxRadius = (Math.Abs(mapWidth - mapHeight) / (mapWidth + mapHeight)) *
                (this.minSeparation * this.numPlayers / (2.0 - (this.starDensity / 100)));
            
            this.updateFrameSize = (int)Math.Max(Math.Ceiling(this.maxRadius), Math.Ceiling((double)this.minSeparation));
        }

        /// <summary>
        /// Genetate a star map.
        /// </summary>
        private void PlaceStars()
        {
            while (true)
            {
                int x = 0;
                int y = 0;

                // count failed attempts to generate star
                int count = 0;
                while (true)
                {
                    x = this.random.Next(this.mapWidth);
                    y = this.random.Next(this.mapHeight);
                    double height = this.random.NextDouble();
                    if (height <= this.density[x, y])
                    {   // the star can be placed at this position
                        break;
                    }
                    count++;

                    // if we have exceeded maximum number of attempts
                    // then the map is filled with stars and we finish
                    if (count > FailuresThreshold)
                    {
                        return;
                    }
                }

                this.stars.Add(new int[] { x, y });
                UpdateDensities(x, y);
            }
        }
        
        /// <summary>
        /// Generate Homeworld locations. 
        /// </summary>
        private void PlaceHomeworlds()
        {
            int x = 0;
            int y = 0;
            double height = 0;
            
            for (int q = 0; q < this.numPlayers; ++q)
            {
                while (true)
                {
                    x = this.random.Next(this.mapWidth);
                    y = this.random.Next(this.mapHeight);
                    height = this.random.NextDouble();
                    
                    if (height <= this.density[x, y])
                    {   // the star can be placed at this position
                        break;
                    }
                }

                this.homeworlds.Add(new int[] { x, y });
                UpdateDensities(x, y);
            }    
        }
        
        /// <summary>
        /// Update Density after the star has been placed at co-ordinate (x,y).
        /// </summary>
        /// <param name="x">x ordinate of newly place star.</param>
        /// <param name="y">y ordinate of newly place star.</param>
        private void UpdateDensities(int x, int y)
        {
            for (int i = (x - (this.updateFrameSize / 2) > 0) ? (x - (this.updateFrameSize / 2)) : 0; i <= ((x + (this.updateFrameSize / 2) < this.mapWidth) ? (x + (this.updateFrameSize / 2)) : (this.mapWidth - 1)); ++i)
            {
                for (int j = (y - (this.updateFrameSize / 2) > 0) ? (y - (this.updateFrameSize / 2)) : 0; j <= ((y + (this.updateFrameSize / 2) < this.mapHeight) ? (y + (this.updateFrameSize / 2)) : (this.mapHeight - 1)); ++j)
                {
                    double d = Math.Sqrt(((x - i) * (x - i)) + ((y - j) * (y - j)));
                    this.density[i, j] -= Reduce(d);
                }
            }
        }
  
        /// <summary>
        /// This function defines the amount the density function value should be 
        /// reduced by at current point based on the distance between current point and
        /// the star.
        /// </summary>
        /// <param name="distance">Distance between the current point and the star.</param>
        /// <returns>Returning 1 means the density function value will be reduced down to zero 
        /// at the current point. Returning 0 means value will not be changed.</returns>
        private double Reduce(double distance)
        {   
            if (distance < this.minSeparation) 
            {
                return 1.0;
            }
            else if (distance < this.maxRadius)
            {
                return (this.maxRadius - distance) / (this.maxRadius - this.minSeparation) * this.baseDensity;
            }
            
            return 0.0;
        }
        
        /// <summary>
        /// Reduces the density function around already placed homeworlds.
        /// Do this before generating the rest of the StarMap.
        /// </summary>
        private void AccountHomeworlds()
        {
            foreach (int[] hwPos in this.homeworlds)
            {
                UpdateDensities(hwPos[0], hwPos[1]);
            }
        }
        
    #endregion
        
    #region Properties
        
        /// <summary>
        /// Returns(only) the list of stars as a List of int[2]; int[0] is x, int[1] is y 
        /// </summary>
        public List<int[]> Stars
        {
            get
            {
                return this.stars;
            }
            
            set{}
                
        }
        
        /// <summary>
        /// Returns(only) the list of homeworlds as a List of int[2]; int[0] is x, int[1] is y 
        /// </summary>
        public List<int[]> Homeworlds
        {
            get
            {
                return this.homeworlds;
            }
            
            set {}
        }
            
        
    #endregion
    }
}

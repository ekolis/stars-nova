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

using System;
using System.Collections.Generic;

namespace Nova.WinForms.NewGame
{
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

        // the width and height of the frame where the density values will be updated after placing the star
        private int UpdateFrameSize;

        // map settings
        private int MapWidth;
        private int MapHeight;
        private int StarSeparation;
        private int StarDensity;
        private int StarUniformity;

        // values calculated from map parameters, which define the shape of reduce function
        private double BaseDensity;
        private double MaxRadius;

        // non-normalized probability density function
        // values are between 0 and 1
        private double[,] Density;

        private Random RNG = new Random();

        // List of stars positions int[2]; int[0] - x, int[1] - y
        private List<int[]> Stars = new List<int[]>();


        /// <summary>
        /// Initializes a new instance of the StarsMapGenerator class.
        /// </summary>
        /// <param name="mapWidth">Width of the map in ly.</param>
        /// <param name="mapHeight">Height of the map in ly.</param>
        public StarsMapGenerator(int mapWidth, int mapHeight, int starSeparation, int starDensity, int starUniformity)
        {
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;

            this.StarSeparation = starSeparation;
            this.StarDensity = starDensity;
            this.StarUniformity = starUniformity;
            
#if(DEBUG)
                // Just to test that the form data has been passed in successfully - Dan 9 May 10
                System.Windows.Forms.MessageBox.Show("Star Separation = " + StarSeparation.ToString() +
                                                     " Star Density = " + StarDensity.ToString() +
                                                     " Star Uniformity = " + StarUniformity.ToString());
#endif

            Density = new double[mapWidth, mapHeight];
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Generate stars and return them as List of int[2]; int[0] - x, int[1] - y
        /// Note that the number of generated stars will be a random value.
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------------------------------
        public List<int[]> Generate()
        {
            BaseDensity = ((2.0 * (StarUniformity - 1)) + (0.11 * (100 - StarUniformity))) / 99.0;
            MaxRadius = ((100.0 * (StarUniformity - 1)) + (400 * (100 - StarUniformity))) / 99.0;

            // middle value of uniformity produces low density (~ x0.63), so equalizing this a bit with border values
            double DensityBalancer = ((1 * Math.Abs(50.0 - StarUniformity)) / 50.0) + (0.63 * (1 - (Math.Abs(50.0 - StarUniformity) / 50.0)));

            BaseDensity *= DensityBalancer;
            MaxRadius *= DensityBalancer;

            double DensityApplied = (0.5 * ((StarDensity - 1) / 99.0)) + ((2.0 * (100 - StarDensity)) / 99.0);

            BaseDensity *= DensityApplied;
            MaxRadius *= DensityApplied;

            UpdateFrameSize = (int)Math.Ceiling(MaxRadius);

            DoGeneration();
            return Stars;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function defines the amount the density function value should be 
        /// reduced by at current point based on the distance between current point and
        /// the star.
        /// </summary>
        /// <param name="distance">Distance between the current point and the star.</param>
        /// <returns>Returning 1 means the density function value will be reduced down to zero 
        /// at the current point. Returning 0 means value will not be changed.</returns>
        /// ----------------------------------------------------------------------------
        private double Reduce(double distance)
        {   
            if (distance < StarSeparation) 
            {
                return 1.0;
            }
            else if (distance < MaxRadius)
            {
                return (MaxRadius - distance) / (MaxRadius - StarSeparation) * BaseDensity;
            }
            
            return 0.0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Genetate a star map.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void DoGeneration()
        {
            // initialize density function
            // the initial values can influence the shape of generated map
            for (int i = 0; i < MapWidth; ++i)
            {
                for (int j = 0; j < MapHeight; ++j)
                {
                    Density[i, j] = 1.0;
                }
            }

            while (true)
            {
                int x = 0;
                int y = 0;

                // count failed attempts to generate star
                int count = 0;
                while (true)
                {
                    x = RNG.Next(MapWidth);
                    y = RNG.Next(MapHeight);
                    double height = RNG.NextDouble();
                    if (Density[x, y] >= height)
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

                Stars.Add(new int[] { x, y });
                UpdateDensities(x, y);

            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update Density after the star has been placed at co-ordinate (x,y).
        /// </summary>
        /// <param name="x">x ordinate of newly place star.</param>
        /// <param name="y">y ordinate of newly place star.</param>
        /// ----------------------------------------------------------------------------
        private void UpdateDensities(int x, int y)
        {
            for (int i = (x - (UpdateFrameSize / 2) > 0) ? (x - (UpdateFrameSize / 2)) : 0; i <= ((x + (UpdateFrameSize / 2) < MapWidth) ? (x + (UpdateFrameSize / 2)) : (MapWidth - 1)); ++i)
            {
                for (int j = (y - (UpdateFrameSize / 2) > 0) ? (y - (UpdateFrameSize / 2)) : 0; j <= ((y + (UpdateFrameSize / 2) < MapHeight) ? (y + (UpdateFrameSize / 2)) : (MapHeight - 1)); ++j)
                {
                    double d = Math.Sqrt(((x - i) * (x - i)) + ((y - j) * (y - j)));
                    Density[i, j] -= Reduce(d);
                }
            }
        }

    }
}

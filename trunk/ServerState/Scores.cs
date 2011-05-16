#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
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
// This module provides score data.
// ===========================================================================
#endregion

using System.Collections;

using Nova.Common;

namespace Nova.Server
{
    /// <summary>
    /// Manipulation of the turn data that is shared between the Console and GUI.
    /// </summary>
    public class Scores
    {
        private ServerState stateData;
  
        public Scores(ServerState serverState)
        {
            this.stateData = serverState;
        }
        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return a list of all scores 
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ArrayList GetScores()
        {
            ArrayList scores = new ArrayList();

            foreach (PlayerSettings player in stateData.AllPlayers)
            {
                scores.Add(GetScoreRecord(player.RaceName));
            }

            SetRanks(scores);

            return scores;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Build a <see cref="ScoreRecord"/> for a given race
        /// </summary>
        /// <param name="raceName">The name fo the race to build a <see cref="ScoreRecord"/> for.</param>
        /// <returns>A <see cref="ScoreRecord"/> for the given race.</returns>
        /// ----------------------------------------------------------------------------
        private ScoreRecord GetScoreRecord(string raceName)
        {
            double totalScore = 0;
            ScoreRecord score = new ScoreRecord();

            // ----------------------------------------------------------------------------
            // Count star-specific values
            // ----------------------------------------------------------------------------

            int starBases = 0;
            int resources = 0;

            foreach (Star star in stateData.AllStars.Values)
            {

                if (star.Owner == raceName)
                {
                    score.Planets++;
                    resources += (int)star.ResourcesOnHand.Energy;

                    if (star.Starbase != null)
                    {
                        starBases++;
                        totalScore += 3;
                    }

                    totalScore += star.Colonists / 100000;
                }
            }

            score.Starbases = starBases;
            score.Resources = resources;
            totalScore += resources / 30;

            // ----------------------------------------------------------------------------
            // Count ship specific values
            // ----------------------------------------------------------------------------

            int unarmedShips = 0;
            int escortShips = 0;
            int capitalShips = 0;


            foreach (Fleet fleet in stateData.AllFleets.Values)
            {

                if (fleet.Owner == raceName)
                {
                    foreach (Ship ship in fleet.FleetShips)
                    {
                        if (ship.HasWeapons == false)
                        {
                            unarmedShips++;
                            totalScore += 0.5;
                        }
                        else
                        {
                            if (ship.PowerRating < 2000)
                            {
                                escortShips++;
                                totalScore += 2;
                            }
                            else
                            {
                                capitalShips++;
                            }
                        }
                    }
                }
            }

            score.UnarmedShips = unarmedShips;
            score.EscortShips = escortShips;
            score.CapitalShips = capitalShips;

            if (capitalShips != 0)
            {
                int stars = score.Planets;
                totalScore += (8 * capitalShips * stars) / (capitalShips + stars);
            }

            // ----------------------------------------------------------------------------
            // Single instance values
            // ----------------------------------------------------------------------------

            score.Race = raceName;
            if (stateData.AllTechLevels.Contains(raceName))
            {
                score.TechLevel = (int)stateData.AllTechLevels[raceName];

                if (score.TechLevel < 4) totalScore += 1;
                if (score.TechLevel > 9) totalScore += 4;
                if (score.TechLevel > 3 && score.TechLevel < 7) totalScore += 2;
                if (score.TechLevel > 6 && score.TechLevel < 10) totalScore += 3;
            }

            score.Score = (int)totalScore;

            return score;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the rank for all races.
        /// </summary>
        /// <param name="scores">An <see cref="ArrayList"/> of <see cref="ScoreRecord"/>s.</param>
        /// ----------------------------------------------------------------------------
        private void SetRanks(ArrayList scores)
        {
            scores.Sort();

            int count = 1;
            foreach (ScoreRecord score in scores)
            {
                score.Rank = count;
                count++;
            }
        }
    }
}



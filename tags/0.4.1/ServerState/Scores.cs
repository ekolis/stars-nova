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
        private static ServerState StateData = null;

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return a list of all scores 
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static ArrayList GetScores()
        {
            ArrayList scores = new ArrayList();

            foreach (PlayerSettings player in ServerState.Data.AllPlayers)
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
        private static ScoreRecord GetScoreRecord(string raceName)
        {
            double TotalScore = 0;
            ScoreRecord Score = new ScoreRecord();
            StateData = ServerState.Data;

            // ----------------------------------------------------------------------------
            // Count star-specific values
            // ----------------------------------------------------------------------------

            int starBases = 0;
            int resources = 0;

            foreach (Star star in StateData.AllStars.Values)
            {

                if (star.Owner == raceName)
                {
                    Score.Planets++;
                    resources += (int)star.ResourcesOnHand.Energy;

                    if (star.Starbase != null)
                    {
                        starBases++;
                        TotalScore += 3;
                    }

                    TotalScore += star.Colonists / 100000;
                }
            }

            Score.Starbases = starBases;
            Score.Resources = resources;
            TotalScore += resources / 30;

            // ----------------------------------------------------------------------------
            // Count ship specific values
            // ----------------------------------------------------------------------------

            int unarmedShips = 0;
            int escortShips = 0;
            int capitalShips = 0;


            foreach (Fleet fleet in StateData.AllFleets.Values)
            {

                if (fleet.Owner == raceName)
                {
                    foreach (Ship ship in fleet.FleetShips)
                    {
                        if (ship.HasWeapons == false)
                        {
                            unarmedShips++;
                            TotalScore += 0.5;
                        }
                        else
                        {
                            if (ship.PowerRating < 2000)
                            {
                                escortShips++;
                                TotalScore += 2;
                            }
                            else
                            {
                                capitalShips++;
                            }
                        }
                    }
                }
            }

            Score.UnarmedShips = unarmedShips;
            Score.EscortShips = escortShips;
            Score.CapitalShips = capitalShips;

            if (capitalShips != 0)
            {
                int stars = Score.Planets;
                TotalScore += (8 * capitalShips * stars) / (capitalShips + stars);
            }

            // ----------------------------------------------------------------------------
            // Single instance values
            // ----------------------------------------------------------------------------

            Score.Race = raceName;
            if (StateData.AllTechLevels.Contains(raceName))
            {
                Score.TechLevel = (int)StateData.AllTechLevels[raceName];

                if (Score.TechLevel < 4) TotalScore += 1;
                if (Score.TechLevel > 9) TotalScore += 4;
                if (Score.TechLevel > 3 && Score.TechLevel < 7) TotalScore += 2;
                if (Score.TechLevel > 6 && Score.TechLevel < 10) TotalScore += 3;
            }

            Score.Score = (int)TotalScore;

            return Score;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Set the rank for all races.
        /// </summary>
        /// <param name="scores">An <see cref="ArrayList"/> of <see cref="ScoreRecord"/>s.</param>
        /// ----------------------------------------------------------------------------
        private static void SetRanks(ArrayList scores)
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



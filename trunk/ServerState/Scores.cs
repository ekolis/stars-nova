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

namespace Nova.Server
{
    using System.Collections.Generic;
    using Nova.Common;
    
    /// <summary>
    /// Class to provide score data.
    /// </summary>
    public class Scores
    {
        private ServerData serverState;
  
        public Scores(ServerData serverState)
        {
            this.serverState = serverState;
        }
        
        /// <summary>
        /// Return a list of all scores.
        /// </summary>
        public List<ScoreRecord> GetScores()
        {
            List<ScoreRecord> scores = new List<ScoreRecord>();

            foreach (EmpireData empire in serverState.AllEmpires.Values)
            {
                scores.Add(GetScoreRecord(empire.Id));
            }

            SetRanks(scores);

            return scores;
        }

        /// <summary>
        /// Build a <see cref="ScoreRecord"/> for a given race.
        /// </summary>
        /// <param name="raceName">The name fo the race to build a <see cref="ScoreRecord"/> for.</param>
        /// <returns>A <see cref="ScoreRecord"/> for the given race.</returns>
        private ScoreRecord GetScoreRecord(int empireId)
        {
            double totalScore = 0;
            ScoreRecord score = new ScoreRecord();

            // ----------------------------------------------------------------------------
            // Count star-specific values
            // ----------------------------------------------------------------------------

            int starBases = 0;
            int resources = 0;

            foreach (Star star in serverState.AllStars.Values)
            {
                if (star.Owner == empireId)
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
            // Count ship specific values.
            // ----------------------------------------------------------------------------

            int unarmedShips = 0;
            int escortShips = 0;
            int capitalShips = 0;


            foreach (Fleet fleet in serverState.AllFleets.Values)
            {
                if (fleet.Owner == empireId)
                {
                    foreach (ShipToken token in fleet.Tokens)
                    {
                        if (token.Design.HasWeapons == false)
                        {
                            unarmedShips++;
                            totalScore += 0.5;
                        }
                        else
                        {
                            if (token.Design.PowerRating < 2000)
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
            // Single instance values.
            // ----------------------------------------------------------------------------

            score.EmpireId = empireId;
            if (serverState.AllTechLevels.ContainsKey(empireId))
            {
                score.TechLevel = serverState.AllTechLevels[empireId];

                if (score.TechLevel < 4)
                {
                    totalScore += 1;
                }
                if (score.TechLevel > 9)
                {
                    totalScore += 4;
                }
                if (score.TechLevel > 3 && score.TechLevel < 7)
                {
                    totalScore += 2;
                }
                if (score.TechLevel > 6 && score.TechLevel < 10)
                {
                    totalScore += 3;
                }
            }

            score.Score = (int)totalScore;

            return score;
        }

        /// <summary>
        /// Set the rank for all races.
        /// </summary>
        private void SetRanks(List<ScoreRecord> scores)
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



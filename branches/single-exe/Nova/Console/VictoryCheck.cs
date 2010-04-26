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
// Check for a victor (doesn't mean the end of a game, though)
// ===========================================================================
#endregion

using System;
using System.Collections;

using NovaCommon;
using NovaServer;

namespace Nova.Console
{
    /// <summary>
    /// Check for victor.
    /// </summary>
    public static class VictoryCheck
    {

        static ServerState StateData = ServerState.Data;
        static bool messageSent = false;

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check for victor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public static void Victor()
        {

            // check for last man standing - doesn't matter the year
            ArrayList remainingPlayers = new ArrayList();
            foreach (Star star in StateData.AllStars.Values)
            {
                if (star.Owner == "") continue;
                if (!remainingPlayers.Contains(star.Owner))
                {
                    remainingPlayers.Add(star.Owner);
                }
            }

            if (remainingPlayers.Count == 1)
            {
                Race race = StateData.AllRaces[remainingPlayers[0]] as Race;
                Message message = new Message();
                message.Audience = "*";
                message.Text = "The " + race.PluralName +
                                   " have won the game";
                ServerState.Data.AllMessages.Add(message);
                return;
            }
            else
            {


                int gameTime = StateData.TurnYear - 2100;

                if (gameTime < GameSettings.Data.MinimumGameTime)
                {
                    return;
                }

                foreach (Race race in StateData.AllRaces.Values)
                {
                    int targetsMet = 0;

                    targetsMet += OccupiedPlanets(race.Name);
                    targetsMet += AttainedTechLevel(race.Name);
                    targetsMet += ScoreExceeded(race.Name);
                    targetsMet += ProductionCapacity(race.Name);
                    targetsMet += CapitalShips(race.Name);
                    targetsMet += HighestScore(race.Name, gameTime);
                    targetsMet += ExceedsSecondPlace(race.Name);

                    if (messageSent == false &&
                        targetsMet >= GameSettings.Data.TargetsToMeet)
                    {

                        messageSent = true;
                        Message message = new Message();
                        message.Audience = "*";
                        message.Text = "The " + race.PluralName +
                                           " have won the game";
                        ServerState.Data.AllMessages.Add(message);
                        return;
                    }
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player owns the required percentage of planets.
        /// </summary>
        /// <param name="raceName">Name of the race to check.</param>
        /// <returns>1 if the required number of planets is occupied, otherwise 0.</returns>
        /// ----------------------------------------------------------------------------
        private static int OccupiedPlanets(string raceName)
        {
            // See if this option has been turned on

            if (GameSettings.Data.PlanetsOwned.IsChecked == false)
            {
                return 0;
            }

            int starsOwned = 0;

            foreach (Star star in StateData.AllStars.Values)
            {
                if (star.Owner == raceName)
                {
                    starsOwned++;
                }
            }

            int percentage = (starsOwned * 100)
                           / StateData.AllStars.Count;

            if (percentage >= GameSettings.Data.PlanetsOwned.NumericValue)
            {
                return 1;
            }

            return 0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player has attained the required tech level in the
        /// specified number of fields.
        /// </summary>
        /// <param name="raceName">Name of the race to check.</param>
        /// <returns>1 if race has attained the required tech, otherwise 0.</returns>
        /// ----------------------------------------------------------------------------
        private static int AttainedTechLevel(string raceName)
        {
            // See if this tech level option has been turned on

            if (GameSettings.Data.TechLevels.IsChecked == false)
            {
                return 0;
            }

            int targetLevel = GameSettings.Data.TechLevels.NumericValue;
            int numberOfFields = GameSettings.Data.NumberOfFields.NumericValue;

            // See if a number of fields sub-option has been turned on.
            // If it hasn't, just use one.

            if (GameSettings.Data.NumberOfFields.IsChecked == false)
            {
                numberOfFields = 1;
            }

            int highestFields = 0;
            TechLevel raceTechLevels = StateData.AllTechLevels[raceName]
                                       as TechLevel;

            foreach (int level in raceTechLevels)
            {
                if (level >= targetLevel)
                    highestFields++;
            }

            if (highestFields >= numberOfFields) return 1;
            return 0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player has exceeded the required score.
        /// </summary>
        /// <param name="raceName">Name of race to check.</param>
        /// <returns>1 if the required score has been met, otherwise 0</returns>
        /// ----------------------------------------------------------------------------
        private static int ScoreExceeded(string raceName)
        {
            if (GameSettings.Data.TotalScore.IsChecked == false)
            {
                return 0;
            }

            ArrayList allScores = Scores.GetScores();

            foreach (ScoreRecord scoreDetail in allScores)
            {
                if (scoreDetail.Race == raceName)
                {
                    if (scoreDetail.Score >= GameSettings.Data.TotalScore.NumericValue)
                    {
                        return 1;
                    }
                    break;
                }
            }

            return 0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player has exceeded the required production capacity (in
        /// K resources).
        /// </summary>
        /// <param name="raceName">Name of the race to check.</param>
        /// <returns>
        /// 1 if the required production capacity has been met, otherwise 0
        /// </returns>
        /// ----------------------------------------------------------------------------
        private static int ProductionCapacity(string raceName)
        {
            if (GameSettings.Data.ProductionCapacity.IsChecked == false)
            {
                return 0;
            }

            int capacity = 0;

            foreach (Star star in StateData.AllStars)
            {
                if (star.Owner == raceName)
                {
                    capacity += (int)star.ResourcesOnHand.Energy / 1000;
                }
            }

            if (capacity >= GameSettings.Data.ProductionCapacity.NumericValue)
            {
                return 1;
            }

            return 0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player has met the required number of capital ships
        /// </summary>
        /// <param name="raceName">Name of the race to check.</param>
        /// <returns>1 if the required number of capital ships has been met, otherwise 0</returns>
        /// ----------------------------------------------------------------------------
        private static int CapitalShips(string raceName)
        {
            if (GameSettings.Data.CapitalShips.IsChecked == false)
            {
                return 0;
            }

            ArrayList allScores = Scores.GetScores();

            foreach (ScoreRecord scoreDetail in allScores)
            {
                if (scoreDetail.Race == raceName)
                {
                    if (scoreDetail.CapitalShips >=
                        GameSettings.Data.CapitalShips.NumericValue)
                    {
                        return 1;
                    }
                    break;
                }
            }

            return 0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player has the highest score after the specified number
        /// of years
        /// </summary>
        /// <param name="raceName">Name of the race to check.</param>
        /// <param name="years">Number of game years/turns that have passed.</param>
        /// <returns>1 if this race has the highest score, otherwise 0</returns>
        /// ----------------------------------------------------------------------------
        private static int HighestScore(string raceName, int years)
        {
            if (GameSettings.Data.HighestScore.IsChecked == false)
            {
                return 0;
            }

            if (years < GameSettings.Data.HighestScore.NumericValue)
            {
                return 0;
            }

            ArrayList allScores = Scores.GetScores();
            int raceScore = 0;

            foreach (ScoreRecord scoreDetail in allScores)
            {
                if (scoreDetail.Race == raceName)
                {
                    raceScore = scoreDetail.Score;
                }
                break;
            }

            foreach (ScoreRecord scoreDetail in allScores)
            {
                if (scoreDetail.Score > raceScore)
                {
                    return 0;
                }
            }

            return 1;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Check to see if the player has exceeded the second place score by the
        /// specified amount.
        /// </summary>
        /// <param name="raceName">Name of the race to check.</param>
        /// <returns>1 if the second place score is exceeded by the required amount, 0 otherwise.</returns>
        /// ----------------------------------------------------------------------------
        private static int ExceedsSecondPlace(string raceName)
        {
            if (GameSettings.Data.CapitalShips.IsChecked == false)
            {
                return 0;
            }

            ArrayList allScores = Scores.GetScores();

            // Get our score and the second place score.

            int ourScore = 0;
            int secondPlaceScore = 0;

            foreach (ScoreRecord scoreDetail in allScores)
            {
                if (scoreDetail.Race == raceName)
                {
                    ourScore = scoreDetail.Score;
                }
                else if (scoreDetail.Rank == 2)
                {
                    secondPlaceScore = scoreDetail.Score;
                }
            }

            secondPlaceScore *= GameSettings.Data.SecondPlaceScore.NumericValue;

            if (ourScore > secondPlaceScore) return 1;
            return 0;
        }


    }
}

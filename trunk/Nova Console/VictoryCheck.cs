// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Check for a victor (doesn't mean the end of a game, though)
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using NovaCommon;

namespace NovaConsole
{
   public static class VictoryCheck
   {

      static ConsoleState StateData   = ConsoleState.Data;
      static bool         messageSent = false;

// ============================================================================
// Check for victor.
// ============================================================================

      public static void Victor()
      {

          // check for last man standing - doesn't matter the year
          ArrayList remainingPlayers = new ArrayList();
          foreach (Star star in StateData.AllStars.Values)
          {
              if (star.Owner == "") continue;
              if ( ! remainingPlayers.Contains(star.Owner))
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
              GlobalTurn.Data.Messages.Add(message);
              return;
          }
          else
          {


              int gameTime = StateData.TurnYear - 2100;

              if (gameTime < StateData.MinimumGameTime)
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
                      targetsMet >= StateData.TargetsToMeet)
                  {

                      messageSent = true;
                      Message message = new Message();
                      message.Audience = "*";
                      message.Text = "The " + race.PluralName +
                                         " have won the game";
                      GlobalTurn.Data.Messages.Add(message);
                      return;
                  }
              }
          }
      }
         

// ============================================================================
// Check to see if the player owns the required percentage of planets.
// ============================================================================

      private static int OccupiedPlanets(string raceName)
      {
         // See if this option has been turned on

         if (StateData.PlanetsOwned.IsChecked == false) {
            return 0;
         }

         int starsOwned = 0;

         foreach (Star star in StateData.AllStars.Values) {
            if (star.Owner == raceName) {
               starsOwned++;
            }
         }

         int percentage = (starsOwned * 100) 
                        / StateData.AllStars.Count;

         if (percentage >= StateData.PlanetsOwned.NumericValue) {
            return 1;
         }

         return 0;
      }


// ============================================================================
// Check to see if the player has attained the required tech level in the
// specified number of fields.
// ============================================================================

      private static int AttainedTechLevel(string raceName)
      {
         // See if this tech level option has been turned on

         if (StateData.TechLevels.IsChecked == false) {
            return 0;
         }

         int targetLevel     = StateData.TechLevels.NumericValue;
         int numberOfFields  = StateData.NumberOfFields.NumericValue;

         // See if a number of fields sub-option has been turned on.
         // If it hasn't, just use one.

         if (StateData.NumberOfFields.IsChecked == false) {
            numberOfFields = 1;
         }

         int       highestFields  = 0;
         TechLevel raceTechLevels = StateData.AllTechLevels[raceName]
                                    as TechLevel;

         foreach (string key in TechLevel.ResearchKeys) { 
             if ((int) raceTechLevels.TechValues[key] >= targetLevel){
                 highestFields++;
             }
         }

         if (highestFields >= numberOfFields) return 1;
         return 0;
   }

// ============================================================================
// Check to see if the player has exceeded the required score.
// ============================================================================

      private static int ScoreExceeded(string raceName)
      {
         if (StateData.TotalScore.IsChecked == false) {
            return 0;
         }

         ArrayList allScores = Scores.GetScores();

         foreach (ScoreRecord scoreDetail in allScores) {
            if (scoreDetail.Race == raceName) {
               if (scoreDetail.Score >= StateData.TotalScore.NumericValue) {
                  return 1;
               }
               break;
            }
         }
         
         return 0;
      }

// ============================================================================
// Check to see if the player has exceeded the required production capacity (in
// K resources).
// ============================================================================

      private static int ProductionCapacity(string raceName)
      { 
         if (StateData.ProductionCapacity.IsChecked == false) {
            return 0;
         }

         int capacity = 0;

         foreach (Star star in StateData.AllStars) {
            if (star.Owner == raceName) {
               capacity += (int) star.ResourcesOnHand.Energy / 1000;
            }
         }

         if (capacity >= StateData.ProductionCapacity.NumericValue) {
            return 1;
         }

         return 0;
      }



// ============================================================================
// Check to see if the player has met the required number of capital ships
// ============================================================================

      private static int CapitalShips(string raceName)
      {
         if (StateData.CapitalShips.IsChecked == false) {
            return 0;
         }

         ArrayList allScores = Scores.GetScores();

         foreach (ScoreRecord scoreDetail in allScores) {
            if (scoreDetail.Race == raceName) {
               if (scoreDetail.CapitalShips >= 
                   StateData.CapitalShips.NumericValue) {
                  return 1;
               }
               break;
            }
         }
         
         return 0;
      }


// ============================================================================
// Check to see if the player has the highest score after the specified number
// of years
// ============================================================================

      private static int HighestScore(string raceName, int years)
      {
         if (StateData.HighestScore.IsChecked == false) {
            return 0;
         }

         if (years < StateData.HighestScore.NumericValue) {
            return 0;
         }

         ArrayList allScores = Scores.GetScores();
         int       raceScore = 0;

         foreach (ScoreRecord scoreDetail in allScores) {
            if (scoreDetail.Race == raceName) {
               raceScore = scoreDetail.Score;
            }
            break;
         }
         
         foreach (ScoreRecord scoreDetail in allScores) {
            if (scoreDetail.Score > raceScore) {
               return 0;
            }
         }

         return 1;
      }


// ============================================================================
// Check to see if the player has exceeded the second place score by the
// specified amount.
// ============================================================================

      private static int ExceedsSecondPlace(string raceName)
      {
         if (StateData.CapitalShips.IsChecked == false) {
            return 0;
         }

         ArrayList allScores = Scores.GetScores();

         // Get our score and the second place score.

         int ourScore         = 0;
         int secondPlaceScore = 0;

         foreach (ScoreRecord scoreDetail in allScores) {
            if (scoreDetail.Race == raceName) {
               ourScore = scoreDetail.Score;
            }
            else if (scoreDetail.Rank == 2) {
               secondPlaceScore = scoreDetail.Score;
            }
         }

         secondPlaceScore *= StateData.SecondPlaceScore.NumericValue;

         if (ourScore > secondPlaceScore) return 1;
         return 0;
      }


   }
}

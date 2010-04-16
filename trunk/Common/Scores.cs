// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module provides score data.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System.Collections;
using System;


// ============================================================================
// Manipulation of the turn data that is shared between the Console and GUI.
// ============================================================================

namespace NovaConsole
{
   public class Score
   {
      private static ConsoleState    StateData = null;
      private static GlobalTurn      TurnData  = null;


// ============================================================================
// Return a list of all scores 
// ============================================================================

      private static ArrayList GetScores()
      {
         ArrayList scores   = new ArrayList();

         foreach (Race race in ConsoleState.Data.AllRaces.Values) {
            scores.Add(GetScoreRecord(race.Name));
         }

         SetRanks(scores);

         return scores;
      }


// ============================================================================
// Build a score record for a particular race
// ============================================================================

      private static ScoreRecord GetScoreRecord(string raceName)
      {
         double TotalScore = 0;
         ScoreRecord Score = new ScoreRecord();
         StateData         = ConsoleState.Data;
         
// ----------------------------------------------------------------------------
// Count star-specific values
// ----------------------------------------------------------------------------

         int starBases = 0;
         int resources = 0;

         foreach (Star star in StateData.AllStars.Values) {

            if (star.Owner == raceName) {
               Score.Planets++;
               resources += (int) star.ResourcesOnHand.Energy;
            
               if (star.Starbase != null) {
                  starBases++;
                  TotalScore += 3;
               }
            
               TotalScore += star.Colonists / 100000;
            }
         }

         Score.Starbases = starBases;
         Score.Resources = resources;
         TotalScore     += resources / 30;

// ----------------------------------------------------------------------------
// Count ship specific values
// ----------------------------------------------------------------------------

         int unarmedShips = 0;
         int escortShips  = 0;
         int capitalShips = 0;


         foreach (Fleet fleet in StateData.AllFleets.Values) {

            if (fleet.Owner == raceName) {
               foreach (Ship ship in fleet.FleetShips) {
                  if (ship.HasWeapons == false) {
                     unarmedShips++;
                     TotalScore += 0.5;
                  }
                  else {
                     if (ship.PowerRating < 2000) {
                        escortShips++;
                        TotalScore += 2;
                     }
                     else {
                        capitalShips++;
                     }
                  }
               }
            }
         }

         Score.UnarmedShips = unarmedShips;
         Score.EscortShips  = escortShips;
         Score.CapitalShips = capitalShips;

         if (capitalShips != 0) {
            int stars   = Score.Planets;
            TotalScore += (8 * capitalShips * stars) / (capitalShips + stars);
         }

// ----------------------------------------------------------------------------
// Single instance values
// ----------------------------------------------------------------------------

         Score.Race      = raceName;
         Score.TechLevel = (int) StateData.AllTechLevels[raceName];

         if (Score.TechLevel < 4) TotalScore += 1;
         if (Score.TechLevel > 9) TotalScore += 4;
         if (Score.TechLevel > 3 && Score.TechLevel < 7)  TotalScore += 2;
         if (Score.TechLevel > 6 && Score.TechLevel < 10) TotalScore += 3;

         Score.Score = (int) TotalScore;

         return Score;
      }


// ============================================================================
// Set the rank for all races.
// ============================================================================

      private static void SetRanks(ArrayList scores)
      {
         scores.Sort();

          int count = 1;
          foreach (ScoreRecord score in scores) {
            score.Rank = count;
            count++;
         }
      }
   }
}



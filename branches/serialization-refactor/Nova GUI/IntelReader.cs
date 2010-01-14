// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module is invoked (once) when a new turn has been received from the
// Nova Console. All the appropriate fields in the GUI state data are updated
// that are relevant to the player's selected race.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using NovaCommon;

namespace Nova
{
 
// ============================================================================
// Class to deal with the input turn data
// ============================================================================

   public static class IntelReader
   {
      private static Intel TurnData  = null;
      private static GuiState   StateData = null;

      /// <summary>
      /// ReadIntel processes the console/server generated intel data for this turn. 
      /// This processing is only performed once when a new turn is received.
      /// </summary>
      public static void ReadIntel()
      {
         StateData = GuiState.Data;
         TurnData  = StateData.InputTurn;

         StateData.Messages.Clear();
         
         DetermineOrbitingFleets();
         DeterminePlayerStars();
         DeterminePlayerFleets();

         ProcessMessages();
         ProcessFleets();
         ProcessReports();
         ProcessResearch();
      }


// ============================================================================
// ReadIntel Messages
//
// Run through the full list of messages and populate the message store in the
// state data with the messages relevant to the player's selected race. The
// actual message control will be populated within the main window
// initialisation.
// ============================================================================

      private static void ProcessMessages()
      {
         foreach (Message message in TurnData.Messages) {
            if ((message.Audience == GuiState.Data.RaceName) ||
                (message.Audience == "*")) {
               StateData.Messages.Add(message);
            }
         }
      }


// ============================================================================
// So that we can put an indication of fleets orbiting a star run through all
// the fleets and, if they are in orbit around a star, set the OrbitingFleets
// flag in the star.
// ============================================================================

      private static void DetermineOrbitingFleets()
      {
         foreach (Star star in TurnData.AllStars.Values) {
            star.OrbitingFleets = false;
         }

         foreach (Fleet fleet in TurnData.AllFleets.Values) {
            if (fleet.InOrbit != null && fleet.Type != "Starbase") {
               Star star           = fleet.InOrbit;
               star.OrbitingFleets = true;
            }
         }
      }


// ============================================================================
// ReadIntel Reports
//
// Advance the age of all star reports by one year. Then, if a star is owned by
// us and has colonists bring the report up to date (just by creating a new
// report).
// ============================================================================

      private static void ProcessReports()
      {
         foreach (StarReport report in StateData.StarReports.Values) {
            report.Age++;
         }

         foreach (Star star in StateData.PlayerStars) {
            if (star.Colonists != 0) {
               StateData.StarReports[star.Name] = new StarReport(star);
            }
         }
      }


// ============================================================================
// ReadIntel Fleet Reports
// ============================================================================

      private static void ProcessFleets()
      {
         foreach (Fleet fleet in StateData.PlayerFleets) {

            if ((fleet.InOrbit != null) && (fleet.LongRangeScan != 0)) {
               Star star = fleet.InOrbit;
               StateData.StarReports[star.Name] = new StarReport(star);
            }
                  
            if (fleet.ShortRangeScan != 0) {
               foreach (Star star in TurnData.AllStars.Values) {
                  if (PointUtilities.Distance(star.Position, fleet.Position)
                      < fleet.ShortRangeScan) {
                     StateData.StarReports[star.Name] = new StarReport(star);
                  }
               }
            }
         }
      }
      

// ============================================================================
// ReadIntel Research
//
// Do the research for this year. Research is performed locally once per turn.
// ============================================================================

      private static void ProcessResearch()
      {
         string area      = StateData.ResearchTopic;
         int areaResource = (int) StateData.ResearchResources.TechValues[area];
         int areaLevel    = (int) StateData.ResearchLevel.TechValues[area];
         areaResource    += (int) StateData.ResearchAllocation;

         StateData.ResearchAllocation                 = 0;
         StateData.ResearchResources.TechValues[area] = areaResource;

         while (true) {
            if (areaResource >= Research.Cost(areaLevel + 1)) {
               areaLevel++;
               ReportLevelUpdate(area, areaLevel);
            }
            else {
               break;
            }
         }
      }


// ============================================================================
// Report an update in tech level and any new components that have became
// available.
// ============================================================================

      private static void ReportLevelUpdate(string area, int level)
      {
          Message techAdvanceMessage =
              new Message(GuiState.Data.RaceName, null, "Your race has advanced to Tech Level "
              + level + " in the " + StateData.ResearchTopic + " field");
          StateData.Messages.Add(techAdvanceMessage);

          Hashtable allComponents = AllComponents.Data.Components;
          TechLevel oldResearchLevel = StateData.ResearchLevel;
          TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

          newResearchLevel.TechValues[area] = level;

          foreach (Component component in allComponents.Values)
          {
              if (component.RequiredTech > oldResearchLevel &&
                  component.RequiredTech <= newResearchLevel)
              {

                  RaceComponents.Add(component, true);
              }
          }

          StateData.ResearchLevel = newResearchLevel;
      }


// ============================================================================
// Determine the fleets owned by the player (this is a convenience function so
// that buttons such as "Next" and "Previous" on the ship detail panel are easy
// to code,
// ============================================================================

      private static void DeterminePlayerFleets()
      {
         StateData.PlayerFleets.Clear();

         foreach (Fleet fleet in TurnData.AllFleets.Values) {
            if (fleet.Owner == StateData.RaceName) {
               if (fleet.Type != "Starbase") {
                  StateData.PlayerFleets.Add(fleet);
               }
            }
         }
      }


// ============================================================================
// Determine the star systems owned by the player (this is a convenience
// function so that buttons such as "Next" and "Previous" on the star detail
// panel are easy to code,
// ============================================================================

      private static void DeterminePlayerStars()
      {
         StateData.PlayerStars.Clear();

         foreach (Star star in TurnData.AllStars.Values) {
            if (star.Owner == StateData.RaceName) {
               StateData.PlayerStars.Add(star);
            }
         }
      }

   }
}

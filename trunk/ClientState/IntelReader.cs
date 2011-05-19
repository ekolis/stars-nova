#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 The Stars-Nova Project
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
// This module is invoked (once) when a new turn has been received from the
// Nova Console. All the appropriate fields in the GUI state data are updated
// that are relevant to the player's selected race.
// ===========================================================================
#endregion

#region Using Statements

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using Nova.Common;
using Nova.Common.Components;
using Nova.Common.DataStructures;

#endregion

namespace Nova.Client
{

   public static class IntelReader
   {
      private static Intel turnData;
      private static ClientState stateData;

      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Read and process the <RaceName>.intel generated by the Nova
      /// Console. 
      /// This file must be present before the GUI will run, since it
      /// contains key data, such as the race name, as well as any information about
      /// events that happend in the previous game year (battles, mine hits, etc. 
      /// It is also used for a kind of boot strapping process as there may or may not
      /// be a <RaceName>.state file to load:
      /// 1. open the .intel and determine the race, and hence the name of any .state file.
      /// 2. open the .state file, if any. This contains any historical information, 
      ///    and is used to reconstruct the ClientState.Data (we can create a new one with 
      ///    no history if required, such as on the first turn)
      /// 3. process the .intel file to update the ClientState.Data
      ///
      /// Note that this file is not read again after the first time a new turn is
      /// received. Once fully loaded all further processing is done using 
      /// ClientState.Data (which is subsequently used to generate <RaceName>.orders.
      /// </summary>
      /// <param name="turnFileName">Path and file name of the RaceName.intel file.</param>
      /// ----------------------------------------------------------------------------
      public static void ReadIntel(string turnFileName)
      {
          if (!File.Exists(turnFileName))
          {
              Report.FatalError("The Nova GUI cannot start unless a turn file is present");
          }

          using (Stream turnFile = new FileStream(turnFileName, FileMode.Open))
          {
              XmlDocument xmldoc = new XmlDocument();

              xmldoc.Load(turnFile);
              Intel newIntel = new Intel(xmldoc);

              // check this is a new turn, not the one just played
              if (newIntel.TurnYear != ClientState.Data.TurnYear)
              {
                  ClientState.Data.RaceName = newIntel.MyRace.Name;
                  ClientState.Data.GameFolder = Path.GetDirectoryName(turnFileName);
                  ClientState.Restore();
                  ClientState.Data.InputTurn = newIntel;
                  ProcessIntel();

              }
              else
              {
                  // exit without saving any files
                  throw new System.Exception("Turn Year missmatch");
              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// This function processes the ClientState.Data.TurnData for this turn
      /// and updates the ClientState.Data.
      /// </summary>
      /// ----------------------------------------------------------------------------
      public static void ProcessIntel()
      {
         stateData = ClientState.Data;

          // copy the raw data from the intel to StateData
         turnData = stateData.InputTurn;
         stateData.TurnYear = turnData.TurnYear;
         stateData.PlayerRace = turnData.MyRace;

          // Clear old turn data from StateData
         stateData.DeletedFleets.Clear();
         stateData.DeletedDesigns.Clear();
         stateData.Messages.Clear();

          // fix object references after loading
         LinkIntelReferences();

          // Process the new intel
         DetermineOrbitingFleets();
         DeterminePlayerStars();
         DeterminePlayerFleets();

         ProcessMessages();
         ProcessFleets();
         ProcessReports();
         ProcessResearch();
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// When intel is loaded from file, objects may contain references to other objects.
      /// As these may be loaded in any order (or be cross linked) it is necessary to tidy
      /// up these references once the file is fully loaded and all objects exist.
      /// In most cases a placeholder object has been created with the Name set from the file,
      /// and we need to find the actual reference using this Name.
      /// Objects can't do this themselves as they don't have access to the state data, 
      /// so we do it here.
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void LinkIntelReferences()
      {
          Intel turnData = ClientState.Data.InputTurn;

          // HullModule reference to a component
          foreach (Design design in turnData.AllDesigns.Values)
          {
              if (design.Type.ToLower() == "ship" || design.Type.ToLower() == "starbase")
              {
                  ShipDesign ship = design as ShipDesign;
                  foreach (HullModule module in ((Hull)ship.ShipHull.Properties["Hull"]).Modules)
                  {
                      if (module.AllocatedComponent != null && module.AllocatedComponent.Name != null)
                      {
                          AllComponents.Data.Components.TryGetValue(module.AllocatedComponent.Name, out module.AllocatedComponent);
                      }
                  }
              }
          }

          // Fleet reference to Star
          foreach (Fleet fleet in turnData.AllFleets.Values)
          {
              if (fleet.InOrbit != null)
              {
                  fleet.InOrbit = turnData.AllStars[fleet.InOrbit.Name];
              }
              // Ship reference to Design
              foreach (Ship ship in fleet.FleetShips)
              {
                  // FIXME (priority 4) - this way of forming the key is a kludge
                  ship.DesignUpdate(turnData.AllDesigns[ship.Owner + "/" + ship.DesignName] as ShipDesign);
              }
          }
          // Star reference to Race
          // Star reference to Fleet (starbase)
          foreach (Star star in turnData.AllStars.Values)
          {

              if (star.ThisRace != null)
              {
                  if (star.Owner == stateData.PlayerRace.Name)
                  {
                      star.ThisRace = stateData.PlayerRace;
                  }
                  else
                  {
                      star.ThisRace = null;
                  }
              }

              if (star.Starbase != null)
              {
                  star.Starbase = turnData.AllFleets[star.Owner + "/" + star.Starbase.FleetID];
              }
              
          }


          // link the ship designs in battle reports to the stacks
          foreach (BattleReport battle in ClientState.Data.InputTurn.Battles)
          {
              foreach (Fleet fleet in battle.Stacks.Values)
              {
                  foreach (Ship ship in fleet.FleetShips)
                  {
                      if (ClientState.Data.KnownEnemyDesigns.ContainsKey(ship.DesignName))
                      {
                          // FIXME (priority 4) - this way of forming the key is a kludge
                          ship.DesignUpdate((ShipDesign)ClientState.Data.KnownEnemyDesigns[ship.Owner + "/" + ship.DesignName]);
                      }
                      else
                      {
                          // FIXME (priority 4) - this way of forming the key is a kludge
                          ship.DesignUpdate((ShipDesign)ClientState.Data.InputTurn.AllDesigns[ship.Owner + "/" + ship.DesignName]);
                      }
                  }
              }
          }

          // Messages to Event objects
          foreach (Message message in turnData.Messages)
          {
              switch (message.Type)
              {
                  case "TechAdvance":
                      // TODO (priority 5) - link the tech advance message to the research control panel.
                      break;

                  case "NewComponent":
                      // TODO (priority 5) - Link the new component message to the technology browser (when it is available in game).
                      break;

                  case "BattleReport":
                      // The message is loaded such that the Event is a string containing the BattleReport.Key.
                      // FIXME (priority 4) - linking battle messages to the right battle report is inefficient because the turnData.Battles does not have a meaningful key.
                      foreach (BattleReport battle in turnData.Battles)
                      {
                          if (battle.Key == ((string)message.Event))
                          {
                              message.Event = battle;
                              break;
                          }
                      }
                      break;

                  default:
                      message.Event = null;
                      break;
              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Run through the full list of messages and populate the message store in the
      /// state data with the messages relevant to the player's selected race. The
      /// actual message control will be populated within the main window
      /// initialisation.
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void ProcessMessages()
      {
          foreach (Message message in turnData.Messages)
          {
              if ((message.Audience == ClientState.Data.RaceName) ||
                  (message.Audience == "*"))
              {
                  stateData.Messages.Add(message);
              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// So that we can put an indication of fleets orbiting a star run through all
      // the fleets and, if they are in orbit around a star, set the OrbitingFleets
      // flag in the star.
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void DetermineOrbitingFleets()
      {
          foreach (Star star in turnData.AllStars.Values)
          {
              star.OrbitingFleets = false;
          }

          foreach (Fleet fleet in turnData.AllFleets.Values)
          {
              if (fleet.InOrbit != null && fleet.Type != "Starbase")
              {
                  fleet.InOrbit.OrbitingFleets = true;                  
              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Advance the age of all star reports by one year. Then, if a star is owned by
      /// us and has colonists bring the report up to date (just by creating a new
      /// report).
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void ProcessReports()
      {
          foreach (StarReport report in stateData.StarReports.Values)
          {
              report.Age++;
          }

          foreach (Star star in stateData.PlayerStars.Values)
          {
              if (star.Colonists != 0)
              {
                  stateData.StarReports[star.Name] = new StarReport(star);
              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      ///  Process Fleet Reports
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void ProcessFleets()
      {
          // update the state data with the current fleets
          foreach (Fleet fleet in stateData.InputTurn.AllFleets.Values)
          {
              if (fleet.Owner == stateData.PlayerRace.Name)
              {
                  stateData.PlayerFleets.Add(fleet);

                  if (fleet.IsStarbase)
                  {
                      // update the reference from the star to its starbase and vice versa (the fleet should know the name of the star, but the reference is a dummy)
                      if ((fleet.InOrbit == null) || (fleet.InOrbit.Name == null))
                      {
                          Report.FatalError("Starbase doesn't know what planet it is orbiting!");
                      }
                      Star star = ClientState.Data.PlayerStars[fleet.InOrbit.Name] as Star;
                      star.Starbase = fleet;
                      fleet.InOrbit = star;
                  }

                  // --------------------------------------------------------------------------------
                  // FIXME (priority 5) - discovery of planetary information should be done by the server. It should not be possible for a hacked client to get this information.

                  
                  if ((fleet.InOrbit != null) && (!fleet.IsStarbase) && fleet.CanScan)
                  {
                      // add to orbiting fleets list
                      Star star = fleet.InOrbit;
                      stateData.StarReports[star.Name] = new StarReport(star);
                  }

                  if (fleet.ShortRangeScan != 0)
                  {
                      foreach (Star star in turnData.AllStars.Values)
                      {
                          if (PointUtilities.Distance(star.Position, fleet.Position)
                              <= fleet.ShortRangeScan)
                          {
                              stateData.StarReports[star.Name] = new StarReport(star);
                          }
                      }
                  }

                  // END OF FIX ME --------------------------------------------------------------------------------

              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Recieve research updates.
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void ProcessResearch()
      {
            // Update the new Tech Levels
            stateData.ResearchLevels = turnData.ResearchLevels;
            // Update the accumulated resources
            stateData.ResearchResources = turnData.ResearchResources;
            
            foreach (TechLevel.ResearchField area in Enum.GetValues(typeof(TechLevel.ResearchField)))
            {                
                if (turnData.ResearchLevelsGained == null)
                {
                    return;
                }
                
                while (turnData.ResearchLevelsGained[area] > 0)
                {
                    // Report new levels.
                    turnData.ResearchLevelsGained[area] = turnData.ResearchLevelsGained[area] - 1;
                    ReportLevelUpdate(area, stateData.ResearchLevels[area]);
                    
                }
            }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Report an update in tech level and any new components that have became
      /// available.
      /// </summary>
      /// <param name="area">The field of research.</param>
      /// <param name="level">The new level obtained.</param>
      /// ----------------------------------------------------------------------------
      private static void ReportLevelUpdate(TechLevel.ResearchField area, int level)
      {
          Message techAdvanceMessage = new Message(
              ClientState.Data.RaceName,
              "Your race has advanced to Tech Level " + level + " in the " + area.ToString() + " field",
              "TechAdvance",
              null);
          stateData.Messages.Add(techAdvanceMessage);

          Dictionary<string, Component> allComponents = AllComponents.Data.Components;
          TechLevel oldResearchLevel = stateData.ResearchLevels;
          TechLevel newResearchLevel = new TechLevel(oldResearchLevel);

          newResearchLevel[area] = level;

          foreach (Component component in allComponents.Values)
          {
              if (component.RequiredTech > oldResearchLevel &&
                  component.RequiredTech <= newResearchLevel)
              {

                  ClientState.Data.AvailableComponents.Add(component); 
                  Message newComponentMessage = null;
                  if (component.Properties.ContainsKey("Scaner") && component.Type == "Planetary Installations")
                  {
                      newComponentMessage = new Message(
                          ClientState.Data.RaceName,
                          null,
                          "All existing planetary scanners has been replaced by " + component.Name + " " + component.Type,
                          null);
                      foreach (Star star in stateData.PlayerStars.Values)
                      {
                          if (star.ScannerType != string.Empty)
                          {
                              star.ScannerType = component.Name;
                          }
                      }
                  }
                  else
                  {
                       newComponentMessage = new Message(
                          ClientState.Data.RaceName,
                          null,
                          "You now have available the " + component.Name + " " + component.Type + " component",
                          null);
                  }
                  ClientState.Data.Messages.Add(newComponentMessage);
              }
          }

          stateData.ResearchLevels = newResearchLevel;
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Determine the fleets owned by the player (this is a convenience function so
      /// that buttons such as "Next" and "Previous" on the ship detail panel are easy
      /// to code.
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void DeterminePlayerFleets()
      {
          stateData.PlayerFleets.Clear();

          foreach (Fleet fleet in turnData.AllFleets.Values)
          {
              if (fleet.Owner == stateData.RaceName)
              {
                  if (fleet.Type != "Starbase")
                  {
                      stateData.PlayerFleets.Add(fleet);
                  }
              }
          }
      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Determine the star systems owned by the player (this is a convenience
      // function so that buttons such as "Next" and "Previous" on the star detail
      // panel are easy to code,
      /// </summary>
      /// ----------------------------------------------------------------------------
      private static void DeterminePlayerStars()
      {
          stateData.PlayerStars.Clear();

          foreach (Star star in turnData.AllStars.Values)
          {
              if (star.Owner == stateData.RaceName)
              {
                  stateData.PlayerStars.Add(star);
                  star.ThisRace = stateData.PlayerRace;
              }
          }
      }

   }
}

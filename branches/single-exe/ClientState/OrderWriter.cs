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
// This GUI module will generate the player's Orders, which are written to 
// file and sent to the Nova Console so that the turn for the next year can 
// be generated. 
//
// This is a static helper object that operates on GuiState and Intel to create
// an Orders object and write the orders to file.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using NovaCommon;
#endregion

namespace NovaClient
{
   public static class OrderWriter
   {

// ---------------------------------------------------------------------------
// Class private data. The turn data itself is stored in the class defined in
// NovaCommon Orders.cs
// ---------------------------------------------------------------------------

      private static ClientState     StateData  = null;
      private static Intel           InputTurn  = null; // TODO (priority 4) - It seems strange to be still looking at the Intel passed to the player here. It should have been integrated into the GuiState by now! -- Dan 07 Jan 10
      private static string          RaceName   = null;

      /// ----------------------------------------------------------------------------
      /// <summary>
      /// WriteOrders the player's turn. We don't bother checking what's changed we just
      /// send the details of everything owned by the player's race and the Nova
      /// Console will update its master copy of everything.
      /// </summary>
      /// ----------------------------------------------------------------------------
      public static void WriteOrders()
      {
         StateData  = ClientState.Data;
         InputTurn  = StateData.InputTurn;
         RaceName   = StateData.RaceName;

         Orders outputTurn = new Orders();
         RaceData playerData = new RaceData();
         
         playerData.TurnYear        = StateData.TurnYear;
         playerData.PlayerRelations = StateData.PlayerRelations;
         playerData.BattlePlans     = StateData.BattlePlans;
         outputTurn.PlayerData      = playerData;
         outputTurn.TechLevel       = CountTechLevels();

         foreach (Fleet fleet in InputTurn.AllFleets.Values)
         {
             if (fleet.Owner == RaceName)
             {
                 outputTurn.RaceFleets.Add(fleet.FleetID, fleet);
             }
         }

         // While adding the details of the stars owned by the player's race
         // tell the star how many of its resources have been allocated to be
         // used for research (used in the Star.Update method). We also keep a
         // local count so that we can "do" the research on the arrival of the
         // next turn.

         StateData.ResearchAllocation = 0;

         foreach (Star star in InputTurn.AllStars.Values)
         {
             if (star.Owner == RaceName)
             {
                 star.ResearchAllocation = (int)((star.ResourcesOnHand.Energy
                                            * StateData.ResearchBudget) / 100);

                 StateData.ResearchAllocation += star.ResearchAllocation;
                 outputTurn.RaceStars.Add(star);
             }
         }

         foreach (Design design in InputTurn.AllDesigns.Values)
         {
             if (design.Owner == RaceName)
             {
                 outputTurn.RaceDesigns.Add(design.Key, design);
             }
         }

         foreach (string fleetName in StateData.DeletedFleets)
         {
             outputTurn.DeletedFleets.Add(fleetName);
         }

         foreach (string designKey in StateData.DeletedDesigns)
         {
             outputTurn.DeletedFleets.Add(designKey);
         }

         string turnFileName = Path.Combine(StateData.GameFolder, RaceName + ".Orders");

         // outputTurn.ToBinary(turnFileName); // old binary serialised format
         outputTurn.ToXml(turnFileName); // human readable xml format

      }


      /// ----------------------------------------------------------------------------
      /// <summary>
      /// Return the sum of the levels reached in all research areas
      /// </summary>
      /// <returns>The sum of all tech levels.</returns>
      /// ----------------------------------------------------------------------------
      private static int CountTechLevels()
      {
          StateData = ClientState.Data;
          int total = 0;

          foreach (int techLevel in StateData.ResearchLevel)
          {
              total += techLevel;
          }

          return total;
      }

   }
}

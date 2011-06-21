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

namespace Nova.Client
{
    #region Using Statements

    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters;
    using System.Runtime.Serialization.Formatters.Binary;
    using Nova.Common;
    using Nova.Common.Components;

    #endregion

    public static class OrderWriter
   {
       // ---------------------------------------------------------------------------
       // Class private data. The turn data itself is stored in the class defined in
       // Nova.Common Orders.cs
       // ---------------------------------------------------------------------------

       private static ClientState stateData;
       private static Intel inputTurn; // TODO (priority 4) - It seems strange to be still looking at the Intel passed to the player here. It should have been integrated into the GuiState by now! -- Dan 07 Jan 10
       private static string raceName;

       /// ----------------------------------------------------------------------------
       /// <summary>
       /// WriteOrders the player's turn. We don't bother checking what's changed we just
       /// send the details of everything owned by the player's race and the Nova
       /// Console will update its master copy of everything.
       /// </summary>
       /// ----------------------------------------------------------------------------
       public static void WriteOrders()
       {
           stateData = ClientState.Data;
           inputTurn = stateData.InputTurn;
           raceName = stateData.RaceName;

           Orders outputTurn = new Orders();
           RaceData playerData = new RaceData();

           playerData.TurnYear = stateData.TurnYear;
           playerData.ResearchBudget = stateData.ResearchBudget;
           playerData.ResearchTopics = stateData.ResearchTopics;
           playerData.ResearchResources = stateData.ResearchResources;
           playerData.ResearchLevels = stateData.ResearchLevels;
           playerData.PlayerRelations = stateData.PlayerRelations;
           playerData.BattlePlans = stateData.BattlePlans;
           outputTurn.PlayerData = playerData;
           outputTurn.TechLevel = CountTechLevels();

           foreach (Fleet fleet in inputTurn.AllFleets.Values)
           {
               if (fleet.Owner == raceName)
               {
                   outputTurn.RaceFleets.Add(fleet.FleetID, fleet);
               }
           }

           // While adding the details of the stars owned by the player's race
           // tell the star how many of its resources have been allocated to be
           // used for research (used in the Star.Update method). We also keep a
           // local count so that we can "do" the research on the arrival of the
           // next turn.

           // Don't keep a local count.
           // stateData.ResearchAllocation = 0;

           foreach (Star star in inputTurn.AllStars.Values)
           {
               if (star.Owner == raceName)
               {
                   // Do not use ResourcesOnHand.Energy here, use
                   // GetResourceRate instead, as ResourcesOnHand
                   // already account for allocation each turn.

                   // Let the server handle this.
                   // star.ResearchAllocation = (star.GetResourceRate() * stateData.ResearchBudget) / 100;

                   // Don't keep a local count.
                   // stateData.ResearchAllocation += (int)star.ResearchAllocation;
                   outputTurn.RaceStars.Add(star);
               }
           }

           foreach (Design design in inputTurn.AllDesigns.Values)
           {
               if (design.Owner == raceName)
               {
                   outputTurn.RaceDesigns.Add(design.Key, design);
               }
           }

           foreach (string fleetKey in stateData.DeletedFleets)
           {
               outputTurn.DeletedFleets.Add(fleetKey);
           }

           foreach (string designKey in stateData.DeletedDesigns)
           {
               outputTurn.DeletedDesigns.Add(designKey);
           }

           string turnFileName = Path.Combine(stateData.GameFolder, raceName + Global.OrdersExtension);

           outputTurn.ToXml(turnFileName);
       }


       /// ----------------------------------------------------------------------------
       /// <summary>
       /// Return the sum of the levels reached in all research areas
       /// </summary>
       /// <returns>The sum of all tech levels.</returns>
       /// ----------------------------------------------------------------------------
       private static int CountTechLevels()
       {
           stateData = ClientState.Data;
           int total = 0;

           foreach (int techLevel in stateData.ResearchLevels)
           {
               total += techLevel;
           }

           return total;
       }
   }
}

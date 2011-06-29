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

namespace Nova.Client
{
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
 
    /// <summary>
    /// This GUI module will generate the player's Orders, which are written to 
    /// file and sent to the Nova Console so that the turn for the next year can 
    /// be generated. 
    ///
    /// This is a static helper object that operates on GuiState and Intel to create
    /// an Orders object and write the orders to file. 
    /// </summary>
    public class OrderWriter
    {
        private ClientState stateData;
        private Intel inputTurn; // TODO (priority 4) - It seems strange to be still looking at the Intel passed to the player here. It should have been integrated into the GuiState by now! -- Dan 07 Jan 10
        private string raceName;
        
        /// <summary>
        /// Default Constructor. 
        /// </summary>
        /// <param name="stateData">
        /// A <see cref="ClientState"/> from where to extract the orders.
        /// </param>
        public OrderWriter(ClientState stateData)
        {
            this.stateData = stateData;
            this.inputTurn = stateData.InputTurn;
            this.raceName = stateData.EmpireIntel.EmpireRace.Name;
        }    
        
        /// <summary>
        /// WriteOrders the player's turn. We don't bother checking what's changed we just
        /// send the details of everything owned by the player's race and the Nova
        /// Console will update its master copy of everything.
        /// </summary>
        public void WriteOrders()
        {
            Orders outputTurn = new Orders();
            
            outputTurn.EmpireStatus = stateData.EmpireIntel;
            
            outputTurn.TechLevel = CountTechLevels();
                        
            foreach (Design design in inputTurn.AllDesigns.Values)
            {
               if (design.Owner == stateData.EmpireIntel.Id)
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
        private int CountTechLevels()
        {
           int total = 0;
        
           foreach (int techLevel in stateData.EmpireIntel.ResearchLevels)
           {
               total += techLevel;
           }
        
           return total;
        }
    }
}

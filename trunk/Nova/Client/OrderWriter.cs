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
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;
 
    /// <summary>
    /// This GUI module will generate the player's Orders, which are written to 
    /// file and sent to the Nova Console so that the turn for the next year can 
    /// be generated. 
    /// </summary>
    public class OrderWriter
    {
        private ClientData clientState;
        
        /// <summary>
        /// Default Constructor. 
        /// </summary>
        /// <param name="stateData">
        /// A <see cref="ClientState"/> from where to extract the orders.
        /// </param>
        public OrderWriter(ClientData clientState)
        {
            this.clientState = clientState;
        }    
        
        /// <summary>
        /// WriteOrders the player's turn. We don't bother checking what's changed we just
        /// send the details of everything owned by the player's race and the Nova
        /// Console will update its master copy of everything.
        /// </summary>
        public void WriteOrders()
        {
            {
                // Advance the turn year, to show this empire has finished with the current turn year.
                clientState.EmpireState.TurnSubmitted = true;
                clientState.EmpireState.LastTurnSubmitted = clientState.EmpireState.TurnYear;

                // Setup the XML document
                XmlDocument xmldoc = new XmlDocument();
                XmlElement xmlRoot = Global.InitializeXmlDocument(xmldoc);

                Global.SaveData(xmldoc, xmlRoot, "Turn", clientState.EmpireState.LastTurnSubmitted.ToString());
                Global.SaveData(xmldoc, xmlRoot, "Id", clientState.EmpireState.Id.ToString("X"));

                // add the orders to the document
                XmlElement xmlelOrders = xmldoc.CreateElement("Orders");
                xmlRoot.AppendChild(xmlelOrders);

                foreach (ICommand command in clientState.Commands)
                {
                    xmlelOrders.AppendChild(command.ToXml(xmldoc));
                }

                string ordersFileName = Path.Combine(clientState.GameFolder, clientState.EmpireState.Race.Name + Global.OrdersExtension);

                bool waitForFile = false;
                double waitTime = 0.0; // seconds
                do
                {
                    try
                    {
                        using (Stream output = new FileStream(ordersFileName, FileMode.Create))
                        {
                            xmldoc.Save(output);
                        }
                        waitForFile = false;
                    }
                    catch (System.IO.IOException)
                    {
                        // IOException. Is the file locked? Try waiting.
                        if (waitTime < Global.TotalFileWaitTime)
                        {
                            waitForFile = true;
                            System.Threading.Thread.Sleep(Global.FileWaitRetryTime);
                            waitTime += 0.1;
                        }
                        else
                        {
                            // Give up, maybe something else is wrong?
                            throw;
                        }
                    }
                }
                while (waitForFile);
            }
        }
    
    
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Return the sum of the levels reached in all research areas.
        /// </summary>
        /// <returns>The sum of all tech levels.</returns>
        /// ----------------------------------------------------------------------------
        private int CountTechLevels()
        {
           int total = 0;
        
           foreach (int techLevel in clientState.EmpireState.ResearchLevels)
           {
               total += techLevel;
           }
        
           return total;
        }
    }
}

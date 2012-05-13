#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2012 The Stars-Nova Project
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

namespace Nova.WinForms.Gui
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Nova.Common;
    using Nova.Common.Commands;

    /// <Summary>
    /// This module provides a control to display the production queue for the 
    /// currently selected Star system.
    /// </Summary>
    public class QueueList : ListView
    {
        /// <summary>
        /// This holds the commands to be applied when the ProductionDialog presses OK.
        /// </summary>
        public Queue<ICommand> ProductionCommands = new Queue<ICommand>();
        
        
        /// <summary>
        /// Reference to the star that holds this Queue.
        /// </summary>
        private string starKey;

        
        /// <Summary>
        /// Populate a QueueList control with the details of a production queue.
        /// </Summary>
        /// <param name="star">The <see cref="Star"/> whose queue will be displaed.</param>
        public void Populate(Star star)
        {   
            // Placed here because it's difficult to pass this on the constructor
            // as the star may not be selected yet when creating the Queue control.            
            starKey = star.Key;
            
            Items.Clear();
            BeginUpdate();

            // Note that we don't add these to the commandqueue, as we only use
            // commands to denote CHANGES in the queue.
            
            foreach (ProductionOrder productionOrder in star.ManufacturingQueue.Queue)
            {
                ListViewItem item = new ListViewItem();
                
                item.Text = productionOrder.Name;
                item.Tag = productionOrder;   
                item.SubItems.Add(productionOrder.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));

                // either set the Checked stat here for Starbases or in the OnLoad Method of ProductionDialog.cs
                Items.Add(item);
            }
            
            UpdateHeader();

            EndUpdate();
        }
        
        
        /// <summary>
        /// Inserts a production order to the Queue at a specific index.
        /// </summary>
        /// <param name="productionOrder">The ProductionOrder to insert</param>
        /// <returns>The added ListViewItem</returns>
        public ListViewItem InsertProductionOrder(ProductionOrder productionOrder, int index)
        {
            ICommand command = new ProductionCommand(CommandMode.Add, productionOrder, starKey, index-1);
            ProductionCommands.Enqueue(command);
            
            ListViewItem item = new ListViewItem();
            item.Text = productionOrder.Name;
            item.Tag = productionOrder;
            item.SubItems.Add(productionOrder.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));            
            
            Items.Insert(index, item);
            Items[index].Selected = true;
                
            UpdateHeader();
            
            return item;    
        }
        
        
        /// <summary>
        /// Edits a production order at a certain queue position
        /// </summary>
        /// <param name="productionOrder">The new production order</param>
        /// <param name="index">The queue position</param>
        /// <returns>The new edited item</returns>
        public ListViewItem EditProductionOrder(ProductionOrder productionOrder, int index)
        {
            // Can't edit header!
            if (index == 0)
            {
                return null;
            }
            
            ICommand command = new ProductionCommand(CommandMode.Edit, productionOrder, starKey, index-1);
            ProductionCommands.Enqueue(command);
            
            Items[index].SubItems.Clear();
            Items[index].Text = productionOrder.Name;
            Items[index].Tag = productionOrder;            
            Items[index].SubItems.Add(productionOrder.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            
            Items[index].Selected = true;
            
            return Items[index];
        }
        
        
        /// <summary>
        /// Removes a production order from the Queue control.
        /// </summary>
        /// <param name="productionOrder">index to remove</param>
        public void RemoveProductionOrder(int index)
        {
            // Can't remove header!
            if (index == 0)
            {
                return;
            }
            
            ICommand command = new ProductionCommand(CommandMode.Delete, null, starKey, index-1);
            ProductionCommands.Enqueue(command);
            
            Items.RemoveAt(index);
            
            UpdateHeader();
        }
        
        
        /// <summary>
        /// Removes a production order from the Queue control.
        /// </summary>
        /// <param name="productionOrder">ListViewItem to remove</param>
        public void RemoveProductionOrder(ListViewItem item)
        {
            if (!Items.Contains(item))
            {
                return; 
            }
            
            ICommand command = new ProductionCommand(CommandMode.Edit, null, starKey, Items.IndexOf(item)-1);
            ProductionCommands.Enqueue(command);
            
            // Can't remove the header!
            if (Items[0] == item)
            {
                return;
            }
            
            Items.Remove(item);
            
            UpdateHeader();
        }
        
        /// <summary>
        /// Updates the header to read "Top of the Queue" or "Queue empty" when necessary.
        /// </summary>
        public void UpdateHeader()
        {
            if (Items.Count == 0)
            {
                return;    
            }
            
            if ((Items[0].Tag as ProductionOrder).Unit is NoProductionUnit)
            {
                if (Items.Count > 1)
                {
                    Items[0].Text = "--- Top of the Queue ---";
                }
                else
                {
                    Items[0].Text = "--- Queue is empty ---";
                }
            }

        }
    }
}

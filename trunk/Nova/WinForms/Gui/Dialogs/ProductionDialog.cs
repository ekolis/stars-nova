#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010. 2011 The Stars-Nova Project
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
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;

    /// <Summary>
    /// Production queue dialog.
    /// </Summary>
    public partial class ProductionDialog : System.Windows.Forms.Form
    {        
        private readonly Star queueStar;
        private readonly ClientData clientState;

        /// <Summary>
        /// Initializes a new instance of the ProductionDialog class.
        /// </Summary>
        /// <param name="Star">The Star to do a production dialog for.</param>
        public ProductionDialog(Star star, ClientData clientState)
        {
            this.queueStar = star;
            this.clientState = clientState;

            InitializeComponent();
        }


        /// <Summary>
        /// Populate the available designs items list box with the things we can build.
        /// Generally, we can build designs created by the player. However, we only
        /// include ships in the list if the Star has a starbase with enough dock
        /// capacity to build the design. 
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OnLoad(object sender, System.EventArgs e)
        {
            // Check the Star's budget state.
            onlyLeftovers.Checked = queueStar.OnlyLeftover;
            
            designList.BeginUpdate();
            
            // Add default items.
            ListViewItem item = new ListViewItem();
            item.Text = "Factory";
            item.Tag = new FactoryProductionUnit(queueStar.ThisRace);
            designList.Items.Add(item);
            
            item = new ListViewItem();
            item.Text = "Mine";
            item.Tag = new MineProductionUnit(queueStar.ThisRace);
            designList.Items.Add(item);

            Fleet starbase = queueStar.Starbase;
            int dockCapacity = 0;

            if (starbase != null)
            {
                dockCapacity = starbase.TotalDockCapacity;
            }

            foreach (ShipDesign design in clientState.EmpireState.Designs.Values)
            {
                // what the purpose of this next line (shadallark) ???
                // Looks like it is ment to prevent the current starbase design being re-used - Dan.
                // prevent the current starbase design from being re-used
                foreach (ShipToken token in starbase.Composition.Values)
                {
                    if (token.Design.Type == ItemType.Starbase && token.Design.Equals(design))
                    {
                        continue;
                    }
                }

                // Check if this design can be built at this Star - ships are limited by dock capacity of the starbase.

                if (design is ShipDesign && dockCapacity > design.Mass)
                {
                    item = new ListViewItem();
                    item.Text = design.Name;
                    item.Tag = new ShipProductionUnit(design);
                    designList.Items.Add(item);
                }

            }

            designList.EndUpdate();
            addToQueue.Enabled = false;
                
            queueList.Populate(queueStar);
            
            // Add the Header after populating as Populate() clears the Item list.
            // Header name doesn't matter as it's handled elsewhere.
            item = new ListViewItem();
            item.Tag = new ProductionOrder(0, new NoProductionUnit(), true);
            item.Text = (item.Tag as ProductionOrder).Name;            
            queueList.Items.Insert(0, item);
            
            queueList.UpdateHeader();
            
            // check if a starbase design is in the Production Queue and if so remove it from the Design List
            ProductionOrder productionOrder;
            // outer loop used for stepping through the Production Queue
            for (int queueLoopCounter = 0; queueLoopCounter < queueList.Items.Count; queueLoopCounter++)
            {
                // is this a ship at all?
                productionOrder = queueList.Items[queueLoopCounter].Tag as ProductionOrder;
                                
                if (!(productionOrder.Unit is ShipProductionUnit))
                {
                    continue;        
                }
    
                // is it a starbase?                
                ShipDesign productionItemDesign = clientState.EmpireState.Designs[(productionOrder.Unit as ShipProductionUnit).DesignKey];

                if (productionItemDesign.Type == ItemType.Starbase)
                {
                    queueList.Items[queueLoopCounter].Checked = true;
                    // inner loop counter used for stepping through the Design List
                    for (int designsLoopCounter = 0; designsLoopCounter < designList.Items.Count; designsLoopCounter++)
                    {
                        if (queueList.Items[queueLoopCounter].Text == designList.Items[designsLoopCounter].Text)
                        {
                            // remove the starbase from the Design List
                            designList.Items.RemoveAt(designsLoopCounter);
                            designsLoopCounter--; // after having removed one Item from the list decrement by 1 to allow the rest of the list to be examined
                        }
                    }
                }
                else
                {
                    queueList.Items[queueLoopCounter].Checked = false;
                }
            }
            
            queueList.Items[queueList.Items.Count - 1].Selected = true;
            
            UpdateProductionCost();
        }

        /// <Summary>
        /// Process a design being selected for possible construction.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void AvailableSelected(object sender, System.EventArgs e)
        {
            if (designList.SelectedItems.Count <= 0)  
            {
                // nothing selected in the design list
                addToQueue.Enabled = false;
                designCost.Value = new Resources();
                return;
            }
            
            addToQueue.Enabled = true;
            designCost.Value = (designList.SelectedItems[0].Tag as IProductionUnit).Cost;
            
            return;
                      
        }

        /// <Summary>
        /// Production queue Item selected changed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueSelected(object sender, EventArgs e)
        {
            // check if selected Item is the "--- Top of Queue ---" which cannot be moved down or removed
            if (queueList.SelectedItems.Count > 0)
            {
                if (queueList.SelectedIndices[0] == 0)
                {
                    // "--- Top of Queue ---" selected
                    this.queueUp.Enabled = false;
                    queueDown.Enabled = false;
                    removeFromQueue.Enabled = false;
                    // addToQueue.Enabled = true;
                }
                else
                {
                    removeFromQueue.Enabled = true;
                    // check if >1 to ignore top two items ("--- Top of Queue ---" placeholder which cannot be moved and Item below it)
                    if (queueList.SelectedIndices[0] > 1)
                    {
                        queueUp.Enabled = true;
                    }
                    else
                    {
                        queueUp.Enabled = false;
                    }
                          
                          
                    if (queueList.SelectedIndices[0] < queueList.Items.Count - 1)
                    {
                        queueDown.Enabled = true;
                    }
                    else
                    {
                        queueDown.Enabled = false;
                    }
                }
            }
            else
            {
                // no items are selected
                queueUp.Enabled = false;
                queueDown.Enabled = false;
                removeFromQueue.Enabled = false;
            }
            
            // it does not matter if an Item is selected the Production Costs can still be updated.
            UpdateProductionCost();
        }
        
        /// <Summary>
        /// Add to queue when double click on Design List
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddToQueue_Click(sender, new EventArgs());
        }

        /// <Summary>
        /// Add to queue button pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void AddToQueue_Click(object sender, System.EventArgs e)
        {
            if (designList.SelectedItems.Count <= 0)
            {
                return;
            }
            
            IProductionUnit productionUnit = designList.SelectedItems[0].Tag as IProductionUnit;

            // Starbases are handled differently to the other component types.            
            
            // Ctrl-Add + 100 items
            // shift-Add +10 items
            // Add +1 items
            int quantity = 1;
            
            if (Button.ModifierKeys == Keys.Control)
            {
                quantity = 100;
            }
            else if (Button.ModifierKeys == Keys.Shift)
            {
                quantity = 10;
            }

            ProductionOrder productionOrder = new ProductionOrder(quantity, productionUnit, false);
            
            AddProduction(productionOrder);
        }

        /// <Summary>
        /// Removes from queue on double click
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RemoveFromQueue_Click(sender, new EventArgs());
        }

        /// <Summary>
        /// Remove from queue button pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void RemoveFromQueue_Click(object sender, EventArgs e)
        {
            int s = queueList.SelectedIndices[0];
            ProductionOrder productionOrder = (queueList.Items[s].Tag as ProductionOrder);

            if (queueList.SelectedItems.Count > 0)
            {
                // Ctrl -Remove 100 items
                // Shift -Remove 10 items
                //       -Remove 1 Item
                int numToRemove = 1;                
                if (Button.ModifierKeys == Keys.Control)
                {
                    numToRemove = 100;
                }
                else if (Button.ModifierKeys == Keys.Shift)
                {
                    numToRemove = 10;
                }

                if (numToRemove >= productionOrder.Quantity)
                {
                    queueList.RemoveProductionOrder(s);
                }
                else
                {
                    productionOrder.Quantity -= numToRemove;
                    queueList.EditProductionOrder(productionOrder, s);
                }

                UpdateProductionCost();
            }
        }

        /// <Summary>
        /// Move selected Item up in queue.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueUp_Click(object sender, EventArgs e)
        {
            if (queueList.SelectedItems.Count > 0)
            {
                int source = queueList.SelectedIndices[0];
                // must be greater than 0 due to "--- Top of Queue ---" Placeholder
                if (source > 0)
                {
                    ListViewItem movedItem = (ListViewItem)queueList.Items[source].Clone();
                    ListViewItem displacedItem = (ListViewItem)queueList.Items[source - 1].Clone();                    
                    
                    queueList.EditProductionOrder(displacedItem.Tag as ProductionOrder, source);
                    queueList.EditProductionOrder(movedItem.Tag as ProductionOrder, source -1);

                    queueDown.Enabled = true;
                }                
            }
            UpdateProductionCost();
        }
        
        /// <Summary>
        /// Move selected Item down in queue.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueDown_Click(object sender, EventArgs e)
        {
            if (queueList.SelectedItems.Count > 0)
            {
                int source = queueList.SelectedIndices[0];
                 // check if > 0 for Top of Queue place holder
                if (source < queueList.Items.Count - 1 && source > 0)
                {
                    ListViewItem movedItem = (ListViewItem)queueList.Items[source].Clone();
                    ListViewItem displacedItem = (ListViewItem)queueList.Items[source + 1].Clone();
                    
                    queueList.EditProductionOrder(displacedItem.Tag as ProductionOrder, source);
                    queueList.EditProductionOrder(movedItem.Tag as ProductionOrder, source +1);                   
                }
            }
            UpdateProductionCost();
        }

        /// <Summary>
        /// Add a selected Item into the production queue. If no Item is selected in
        /// the queue, add the new one on the end. If an Item is selected and it is the
        /// same type as the one being added then just increment the quantity, if it is not
        /// the same type check if the next Item in the queue is the same type, if it is then
        /// increment the quantity of that Item, if it does not match, insert the design after
        /// the selected Item in the production queue.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void AddProduction(ProductionOrder productionOrder)
        {
            ListViewItem newItem = null;
            
            // if no items are selected add the quantity of design as indicated
            if (queueList.SelectedItems.Count == 0)
            {
                newItem = queueList.InsertProductionOrder(productionOrder, queueList.Items.Count);
            }
            else
            {
                int s = queueList.SelectedIndices[0];
                
                // if the Item selected in the queue is the same as the design being added increase the quantity
                if (productionOrder.Name == (queueList.Items[s].Tag as ProductionOrder).Name)
                {
                    productionOrder.Quantity += (queueList.Items[s].Tag as ProductionOrder).Quantity;
                    
                    newItem = queueList.EditProductionOrder(productionOrder, s);
                }
                else
                {
                    // as the Item selected in the queue is different from the design being added check the Item
                    // below the selected Item (first confirm it exists) to see if it matches, if so increase its
                    // quantity and have it become the selected Item, if not add the Item after the Item selected in the queue
                    int nextIndex = s + 1;
                    if (queueList.Items.Count > nextIndex)    
                    {
                        if (productionOrder.Name == (queueList.Items[nextIndex].Tag as ProductionOrder).Name)
                        {    
                            // the design is the same as the Item after the selected Item in the queue so update the Item after
                            productionOrder.Quantity += (queueList.Items[nextIndex].Tag as ProductionOrder).Quantity;

                            newItem = queueList.EditProductionOrder(productionOrder, nextIndex);
                        }
                        else
                        {
                            // add the design after the Item selected in the queue
                            newItem = queueList.InsertProductionOrder(productionOrder, nextIndex);                            
                        }
                    }
                    else
                    {                        
                        queueList.Items[s].Selected = false;
                        newItem = queueList.InsertProductionOrder(productionOrder, queueList.Items.Count);
                    }
                }
            }

            // Limit the number of defenses built.
            // TODO (Priority 4) - update this section to handle the quantity when too many have been added!
            if (productionOrder.Unit is DefenseProductionUnit)
            {
                int newDefensesAllowed = Global.MaxDefenses - queueStar.Defenses;
                
                if (productionOrder.Quantity > newDefensesAllowed)
                {
                    if (newDefensesAllowed <= 0)
                    {
                        queueList.RemoveProductionOrder(newItem);
                    }
                    else
                    {
                        productionOrder.Quantity = newDefensesAllowed;
                        queueList.EditProductionOrder(productionOrder, queueList.Items.IndexOf(newItem));
                    }
                }
            }

            UpdateProductionCost();
        }

        /// <Summary>
        /// OK button pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OK_Click(object sender, System.EventArgs e)
        {   
            //Reverse the stack, so that older production commands are on top.            
            queueList.ProductionCommands = new Stack<ICommand>(queueList.ProductionCommands.ToArray());
            
            while (queueList.ProductionCommands.Count > 0)
            {
                ICommand command = queueList.ProductionCommands.Pop();
                if (command.isValid(clientState.EmpireState))
                {
                    clientState.Commands.Push(command);
                    command.ApplyToState(clientState.EmpireState);
                }
            }
            
            Close();
        }
        
        private void CheckChanged(object sender, EventArgs e)
        {
            // Update estimated time if user changes resource
            // contribution method.
            queueStar.OnlyLeftover = onlyLeftovers.Checked;
            UpdateProductionCost();
        }


        /// <Summary> Add a starbase to the production queue. </Summary>
        /// <remarks>
        /// Starbases are special in that there
        /// can ony ever be one in the production queue, no matter how many he tries to
        /// add.
        /// FIXME (priority 6) - Dan - What if I want to build a small base first, then add a larger base latter. I can queue two different base designs in Stars! 
        /// </remarks>
        private void AddStarbase(ShipDesign design)
        {
            // First run through the production queue to see if there is already a
            // starbase there. If there is remove it.
            int i = 0;
            for (i = 0; i < designList.Items.Count; i++)
            {
                if (designList.Items[i].Text == design.Name)
                {
                    break;
                }
            }

            designList.Items.RemoveAt(i);

            //AddProduction(design, 1);
        }

        /// <Summary>
        /// Update production queue cost
        /// For each stack of items the first Item might be partially built as defined by
        /// the BuildState and therefore require less Resources than the rest of the items
        /// in the stack which require the design.cost
        /// </Summary>
        private void UpdateProductionCost()
        {
            Resources wholeQueueCost = new Resources(0, 0, 0, 0);      // resources required to build everything in the Production Queue
            Resources selectedItemCost = new Resources(0, 0, 0, 0);    // resources required to build the selected Item stack in the Production Queue
            double percentComplete = 0.0;
            int minesInQueue = 0;
            int factoriesInQueue = 0;
            int minYearsCurrent, maxYearsCurrent, minYearsSelected, maxYearsSelected, minYearsTotal, maxYearsTotal;
            int yearsSoFar, yearsToCompleteOne;
            minYearsSelected = maxYearsSelected = minYearsTotal = maxYearsTotal = 0;

            Race starOwnerRace = queueStar.ThisRace;
            Resources potentialResources = new Resources();

            if (starOwnerRace == null)
            {
                // set potentialResources to zero as they are unknown without knowing the Race that owns the Star
                // and set yearsSoFar to -1 to cause the calculations of years to complete to be skipped
                yearsSoFar = -1;
            }
            else
            {
                yearsSoFar = 1;
                // initialize the mineral portion of the potentialResources
                potentialResources.Ironium = queueStar.ResourcesOnHand.Ironium;
                potentialResources.Boranium = queueStar.ResourcesOnHand.Boranium;
                potentialResources.Germanium = queueStar.ResourcesOnHand.Germanium;
            }

            // check if there are any items in the Production Queue before attempting to determine costs
            // requires checking if queueList.Items.Count > than 1 due to Placeholder value at Top of the Queue
            if (queueList.Items.Count > 1)
            {
                int selectedItemIndex = 0;
                if (queueList.SelectedItems.Count > 0)
                {
                    selectedItemIndex = queueList.SelectedIndices[0];
                    if (selectedItemIndex == 0)
                    {
                        minYearsSelected = maxYearsSelected = -2;
                    }
                }
                else
                {
                    minYearsSelected = maxYearsSelected = -2;
                }

                // initialize / setup variables:
                //  currentStackCost : used to store the cost of the Stack of items being evaluated
                //  allBuilt : used to determine if all items remaining in the stack can be built
                //  quantityYetToBuild : used to track how many items in the current stack still need to have their build time estimated
                Resources currentStackCost = null;
                ProductionOrder productionOrder = null;
                bool allBuilt = false;
                int quantityYetToBuild;

                for (int queueIndex = 1; queueIndex < queueList.Items.Count; queueIndex++)
                {
                    productionOrder = (queueList.Items[queueIndex].Tag as ProductionOrder);
                    quantityYetToBuild = productionOrder.Quantity;
                    currentStackCost = productionOrder.NeededResources();
                    wholeQueueCost += currentStackCost;

                    if (yearsSoFar < 0)
                    {   // if yearsSoFar is less than zero than the Item cannot currently be built because
                        // an Item further up the queue cannot be built
                        queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Red;
                        minYearsCurrent = maxYearsTotal = -1;
                        if (minYearsTotal == 0)
                        {
                            minYearsTotal = -1;
                        }
                        maxYearsTotal = -1;
                        if (queueIndex == selectedItemIndex)
                        {
                            minYearsSelected = maxYearsSelected = -1;
                        }
                    }
                    else
                    {
                        // loop to determine the number of years to complete the current stack of items
                        allBuilt = false;
                        minYearsCurrent = maxYearsCurrent = yearsToCompleteOne = 0;
                        while (yearsSoFar >= 0 && !allBuilt)
                        {
                            // need to determine / update the resources available at this Point (resources on
                            // planet plus those already handled in the queue - including those from
                            // each year of this loop)

                            // determine the potentialResources based on the population, factories, and mines that actually exist
                            // determine the number of years to build this based on current mines / factories
                            // then update to include the effect of any mines or factories in the queue
                            // UPDATED May 11: to use native Star method of resource rate prediction. -Aeglos
                            potentialResources.Energy += queueStar.GetFutureResourceRate(factoriesInQueue);
                            
                            // Account for resources destinated for research.
                            // Use the set client percentage, not the planet allocation. This is because the
                            // allocated resources only change during turn generation, but the budget may change
                            // many times while playing a turn. This makes the Star's values out of sync, so,
                            // predict them for now.
                            // Only do this if the Star is respecting research budget.
                            if (queueStar.OnlyLeftover == false)
                            {
                                potentialResources.Energy -= potentialResources.Energy * clientState.EmpireState.ResearchBudget / 100;
                            }

                            // need to know how much of each mineral is currently available on the Star (queueStar.ResourcesOnHand)
                            // need race information to determine how many minerals are produced by each mine each year
                            // need to make sure that no more than the maximum number of mines operable by colonists are being operated
                            // add one year of mining results to the remaining potentialResources
                            // UPDATED May 11: to use native Star methods of mining rate prediction.
                            potentialResources.Ironium += queueStar.GetFutureMiningRate(queueStar.MineralConcentration.Ironium, minesInQueue);
                            potentialResources.Boranium += queueStar.GetFutureMiningRate(queueStar.MineralConcentration.Boranium, minesInQueue);
                            potentialResources.Germanium += queueStar.GetFutureMiningRate(queueStar.MineralConcentration.Germanium, minesInQueue);

                            // check how much can be done with resources available

                            if (potentialResources >= currentStackCost)
                            {
                                // everything remaining in this stack can be done
                                // therefore set allBuilt to true and reduce potential resources
                                // do not increment the yearsSoFar as other items might be able to be completed
                                //    this year with the remaining resources
                                allBuilt = true;
                                yearsToCompleteOne = 0;
                                potentialResources = potentialResources - currentStackCost;
                                if (minYearsCurrent == 0)
                                {
                                    minYearsCurrent = yearsSoFar;
                                }
                                maxYearsCurrent = yearsSoFar;

                                if (queueIndex == selectedItemIndex)
                                {
                                    if (minYearsSelected == 0)
                                    {
                                        minYearsSelected = yearsSoFar;
                                    }
                                    maxYearsSelected = yearsSoFar;
                                }
                                if (minYearsTotal == 0)
                                {
                                    minYearsTotal = yearsSoFar;
                                }
                                maxYearsTotal = yearsSoFar;
                                if (productionOrder.Unit is MineProductionUnit)
                                {
                                    minesInQueue += quantityYetToBuild;
                                }
                                if (productionOrder.Unit is FactoryProductionUnit)
                                {
                                    factoriesInQueue += quantityYetToBuild;
                                }
                            }
                            else
                            {
                                // not everything in the stack can be built this year
                                maxYearsSelected = -1;

                                // the current build state is the remaining cost of the production unit.
                                Resources currentBuildState = productionOrder.Unit.RemainingCost;

                                // Normialized to 1.0 = 100%
                                double fractionComplete = 1.0;
                                
                                // determine the percentage able to be completed by whichever resource is limiting production
                                if (fractionComplete > (double)potentialResources.Ironium / currentStackCost.Ironium && currentStackCost.Ironium > 0)
                                {
                                    fractionComplete = (double)potentialResources.Ironium / currentStackCost.Ironium;
                                }
                                if (fractionComplete > (double)potentialResources.Boranium / currentStackCost.Boranium && currentStackCost.Boranium > 0)
                                {
                                    fractionComplete = (double)potentialResources.Boranium / currentStackCost.Boranium;
                                }
                                if (fractionComplete > (double)potentialResources.Germanium / currentStackCost.Germanium && currentStackCost.Germanium > 0)
                                {
                                    fractionComplete = (double)potentialResources.Germanium / currentStackCost.Germanium;
                                }
                                if (fractionComplete > (double)potentialResources.Energy / currentStackCost.Energy && currentStackCost.Energy > 0)
                                {
                                    fractionComplete = (double)potentialResources.Energy / currentStackCost.Energy;
                                }
                                // apply this percentage to the currentStackCost to determine how much to remove from
                                // potentialResources and currentStackCost
                                Resources amountUsed = fractionComplete * currentStackCost;
                                potentialResources -= amountUsed;
                                currentStackCost -= amountUsed;

                                // check if at least the top Item in the stack can be built "this" year
                                if (amountUsed >= currentBuildState)
                                {
                                    // at least one Item can be built this year
                                    yearsToCompleteOne = 0;
                                    if (minYearsCurrent == 0)
                                    {
                                        minYearsCurrent = yearsSoFar;
                                    }
                                    if (queueIndex == selectedItemIndex && minYearsSelected == 0)
                                    {
                                        minYearsSelected = yearsSoFar;
                                    }
                                    if (minYearsTotal == 0)
                                    {
                                        minYearsTotal = yearsSoFar;
                                    }
                                    // determine how many items are able to be built and reduce quantity accordingly
                                    for (int quantityStepper = 1; quantityStepper < quantityYetToBuild; quantityStepper++)
                                    {
                                        if (amountUsed <= (currentBuildState + (quantityStepper * productionOrder.Unit.Cost)))
                                        {
                                            quantityYetToBuild -= quantityStepper;
                                            quantityStepper = quantityYetToBuild + 1;
                                        }
                                    }
                                }
                                else
                                {
                                    // not able to complete even one Item this year
                                    yearsToCompleteOne++;
                                }


                                if (yearsToCompleteOne > 20)
                                {
                                    // an Item is considered to be unbuildable if it will take more than
                                    // 20 years to build it
                                    yearsSoFar = -1;
                                    if (minYearsCurrent == 0)
                                    {
                                        minYearsCurrent = -1;
                                    }
                                    maxYearsCurrent = -1;
                                    if (minYearsTotal == 0)
                                    {
                                        minYearsTotal = -1;
                                    }
                                    maxYearsTotal = -1;
                                }
                                allBuilt = false;
                                if (yearsSoFar >= 0)
                                {
                                    yearsSoFar++;
                                }
                            }
                        }
                        // end of the while loop to determine years to build items in the stack

                        // once *YearsCurrent have been determined set font colour appropriately
                        if (minYearsCurrent == 1)
                        {
                            if (maxYearsCurrent == 1)
                            {
                                queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                        else
                        {
                            if (minYearsCurrent == -1)
                            {
                                queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Black;
                            }
                        }
                    }

                    // Update for the currently selected Item.
                    if (queueIndex == selectedItemIndex)
                    {
                        selectedItemCost = productionOrder.NeededResources();

                        Resources currentBuildState = productionOrder.Unit.RemainingCost;

                        // determine the percent complete; defined as the resource that is the most complete
                        if (productionOrder.Unit.Cost.Ironium > 0)
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Ironium) / productionOrder.Unit.Cost.Ironium));
                        }
                        if (productionOrder.Unit.Cost.Boranium > 0 && percentComplete < 100 * (1.0 - (Convert.ToDouble(currentBuildState.Boranium) / productionOrder.Unit.Cost.Boranium)))
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Boranium) / productionOrder.Unit.Cost.Boranium));
                        }
                        if (productionOrder.Unit.Cost.Germanium > 0 && percentComplete < 100 * (1.0 - (Convert.ToDouble(currentBuildState.Germanium) / productionOrder.Unit.Cost.Germanium)))
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Germanium) / productionOrder.Unit.Cost.Germanium));
                        }
                        if (productionOrder.Unit.Cost.Energy > 0 && percentComplete < 100 * (1.0 - (Convert.ToDouble(currentBuildState.Energy) / productionOrder.Unit.Cost.Energy)))
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Energy) / productionOrder.Unit.Cost.Energy));
                        }
                    }
                }
            }
            else
            {       // as there are no items in the queue set all fields to "0"
                wholeQueueCost = new Resources(0, 0, 0, 0);
                selectedItemCost = wholeQueueCost;
                minYearsSelected = maxYearsSelected = -2;
                minYearsTotal = maxYearsTotal = -2;
                yearsSoFar = -1;
            }


            // set the costs display fields
            totalCostIronium.Text = wholeQueueCost.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture);
            totalCostBoranium.Text = wholeQueueCost.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture);
            totalCostGermanium.Text = wholeQueueCost.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture);
            totalCostEnergy.Text = wholeQueueCost.Energy.ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (yearsSoFar < 0)
            {
                if (minYearsTotal == -2)
                {
                    totalCostYears.Text = "N / A";
                }
                else
                {
                    totalCostYears.Text = "Never!";
                }
            }
            else
            {
                if (minYearsTotal == maxYearsTotal && minYearsTotal == 1)
                {
                    totalCostYears.Text = "1 year.";
                }
                else
                {
                    if (maxYearsTotal < 0)
                    {
                        totalCostYears.Text = minYearsTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + " - ??? years.";
                    }
                    else
                    {
                        if (minYearsTotal == maxYearsTotal)
                        {
                            totalCostYears.Text = minYearsTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + " years.";
                        }
                        else
                        {
                            totalCostYears.Text = minYearsTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + " - " + maxYearsTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + " years.";
                        }
                    }
                }
            }
            selectedCostIronium.Text = selectedItemCost.Ironium.ToString(System.Globalization.CultureInfo.InvariantCulture);
            selectedCostBoranium.Text = selectedItemCost.Boranium.ToString(System.Globalization.CultureInfo.InvariantCulture);
            selectedCostGermanium.Text = selectedItemCost.Germanium.ToString(System.Globalization.CultureInfo.InvariantCulture);
            selectedCostEnergy.Text = selectedItemCost.Energy.ToString(System.Globalization.CultureInfo.InvariantCulture);
            selectedPercentComplete.Text = percentComplete.ToString("N1");
            if (minYearsSelected < 0)
            {
                if (minYearsSelected == -2)
                {
                    selectedCostYears.Text = "N / A";
                }
                else
                {
                    selectedCostYears.Text = "Never!";
                }
            }
            else
            {
                if (minYearsSelected == 1 && maxYearsSelected == 1)
                {
                    selectedCostYears.Text = "1 year.";
                }
                else
                {
                    if (maxYearsSelected < 0)
                    {
                        selectedCostYears.Text = minYearsSelected.ToString(System.Globalization.CultureInfo.InvariantCulture) + " - ???? years.";
                    }
                    else
                    {
                        if (minYearsSelected == maxYearsSelected)
                        {
                            selectedCostYears.Text = minYearsSelected.ToString(System.Globalization.CultureInfo.InvariantCulture) + " years.";
                        }
                        else
                        {
                            selectedCostYears.Text = minYearsSelected.ToString(System.Globalization.CultureInfo.InvariantCulture) + " - " + maxYearsSelected.ToString(System.Globalization.CultureInfo.InvariantCulture) + " years.";
                        }
                    }
                }
            }
        }
    }
}

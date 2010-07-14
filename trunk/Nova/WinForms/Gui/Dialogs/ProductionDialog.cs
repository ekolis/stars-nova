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
// Dialog to manipulate a planet's production queue.
// ===========================================================================
#endregion

using System;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// Production queue dialog.
    /// </summary>
    public class ProductionDialog : System.Windows.Forms.Form
    {
        // ----------------------------------------------------------------------------
        // Non-designer generated data items
        // ----------------------------------------------------------------------------

        private readonly Star queueStar;
        private readonly ClientState stateData;
        private readonly Intel turnData;


        #region Desginger generated varaiables
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button addToQueue;
        private System.Windows.Forms.ListView designList;
        private System.Windows.Forms.ListView queueList;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ColumnHeader queueDescription;
        private System.Windows.Forms.ColumnHeader queueQuantity;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private ControlLibrary.ResourceDisplay designCost;
        private Button removeFromQueue;
        private Button queueUp;
        private Button queueDown;
        private ControlLibrary.ResourceDisplay productionCost;
        #endregion

        #region Construction and Disposal

        /// <summary>
        /// Initializes a new instance of the ProductionDialog class.
        /// </summary>
        /// <param name="star">The star to do a production dialog for.</param>
        public ProductionDialog(Star star)
        {
            this.queueStar = star;
            this.stateData = ClientState.Data;
            this.turnData = this.stateData.InputTurn;

            InitializeComponent();
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.designList = new System.Windows.Forms.ListView();
            this.description = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.queueList = new System.Windows.Forms.ListView();
            this.queueDescription = new System.Windows.Forms.ColumnHeader();
            this.queueQuantity = new System.Windows.Forms.ColumnHeader();
            this.addToQueue = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.designCost = new Nova.ControlLibrary.ResourceDisplay();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.productionCost = new Nova.ControlLibrary.ResourceDisplay();
            this.removeFromQueue = new System.Windows.Forms.Button();
            this.queueUp = new System.Windows.Forms.Button();
            this.queueDown = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.designList);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 328);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Designs";
            // 
            // DesignList
            // 
            this.designList.AutoArrange = false;
            this.designList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.designList.CausesValidation = false;
            this.designList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.description});
            this.designList.FullRowSelect = true;
            this.designList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.designList.HideSelection = false;
            this.designList.Location = new System.Drawing.Point(8, 16);
            this.designList.MultiSelect = false;
            this.designList.Name = "DesignList";
            this.designList.Size = new System.Drawing.Size(240, 304);
            this.designList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.designList.TabIndex = 0;
            this.designList.TabStop = false;
            this.designList.UseCompatibleStateImageBehavior = false;
            this.designList.View = System.Windows.Forms.View.Details;
            this.designList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DesignList_MouseDoubleClick);
            this.designList.SelectedIndexChanged += new System.EventHandler(this.AvailableSelected);
            // 
            // Description
            // 
            this.description.Text = "Description";
            this.description.Width = 236;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.queueList);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(328, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 328);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Production Queue";
            // 
            // QueueList
            // 
            this.queueList.AutoArrange = false;
            this.queueList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.queueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.queueDescription,
            this.queueQuantity});
            this.queueList.FullRowSelect = true;
            this.queueList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.queueList.HideSelection = false;
            this.queueList.Location = new System.Drawing.Point(8, 16);
            this.queueList.MultiSelect = false;
            this.queueList.Name = "QueueList";
            this.queueList.Size = new System.Drawing.Size(240, 304);
            this.queueList.TabIndex = 0;
            this.queueList.TabStop = false;
            this.queueList.UseCompatibleStateImageBehavior = false;
            this.queueList.View = System.Windows.Forms.View.Details;
            this.queueList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.QueueList_MouseDoubleClick);
            this.queueList.SelectedIndexChanged += new System.EventHandler(this.QueueSelected);
            // 
            // QueueDescription
            // 
            this.queueDescription.Text = "Description";
            this.queueDescription.Width = 182;
            // 
            // QueueQuantity
            // 
            this.queueQuantity.Text = "Quantity";
            this.queueQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.queueQuantity.Width = 54;
            // 
            // AddToQueue
            // 
            this.addToQueue.Enabled = false;
            this.addToQueue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addToQueue.Location = new System.Drawing.Point(272, 120);
            this.addToQueue.Name = "addToQueue";
            this.addToQueue.Size = new System.Drawing.Size(48, 24);
            this.addToQueue.TabIndex = 2;
            this.addToQueue.Text = "Add";
            this.addToQueue.Click += new System.EventHandler(this.AddToQueue_Click);
            // 
            // OK
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ok.Location = new System.Drawing.Point(440, 440);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(64, 24);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancel.Location = new System.Drawing.Point(520, 440);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(64, 24);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.designCost);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(8, 344);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 88);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Design Cost";
            // 
            // DesignCost
            // 
            this.designCost.Location = new System.Drawing.Point(8, 16);
            this.designCost.Name = "designCost";
            this.designCost.Size = new System.Drawing.Size(240, 64);
            this.designCost.TabIndex = 0;
            this.designCost.Value = new Nova.Common.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.productionCost);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(328, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 88);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Production Cost";
            // 
            // ProductionCost
            // 
            this.productionCost.Location = new System.Drawing.Point(8, 16);
            this.productionCost.Name = "productionCost";
            this.productionCost.Size = new System.Drawing.Size(240, 64);
            this.productionCost.TabIndex = 0;
            this.productionCost.Value = new Nova.Common.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
            // 
            // RemoveFromQueue
            // 
            this.removeFromQueue.Enabled = false;
            this.removeFromQueue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.removeFromQueue.Location = new System.Drawing.Point(272, 210);
            this.removeFromQueue.Name = "removeFromQueue";
            this.removeFromQueue.Size = new System.Drawing.Size(48, 24);
            this.removeFromQueue.TabIndex = 7;
            this.removeFromQueue.Text = "Remove";
            this.removeFromQueue.Click += new System.EventHandler(this.RemoveFromQueue_Click);
            // 
            // QueueUp
            // 
            this.queueUp.Enabled = false;
            this.queueUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.queueUp.Location = new System.Drawing.Point(272, 150);
            this.queueUp.Name = "queueUp";
            this.queueUp.Size = new System.Drawing.Size(48, 24);
            this.queueUp.TabIndex = 8;
            this.queueUp.Text = "Up";
            this.queueUp.Click += new System.EventHandler(this.QueueUp_Click);
            // 
            // QueueDown
            // 
            this.queueDown.Enabled = false;
            this.queueDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.queueDown.Location = new System.Drawing.Point(272, 180);
            this.queueDown.Name = "queueDown";
            this.queueDown.Size = new System.Drawing.Size(48, 24);
            this.queueDown.TabIndex = 9;
            this.queueDown.Text = "Down";
            this.queueDown.Click += new System.EventHandler(this.QueueDown_Click);
            // 
            // ProductionDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(594, 472);
            this.Controls.Add(this.queueDown);
            this.Controls.Add(this.queueUp);
            this.Controls.Add(this.removeFromQueue);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.addToQueue);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ProductionDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Production Queue";
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate the available designs items list box with the things we can build.
        /// Generally, we can build designs created by the player. However, we only
        /// include ships in the list if the star has a starbase with enough dock
        /// capacity to build the design. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, System.EventArgs e)
        {
            this.designList.BeginUpdate();

            Fleet starbase = this.queueStar.Starbase;
            int dockCapacity = 0;

            if (starbase != null)
            {
                dockCapacity = starbase.DockCapacity;
            }

            foreach (Design design in this.turnData.AllDesigns.Values)
            {
                if (design.Owner == ClientState.Data.RaceName || design.Owner == "*")
                {
                    if (starbase != null && starbase.Composition.ContainsKey(design.Name)) continue;

                    if (design.Type == "Ship")
                    {
                        if (dockCapacity > design.Mass)
                        {
                            this.designList.Items.Add(new ListViewItem(design.Name));
                        }
                    }
                    else
                    {
                        this.designList.Items.Add(new ListViewItem(design.Name));
                    }
                }
            }

            this.designList.EndUpdate();

            Gui.QueueList.Populate(this.queueList, this.queueStar.ManufacturingQueue);
            UpdateProductionCost();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Process a design being selected for possible construction.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AvailableSelected(object sender, System.EventArgs e)
        {
            if (this.designList.SelectedItems.Count <= 0) return;

            string name = this.designList.SelectedItems[0].Text;

            Design design =
               this.turnData.AllDesigns[this.stateData.RaceName + "/" + name] as Design;

            this.designCost.Value = design.Cost;

            if (this.addToQueue.Enabled == false)
            {
                this.addToQueue.Enabled = true;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Production queue item selected changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void QueueSelected(object sender, EventArgs e)
        {
            if (this.queueList.SelectedItems.Count > 0)
            {
                this.removeFromQueue.Enabled = true;
                if (this.queueList.SelectedIndices[0] > 0)
                {
                    this.queueUp.Enabled = true;
                }
                else
                {
                    this.queueUp.Enabled = false;
                }
                if (this.queueList.SelectedIndices[0] < this.queueList.Items.Count - 1)
                {
                    this.queueDown.Enabled = true;
                }
                else
                {
                    this.queueDown.Enabled = false;
                }
            }
            else
            {
                this.removeFromQueue.Enabled = false;
            }
        }
        /// <summary>
        /// Add to queue when double click on Design List
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DesignList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddToQueue_Click(sender, new EventArgs());
        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add to queue button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AddToQueue_Click(object sender, System.EventArgs e)
        {
            if (this.designList.SelectedItems.Count <= 0) return;

            string name = this.designList.SelectedItems[0].Text;
            Design design =
               this.turnData.AllDesigns[this.stateData.RaceName + "/" + name] as Design;

            // Starbases are handled differently to the other component types.
            if (design.Type == "Starbase")
            {
                AddStarbase(design);
                return;
            }

            // Ctrl-Add + 100 items
            // shift-Add +10 items
            // Add +1 items
            if (Button.ModifierKeys == Keys.Control)
            {
                AddDesign(design, 100);
            }
            else if (Button.ModifierKeys == Keys.Shift)
            {
                AddDesign(design, 10);
            }
            else
            {
                AddDesign(design, 1);
            }

        }

        /// <summary>
        /// Removes from queue on double click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RemoveFromQueue_Click(sender, new EventArgs());
        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Remove from queue button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RemoveFromQueue_Click(object sender, EventArgs e)
        {
            if (this.queueList.SelectedItems.Count > 0)
            {
                Design tmp = queueList.Items[queueList.SelectedIndices[0]].Tag as Design;
                if (tmp != null && tmp.Type == "Starbase")
                {
                    designList.Items.Add(new ListViewItem(tmp.Name));
                }
                queueList.Items.RemoveAt(queueList.SelectedIndices[0]);

                UpdateProductionCost();
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Move selected item up in queue
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void QueueUp_Click(object sender, EventArgs e)
        {
            if (this.queueList.SelectedItems.Count > 0)
            {
                int source = this.queueList.SelectedIndices[0];
                if (source > 0)
                {
                    ListViewItem newItem = this.queueList.Items[source];
                    ListViewItem oldItem = this.queueList.Items[source - 1];
                    this.queueList.Items.RemoveAt(source);
                    this.queueList.Items.RemoveAt(source - 1);
                    this.queueList.Items.Insert(source - 1, newItem);
                    this.queueList.Items.Insert(source, oldItem);
                }
            }
        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Move selected item down in queue 
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void QueueDown_Click(object sender, EventArgs e)
        {
            if (this.queueList.SelectedItems.Count > 0)
            {
                int source = this.queueList.SelectedIndices[0];
                if (source < this.queueList.Items.Count - 1)
                {
                    ListViewItem newItem = this.queueList.Items[source];
                    ListViewItem oldItem = this.queueList.Items[source + 1];
                    this.queueList.Items.RemoveAt(source + 1);
                    this.queueList.Items.RemoveAt(source);
                    this.queueList.Items.Insert(source, oldItem);
                    this.queueList.Items.Insert(source + 1, newItem);
                }
            }
        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Add a selected item into the production queue. If no item is selected in
        /// the queue, add the new one on the end. If an item is selected and it is the
        /// same type as the one being added then just increment the quantity.
        /// Otherwise, just add the item on at the end of the queue.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AddDesign(Design design, int quantity)
        {
            ListViewItem itemToAdd = new ListViewItem();
            ListViewItem itemAdded;

            itemToAdd.Text = design.Name;
            itemToAdd.Tag = design;
            itemToAdd.SubItems.Add(quantity.ToString());

            if (this.queueList.SelectedItems.Count == 0)
            {
                itemAdded = this.queueList.Items.Add(itemToAdd);
                itemToAdd.Selected = true;
            }
            else
            {
                int s = this.queueList.SelectedIndices[0];

                if (design.Name == this.queueList.Items[s].Text)
                {
                    itemAdded = this.queueList.Items[s];
                    int total = quantity;
                    total += Convert.ToInt32(this.queueList.Items[s].SubItems[1].Text);
                    this.queueList.Items[s].SubItems[1].Text = total.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    this.queueList.Items[s].Selected = false;
                    itemAdded = this.queueList.Items.Add(itemToAdd);
                    itemAdded.Selected = true;
                }
            }

            // Limit the number of defenses built.
            if (design.Name == "Defenses")
            {
                int newDefensesAllowed = Global.MaxDefenses - this.queueStar.Defenses;
                int newDefensesInQueue = Convert.ToInt32(itemAdded.SubItems[1].Text);
                if (newDefensesInQueue > newDefensesAllowed)
                {
                    if (newDefensesAllowed <= 0)
                    {
                        this.queueList.Items.Remove(itemAdded);
                    }
                    else
                    {
                        itemAdded.SubItems[1].Text = newDefensesAllowed.ToString();
                    }

                }

            }
            UpdateProductionCost();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// OK button pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OK_Click(object sender, System.EventArgs e)
        {
            this.queueStar.ManufacturingQueue.Queue.Clear();

            foreach (ListViewItem i in this.queueList.Items)
            {
                ProductionQueue.Item item = new ProductionQueue.Item();

                item.Name = i.SubItems[0].Text;
                item.Quantity = Convert.ToInt32(i.SubItems[1].Text);

                Design design = this.turnData.AllDesigns[this.stateData.RaceName + "/" + item.Name] as Design;

                item.BuildState = design.Cost;

                this.queueStar.ManufacturingQueue.Queue.Add(item);
            }

            Close();
        }

        #endregion

        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary> Add a starbase to the production queue. </summary>
        /// <remarks>
        /// Starbases are special in that there
        /// can ony ever be one in the production queue, no matter how many he tries to
        /// add.
        /// FIXME (priority 6) - Dan - What if I want to build a small base first, then add a larger base latter. I can queue two different base designs in Stars! 
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void AddStarbase(Design design)
        {
            // First run through the production queue to see if there is already a
            // starbase there. If there is remove it.
            int i = 0;
            for (i = 0; i < designList.Items.Count; i++)
                if (designList.Items[i].Text == design.Name)
                    break;
            designList.Items.RemoveAt(i);

            AddDesign(design, 1);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Update production queue cost
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void UpdateProductionCost()
        {
            Nova.Common.Resources cost = new Nova.Common.Resources();

            foreach (ListViewItem item in this.queueList.Items)
            {
                string name = item.Text;

                Design design = this.turnData.AllDesigns[this.stateData.RaceName + "/" + name] as Design;

                if (design == null)
                {
                    Report.FatalError("ProducationDialog.cs UpdateProducionCost() - Design \"" + this.stateData.RaceName + "/" + name + "\" no longer exists.");
                }

                int quantity = Convert.ToInt32(item.SubItems[1].Text);

                cost.Ironium += design.Cost.Ironium * quantity;
                cost.Boranium += design.Cost.Boranium * quantity;
                cost.Germanium += design.Cost.Germanium * quantity;
                cost.Energy += design.Cost.Energy * quantity;
            }

            this.productionCost.Value = cost;
        }

        #endregion
    }

}

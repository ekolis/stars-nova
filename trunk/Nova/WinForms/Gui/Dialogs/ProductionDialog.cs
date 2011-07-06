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

namespace Nova.WinForms.Gui
{
    using System;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Components;

    /// <Summary>
    /// Production queue dialog.
    /// </Summary>
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
        private Label label16;
        private Label label15;
        private Label totalCostBoranium;
        private Label totalCostGermanium;
        private Label totalCostEnergy;
        private Label totalCostIronium;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label selectedCostBoranium;
        private Label selectedCostGermanium;
        private Label selectedCostEnergy;
        private Label selectedCostIronium;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label totalCostYears;
        private Label selectedCostYears;
        private Label label17;
        private Label label10;
        private Label label8;
        private Label selectedPercentComplete;
        private Button queueDown;
        private System.Windows.Forms.CheckBox onlyLeftovers;
        #endregion

        #region Construction and Disposal

        /// <Summary>
        /// Initializes a new instance of the ProductionDialog class.
        /// </Summary>
        /// <param name="Star">The Star to do a production dialog for.</param>
        public ProductionDialog(Star star, ClientState stateData)
        {
            this.queueStar = star;
            this.stateData = stateData;
            this.turnData = this.stateData.InputTurn;

            InitializeComponent();
        }

        #endregion

        #region Windows Form Designer generated code
        /// <Summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </Summary>
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
            this.selectedPercentComplete = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.totalCostYears = new System.Windows.Forms.Label();
            this.selectedCostYears = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.totalCostBoranium = new System.Windows.Forms.Label();
            this.totalCostGermanium = new System.Windows.Forms.Label();
            this.totalCostEnergy = new System.Windows.Forms.Label();
            this.totalCostIronium = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.selectedCostBoranium = new System.Windows.Forms.Label();
            this.selectedCostGermanium = new System.Windows.Forms.Label();
            this.selectedCostEnergy = new System.Windows.Forms.Label();
            this.selectedCostIronium = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.removeFromQueue = new System.Windows.Forms.Button();
            this.queueUp = new System.Windows.Forms.Button();
            this.queueDown = new System.Windows.Forms.Button();
            this.onlyLeftovers = new System.Windows.Forms.CheckBox();
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
            // designList
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
            this.designList.Name = "designList";
            this.designList.Size = new System.Drawing.Size(240, 304);
            this.designList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.designList.TabIndex = 0;
            this.designList.TabStop = false;
            this.designList.UseCompatibleStateImageBehavior = false;
            this.designList.View = System.Windows.Forms.View.Details;
            this.designList.SelectedIndexChanged += new System.EventHandler(this.AvailableSelected);
            this.designList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DesignList_MouseDoubleClick);
            // 
            // description
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
            // queueList
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
            this.queueList.Name = "queueList";
            this.queueList.Size = new System.Drawing.Size(240, 304);
            this.queueList.TabIndex = 0;
            this.queueList.TabStop = false;
            this.queueList.UseCompatibleStateImageBehavior = false;
            this.queueList.View = System.Windows.Forms.View.Details;
            this.queueList.SelectedIndexChanged += new System.EventHandler(this.QueueSelected);
            this.queueList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.QueueList_MouseDoubleClick);
            // 
            // queueDescription
            // 
            this.queueDescription.Text = "Description";
            this.queueDescription.Width = 182;
            // 
            // queueQuantity
            // 
            this.queueQuantity.Text = "Quantity";
            this.queueQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.queueQuantity.Width = 54;
            // 
            // addToQueue
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
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ok.Location = new System.Drawing.Point(446, 494);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(64, 24);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.OK_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancel.Location = new System.Drawing.Point(526, 494);
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
            // designCost
            // 
            this.designCost.Location = new System.Drawing.Point(8, 16);
            this.designCost.Name = "designCost";
            this.designCost.Size = new System.Drawing.Size(240, 64);
            this.designCost.TabIndex = 0;
            this.designCost.Value = new Nova.Common.Resources(0, 0, 0, 0);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.selectedPercentComplete);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.totalCostYears);
            this.groupBox4.Controls.Add(this.selectedCostYears);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.totalCostBoranium);
            this.groupBox4.Controls.Add(this.totalCostGermanium);
            this.groupBox4.Controls.Add(this.totalCostEnergy);
            this.groupBox4.Controls.Add(this.totalCostIronium);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.selectedCostBoranium);
            this.groupBox4.Controls.Add(this.selectedCostGermanium);
            this.groupBox4.Controls.Add(this.selectedCostEnergy);
            this.groupBox4.Controls.Add(this.selectedCostIronium);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(328, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 142);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Production Cost";
            // 
            // selectedPercentComplete
            // 
            this.selectedPercentComplete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedPercentComplete.Location = new System.Drawing.Point(87, 112);
            this.selectedPercentComplete.Name = "selectedPercentComplete";
            this.selectedPercentComplete.Size = new System.Drawing.Size(56, 16);
            this.selectedPercentComplete.TabIndex = 37;
            this.selectedPercentComplete.Text = "0";
            this.selectedPercentComplete.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(143, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "% Complete";
            // 
            // totalCostYears
            // 
            this.totalCostYears.AutoSize = true;
            this.totalCostYears.Location = new System.Drawing.Point(204, 95);
            this.totalCostYears.Name = "totalCostYears";
            this.totalCostYears.Size = new System.Drawing.Size(25, 13);
            this.totalCostYears.TabIndex = 33;
            this.totalCostYears.Text = "???";
            // 
            // selectedCostYears
            // 
            this.selectedCostYears.AutoSize = true;
            this.selectedCostYears.Location = new System.Drawing.Point(118, 95);
            this.selectedCostYears.Name = "selectedCostYears";
            this.selectedCostYears.Size = new System.Drawing.Size(25, 13);
            this.selectedCostYears.TabIndex = 32;
            this.selectedCostYears.Text = "???";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 95);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 31;
            this.label17.Text = "Years";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(198, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "Total";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(94, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Selected";
            // 
            // totalCostBoranium
            // 
            this.totalCostBoranium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCostBoranium.Location = new System.Drawing.Point(173, 47);
            this.totalCostBoranium.Name = "totalCostBoranium";
            this.totalCostBoranium.Size = new System.Drawing.Size(56, 16);
            this.totalCostBoranium.TabIndex = 28;
            this.totalCostBoranium.Text = "0";
            this.totalCostBoranium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // totalCostGermanium
            // 
            this.totalCostGermanium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCostGermanium.Location = new System.Drawing.Point(173, 63);
            this.totalCostGermanium.Name = "totalCostGermanium";
            this.totalCostGermanium.Size = new System.Drawing.Size(56, 16);
            this.totalCostGermanium.TabIndex = 27;
            this.totalCostGermanium.Text = "0";
            this.totalCostGermanium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // totalCostEnergy
            // 
            this.totalCostEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCostEnergy.Location = new System.Drawing.Point(173, 79);
            this.totalCostEnergy.Name = "totalCostEnergy";
            this.totalCostEnergy.Size = new System.Drawing.Size(56, 16);
            this.totalCostEnergy.TabIndex = 26;
            this.totalCostEnergy.Text = "0";
            this.totalCostEnergy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // totalCostIronium
            // 
            this.totalCostIronium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCostIronium.Location = new System.Drawing.Point(173, 31);
            this.totalCostIronium.Name = "totalCostIronium";
            this.totalCostIronium.Size = new System.Drawing.Size(56, 16);
            this.totalCostIronium.TabIndex = 25;
            this.totalCostIronium.Text = "0";
            this.totalCostIronium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.Location = new System.Drawing.Point(229, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 16);
            this.label12.TabIndex = 24;
            this.label12.Text = "kT";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(229, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 16);
            this.label13.TabIndex = 23;
            this.label13.Text = "kT";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(229, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 22;
            this.label14.Text = "kT";
            // 
            // selectedCostBoranium
            // 
            this.selectedCostBoranium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedCostBoranium.Location = new System.Drawing.Point(87, 47);
            this.selectedCostBoranium.Name = "selectedCostBoranium";
            this.selectedCostBoranium.Size = new System.Drawing.Size(56, 16);
            this.selectedCostBoranium.TabIndex = 21;
            this.selectedCostBoranium.Text = "0";
            this.selectedCostBoranium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // selectedCostGermanium
            // 
            this.selectedCostGermanium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedCostGermanium.Location = new System.Drawing.Point(87, 63);
            this.selectedCostGermanium.Name = "selectedCostGermanium";
            this.selectedCostGermanium.Size = new System.Drawing.Size(56, 16);
            this.selectedCostGermanium.TabIndex = 20;
            this.selectedCostGermanium.Text = "0";
            this.selectedCostGermanium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // selectedCostEnergy
            // 
            this.selectedCostEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedCostEnergy.Location = new System.Drawing.Point(87, 79);
            this.selectedCostEnergy.Name = "selectedCostEnergy";
            this.selectedCostEnergy.Size = new System.Drawing.Size(56, 16);
            this.selectedCostEnergy.TabIndex = 19;
            this.selectedCostEnergy.Text = "0";
            this.selectedCostEnergy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // selectedCostIronium
            // 
            this.selectedCostIronium.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedCostIronium.Location = new System.Drawing.Point(87, 31);
            this.selectedCostIronium.Name = "selectedCostIronium";
            this.selectedCostIronium.Size = new System.Drawing.Size(56, 16);
            this.selectedCostIronium.TabIndex = 18;
            this.selectedCostIronium.Text = "0";
            this.selectedCostIronium.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(143, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "kT";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(143, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "kT";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(143, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "kT";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.YellowGreen;
            this.label4.Location = new System.Drawing.Point(7, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Boranium";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Goldenrod;
            this.label3.Location = new System.Drawing.Point(7, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Germanium";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(7, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Resources";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ironium";
            // 
            // removeFromQueue
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
            // queueUp
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
            // queueDown
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
            // onlyLeftovers
            // 
            this.onlyLeftovers.Location = new System.Drawing.Point(16, 456);
            this.onlyLeftovers.Name = "onlyLeftovers";
            this.onlyLeftovers.Size = new System.Drawing.Size(248, 30);
            this.onlyLeftovers.TabIndex = 10;
            this.onlyLeftovers.Text = "Contribute only leftover resources to research";
            this.onlyLeftovers.UseVisualStyleBackColor = true;
            this.onlyLeftovers.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // ProductionDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 526);
            this.Controls.Add(this.onlyLeftovers);
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
            this.Controls.Add(this.onlyLeftovers);
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
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
        }
        
        #endregion

        #region Event Methods

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
            this.onlyLeftovers.Checked = this.queueStar.OnlyLeftover;
            
            this.designList.BeginUpdate();

            Fleet starbase = this.queueStar.Starbase;
            int dockCapacity = 0;

            if (starbase != null)
            {
                dockCapacity = starbase.TotalDockCapacity;
            }

            foreach (Design design in this.turnData.AllDesigns.Values)
            {
                // check if this design belongs to this race
                if (design.Owner == stateData.EmpireIntel.Id || design.Owner == Global.AllEmpires)
                {
                    // what the purpose of this next line (shadallark) ???
                    // Looks like it is ment to prevent the current starbase design being re-used - Dan.
                    // prevent the current starbase design from being re-used
                    if (starbase != null && starbase.Composition.ContainsKey(design.Name))
                    {
                        continue;
                    }

                    // Check if this design can be built at this Star - ships are limited by dock capacity of the starbase.
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
            this.addToQueue.Enabled = false;

            Gui.QueueList.Populate(this.queueList, this.queueStar.ManufacturingQueue);
            // check if a starbase design is in the Production Queue and if so remove it from the Design List
            int itemLoopCounter = 0; // outer loop counter used for stepping through the Production Queue
            for (itemLoopCounter = 0; itemLoopCounter < this.queueList.Items.Count; itemLoopCounter++)
            {
                // is it a starbase?
                string tempName = this.queueList.Items[itemLoopCounter].Text;
                Design tempDesign = turnData.AllDesigns[stateData.EmpireIntel.Id + "/" + tempName];
                if (tempDesign.Type == "Starbase")
                {
                    this.queueList.Items[itemLoopCounter].Checked = true;
                    int designsLoopCounter = 0; // inner loop counter used for stepping through the Design List
                    for (designsLoopCounter = 0; designsLoopCounter < this.designList.Items.Count; designsLoopCounter++)
                    {
                        if (this.queueList.Items[itemLoopCounter].Text == this.designList.Items[designsLoopCounter].Text)
                        {
                            // remove the starbase from the Design List
                            designList.Items.RemoveAt(designsLoopCounter);
                            designsLoopCounter--; // after having removed one Item from the list decrement by 1 to allow the rest of the list to be examined
                        }
                    }
                }
                else
                {
                    this.queueList.Items[itemLoopCounter].Checked = false;
                }
            }
            
            // add the "--- Top of Queue ---" or "--- Queue Empty ---" message to the production Queue
            if (this.queueList.Items.Count == 0)
            {
                this.queueList.Items.Insert(0, "--- Queue is Empty ---");
            }
            else
            {
                this.queueList.Items.Insert(0, "--- Top of Queue ---");
            }
            this.queueList.Items[this.queueList.Items.Count - 1].Selected = true;
            
            UpdateProductionCost();
        }

        /// <Summary>
        /// Process a design being selected for possible construction.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void AvailableSelected(object sender, System.EventArgs e)
        {
            if (this.designList.SelectedItems.Count <= 0)  
            {
                // nothing selected in the design list
                this.addToQueue.Enabled = false;
                Resources emptyResources = new Resources();
                this.designCost.Value = emptyResources;
                return;
            }
            else
            {
                this.addToQueue.Enabled = true;
                string name = this.designList.SelectedItems[0].Text;

                Design design = turnData.AllDesigns[stateData.EmpireIntel.Id + "/" + name];

                this.designCost.Value = design.Cost;
            }
        }

        /// <Summary>
        /// Production queue Item selected changed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueSelected(object sender, EventArgs e)
        {
            // check if selected Item is the "--- Top of Queue ---" which cannot be moved down or removed
            if (this.queueList.SelectedItems.Count > 0)
            {
                if (this.queueList.SelectedIndices[0] == 0)
                {
                    // "--- Top of Queue ---" selected
                    this.queueUp.Enabled = false;
                    this.queueDown.Enabled = false;
                    this.removeFromQueue.Enabled = false;
                    // this.addToQueue.Enabled = true;
                }
                else
                {
                    this.removeFromQueue.Enabled = true;
                    // check if >1 to ignore top two items ("--- Top of Queue ---" placeholder which cannot be moved and Item below it)
                    if (this.queueList.SelectedIndices[0] > 1)
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
            }
            else
            {
                // no items are selected
                this.queueUp.Enabled = false;
                this.queueDown.Enabled = false;
                this.removeFromQueue.Enabled = false;
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
            if (this.designList.SelectedItems.Count <= 0)
            {
                return;
            }

            string name = this.designList.SelectedItems[0].Text;
            Design design = turnData.AllDesigns[stateData.EmpireIntel.Id + "/" + name];

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
            
            // confirm the Production Queue is not empty and ensure top of queue placeholder is labelled correctly
            if (this.queueList.Items.Count == 1)
            {
                this.queueList.Items.RemoveAt(0);
                this.queueList.Items.Insert(0, "--- Top of Queue ---");
                // this.queueList.Items[0].Selected = true;
            }
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
            int currentQuantity = Convert.ToInt32(queueList.Items[s].SubItems[1].Text);
            int numToRemove = 0;

            if (this.queueList.SelectedItems.Count > 0)
            {
                // Design selectedDesign = queueList.Items[queueList.SelectedIndices[0]].Tag as Design;
                string designName = this.queueList.Items[queueList.SelectedIndices[0]].Text;
                // Design selectedDesign = this.turnData.AllDesigns[this.stateData.RaceName + "/" + designName] as Design;
                // if (selectedDesign != null && selectedDesign.Type == "Starbase")
                if (queueList.Items[s].Checked == true)
                {
                    designList.Items.Add(new ListViewItem(designName));
                }

                // Ctrl -Remove 100 items
                // Shift -Remove 10 items
                //       -Remove 1 Item
                switch (Button.ModifierKeys)
                {
                    case Keys.Control:
                        numToRemove = 100;
                        break;
                    case Keys.Shift:
                        numToRemove = 10;
                        break;
                    default:
                        numToRemove = 1;
                        break;
                }

                if (numToRemove >= currentQuantity)
                {
                    queueList.Items.RemoveAt(queueList.SelectedIndices[0]);
                }
                else
                {
                    int remaining = currentQuantity - numToRemove;
                    queueList.Items[s].SubItems[1].Text = remaining.ToString();
                }

                UpdateProductionCost();
                
                // check if the Production Queue is now empty and if so change the text of the top of queue place holder
                if (this.queueList.Items.Count == 1)
                {
                    this.queueList.Items.RemoveAt(0);
                    this.queueList.Items.Insert(0, "--- Queue is Empty ---");
                    this.queueList.Items[0].Selected = true;
                }
            }
        }

        /// <Summary>
        /// Move selected Item up in queue
        /// </Summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueUp_Click(object sender, EventArgs e)
        {
            if (this.queueList.SelectedItems.Count > 0)
            {
                int source = this.queueList.SelectedIndices[0];
                // must be greater than 0 due to "--- Top of Queue ---" Placeholder
                if (source > 0)
                {
                    ListViewItem newItem = this.queueList.Items[source];
                    ListViewItem oldItem = this.queueList.Items[source - 1];
                    this.queueList.Items.RemoveAt(source);
                    this.queueList.Items.RemoveAt(source - 1);
                    this.queueList.Items.Insert(source - 1, newItem);
                    this.queueList.Items.Insert(source, oldItem);
                    this.queueDown.Enabled = true;
                }                
            }
            UpdateProductionCost();
        }
        
        /// <Summary>
        /// Move selected Item down in queue 
        /// </Summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void QueueDown_Click(object sender, EventArgs e)
        {
            if (this.queueList.SelectedItems.Count > 0)
            {
                int source = this.queueList.SelectedIndices[0];
                 // check if > 0 for Top of Queue place holder
                if (source < this.queueList.Items.Count - 1 && source > 0)
                {
                    ListViewItem newItem = this.queueList.Items[source];
                    ListViewItem oldItem = this.queueList.Items[source + 1];
                    this.queueList.Items.RemoveAt(source + 1);
                    this.queueList.Items.RemoveAt(source);
                    this.queueList.Items.Insert(source, oldItem);
                    this.queueList.Items.Insert(source + 1, newItem);
                    this.queueList.Items[source + 1].Selected = true;
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
        private void AddDesign(Design design, int quantity)
        {
            ListViewItem itemToAdd = new ListViewItem();
            ListViewItem itemAdded;

            itemToAdd.Text = design.Name;
            itemToAdd.SubItems.Add(quantity.ToString());
            itemToAdd.Tag = design.Cost;    // when first added the partial BuildState is the full design cost
            // set the Checked Status if this is a Starbase
            if (design.Type == "Starbase")
            {
                itemToAdd.Checked = true;
            }
            else
            {
                itemToAdd.Checked = false;
            }


            // if no items are selected add the quantity of design as indicated
            if (this.queueList.SelectedItems.Count == 0)
            {
                itemAdded = this.queueList.Items.Add(itemToAdd);
                itemToAdd.Selected = true;
            }
            else
            {
                int selectedProduction = this.queueList.SelectedIndices[0];

                // if the Item selected in the queue is the same as the design being added increase the quantity
                if (design.Name == this.queueList.Items[selectedProduction].Text)
                {
                    itemAdded = this.queueList.Items[selectedProduction];
                    int total = quantity;
                    total += Convert.ToInt32(this.queueList.Items[selectedProduction].SubItems[1].Text);
                    this.queueList.Items[selectedProduction].SubItems[1].Text = total.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    // as the Item selected in the queue is different from the design being added check the Item
                    // below the selected Item (first confirm it exists) to see if it matches, if so increase its
                    // quantity and have it become the selected Item, if not add the Item after the Item selected in the queue
                    int numInQueue = this.queueList.Items.Count;
                    int nextIndex = selectedProduction + 1;
                    if (numInQueue > nextIndex)    
                    {
                        if (design.Name == this.queueList.Items[nextIndex].Text)
                        {    
                            // the design is the same as the Item after the selected Item in the queue so update the Item after
                            itemAdded = this.queueList.Items[nextIndex];
                            int total = quantity;
                            total += Convert.ToInt32(this.queueList.Items[nextIndex].SubItems[1].Text);
                            this.queueList.Items[nextIndex].SubItems[1].Text = total.ToString(System.Globalization.CultureInfo.InvariantCulture);
                            this.queueList.Items[nextIndex].Selected = true;
                        }
                        else
                        {
                            // add the design after the Item selected in the queue
                            itemAdded = this.queueList.Items.Insert(nextIndex, itemToAdd);
                            this.queueList.Items[nextIndex].Selected = true;
                        }
                    }
                    else
                    {
                        this.queueList.Items[selectedProduction].Selected = false;
                        itemAdded = this.queueList.Items.Add(itemToAdd);
                        itemAdded.Selected = true;
                    }
                }
            }

            // Limit the number of defenses built.
            // TODO (Priority 4) - update this section to handle the quantity when too many have been added!
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
                        itemAdded.SubItems[1].Text = newDefensesAllowed.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }
            this.queueList.Items[0].Text = "--- Top of Queue ---";
            UpdateProductionCost();
        }

        /// <Summary>
        /// OK button pressed.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void OK_Click(object sender, System.EventArgs e)
        {            
            this.queueStar.ManufacturingQueue.Queue.Clear();
            
            // remove the "--- Top of Queue ---" / "--- Queue Empty ---" placeholder prior to saving the Production Queue
            queueList.Items.RemoveAt(0);
            
            foreach (ListViewItem itemInList in this.queueList.Items)
            {
                ProductionQueue.Item item = new ProductionQueue.Item();

                item.Name = itemInList.SubItems[0].Text;
                item.Quantity = Convert.ToInt32(itemInList.SubItems[1].Text);

                Design design = turnData.AllDesigns[stateData.EmpireIntel.Id + "/" + item.Name];

                item.BuildState = itemInList.Tag as Resources;

                this.queueStar.ManufacturingQueue.Queue.Add(item);
            }
            
            Close();
        }
        
        private void CheckChanged(object sender, EventArgs e)
        {
            // Update estimated time if user changes resource
            // contribution method.
            this.queueStar.OnlyLeftover = this.onlyLeftovers.Checked;
            UpdateProductionCost();
        }

        #endregion

        #region Utility Methods

        /// <Summary> Add a starbase to the production queue. </Summary>
        /// <remarks>
        /// Starbases are special in that there
        /// can ony ever be one in the production queue, no matter how many he tries to
        /// add.
        /// FIXME (priority 6) - Dan - What if I want to build a small base first, then add a larger base latter. I can queue two different base designs in Stars! 
        /// </remarks>
        private void AddStarbase(Design design)
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

            AddDesign(design, 1);
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

            Race starOwnerRace = this.queueStar.ThisRace;
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
                potentialResources.Ironium = this.queueStar.ResourcesOnHand.Ironium;
                potentialResources.Boranium = this.queueStar.ResourcesOnHand.Boranium;
                potentialResources.Germanium = this.queueStar.ResourcesOnHand.Germanium;
            }

            // check if there are any items in the Production Queue before attempting to determine costs
            // requires checking if this.queueList.Items.Count > than 1 due to Placeholder value at Top of the Queue
            if (this.queueList.Items.Count > 1)
            {
                int selectedItemIndex = 0;
                if (this.queueList.SelectedItems.Count > 0)
                {
                    selectedItemIndex = this.queueList.SelectedIndices[0];
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
                Resources currentStackCost = new Resources();
                bool allBuilt = false;
                int quantityYetToBuild;

                for (int queueIndex = 1; queueIndex < queueList.Items.Count; queueIndex++)
                {
                    string designName = queueList.Items[queueIndex].Text;
                    Design currentDesign = turnData.AllDesigns[stateData.EmpireIntel.Id + "/" + designName];

                    quantityYetToBuild = Convert.ToInt32(queueList.Items[queueIndex].SubItems[1].Text);
                    currentStackCost = GetProductionCosts(queueList.Items[queueIndex]);
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
                            potentialResources.Energy += this.queueStar.GetFutureResourceRate(factoriesInQueue);
                            
                            // Account for resources destinated for research.
                            // Use the set client percentage, not the planet allocation. This is because the
                            // allocated resources only change during turn generation, but the budget may change
                            // many times while playing a turn. This makes the Star's values out of sync, so,
                            // predict them for now.
                            // Only do this if the Star is respecting research budget.
                            if (this.queueStar.OnlyLeftover == false)
                            {
                                potentialResources.Energy -= potentialResources.Energy * stateData.EmpireIntel.ResearchBudget / 100;
                            }

                            // need to know how much of each mineral is currently available on the Star (this.queueStar.ResourcesOnHand)
                            // need race information to determine how many minerals are produced by each mine each year
                            // need to make sure that no more than the maximum number of mines operable by colonists are being operated
                            // add one year of mining results to the remaining potentialResources
                            // UPDATED May 11: to use native Star methods of mining rate prediction.
                            potentialResources.Ironium += this.queueStar.GetFutureMiningRate(this.queueStar.MineralConcentration.Ironium, minesInQueue);
                            potentialResources.Boranium += this.queueStar.GetFutureMiningRate(this.queueStar.MineralConcentration.Boranium, minesInQueue);
                            potentialResources.Germanium += this.queueStar.GetFutureMiningRate(this.queueStar.MineralConcentration.Germanium, minesInQueue);

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
                                if (currentDesign.Type == "Mine")
                                {
                                    minesInQueue += quantityYetToBuild;
                                }
                                if (currentDesign.Type == "Factory")
                                {
                                    factoriesInQueue += quantityYetToBuild;
                                }
                            }
                            else
                            {
                                // not everything in the stack can be built this year
                                maxYearsSelected = -1;

                                // the current build state is the cost of the first Item found by (total cost - (cost of all but one))
                                Resources currentBuildState = new Resources();
                                currentBuildState = currentStackCost - ((quantityYetToBuild - 1) * currentDesign.Cost);

                                // determine the percentage able to be completed by whichever resource is limiting production
                                double fractionComplete = 1.0;
                                if (currentStackCost.Ironium > 0)
                                {
                                    fractionComplete = Convert.ToDouble(potentialResources.Ironium) / currentStackCost.Ironium;
                                }
                                if (currentStackCost.Boranium > 0)
                                {
                                    if (Convert.ToDouble(potentialResources.Boranium) / currentStackCost.Boranium < fractionComplete)
                                    {
                                        fractionComplete = Convert.ToDouble(potentialResources.Boranium) / currentStackCost.Boranium;
                                    }
                                }
                                if (currentStackCost.Germanium > 0)
                                {
                                    if (Convert.ToDouble(potentialResources.Germanium) / currentStackCost.Germanium < fractionComplete)
                                    {
                                        fractionComplete = Convert.ToDouble(potentialResources.Germanium) / currentStackCost.Germanium;
                                    }
                                }
                                if (currentStackCost.Energy > 0)
                                {
                                    if (Convert.ToDouble(potentialResources.Energy) / currentStackCost.Energy < fractionComplete)
                                    {
                                        fractionComplete = Convert.ToDouble(potentialResources.Energy) / currentStackCost.Energy;
                                    }
                                }
                                // apply this percentage to the currentStackCost to determine how much to remove from
                                // potentialResources and currentStackCost
                                Resources amountUsed = new Resources();
                                
                                // Use the Resource operator * instead! -Aeglos
                                // amountUsed.Ironium = fractionComplete * currentStackCost.Ironium;
                                // amountUsed.Boranium = fractionComplete * currentStackCost.Boranium;
                                // amountUsed.Germanium = fractionComplete * currentStackCost.Germanium;
                                // amountUsed.Energy = fractionComplete * currentStackCost.Energy;
                                amountUsed = fractionComplete * currentStackCost;
                                
                                potentialResources = potentialResources - amountUsed;
                                currentStackCost = currentStackCost - amountUsed;

                                // check if at least one Item in the stack can be built "this" year
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
                                        if (amountUsed <= (currentBuildState + (quantityStepper * currentDesign.Cost)))
                                        {
                                            quantityYetToBuild = quantityYetToBuild - quantityStepper;
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
                                this.queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                this.queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                        else
                        {
                            if (minYearsCurrent == -1)
                            {
                                this.queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                this.queueList.Items[queueIndex].ForeColor = System.Drawing.Color.Black;
                            }
                        }
                    }

                    if (queueIndex == selectedItemIndex)
                    {
                        selectedItemCost = GetProductionCosts(this.queueList.Items[queueIndex]);

                        Resources currentBuildState = this.queueList.Items[queueIndex].Tag as Resources;

                        // determine the percent complete; defined as the resource that is the most complete
                        if (currentDesign.Cost.Ironium > 0)
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Ironium) / currentDesign.Cost.Ironium));
                        }
                        if (currentDesign.Cost.Boranium > 0 && percentComplete < 100 * (1.0 - (Convert.ToDouble(currentBuildState.Boranium) / currentDesign.Cost.Boranium)))
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Boranium) / currentDesign.Cost.Boranium));
                        }
                        if (currentDesign.Cost.Germanium > 0 && percentComplete < 100 * (1.0 - (Convert.ToDouble(currentBuildState.Germanium) / currentDesign.Cost.Germanium)))
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Germanium) / currentDesign.Cost.Germanium));
                        }
                        if (currentDesign.Cost.Energy > 0 && percentComplete < 100 * (1.0 - (Convert.ToDouble(currentBuildState.Energy) / currentDesign.Cost.Energy)))
                        {
                            percentComplete = 100 * (1.0 - (Convert.ToDouble(currentBuildState.Energy) / currentDesign.Cost.Energy));
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

        /// <Summary>
        /// Determine the amount of Resources required to produce a stack of items from the ListView
        /// For each stack of items the first Item might be partially built as defined by
        /// the BuildState and therefore require less Resources than the rest of the items
        /// in the stack which require the design.cost
        /// </Summary>
        /// <returns>The resources required to produce the Item(s) of interest.</returns>
        /// <param name="itemOfInterest">The Item(s) from the Production ListView to be evaluated</param>
        private Resources GetProductionCosts(ListViewItem stackOfInterest)
        {
            Resources costsToProduce = new Resources();

            if (stackOfInterest.Tag == null)
            {
                Report.Error("No Buildstate associated with: " + stackOfInterest.Text);
                return costsToProduce;
            }
            else
            {
                costsToProduce = stackOfInterest.Tag as Resources;
            }

            int stackQuantity = Convert.ToInt32(stackOfInterest.SubItems[1].Text);

            // this check should not be required, but better to be safe than sorry
            if (stackQuantity > 1)  
            {
                string designName = stackOfInterest.Text;
                Design currentDesign = turnData.AllDesigns[stateData.EmpireIntel.Id + "/" + designName];
                    // as the first Item in the stack costs BuildState to complete the design cost
                    // is multiplied by the quantity - 1
                costsToProduce += currentDesign.Cost * (stackQuantity - 1);
            }

            return costsToProduce;
        }

        #endregion
    }
}

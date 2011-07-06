using System;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.WinForms.Gui
{
    public partial class ProductionDialog : System.Windows.Forms.Form
    {
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
    
    }
}
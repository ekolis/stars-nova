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
// Dialog for designing a ship or starbase.
// ===========================================================================
#endregion

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Nova.Common;
using Nova.Common.Components;
using Nova.ControlLibrary;
using Nova.Client;


namespace Nova.WinForms.Gui
{

    /// <summary>
    /// Ship Design Dialog
    /// </summary>
    public class ShipDesignDialog : System.Windows.Forms.Form
    {
        private ClientState StateData = null;
        private Hashtable AllComponents = null;
        private Hashtable AllDesigns = null;
        private Hashtable ImageIndices = new Hashtable();
        private Nova.Common.Components.Component SelectedHull = null;

        private ImageList ComponentImages = new ImageList();
        private int DesignMass = 0;
        private int ShipCargoCapacity = 0;

        #region Designer Generated Code

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DesignName;
        private ControlLibrary.ResourceDisplay DesignResources;
        private System.Windows.Forms.Label label9;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label8;
        private Label label5;
        private Label label4;
        private Label CapacityType;
        private Label label2;
        private Label label10;
        private ComboBox HullList;
        private Label label11;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label CapacityUnits;
        private Label ShipMass;
        private Label ShipCloak;
        private Label ShipShields;
        private Label ShipArmor;
        private Label MaxCapacity;
        private GroupBox groupBox1;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private TreeView TreeView;
        private ListView ListView;
        private GroupBox groupBox5;
        private ControlLibrary.ResourceDisplay ComponentCost;
        private Label label6;
        private Label ComponentMass;
        private Label label7;
        private GroupBox groupBox6;
        private TextBox Description;
        private PictureBox HullImage;
        private Label label12;
        private Label CargoCapacity;
        private Label label3;
        private ControlLibrary.HullGrid HullGrid;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipDesignDialog));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Armor");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Beam Weapons");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Bombs");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("CargoPod");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Engines");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Mechanical");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Mine Layer");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Missiles");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Orbital");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Scanners");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Shields");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Available Technology", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            this.Cancel = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.HullImage = new System.Windows.Forms.PictureBox();
            this.HullGrid = new ControlLibrary.HullGrid();
            this.label10 = new System.Windows.Forms.Label();
            this.HullList = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CargoCapacity = new System.Windows.Forms.Label();
            this.ShipCloak = new System.Windows.Forms.Label();
            this.ShipShields = new System.Windows.Forms.Label();
            this.ShipArmor = new System.Windows.Forms.Label();
            this.MaxCapacity = new System.Windows.Forms.Label();
            this.ShipMass = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.CapacityUnits = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CapacityType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DesignResources = new ControlLibrary.ResourceDisplay();
            this.DesignName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Description = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ComponentMass = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ComponentCost = new ControlLibrary.ResourceDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TreeView = new System.Windows.Forms.TreeView();
            this.ListView = new System.Windows.Forms.ListView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HullImage)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Cancel.Location = new System.Drawing.Point(698, 587);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 0;
            this.Cancel.Text = "Cancel";
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SaveButton.Location = new System.Drawing.Point(618, 587);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.Click += new System.EventHandler(this.OK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.HullImage);
            this.groupBox2.Controls.Add(this.HullGrid);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.HullList);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.DesignName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(393, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 563);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New Design";
            // 
            // HullImage
            // 
            this.HullImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HullImage.Location = new System.Drawing.Point(288, 18);
            this.HullImage.Name = "HullImage";
            this.HullImage.Size = new System.Drawing.Size(64, 64);
            this.HullImage.TabIndex = 19;
            this.HullImage.TabStop = false;
            // 
            // HullGrid
            // 
            this.HullGrid.ActiveModules = ((System.Collections.ArrayList)(resources.GetObject("HullGrid.ActiveModules")));
            this.HullGrid.HideEmptyModules = true;
            this.HullGrid.Location = new System.Drawing.Point(23, 88);
            this.HullGrid.Name = "HullGrid";
            this.HullGrid.Size = new System.Drawing.Size(338, 338);
            this.HullGrid.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Hull Type";
            // 
            // HullList
            // 
            this.HullList.FormattingEnabled = true;
            this.HullList.Location = new System.Drawing.Point(88, 51);
            this.HullList.Name = "HullList";
            this.HullList.Size = new System.Drawing.Size(160, 21);
            this.HullList.Sorted = true;
            this.HullList.TabIndex = 16;
            this.HullList.SelectedValueChanged += new System.EventHandler(this.HullList_SelectedValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.CargoCapacity);
            this.groupBox4.Controls.Add(this.ShipCloak);
            this.groupBox4.Controls.Add(this.ShipShields);
            this.groupBox4.Controls.Add(this.ShipArmor);
            this.groupBox4.Controls.Add(this.MaxCapacity);
            this.groupBox4.Controls.Add(this.ShipMass);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.CapacityUnits);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.CapacityType);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(196, 435);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(165, 115);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Primary Characteristics";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Cargo Capacity";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(144, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "kT";
            // 
            // CargoCapacity
            // 
            this.CargoCapacity.Location = new System.Drawing.Point(95, 96);
            this.CargoCapacity.Name = "CargoCapacity";
            this.CargoCapacity.Size = new System.Drawing.Size(50, 13);
            this.CargoCapacity.TabIndex = 16;
            this.CargoCapacity.Text = "0";
            this.CargoCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShipCloak
            // 
            this.ShipCloak.Location = new System.Drawing.Point(95, 79);
            this.ShipCloak.Name = "ShipCloak";
            this.ShipCloak.Size = new System.Drawing.Size(50, 13);
            this.ShipCloak.TabIndex = 14;
            this.ShipCloak.Text = "0";
            this.ShipCloak.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShipShields
            // 
            this.ShipShields.Location = new System.Drawing.Point(95, 65);
            this.ShipShields.Name = "ShipShields";
            this.ShipShields.Size = new System.Drawing.Size(50, 13);
            this.ShipShields.TabIndex = 13;
            this.ShipShields.Text = "0";
            this.ShipShields.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShipArmor
            // 
            this.ShipArmor.Location = new System.Drawing.Point(95, 50);
            this.ShipArmor.Name = "ShipArmor";
            this.ShipArmor.Size = new System.Drawing.Size(50, 13);
            this.ShipArmor.TabIndex = 12;
            this.ShipArmor.Text = "0";
            this.ShipArmor.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MaxCapacity
            // 
            this.MaxCapacity.Location = new System.Drawing.Point(95, 34);
            this.MaxCapacity.Name = "MaxCapacity";
            this.MaxCapacity.Size = new System.Drawing.Size(50, 13);
            this.MaxCapacity.TabIndex = 11;
            this.MaxCapacity.Text = "0";
            this.MaxCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShipMass
            // 
            this.ShipMass.Location = new System.Drawing.Point(95, 19);
            this.ShipMass.Name = "ShipMass";
            this.ShipMass.Size = new System.Drawing.Size(50, 13);
            this.ShipMass.TabIndex = 10;
            this.ShipMass.Text = "0";
            this.ShipMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(146, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(143, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "dp";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(143, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "dp";
            // 
            // CapacityUnits
            // 
            this.CapacityUnits.AutoSize = true;
            this.CapacityUnits.Location = new System.Drawing.Point(143, 34);
            this.CapacityUnits.Name = "CapacityUnits";
            this.CapacityUnits.Size = new System.Drawing.Size(21, 13);
            this.CapacityUnits.TabIndex = 6;
            this.CapacityUnits.Text = "mg";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(144, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "kT";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Cloak and Jam";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Shields";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Armor";
            // 
            // CapacityType
            // 
            this.CapacityType.AutoSize = true;
            this.CapacityType.Location = new System.Drawing.Point(6, 34);
            this.CapacityType.Name = "CapacityType";
            this.CapacityType.Size = new System.Drawing.Size(94, 13);
            this.CapacityType.TabIndex = 1;
            this.CapacityType.Text = "Max Fuel Capacity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mass";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DesignResources);
            this.groupBox3.Location = new System.Drawing.Point(14, 435);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(166, 115);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Build Cost";
            // 
            // DesignResources
            // 
            this.DesignResources.Location = new System.Drawing.Point(9, 19);
            this.DesignResources.Name = "DesignResources";
            this.DesignResources.Size = new System.Drawing.Size(150, 64);
            this.DesignResources.TabIndex = 10;
            this.DesignResources.Value = new Nova.Common.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
            // 
            // DesignName
            // 
            this.DesignName.Location = new System.Drawing.Point(88, 24);
            this.DesignName.Name = "DesignName";
            this.DesignName.Size = new System.Drawing.Size(160, 20);
            this.DesignName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Design Name";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 563);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Technology";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Description);
            this.groupBox6.Location = new System.Drawing.Point(14, 24);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(344, 112);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Component Summary";
            // 
            // Description
            // 
            this.Description.AcceptsTab = true;
            this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Description.Location = new System.Drawing.Point(7, 18);
            this.Description.Multiline = true;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Size = new System.Drawing.Size(326, 81);
            this.Description.TabIndex = 8;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ComponentMass);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.ComponentCost);
            this.groupBox5.Location = new System.Drawing.Point(173, 435);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(166, 115);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Component Build Cost";
            // 
            // ComponentMass
            // 
            this.ComponentMass.Location = new System.Drawing.Point(94, 83);
            this.ComponentMass.Name = "ComponentMass";
            this.ComponentMass.Size = new System.Drawing.Size(50, 13);
            this.ComponentMass.TabIndex = 15;
            this.ComponentMass.Text = "0";
            this.ComponentMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "kT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Mass";
            // 
            // ComponentCost
            // 
            this.ComponentCost.Location = new System.Drawing.Point(8, 19);
            this.ComponentCost.Name = "ComponentCost";
            this.ComponentCost.Size = new System.Drawing.Size(150, 64);
            this.ComponentCost.TabIndex = 0;
            this.ComponentCost.Value = new Nova.Common.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(14, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 271);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ListView);
            this.splitContainer1.Size = new System.Drawing.Size(344, 271);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 0;
            // 
            // TreeView
            // 
            this.TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView.HideSelection = false;
            this.TreeView.Location = new System.Drawing.Point(0, 0);
            this.TreeView.Name = "TreeView";
            treeNode1.Name = "Armor";
            treeNode1.Tag = "Armor";
            treeNode1.Text = "Armor";
            treeNode2.Name = "Weapons";
            treeNode2.Tag = "Beam";
            treeNode2.Text = "Beam Weapons";
            treeNode3.Name = "Bombs";
            treeNode3.Tag = "Bomb";
            treeNode3.Text = "Bombs";
            treeNode4.Name = "CargoPod";
            treeNode4.Tag = "CargoPod";
            treeNode4.Text = "CargoPod";
            treeNode5.Name = "Engines";
            treeNode5.Tag = "Engine";
            treeNode5.Text = "Engines";
            treeNode6.Name = "Mechanical";
            treeNode6.Tag = "Mechanical";
            treeNode6.Text = "Mechanical";
            treeNode7.Name = "MineLayer";
            treeNode7.Tag = "MineLayer";
            treeNode7.Text = "Mine Layer";
            treeNode8.Name = "Missiles";
            treeNode8.Tag = "Missile";
            treeNode8.Text = "Missiles";
            treeNode9.Name = "Orbital";
            treeNode9.Tag = "Orbital";
            treeNode9.Text = "Orbital";
            treeNode10.Name = "Scanners";
            treeNode10.Tag = "Scanner";
            treeNode10.Text = "Scanners";
            treeNode11.Name = "Shields";
            treeNode11.Tag = "Shield";
            treeNode11.Text = "Shields";
            treeNode12.Name = "";
            treeNode12.Tag = "Armor";
            treeNode12.Text = "Available Technology";
            this.TreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12});
            this.TreeView.Size = new System.Drawing.Size(148, 271);
            this.TreeView.TabIndex = 0;
            this.TreeView.Tag = "";
            this.TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeNodeSelected);
            // 
            // ListView
            // 
            this.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView.Location = new System.Drawing.Point(0, 0);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.Size = new System.Drawing.Size(192, 271);
            this.ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListView.TabIndex = 0;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.SelectedIndexChanged += new System.EventHandler(this.ListSelectionChanged);
            this.ListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDown);
            // 
            // ShipDesignDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(780, 617);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ShipDesignDialog";
            this.ShowInTaskbar = false;
            this.Text = "Nova Ship Designer";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HullImage)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        #region Initialisation and Disposal


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction and initialisation
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ShipDesignDialog()
        {
            InitializeComponent();

            // Some abbreviations (just to save a bit of typing)

            StateData = ClientState.Data;
            AllComponents = Nova.Common.Components.AllComponents.Data.Components;
            AllDesigns = StateData.InputTurn.AllDesigns;

            ComponentImages.ImageSize = new Size(64, 64);
            ComponentImages.ColorDepth = ColorDepth.Depth32Bit;

            PopulateComponentList();
            TreeView.ExpandAll();

            // Populate the combo-box control with the available hulls, select the
            // first one in the list as the default. Also, make the default design
            // name the same as the hull name as a first guess. 
            HullList.Items.Clear();
            foreach (Component component in StateData.AvailableComponents.Values)
            {
                if (component.Properties.ContainsKey("Hull"))
                {
                    HullList.Items.Add(component.Name);
                }
            }

            if (HullList.Items.Count != 0)
            {
                HullList.SelectedIndex = 0;
                string selectedHullName = HullList.SelectedItem as string;
                SelectedHull = StateData.AvailableComponents[selectedHullName];
                SelectedHull.Name = selectedHullName;
                UpdateHullFields();
                SaveButton.Enabled = true;
            }
            else
            {
                SaveButton.Enabled = false;
            }

            // Populate the tree view control from the AvailableComponents
            ArrayList techList = new ArrayList();

            foreach (Component component in StateData.AvailableComponents.Values)
            {
                if (component.Type.Contains("Planetary")) continue;
                if (component.Type.Contains("Defenses")) continue;
                if (component.Type.Contains("Hull")) continue;

                if (!techList.Contains(component.Type))
                {
                    techList.Add(component.Type);
                }
            }
            techList.Sort();

            TreeView.BeginUpdate();
            TreeView.Nodes.Clear();
            TreeView.Nodes.Add("Available Technology");
            foreach (String techGroup in techList)
            {
                if (!TreeView.Nodes[0].Nodes.ContainsKey(techGroup))
                {
                    TreeView.Nodes[0].Nodes.Add(techGroup, techGroup); // key, Text
                }
            }

            foreach (TreeNode node in TreeView.Nodes[0].Nodes)
            {
                node.Tag = node.Text;
            }
            TreeView.EndUpdate();

        }//ShipDesignDialog


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        /// ----------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion


        #region Event Methods


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Save the the design when the OK button is pressed
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OK_Click(object sender, System.EventArgs e)
        {
            ShipDesign newDesign = new ShipDesign();
            Hull HullProperties = SelectedHull.Properties["Hull"] as Hull;

            HullProperties.Modules = HullGrid.ActiveModules;
            newDesign.Name = DesignName.Text;
            newDesign.Owner = StateData.RaceName;
            newDesign.ShipHull = SelectedHull;
            newDesign.Cost = DesignResources.Value;
            newDesign.Mass = Convert.ToInt32(ShipMass.Text);
            newDesign.Update();

            if (HullProperties.IsStarbase)
            {
                newDesign.Type = "Starbase";
            }
            else
            {
                newDesign.Type = "Ship";
                if (newDesign.Engine == null)
                {
                    Report.Error("A ship design must have an engine");
                    return;
                }
            }

            /* 
             * If you create a new design you might accidently give it the same 
             * name as another design, esspecialy if you keep the hull name for 
             * the ship name. However if you are edditing a design then this 
             * might be exactly what you want to do. Need to at least ask the 
             * user. TODO (priority 3).
           if (AllDesigns.Contains(newDesign.Key)) 
           {
              Report.Error("Design names must be unique");
              return;
             
           }
            */

            AllDesigns[newDesign.Key] = newDesign;
            Close();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Build a WeaponSystem. We have a simplification that only weapons of the
        /// same type can be in the module.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// <returns>Returns a <see cref="Weapon"/> representing all the weapon components in a single module.</returns>
        /// ----------------------------------------------------------------------------
        private Weapon BuildWeaponSystem(int number, Nova.Common.Components.Component component)
        {
            if (component == null) return null;

            // Find the weapon property of the component
            if (!component.Properties.ContainsKey("Weapon")) return null;
            Weapon weaponSystem = new Weapon((Weapon)component.Properties["Weapon"]);

            return weaponSystem;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A new tree node has been selected. Update the list control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void TreeNodeSelected(object sender, TreeViewEventArgs e)
        {
            Description.Text = null;

            TreeNode node = e.Node;
            if (node.Parent == null) return;

            string nodeType = node.Text as string;

            ListView.Items.Clear();
            if (nodeType == null) return;

            ListView.LargeImageList = ComponentImages;

            foreach (Component component in StateData.AvailableComponents.Values)
            {
                if (component.Type == nodeType)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = component.Name;
                    item.ImageIndex = (int)ImageIndices[component.Name];

                    ListView.Items.Add(item);
                }
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// A new item has been selected. Update the cost box and description.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ListSelectionChanged(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count <= 0) return;

            ListViewItem item = ListView.SelectedItems[0];
            Component selection = AllComponents[item.Text] as Component;
            ComponentCost.Value = selection.Cost;
            ComponentMass.Text = selection.Mass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            Description.Text = selection.Description;

            // Call the Mouse down routine (it must have gone down to change the
            // selection) so that we can select and drag in one operation (rather
            // than select and then drag as two separate steps).

            ListView_MouseDown(null, null);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Instigate Drag and Drop of the selected ListView item
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (ListView.SelectedItems.Count <= 0) return;

            HullGrid.DragDropData dragData = new HullGrid.DragDropData();

            ListViewItem item = ListView.SelectedItems[0];
            dragData.HullName = SelectedHull.Name;
            dragData.ComponentCount = 1;
            dragData.SelectedComponent = AllComponents[item.Text]
                                         as Nova.Common.Components.Component;

            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                dragData.ComponentCount = 10;
            }

            DragDropEffects result = DoDragDrop(dragData, DragDropEffects.Copy);

            if (result != DragDropEffects.Copy)
            {
                return;
            }

            // The component has been dropped into the design. Update the relevant
            // design summary fields.
            UpdateDesignParameters();
        }

        /// <summary>
        /// Update cost and primary characteristics.
        /// </summary>
        private void UpdateDesignParameters()
        {
            Hull hull = SelectedHull.Properties["Hull"] as Hull;
            Resources cost = SelectedHull.Cost;
            int mass = SelectedHull.Mass;
            int armor = hull.ArmorStrength;
            int shield = 0;
            int cargo = hull.BaseCargo;
            int fuel = hull.FuelCapacity;

            foreach (HullModule module in HullGrid.ActiveModules)
            {
                Component component = module.AllocatedComponent;
                if (component == null) continue;

                cost += module.ComponentCount * component.Cost;
                mass += module.ComponentCount * component.Mass;

                if (component.Properties.ContainsKey("Armor"))
                {
                    IntegerProperty armorProperty = component.Properties["Armor"] as IntegerProperty;
                    armor += module.ComponentCount * armorProperty.Value;
                }
                if (component.Properties.ContainsKey("Shield"))
                {
                    IntegerProperty shieldProperty = component.Properties["Shield"] as IntegerProperty;
                    shield += module.ComponentCount * shieldProperty.Value;
                }
                if (component.Properties.ContainsKey("Cargo"))
                {
                    IntegerProperty cargoProperty = component.Properties["Cargo"] as IntegerProperty;
                    cargo += module.ComponentCount * cargoProperty.Value;
                }
                if (component.Properties.ContainsKey("Fuel"))
                {
                    Fuel fuelProperty = component.Properties["Fuel"] as Fuel;
                    fuel += module.ComponentCount * fuelProperty.Capacity;
                }
            }

            DesignResources.Value = cost;
            DesignMass = mass;

            ShipMass.Text = DesignMass.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ShipArmor.Text = armor.ToString(System.Globalization.CultureInfo.InvariantCulture);
            ShipShields.Text = shield.ToString(System.Globalization.CultureInfo.InvariantCulture);
            CargoCapacity.Text = cargo.ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (!hull.IsStarbase)
            {
                MaxCapacity.Text = fuel.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Hull selection changed. Ensure we take a copy of the hull design so that we
        /// don't end up messing with the master copy.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void HullList_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedHullName = HullList.SelectedItem as string;

            DesignName.Text = selectedHullName;
            Nova.Common.Components.Component hull = StateData.AvailableComponents[selectedHullName];
            SelectedHull = new Nova.Common.Components.Component(hull);
            SelectedHull.Name = selectedHullName;

            UpdateHullFields();
        }


        #endregion


        #region Utility Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw the seleced hull design by filling in the hull grid and the populating
        /// the costs and characteristics fields on the form.
        /// </summary>
        /// <remarks>
        /// ??? (priority 3) We don't seem to have a ShipDesign at this stage, just a Hull component
        /// with attached modules? This makes determining summary information difficult
        /// as that is what the ShipDesign is for. Need to decide if using a ShipDesign
        /// from the start would be better.
        /// </remarks>
        /// ----------------------------------------------------------------------------
        private void UpdateHullFields()
        {

            Hull HullProperties = SelectedHull.Properties["Hull"] as Hull;
            HullGrid.ActiveModules = HullProperties.Modules;
            HullImage.Image = SelectedHull.ComponentImage;
            Description.Text = SelectedHull.Description;

            if (HullProperties.IsStarbase)
            {
                CapacityType.Text = "Dock Capacity";
                CapacityUnits.Text = "kT";
                MaxCapacity.Text = HullProperties.DockCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                CapacityType.Text = "Fuel Capacity";
                CapacityUnits.Text = "mg";
                MaxCapacity.Text = HullProperties.FuelCapacity.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }

            UpdateDesignParameters();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Build a table of components available. Note that some components are only
        /// availble if certain racial traits are selected.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void PopulateComponentList()
        {
            int index = 0;

            foreach (Component component in StateData.AvailableComponents.Values)
            {
                // TODO (priority 1) - work out why it sometimes is null.
                if (component != null)
                {
                    ImageIndices[component.Name] = index;
                    ComponentImages.Images.Add(component.ComponentImage);
                    index++;
                }
            }
        }

        #endregion
    }
}

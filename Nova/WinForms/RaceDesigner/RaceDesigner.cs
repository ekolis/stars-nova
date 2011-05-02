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
// Race Designer: Create (or examine) the race parameters to be fed into the
// Nova main game.
// ===========================================================================
#endregion

#region Using Statements

using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml;

using Nova.Common;

#endregion

namespace Nova.WinForms.RaceDesigner
{

    /// <summary>
    /// Race designer main (and only) application form
    /// </summary>
    [Serializable]
    public class RaceDesignerForm : System.Windows.Forms.Form
    {
        private RaceIcon currentRaceIcon;

        //---------------------------------------------------------------------------- 
        //  Non-designer generated variables
        //---------------------------------------------------------------------------- 
        private int advantagePoints = 53;
        private TraitEntry selectedRace = AllTraits.Data.All["JOAT"];
        private bool parametersChanged;

        #region Designer Generated Variables
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabConrol;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button finish;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox raceName;
        private System.Windows.Forms.TextBox pluralRaceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown operableFactories;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label primaryTraitDescription;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton jackOfAllTrades;
        private System.Windows.Forms.RadioButton hyperExpansion;
        private System.Windows.Forms.RadioButton spaceDemolition;
        private System.Windows.Forms.RadioButton packetPhysics;
        private System.Windows.Forms.RadioButton interStellarTraveller;
        private System.Windows.Forms.RadioButton alternateReality;
        private System.Windows.Forms.RadioButton superStealth;
        private System.Windows.Forms.RadioButton warMonger;
        private System.Windows.Forms.RadioButton claimAdjuster;
        private System.Windows.Forms.RadioButton innerStrength;
        private System.Windows.Forms.CheckBox regeneratingShields;
        private System.Windows.Forms.CheckBox bleedingEdgeTechnology;
        private System.Windows.Forms.CheckBox lowStartingPopulation;
        private System.Windows.Forms.CheckBox noAdvancedScanners;
        private System.Windows.Forms.CheckBox basicRemoteMining;
        private System.Windows.Forms.CheckBox cheapEngines;
        private System.Windows.Forms.CheckBox noRamEngines;
        private System.Windows.Forms.CheckBox mineralAlchemy;
        private System.Windows.Forms.CheckBox ultimateRecycling;
        private System.Windows.Forms.CheckBox generalisedResearch;
        private System.Windows.Forms.CheckBox advancedRemoteMining;
        private System.Windows.Forms.CheckBox totalTerraforming;
        private System.Windows.Forms.CheckBox improvedFuelEfficiency;
        private System.Windows.Forms.TabPage traitsTab;
        private System.Windows.Forms.TabPage raceTab;
        private System.Windows.Forms.TabPage productionTab;
        private System.Windows.Forms.TabPage researchTab;
        private System.Windows.Forms.TabPage environmentTab;
        private System.Windows.Forms.Label secondaryTraitDescription;
        private System.Windows.Forms.CheckBox improvedStarbases;
        private System.Windows.Forms.Label availablePoints;
        private System.Windows.Forms.NumericUpDown factoryBuildCost;
        private System.Windows.Forms.NumericUpDown mineralProduction;
        private System.Windows.Forms.NumericUpDown resourcesPerMine;
        private System.Windows.Forms.NumericUpDown operableMines;
        private ControlLibrary.ResearchCost energyResearch;
        private ControlLibrary.ResearchCost weaponsResearch;
        private ControlLibrary.ResearchCost propulsionResearch;
        private ControlLibrary.ResearchCost constructionResearch;
        private ControlLibrary.ResearchCost electronicsResearch;
        private ControlLibrary.ResearchCost biotechnologyResearch;
        private ControlLibrary.Range gravityTolerance;
        private ControlLibrary.Range temperatureTolerance;
        private ControlLibrary.Range radiationTolerance;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox unusedPointsTarget;
        private System.Windows.Forms.NumericUpDown resourceProduction;
        private System.Windows.Forms.NumericUpDown colonistProduction;
        private System.Windows.Forms.Button exit;
        private GroupBox groupBox12;
        private GroupBox groupBox11;
        private MenuStrip mainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TextBox password;
        private Label label12;
        private Button previousImage;
        private Button nextImage;
        private PictureBox pictureBox;
        private Label iconIndex;
        private ToolStripMenuItem loadRaceFile;
        private OpenFileDialog openFileDialog;
        private NumericUpDown maxGrowth;
        private Label label4;
        private CheckBox extraTech;
        private CheckBox cheapFactories;

        #endregion Designer Generated Variables

        #region Construction Initialisation Disposal

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction and dynamic initialisation which consists of ensuring
        /// that there is a default primary racial trait selected and that what to
        /// spend unused advantage points on has a default.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public RaceDesignerForm()
        {
            InitializeComponent();

            this.jackOfAllTrades.Checked = true;
            this.selectedRace = AllTraits.Data.Primary["JOAT"];
            this.primaryTraitDescription.Text = this.selectedRace.Description;

            this.parametersChanged = false;
            this.unusedPointsTarget.SelectedIndex = 0;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when the form is loaded. 
        /// Load the race icon data. Load the first race icon into
        /// its image box, record the index of the current image (0) and make a note
        /// of the number of images so that we can just cycle through them.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void OnLoad(object sender, EventArgs e)
        {
            AllRaceIcons.Restore();
            this.currentRaceIcon = (RaceIcon)AllRaceIcons.Data.IconList[0];
            this.pictureBox.Image = this.currentRaceIcon.Image;
            this.iconIndex.Text = Path.GetFileNameWithoutExtension(this.currentRaceIcon.Source);

            // Can't trust the windows designer generate code to set the environment range before setting the environment value, so set it here to be sure.
            this.temperatureTolerance.MinimumValue = 15;
            this.temperatureTolerance.MaximumValue = 85;
            this.temperatureTolerance.ActivateRangeChange();

            this.gravityTolerance.MinimumValue = 15;
            this.gravityTolerance.MaximumValue = 85;
            this.gravityTolerance.ActivateRangeChange();

            this.radiationTolerance.MinimumValue = 15;
            this.radiationTolerance.MaximumValue = 85;
            this.radiationTolerance.ActivateRangeChange();
        }

        #endregion

        #region Windows Form Designer generated code

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RaceDesignerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.availablePoints = new System.Windows.Forms.Label();
            this.tabConrol = new System.Windows.Forms.TabControl();
            this.raceTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pluralRaceName = new System.Windows.Forms.TextBox();
            this.previousImage = new System.Windows.Forms.Button();
            this.nextImage = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.password = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.raceName = new System.Windows.Forms.TextBox();
            this.iconIndex = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.primaryTraitDescription = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.jackOfAllTrades = new System.Windows.Forms.RadioButton();
            this.alternateReality = new System.Windows.Forms.RadioButton();
            this.interStellarTraveller = new System.Windows.Forms.RadioButton();
            this.packetPhysics = new System.Windows.Forms.RadioButton();
            this.spaceDemolition = new System.Windows.Forms.RadioButton();
            this.innerStrength = new System.Windows.Forms.RadioButton();
            this.claimAdjuster = new System.Windows.Forms.RadioButton();
            this.warMonger = new System.Windows.Forms.RadioButton();
            this.superStealth = new System.Windows.Forms.RadioButton();
            this.hyperExpansion = new System.Windows.Forms.RadioButton();
            this.traitsTab = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.secondaryTraitDescription = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.regeneratingShields = new System.Windows.Forms.CheckBox();
            this.bleedingEdgeTechnology = new System.Windows.Forms.CheckBox();
            this.lowStartingPopulation = new System.Windows.Forms.CheckBox();
            this.noAdvancedScanners = new System.Windows.Forms.CheckBox();
            this.basicRemoteMining = new System.Windows.Forms.CheckBox();
            this.cheapEngines = new System.Windows.Forms.CheckBox();
            this.noRamEngines = new System.Windows.Forms.CheckBox();
            this.mineralAlchemy = new System.Windows.Forms.CheckBox();
            this.ultimateRecycling = new System.Windows.Forms.CheckBox();
            this.generalisedResearch = new System.Windows.Forms.CheckBox();
            this.improvedStarbases = new System.Windows.Forms.CheckBox();
            this.advancedRemoteMining = new System.Windows.Forms.CheckBox();
            this.totalTerraforming = new System.Windows.Forms.CheckBox();
            this.improvedFuelEfficiency = new System.Windows.Forms.CheckBox();
            this.environmentTab = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.maxGrowth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.radiationTolerance = new Nova.ControlLibrary.Range();
            this.temperatureTolerance = new Nova.ControlLibrary.Range();
            this.gravityTolerance = new Nova.ControlLibrary.Range();
            this.productionTab = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.mineralProduction = new System.Windows.Forms.NumericUpDown();
            this.operableMines = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.resourcesPerMine = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.cheapFactories = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.resourceProduction = new System.Windows.Forms.NumericUpDown();
            this.factoryBuildCost = new System.Windows.Forms.NumericUpDown();
            this.operableFactories = new System.Windows.Forms.NumericUpDown();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.unusedPointsTarget = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.colonistProduction = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.researchTab = new System.Windows.Forms.TabPage();
            this.extraTech = new System.Windows.Forms.CheckBox();
            this.biotechnologyResearch = new Nova.ControlLibrary.ResearchCost();
            this.electronicsResearch = new Nova.ControlLibrary.ResearchCost();
            this.constructionResearch = new Nova.ControlLibrary.ResearchCost();
            this.propulsionResearch = new Nova.ControlLibrary.ResearchCost();
            this.weaponsResearch = new Nova.ControlLibrary.ResearchCost();
            this.energyResearch = new Nova.ControlLibrary.ResearchCost();
            this.finish = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRaceFile = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.tabConrol.SuspendLayout();
            this.raceTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.traitsTab.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.environmentTab.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxGrowth)).BeginInit();
            this.productionTab.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mineralProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operableMines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourcesPerMine)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resourceProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryBuildCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operableFactories)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colonistProduction)).BeginInit();
            this.researchTab.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Advantage Points:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.availablePoints);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(16, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 40);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // AvailablePoints
            // 
            this.availablePoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availablePoints.Location = new System.Drawing.Point(112, 10);
            this.availablePoints.Name = "availablePoints";
            this.availablePoints.Size = new System.Drawing.Size(104, 24);
            this.availablePoints.TabIndex = 99;
            this.availablePoints.Text = "53";
            this.availablePoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabConrol
            // 
            this.tabConrol.Controls.Add(this.raceTab);
            this.tabConrol.Controls.Add(this.traitsTab);
            this.tabConrol.Controls.Add(this.environmentTab);
            this.tabConrol.Controls.Add(this.productionTab);
            this.tabConrol.Controls.Add(this.researchTab);
            this.tabConrol.ItemSize = new System.Drawing.Size(0, 18);
            this.tabConrol.Location = new System.Drawing.Point(16, 73);
            this.tabConrol.Name = "tabConrol";
            this.tabConrol.SelectedIndex = 0;
            this.tabConrol.Size = new System.Drawing.Size(400, 422);
            this.tabConrol.TabIndex = 0;
            // 
            // RaceTab
            // 
            this.raceTab.Controls.Add(this.label2);
            this.raceTab.Controls.Add(this.label3);
            this.raceTab.Controls.Add(this.groupBox2);
            this.raceTab.Controls.Add(this.groupBox4);
            this.raceTab.Controls.Add(this.groupBox3);
            this.raceTab.Location = new System.Drawing.Point(4, 22);
            this.raceTab.Name = "raceTab";
            this.raceTab.Size = new System.Drawing.Size(392, 396);
            this.raceTab.TabIndex = 0;
            this.raceTab.Text = "Race";
            this.raceTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Plural race name";
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(16, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Race name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pluralRaceName);
            this.groupBox2.Controls.Add(this.previousImage);
            this.groupBox2.Controls.Add(this.nextImage);
            this.groupBox2.Controls.Add(this.pictureBox);
            this.groupBox2.Controls.Add(this.password);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.raceName);
            this.groupBox2.Controls.Add(this.iconIndex);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(8, -2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 86);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // PluralRaceName
            // 
            this.pluralRaceName.Location = new System.Drawing.Point(104, 36);
            this.pluralRaceName.Name = "pluralRaceName";
            this.pluralRaceName.Size = new System.Drawing.Size(132, 20);
            this.pluralRaceName.TabIndex = 2;
            this.pluralRaceName.Text = "Humanoids";
            // 
            // PreviousImage
            // 
            this.previousImage.Location = new System.Drawing.Point(325, 36);
            this.previousImage.Name = "previousImage";
            this.previousImage.Size = new System.Drawing.Size(44, 23);
            this.previousImage.TabIndex = 6;
            this.previousImage.Text = "-";
            this.previousImage.UseVisualStyleBackColor = true;
            this.previousImage.Click += new System.EventHandler(this.PreviousImage_Click);
            // 
            // NextImage
            // 
            this.nextImage.Location = new System.Drawing.Point(325, 13);
            this.nextImage.Name = "nextImage";
            this.nextImage.Size = new System.Drawing.Size(44, 23);
            this.nextImage.TabIndex = 5;
            this.nextImage.Text = "+";
            this.nextImage.UseVisualStyleBackColor = true;
            this.nextImage.Click += new System.EventHandler(this.NextImage_Click);
            // 
            // PictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(255, 14);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(64, 64);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // Password
            // 
            this.password.Location = new System.Drawing.Point(103, 60);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(132, 20);
            this.password.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(5, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "Password";
            // 
            // RaceName
            // 
            this.raceName.Location = new System.Drawing.Point(104, 13);
            this.raceName.Name = "raceName";
            this.raceName.Size = new System.Drawing.Size(132, 20);
            this.raceName.TabIndex = 1;
            this.raceName.Text = "Humanoid";
            // 
            // IconIndex
            // 
            this.iconIndex.Location = new System.Drawing.Point(324, 62);
            this.iconIndex.Name = "iconIndex";
            this.iconIndex.Size = new System.Drawing.Size(43, 17);
            this.iconIndex.TabIndex = 7;
            this.iconIndex.Text = "31";
            this.iconIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.primaryTraitDescription);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(7, 244);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(376, 137);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trait Description";
            // 
            // PrimaryTraitDescription
            // 
            this.primaryTraitDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.primaryTraitDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.primaryTraitDescription.Location = new System.Drawing.Point(3, 16);
            this.primaryTraitDescription.Name = "primaryTraitDescription";
            this.primaryTraitDescription.Size = new System.Drawing.Size(370, 118);
            this.primaryTraitDescription.TabIndex = 0;
            this.primaryTraitDescription.Text = "Will hold race description.";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.jackOfAllTrades);
            this.groupBox3.Controls.Add(this.alternateReality);
            this.groupBox3.Controls.Add(this.interStellarTraveller);
            this.groupBox3.Controls.Add(this.packetPhysics);
            this.groupBox3.Controls.Add(this.spaceDemolition);
            this.groupBox3.Controls.Add(this.innerStrength);
            this.groupBox3.Controls.Add(this.claimAdjuster);
            this.groupBox3.Controls.Add(this.warMonger);
            this.groupBox3.Controls.Add(this.superStealth);
            this.groupBox3.Controls.Add(this.hyperExpansion);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(7, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(374, 144);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Primary Racial Traits";
            // 
            // JackOfAllTrades
            // 
            this.jackOfAllTrades.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.jackOfAllTrades.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.jackOfAllTrades.Location = new System.Drawing.Point(239, 112);
            this.jackOfAllTrades.Name = "jackOfAllTrades";
            this.jackOfAllTrades.Size = new System.Drawing.Size(126, 24);
            this.jackOfAllTrades.TabIndex = 9;
            this.jackOfAllTrades.Tag = "JOAT";
            this.jackOfAllTrades.Text = "Jack of all Trades";
            this.jackOfAllTrades.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // AlternateReality
            // 
            this.alternateReality.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.alternateReality.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.alternateReality.Location = new System.Drawing.Point(239, 88);
            this.alternateReality.Name = "alternateReality";
            this.alternateReality.Size = new System.Drawing.Size(126, 24);
            this.alternateReality.TabIndex = 8;
            this.alternateReality.Tag = "AR";
            this.alternateReality.Text = "Alternate Reality";
            this.alternateReality.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // InterStellarTraveller
            // 
            this.interStellarTraveller.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.interStellarTraveller.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.interStellarTraveller.Location = new System.Drawing.Point(239, 64);
            this.interStellarTraveller.Name = "interStellarTraveller";
            this.interStellarTraveller.Size = new System.Drawing.Size(126, 24);
            this.interStellarTraveller.TabIndex = 7;
            this.interStellarTraveller.Tag = "IT";
            this.interStellarTraveller.Text = "Inter-stellar Traveller";
            this.interStellarTraveller.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // PacketPhysics
            // 
            this.packetPhysics.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.packetPhysics.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.packetPhysics.Location = new System.Drawing.Point(239, 42);
            this.packetPhysics.Name = "packetPhysics";
            this.packetPhysics.Size = new System.Drawing.Size(126, 24);
            this.packetPhysics.TabIndex = 6;
            this.packetPhysics.Tag = "PP";
            this.packetPhysics.Text = "Packet Physics";
            this.packetPhysics.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // SpaceDemolition
            // 
            this.spaceDemolition.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.spaceDemolition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.spaceDemolition.Location = new System.Drawing.Point(239, 20);
            this.spaceDemolition.Name = "spaceDemolition";
            this.spaceDemolition.Size = new System.Drawing.Size(126, 24);
            this.spaceDemolition.TabIndex = 5;
            this.spaceDemolition.Tag = "SD";
            this.spaceDemolition.Text = "Space Demolition";
            this.spaceDemolition.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // InnerStrength
            // 
            this.innerStrength.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.innerStrength.Location = new System.Drawing.Point(8, 112);
            this.innerStrength.Name = "innerStrength";
            this.innerStrength.Size = new System.Drawing.Size(104, 24);
            this.innerStrength.TabIndex = 4;
            this.innerStrength.Tag = "IS";
            this.innerStrength.Text = "Inner Strength";
            this.innerStrength.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // ClaimAdjuster
            // 
            this.claimAdjuster.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.claimAdjuster.Location = new System.Drawing.Point(8, 88);
            this.claimAdjuster.Name = "claimAdjuster";
            this.claimAdjuster.Size = new System.Drawing.Size(104, 24);
            this.claimAdjuster.TabIndex = 3;
            this.claimAdjuster.Tag = "CA";
            this.claimAdjuster.Text = "Claim Adjuster";
            this.claimAdjuster.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // WarMonger
            // 
            this.warMonger.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.warMonger.Location = new System.Drawing.Point(8, 64);
            this.warMonger.Name = "warMonger";
            this.warMonger.Size = new System.Drawing.Size(104, 24);
            this.warMonger.TabIndex = 2;
            this.warMonger.Tag = "WM";
            this.warMonger.Text = "War Monger";
            this.warMonger.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // SuperStealth
            // 
            this.superStealth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.superStealth.Location = new System.Drawing.Point(8, 42);
            this.superStealth.Name = "superStealth";
            this.superStealth.Size = new System.Drawing.Size(104, 24);
            this.superStealth.TabIndex = 1;
            this.superStealth.Tag = "SS";
            this.superStealth.Text = "Super Stealth";
            this.superStealth.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // HyperExpansion
            // 
            this.hyperExpansion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.hyperExpansion.Location = new System.Drawing.Point(8, 19);
            this.hyperExpansion.Name = "hyperExpansion";
            this.hyperExpansion.Size = new System.Drawing.Size(110, 24);
            this.hyperExpansion.TabIndex = 0;
            this.hyperExpansion.Tag = "HE";
            this.hyperExpansion.Text = "Hyper-Expansion";
            this.hyperExpansion.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // TraitsTab
            // 
            this.traitsTab.Controls.Add(this.groupBox7);
            this.traitsTab.Controls.Add(this.groupBox5);
            this.traitsTab.Location = new System.Drawing.Point(4, 22);
            this.traitsTab.Name = "traitsTab";
            this.traitsTab.Size = new System.Drawing.Size(392, 396);
            this.traitsTab.TabIndex = 1;
            this.traitsTab.Text = "Traits";
            this.traitsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.secondaryTraitDescription);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox7.Location = new System.Drawing.Point(8, 245);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(374, 124);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Trait Description";
            // 
            // SecondaryTraitDescription
            // 
            this.secondaryTraitDescription.Location = new System.Drawing.Point(10, 16);
            this.secondaryTraitDescription.Name = "secondaryTraitDescription";
            this.secondaryTraitDescription.Size = new System.Drawing.Size(346, 59);
            this.secondaryTraitDescription.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.regeneratingShields);
            this.groupBox5.Controls.Add(this.bleedingEdgeTechnology);
            this.groupBox5.Controls.Add(this.lowStartingPopulation);
            this.groupBox5.Controls.Add(this.noAdvancedScanners);
            this.groupBox5.Controls.Add(this.basicRemoteMining);
            this.groupBox5.Controls.Add(this.cheapEngines);
            this.groupBox5.Controls.Add(this.noRamEngines);
            this.groupBox5.Controls.Add(this.mineralAlchemy);
            this.groupBox5.Controls.Add(this.ultimateRecycling);
            this.groupBox5.Controls.Add(this.generalisedResearch);
            this.groupBox5.Controls.Add(this.improvedStarbases);
            this.groupBox5.Controls.Add(this.advancedRemoteMining);
            this.groupBox5.Controls.Add(this.totalTerraforming);
            this.groupBox5.Controls.Add(this.improvedFuelEfficiency);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox5.Location = new System.Drawing.Point(8, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(374, 226);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Secondary Racial Traits";
            // 
            // RegeneratingShields
            // 
            this.regeneratingShields.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.regeneratingShields.Location = new System.Drawing.Point(211, 168);
            this.regeneratingShields.Name = "regeneratingShields";
            this.regeneratingShields.Size = new System.Drawing.Size(131, 24);
            this.regeneratingShields.TabIndex = 13;
            this.regeneratingShields.Tag = "RS";
            this.regeneratingShields.Text = "Regenerating shields";
            this.regeneratingShields.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // BleedingEdgeTechnology
            // 
            this.bleedingEdgeTechnology.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bleedingEdgeTechnology.Location = new System.Drawing.Point(211, 144);
            this.bleedingEdgeTechnology.Name = "bleedingEdgeTechnology";
            this.bleedingEdgeTechnology.Size = new System.Drawing.Size(145, 24);
            this.bleedingEdgeTechnology.TabIndex = 12;
            this.bleedingEdgeTechnology.Tag = "BET";
            this.bleedingEdgeTechnology.Text = "Bleeding edge technology";
            this.bleedingEdgeTechnology.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // LowStartingPopulation
            // 
            this.lowStartingPopulation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lowStartingPopulation.Location = new System.Drawing.Point(211, 120);
            this.lowStartingPopulation.Name = "lowStartingPopulation";
            this.lowStartingPopulation.Size = new System.Drawing.Size(130, 24);
            this.lowStartingPopulation.TabIndex = 11;
            this.lowStartingPopulation.Tag = "LSP";
            this.lowStartingPopulation.Text = "Low starting population";
            this.lowStartingPopulation.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // NoAdvancedScanners
            // 
            this.noAdvancedScanners.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.noAdvancedScanners.Location = new System.Drawing.Point(211, 96);
            this.noAdvancedScanners.Name = "noAdvancedScanners";
            this.noAdvancedScanners.Size = new System.Drawing.Size(133, 24);
            this.noAdvancedScanners.TabIndex = 10;
            this.noAdvancedScanners.Tag = "NAS";
            this.noAdvancedScanners.Text = "No advanced scanners";
            this.noAdvancedScanners.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // BasicRemoteMining
            // 
            this.basicRemoteMining.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.basicRemoteMining.Location = new System.Drawing.Point(211, 72);
            this.basicRemoteMining.Name = "basicRemoteMining";
            this.basicRemoteMining.Size = new System.Drawing.Size(141, 24);
            this.basicRemoteMining.TabIndex = 9;
            this.basicRemoteMining.Tag = "OBRM";
            this.basicRemoteMining.Text = "Only basic remote mining";
            this.basicRemoteMining.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // CheapEngines
            // 
            this.cheapEngines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cheapEngines.Location = new System.Drawing.Point(211, 48);
            this.cheapEngines.Name = "cheapEngines";
            this.cheapEngines.Size = new System.Drawing.Size(131, 24);
            this.cheapEngines.TabIndex = 8;
            this.cheapEngines.Tag = "CE";
            this.cheapEngines.Text = "Cheap engines";
            this.cheapEngines.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // NoRAMEngines
            // 
            this.noRamEngines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.noRamEngines.Location = new System.Drawing.Point(211, 24);
            this.noRamEngines.Name = "noRamEngines";
            this.noRamEngines.Size = new System.Drawing.Size(134, 24);
            this.noRamEngines.TabIndex = 7;
            this.noRamEngines.Tag = "NRS";
            this.noRamEngines.Text = "No RAM scoop engines";
            this.noRamEngines.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // MineralAlchemy
            // 
            this.mineralAlchemy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.mineralAlchemy.Location = new System.Drawing.Point(16, 168);
            this.mineralAlchemy.Name = "mineralAlchemy";
            this.mineralAlchemy.Size = new System.Drawing.Size(152, 24);
            this.mineralAlchemy.TabIndex = 6;
            this.mineralAlchemy.Tag = "MA";
            this.mineralAlchemy.Text = "Mineral alchemy";
            this.mineralAlchemy.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // UltimateRecycling
            // 
            this.ultimateRecycling.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ultimateRecycling.Location = new System.Drawing.Point(16, 144);
            this.ultimateRecycling.Name = "ultimateRecycling";
            this.ultimateRecycling.Size = new System.Drawing.Size(152, 24);
            this.ultimateRecycling.TabIndex = 5;
            this.ultimateRecycling.Tag = "UR";
            this.ultimateRecycling.Text = "Ultimate recycling";
            this.ultimateRecycling.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // GeneralisedResearch
            // 
            this.generalisedResearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.generalisedResearch.Location = new System.Drawing.Point(16, 120);
            this.generalisedResearch.Name = "generalisedResearch";
            this.generalisedResearch.Size = new System.Drawing.Size(152, 24);
            this.generalisedResearch.TabIndex = 4;
            this.generalisedResearch.Tag = "GR";
            this.generalisedResearch.Text = "Generalised reseach";
            this.generalisedResearch.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // ImprovedStarbases
            // 
            this.improvedStarbases.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.improvedStarbases.Location = new System.Drawing.Point(16, 96);
            this.improvedStarbases.Name = "improvedStarbases";
            this.improvedStarbases.Size = new System.Drawing.Size(152, 24);
            this.improvedStarbases.TabIndex = 3;
            this.improvedStarbases.Tag = "ISB";
            this.improvedStarbases.Text = "Improved starbases";
            this.improvedStarbases.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // AdvancedRemoteMining
            // 
            this.advancedRemoteMining.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.advancedRemoteMining.Location = new System.Drawing.Point(16, 72);
            this.advancedRemoteMining.Name = "advancedRemoteMining";
            this.advancedRemoteMining.Size = new System.Drawing.Size(152, 24);
            this.advancedRemoteMining.TabIndex = 2;
            this.advancedRemoteMining.Tag = "ARM";
            this.advancedRemoteMining.Text = "Advanced remote mining";
            this.advancedRemoteMining.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // TotalTerraforming
            // 
            this.totalTerraforming.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.totalTerraforming.Location = new System.Drawing.Point(16, 48);
            this.totalTerraforming.Name = "totalTerraforming";
            this.totalTerraforming.Size = new System.Drawing.Size(152, 24);
            this.totalTerraforming.TabIndex = 1;
            this.totalTerraforming.Tag = "TT";
            this.totalTerraforming.Text = "Total terraforming";
            this.totalTerraforming.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // ImprovedFuelEfficiency
            // 
            this.improvedFuelEfficiency.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.improvedFuelEfficiency.Location = new System.Drawing.Point(16, 24);
            this.improvedFuelEfficiency.Name = "improvedFuelEfficiency";
            this.improvedFuelEfficiency.Size = new System.Drawing.Size(152, 24);
            this.improvedFuelEfficiency.TabIndex = 0;
            this.improvedFuelEfficiency.Tag = "IFE";
            this.improvedFuelEfficiency.Text = "Improved fuel efficiency";
            this.improvedFuelEfficiency.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // EnvironmentTab
            // 
            this.environmentTab.Controls.Add(this.groupBox9);
            this.environmentTab.Location = new System.Drawing.Point(4, 22);
            this.environmentTab.Name = "environmentTab";
            this.environmentTab.Size = new System.Drawing.Size(392, 396);
            this.environmentTab.TabIndex = 4;
            this.environmentTab.Text = "Environment";
            this.environmentTab.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.maxGrowth);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.radiationTolerance);
            this.groupBox9.Controls.Add(this.temperatureTolerance);
            this.groupBox9.Controls.Add(this.gravityTolerance);
            this.groupBox9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox9.Location = new System.Drawing.Point(17, 17);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(353, 353);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Environment Tolerance";
            // 
            // MaxGrowth
            // 
            this.maxGrowth.Location = new System.Drawing.Point(235, 320);
            this.maxGrowth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.maxGrowth.Name = "maxGrowth";
            this.maxGrowth.Size = new System.Drawing.Size(94, 20);
            this.maxGrowth.TabIndex = 4;
            this.maxGrowth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxGrowth.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.maxGrowth.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Maximum Colinists Growth Per Year";
            // 
            // RadiationTolerance
            // 
            this.radiationTolerance.MinimumValue = 15;
            this.radiationTolerance.MaximumValue = 85;
            this.radiationTolerance.Location = new System.Drawing.Point(14, 220);
            this.radiationTolerance.Name = "radiationTolerance";
            this.radiationTolerance.RangeBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.radiationTolerance.RangeTitle = "Radiation";
            this.radiationTolerance.RangeUnits = "mR";
            this.radiationTolerance.Size = new System.Drawing.Size(324, 95);
            this.radiationTolerance.TabIndex = 2;
            this.radiationTolerance.RangeChanged += new Nova.ControlLibrary.Range.RangeChangedHandler(this.Tolerance_RangeChanged);
            this.radiationTolerance.CheckChanged += new Nova.ControlLibrary.Range.CheckChangedHandler(this.Tolerance_CheckChanged);
            // 
            // TemperatureTolerance
            // 
            this.temperatureTolerance.MinimumValue = 15;
            this.temperatureTolerance.MaximumValue = 85;
            this.temperatureTolerance.Location = new System.Drawing.Point(14, 118);
            this.temperatureTolerance.Name = "temperatureTolerance";
            this.temperatureTolerance.RangeBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.temperatureTolerance.RangeTitle = "Temperature";
            this.temperatureTolerance.RangeUnits = "°C";
            this.temperatureTolerance.Size = new System.Drawing.Size(324, 95);
            this.temperatureTolerance.TabIndex = 1;
            this.temperatureTolerance.RangeChanged += new Nova.ControlLibrary.Range.RangeChangedHandler(this.Tolerance_RangeChanged);
            this.temperatureTolerance.CheckChanged += new Nova.ControlLibrary.Range.CheckChangedHandler(this.Tolerance_CheckChanged);
            // 
            // GravityTolerance
            //
            this.gravityTolerance.MinimumValue = 15;
            this.gravityTolerance.MaximumValue = 85;
            this.gravityTolerance.Location = new System.Drawing.Point(14, 19);
            this.gravityTolerance.Name = "gravityTolerance";
            this.gravityTolerance.RangeBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gravityTolerance.RangeTitle = "Gravity";
            this.gravityTolerance.RangeUnits = "g"; //Gravity.GetUnit();
            this.gravityTolerance.Size = new System.Drawing.Size(324, 95);
            this.gravityTolerance.TabIndex = 0;
            this.gravityTolerance.RangeChanged += new Nova.ControlLibrary.Range.RangeChangedHandler(this.Tolerance_RangeChanged);
            this.gravityTolerance.CheckChanged += new Nova.ControlLibrary.Range.CheckChangedHandler(this.Tolerance_CheckChanged);
            // 
            // ProductionTab
            // 
            this.productionTab.Controls.Add(this.groupBox12);
            this.productionTab.Controls.Add(this.groupBox11);
            this.productionTab.Controls.Add(this.groupBox10);
            this.productionTab.Controls.Add(this.groupBox8);
            this.productionTab.Location = new System.Drawing.Point(4, 22);
            this.productionTab.Name = "productionTab";
            this.productionTab.Size = new System.Drawing.Size(392, 396);
            this.productionTab.TabIndex = 2;
            this.productionTab.Text = "Production";
            this.productionTab.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label9);
            this.groupBox12.Controls.Add(this.mineralProduction);
            this.groupBox12.Controls.Add(this.operableMines);
            this.groupBox12.Controls.Add(this.label10);
            this.groupBox12.Controls.Add(this.resourcesPerMine);
            this.groupBox12.Controls.Add(this.label11);
            this.groupBox12.Location = new System.Drawing.Point(11, 190);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(363, 95);
            this.groupBox12.TabIndex = 16;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Mines";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(10, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(231, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "Mineral production (kT) per  set of 10 mines";
            // 
            // MineralProduction
            // 
            this.mineralProduction.Location = new System.Drawing.Point(292, 16);
            this.mineralProduction.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mineralProduction.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.mineralProduction.Name = "mineralProduction";
            this.mineralProduction.Size = new System.Drawing.Size(56, 20);
            this.mineralProduction.TabIndex = 10;
            this.mineralProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mineralProduction.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mineralProduction.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // OperableMines
            // 
            this.operableMines.Location = new System.Drawing.Point(292, 41);
            this.operableMines.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.operableMines.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.operableMines.Name = "operableMines";
            this.operableMines.Size = new System.Drawing.Size(56, 20);
            this.operableMines.TabIndex = 14;
            this.operableMines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.operableMines.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.operableMines.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(231, 14);
            this.label10.TabIndex = 11;
            this.label10.Text = "Number of resources required to build a mine";
            // 
            // ResourcesPerMine
            // 
            this.resourcesPerMine.Location = new System.Drawing.Point(293, 66);
            this.resourcesPerMine.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.resourcesPerMine.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.resourcesPerMine.Name = "resourcesPerMine";
            this.resourcesPerMine.Size = new System.Drawing.Size(56, 20);
            this.resourcesPerMine.TabIndex = 12;
            this.resourcesPerMine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.resourcesPerMine.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.resourcesPerMine.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(231, 14);
            this.label11.TabIndex = 13;
            this.label11.Text = "Mines that can be operated by 10k colonists";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.cheapFactories);
            this.groupBox11.Controls.Add(this.label8);
            this.groupBox11.Controls.Add(this.label7);
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Controls.Add(this.resourceProduction);
            this.groupBox11.Controls.Add(this.factoryBuildCost);
            this.groupBox11.Controls.Add(this.operableFactories);
            this.groupBox11.Location = new System.Drawing.Point(11, 60);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(363, 124);
            this.groupBox11.TabIndex = 15;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Factories";
            // 
            // CheapFactories
            // 
            this.cheapFactories.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cheapFactories.Location = new System.Drawing.Point(13, 92);
            this.cheapFactories.Name = "cheapFactories";
            this.cheapFactories.Size = new System.Drawing.Size(186, 24);
            this.cheapFactories.TabIndex = 15;
            this.cheapFactories.Tag = "CF";
            this.cheapFactories.Text = "Factories cost 1kT less Germanium";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(10, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(245, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "Factories that can be operated by 10k colonists";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(10, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(261, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Resources required to build each factory";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(10, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(261, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Resources produced by each set of 10 factories";
            // 
            // ResourceProduction
            // 
            this.resourceProduction.Location = new System.Drawing.Point(292, 19);
            this.resourceProduction.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.resourceProduction.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.resourceProduction.Name = "resourceProduction";
            this.resourceProduction.Size = new System.Drawing.Size(56, 20);
            this.resourceProduction.TabIndex = 3;
            this.resourceProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.resourceProduction.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.resourceProduction.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // FactoryBuildCost
            // 
            this.factoryBuildCost.Location = new System.Drawing.Point(292, 69);
            this.factoryBuildCost.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.factoryBuildCost.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.factoryBuildCost.Name = "factoryBuildCost";
            this.factoryBuildCost.Size = new System.Drawing.Size(56, 20);
            this.factoryBuildCost.TabIndex = 5;
            this.factoryBuildCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.factoryBuildCost.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.factoryBuildCost.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // OperableFactories
            // 
            this.operableFactories.Location = new System.Drawing.Point(292, 44);
            this.operableFactories.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.operableFactories.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.operableFactories.Name = "operableFactories";
            this.operableFactories.Size = new System.Drawing.Size(56, 20);
            this.operableFactories.TabIndex = 8;
            this.operableFactories.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.operableFactories.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.operableFactories.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.unusedPointsTarget);
            this.groupBox10.Location = new System.Drawing.Point(11, 291);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(363, 58);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Spend Unused Allocation Points On:";
            // 
            // UnusedPointsTarget
            // 
            this.unusedPointsTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unusedPointsTarget.Items.AddRange(new object[] {
            "Minerals",
            "Mines",
            "Resources"});
            this.unusedPointsTarget.Location = new System.Drawing.Point(14, 21);
            this.unusedPointsTarget.Name = "unusedPointsTarget";
            this.unusedPointsTarget.Size = new System.Drawing.Size(323, 21);
            this.unusedPointsTarget.Sorted = true;
            this.unusedPointsTarget.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.colonistProduction);
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox8.Location = new System.Drawing.Point(11, 10);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(363, 44);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Colonists";
            // 
            // ColonistProduction
            // 
            this.colonistProduction.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.colonistProduction.Location = new System.Drawing.Point(292, 18);
            this.colonistProduction.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.colonistProduction.Minimum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.colonistProduction.Name = "colonistProduction";
            this.colonistProduction.Size = new System.Drawing.Size(56, 20);
            this.colonistProduction.TabIndex = 1;
            this.colonistProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colonistProduction.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.colonistProduction.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(277, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Colonists required to generate one resource each year";
            // 
            // ResearchTab
            // 
            this.researchTab.Controls.Add(this.extraTech);
            this.researchTab.Controls.Add(this.biotechnologyResearch);
            this.researchTab.Controls.Add(this.electronicsResearch);
            this.researchTab.Controls.Add(this.constructionResearch);
            this.researchTab.Controls.Add(this.propulsionResearch);
            this.researchTab.Controls.Add(this.weaponsResearch);
            this.researchTab.Controls.Add(this.energyResearch);
            this.researchTab.Location = new System.Drawing.Point(4, 22);
            this.researchTab.Name = "researchTab";
            this.researchTab.Size = new System.Drawing.Size(392, 396);
            this.researchTab.TabIndex = 3;
            this.researchTab.Text = "Research";
            this.researchTab.UseVisualStyleBackColor = true;
            // 
            // ExtraTech
            // 
            this.extraTech.AutoSize = true;
            this.extraTech.Location = new System.Drawing.Point(16, 369);
            this.extraTech.Name = "extraTech";
            this.extraTech.Size = new System.Drawing.Size(233, 17);
            this.extraTech.TabIndex = 6;
            this.extraTech.Text = "All extra cost technologies starts with level 3";
            this.extraTech.UseVisualStyleBackColor = true;
            this.extraTech.CheckedChanged += new System.EventHandler(this.ExtraTech_CheckedChanged);
            // 
            // BiotechnologyResearch
            // 
            this.biotechnologyResearch.Cost = 100;
            this.biotechnologyResearch.Location = new System.Drawing.Point(193, 250);
            this.biotechnologyResearch.Name = "biotechnologyResearch";
            this.biotechnologyResearch.Size = new System.Drawing.Size(200, 128);
            this.biotechnologyResearch.TabIndex = 5;
            this.biotechnologyResearch.Title = "Biotechnology Research";
            this.biotechnologyResearch.SelectionChanged += new Nova.ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // ElectronicsResearch
            // 
            this.electronicsResearch.Cost = 100;
            this.electronicsResearch.Location = new System.Drawing.Point(196, 125);
            this.electronicsResearch.Name = "electronicsResearch";
            this.electronicsResearch.Size = new System.Drawing.Size(200, 128);
            this.electronicsResearch.TabIndex = 4;
            this.electronicsResearch.Title = "Electronics Research";
            this.electronicsResearch.SelectionChanged += new Nova.ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // ConstructionResearch
            // 
            this.constructionResearch.Cost = 100;
            this.constructionResearch.Location = new System.Drawing.Point(193, 9);
            this.constructionResearch.Name = "constructionResearch";
            this.constructionResearch.Size = new System.Drawing.Size(200, 128);
            this.constructionResearch.TabIndex = 3;
            this.constructionResearch.Title = "Construction Research";
            this.constructionResearch.SelectionChanged += new Nova.ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // PropulsionResearch
            // 
            this.propulsionResearch.Cost = 100;
            this.propulsionResearch.Location = new System.Drawing.Point(3, 250);
            this.propulsionResearch.Name = "propulsionResearch";
            this.propulsionResearch.Size = new System.Drawing.Size(200, 128);
            this.propulsionResearch.TabIndex = 2;
            this.propulsionResearch.Title = "Propulsion Research";
            this.propulsionResearch.SelectionChanged += new Nova.ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // WeaponsResearch
            // 
            this.weaponsResearch.Cost = 100;
            this.weaponsResearch.Location = new System.Drawing.Point(6, 125);
            this.weaponsResearch.Name = "weaponsResearch";
            this.weaponsResearch.Size = new System.Drawing.Size(200, 128);
            this.weaponsResearch.TabIndex = 1;
            this.weaponsResearch.Title = "Weapons Research";
            this.weaponsResearch.SelectionChanged += new Nova.ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // EnergyResearch
            // 
            this.energyResearch.Cost = 100;
            this.energyResearch.Location = new System.Drawing.Point(3, 9);
            this.energyResearch.Name = "energyResearch";
            this.energyResearch.Size = new System.Drawing.Size(200, 128);
            this.energyResearch.TabIndex = 0;
            this.energyResearch.Title = "Energy Research";
            this.energyResearch.SelectionChanged += new Nova.ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // Finish
            // 
            this.finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finish.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.finish.Location = new System.Drawing.Point(20, 501);
            this.finish.Name = "finish";
            this.finish.Size = new System.Drawing.Size(80, 24);
            this.finish.TabIndex = 2;
            this.finish.Text = "Generate";
            this.finish.Click += new System.EventHandler(this.Finish_Click);
            // 
            // Exit
            // 
            this.exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.exit.Location = new System.Drawing.Point(332, 499);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(80, 26);
            this.exit.TabIndex = 3;
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // MainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(426, 24);
            this.mainMenu.TabIndex = 5;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.loadRaceFile});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
            // 
            // LoadRaceFile
            // 
            this.loadRaceFile.Name = "LoadRaceFile";
            this.loadRaceFile.Size = new System.Drawing.Size(149, 22);
            this.loadRaceFile.Text = "Load Race File";
            this.loadRaceFile.Click += new System.EventHandler(this.LoadRaceFile_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // OpenFileDialog
            // 
            this.openFileDialog.FileName = "OpenFileDialog";
            this.openFileDialog.Filter = "Race Definition File | *.race";
            this.openFileDialog.Title = "Nova - Specify Race Definition File";
            // 
            // RaceDesignerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(426, 539);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.tabConrol);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.finish);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(432, 544);
            this.Name = "RaceDesignerForm";
            this.Text = "Nova Race Designer";
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.tabConrol.ResumeLayout(false);
            this.raceTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.traitsTab.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.environmentTab.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxGrowth)).EndInit();
            this.productionTab.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mineralProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operableMines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resourcesPerMine)).EndInit();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resourceProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factoryBuildCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operableFactories)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colonistProduction)).EndInit();
            this.researchTab.ResumeLayout(false);
            this.researchTab.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Main

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new RaceDesignerForm());
        }

        #endregion

        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This funtion is invoked whenever a primary racial traits radio button
        /// is selected. 
        ///
        /// If a button has moved to the unchecked state we "pay back" the advantage
        /// point cost of the deselected race. If the button has moved to the selected
        /// state then we adjust the advantage point balance by the appropriate cost.
        ///
        /// All primary racial traits are defined in the file PrimaryTraits.cs.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            foreach (DictionaryEntry dict in AllTraits.Data.Primary)
            {
                TraitEntry trait = (TraitEntry)dict.Value;
                if (trait.Code == radioButton.Tag.ToString())
                {
                    if (radioButton.Checked)
                    {
                        this.selectedRace = AllTraits.Data.Primary[radioButton.Tag.ToString()];
                        this.advantagePoints -= trait.Cost;
                        this.primaryTraitDescription.Text = trait.Description;
                    }
                    else
                    {
                        this.advantagePoints += trait.Cost;
                    }

                    this.parametersChanged = true;
                    showAvailablePoints();
                    break;
                }
            }
        }

        private void showAvailablePoints()
        {
            this.availablePoints.Text = this.advantagePoints.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This funtion is invoked whenever a secondary racial traits radio button is
        /// selected. Based on the value in the Name property, the description and
        /// advantage point value will be adjusted to suit the selected race (in a
        /// similar manner is as done for the primary racial trait above). 
        /// <para>
        /// All secondary racial traits are defined in the file SecondaryTraits.cs.
        /// </para>
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void SecondaryTraits_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            foreach (DictionaryEntry de in AllTraits.Data.Secondary)
            {
                TraitEntry trait = de.Value as TraitEntry;
                if (trait.Code == checkBox.Tag.ToString())
                {

                    if (checkBox.Checked)
                    {
                        this.advantagePoints -= trait.Cost;
                        this.secondaryTraitDescription.Text = trait.Description;
                    }
                    else
                    {
                        this.advantagePoints += trait.Cost;
                    }

                    showAvailablePoints();
                    this.parametersChanged = true;
                    break;
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// <para>
        /// Called when an up/down counter has changed. Put back the previous advantage
        /// point value taken and now use the newly selected value. 
        /// </para></summary>
        /// <remarks>
        /// <para>
        /// Explicitly check for ColonistProduction as array indeces need dividing by
        /// 100 to match its units. 
        /// </para><para>
        /// Note that element zero of each entry in the parameter definition array is
        /// used to hold the previous valueof the up-down counter (so that we can "pay
        /// back" advantage points on a parameter change)
        /// </para><para>
        /// The actual advantage point costs are defined in the file ParameterCosts.cs.
        /// </para></remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void UpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown upDown = (NumericUpDown)sender;

            foreach (ParameterEntry parameter in ParameterCosts.Parameters)
            {
                if (parameter.ParameterName == upDown.Name)
                {
                    int newValue = (int)upDown.Value;
                    int oldValue = parameter.Cost[0];

                    if (upDown.Name == "colonistProduction")
                    {
                        this.advantagePoints += parameter.Cost[oldValue / 100];
                        this.advantagePoints -= parameter.Cost[newValue / 100];
                    }
                    else
                    {
                        this.advantagePoints += parameter.Cost[oldValue];
                        this.advantagePoints -= parameter.Cost[newValue];
                    }

                    parameter.Cost[0] = newValue;
                    showAvailablePoints();
                    this.parametersChanged = true;
                    break;
                }
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when a research cost has changed. Note that the ResearchCost control
        /// retuns the appropriate advantage points adjustment for both the button going
        /// off and for the new selection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="value">Change in advantage points.</param>
        /// ----------------------------------------------------------------------------
        private void ResearchCost_SelectionChanged(object sender, int value)
        {
            this.advantagePoints += value;
            showAvailablePoints();
            this.parametersChanged = true;
        }
        
        /// <summary>
        /// Called when the Extratech combobox is checked
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void ExtraTech_CheckedChanged(object sender, EventArgs e)
        {
            int cost = 60;
            if (this.extraTech.Checked == true)
            {
                this.advantagePoints -= cost;
            }
            else
            {
                this.advantagePoints += cost;
            }
            showAvailablePoints();
            this.parametersChanged = true;
        }
        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when a tolerance range has changed. The new width of the range and
        /// its position (based on the middle of the bar) both impact the advantage
        /// point value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="newLeftPos"></param>
        /// <param name="newRightPos"></param>
        /// <param name="oldLeftPos"></param>
        /// <param name="oldRightPos"></param>
        /// ----------------------------------------------------------------------------
        private void Tolerance_RangeChanged(object sender, int newLeftPos, int newRightPos, int oldLeftPos, int oldRightPos)
        {

            this.advantagePoints -= Utilities.BarWidthCost(oldLeftPos, oldRightPos);
            this.advantagePoints += Utilities.BarWidthCost(newLeftPos, newRightPos);

            this.advantagePoints -= Utilities.BarPositionCost(oldLeftPos, oldRightPos);
            this.advantagePoints += Utilities.BarPositionCost(newLeftPos, newRightPos);

            showAvailablePoints();
            this.parametersChanged = true;
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when an immunity checkbox has changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="value"></param>
        /// ----------------------------------------------------------------------------
        private void Tolerance_CheckChanged(object sender, int value)
        {
            this.advantagePoints += value;
            showAvailablePoints();
            this.parametersChanged = true;
        }
        
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function is called when the Exit button is pressed. Provide a warning
        /// that this will discard the race definition and see if he really wants
        /// to do this.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Exit_Click(object sender, EventArgs e)
        {
            if (this.parametersChanged)
            {
                DialogResult result = Utilities.CancelWarning(this);

                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// This function is called when the Generate button is pressed. Providing that
        /// the advantage points total is not negative we then populate the race details
        /// structure and use it to generate the race definition file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void Finish_Click(object sender, System.EventArgs e)
        {
            if (this.advantagePoints < 0)
            {
                Report.Error("You are not allowed to generate a race file" +
                             "when you have less than zero Advantage Points");
                return;
            }

            // ----------------------------------------------------------------------------
            // Primary Racial Traits
            // ----------------------------------------------------------------------------

            if (this.raceName.Text == "" || this.pluralRaceName.Text == "")
            {
                Report.Error("The race name canonot be blank when generating" +
                             "a new race file - nor can the plural name");
                return;
            }

            if (this.password.Text == "")
            {
                Report.Error("The password field cannot be blank when " +
                             "generating a new race file. You will need it to " +
                             "access your turn during a game. Note, passwords " +
                             "are case-sensitive.");
                return;
            }

            Race raceParameters       = new Race();
            raceParameters.Traits.SetPrimary(this.selectedRace);
            raceParameters.Name       = this.raceName.Text;
            raceParameters.PluralName = this.pluralRaceName.Text;

            string passwordHash = FormsAuthentication.
               HashPasswordForStoringInConfigFile(this.password.Text, "MD5");

            raceParameters.Password = passwordHash;
            raceParameters.Icon     = this.currentRaceIcon;

            // ----------------------------------------------------------------------------
            // Secondary Racial Traits
            // ----------------------------------------------------------------------------
            if (this.improvedFuelEfficiency.Checked) raceParameters.Traits.Add("IFE");
            if (this.noRamEngines.Checked) raceParameters.Traits.Add("NRS");
            if (this.totalTerraforming.Checked) raceParameters.Traits.Add("TT");
            if (this.cheapEngines.Checked) raceParameters.Traits.Add("CE");
            if (this.advancedRemoteMining.Checked) raceParameters.Traits.Add("ARM");
            if (this.basicRemoteMining.Checked) raceParameters.Traits.Add("OBRM");
            if (this.improvedStarbases.Checked) raceParameters.Traits.Add("ISB");
            if (this.noAdvancedScanners.Checked) raceParameters.Traits.Add("NAS");
            if (this.generalisedResearch.Checked) raceParameters.Traits.Add("GR");
            if (this.lowStartingPopulation.Checked) raceParameters.Traits.Add("LSP");
            if (this.ultimateRecycling.Checked) raceParameters.Traits.Add("UR");
            if (this.bleedingEdgeTechnology.Checked) raceParameters.Traits.Add("BET");
            if (this.mineralAlchemy.Checked) raceParameters.Traits.Add("MA");
            if (this.regeneratingShields.Checked) raceParameters.Traits.Add("RS");
            if (this.cheapFactories.Checked) raceParameters.Traits.Add("CF");
            if (this.extraTech.Checked) raceParameters.Traits.Add("ExtraTech");
            // ----------------------------------------------------------------------------
            // Production Costs and Rates
            // ----------------------------------------------------------------------------

            raceParameters.ColonistsPerResource = (int)this.colonistProduction.Value;
            raceParameters.OperableFactories    = (int)this.operableFactories.Value;
            raceParameters.MineProductionRate   = (int)this.mineralProduction.Value;
            raceParameters.OperableMines        = (int)this.operableMines.Value;
            raceParameters.FactoryBuildCost     = (int)this.factoryBuildCost.Value;
            raceParameters.MineBuildCost        = (int)this.resourcesPerMine.Value;
            raceParameters.FactoryProduction    = (int)this.resourceProduction.Value;

            // ----------------------------------------------------------------------------
            // Environmental Tolerance
            // ----------------------------------------------------------------------------

            raceParameters.GravityTolerance.MinimumValue = this.gravityTolerance.MinimumValue;
            raceParameters.GravityTolerance.MaximumValue = this.gravityTolerance.MaximumValue;
            raceParameters.GravityTolerance.Immune = this.gravityTolerance.Immune;
            raceParameters.RadiationTolerance.MinimumValue = this.radiationTolerance.MinimumValue;
            raceParameters.RadiationTolerance.MaximumValue = this.radiationTolerance.MaximumValue;
            raceParameters.RadiationTolerance.Immune = this.radiationTolerance.Immune;
            raceParameters.TemperatureTolerance.MinimumValue = this.temperatureTolerance.MinimumValue;
            raceParameters.TemperatureTolerance.MaximumValue = this.temperatureTolerance.MaximumValue;
            raceParameters.TemperatureTolerance.Immune = this.temperatureTolerance.Immune;
            raceParameters.GrowthRate = (double)this.maxGrowth.Value;

            // ----------------------------------------------------------------------------
            // Research Costs
            // ----------------------------------------------------------------------------

            raceParameters.ResearchCosts[TechLevel.ResearchField.Energy]        = this.energyResearch.Cost;
            raceParameters.ResearchCosts[TechLevel.ResearchField.Weapons]       = this.weaponsResearch.Cost;
            raceParameters.ResearchCosts[TechLevel.ResearchField.Propulsion]    = this.propulsionResearch.Cost;
            raceParameters.ResearchCosts[TechLevel.ResearchField.Construction]  = this.constructionResearch.Cost;
            raceParameters.ResearchCosts[TechLevel.ResearchField.Electronics]   = this.electronicsResearch.Cost;
            raceParameters.ResearchCosts[TechLevel.ResearchField.Biotechnology] = this.biotechnologyResearch.Cost;

            // ----------------------------------------------------------------------------
            // Generate the race definition file
            //
            // Note if the same folder on the same machine is chosen for all Nova GUI and
            // Nova Console files it is possible to play a game with multiple races using
            // the same log-in name. This is a useful debugging aid.
            // ----------------------------------------------------------------------------

            try
            {
                string raceFilePath = FileSearcher.GetFolder(Global.RaceFolderKey, Global.RaceFolderName);

                SaveFileDialog fd = new SaveFileDialog();
                fd.Title = "Save Race - " + raceParameters.Name;
                fd.FileName = raceParameters.Name + Global.RaceExtension;
                fd.InitialDirectory = raceFilePath;
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    raceFilePath = fd.FileName;
                }
                else
                {
                    Report.Error("Race has not been saved.");
                    return;
                }

                FileStream saveFile = new FileStream(raceFilePath, FileMode.Create);

                // Setup the XML document
                XmlDocument xmldoc = new XmlDocument();
                Global.InitializeXmlDocument(xmldoc);

                // add the components to the document
                xmldoc.ChildNodes.Item(1).AppendChild(raceParameters.ToXml(xmldoc));

                xmldoc.Save(saveFile);
                saveFile.Close();

                Report.Information("The " + raceParameters.PluralName + " have been saved to " + raceFilePath);

                using (Config conf = new Config())
                {
                    conf[Global.RaceFolderKey] = System.IO.Path.GetDirectoryName(raceFilePath);
                }
                // Remove the warning message for exiting with unsaved changes.
                this.parametersChanged = false;

                return;
            }
            catch (System.IO.FileNotFoundException)
            {

                Report.Error("File path not specified.");
                return;

            }
            catch (Exception exception)
            {
                Report.Error("Failed to save race file. " + exception.Message);
                return;
            }

        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Display the about box
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when the next image is to be selected
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void NextImage_Click(object sender, EventArgs e)
        {
            ++this.currentRaceIcon;
            this.pictureBox.Image = this.currentRaceIcon.Image;
            this.iconIndex.Text = Path.GetFileNameWithoutExtension(this.currentRaceIcon.Source);

        }
        

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Called when the previous image is to be selected
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void PreviousImage_Click(object sender, EventArgs e)
        {
            --this.currentRaceIcon;
            this.pictureBox.Image = this.currentRaceIcon.Image;
            this.iconIndex.Text = Path.GetFileNameWithoutExtension(this.currentRaceIcon.Source);
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Load an existing race file for examination or modification (however, once
        /// submitted there is no point in changing the race definition. It's too late
        /// and it's "gone into the system").
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void LoadRaceFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog.CheckFileExists = true;
            this.openFileDialog.FileName = "Humanoid" + Global.RaceExtension;
            if (this.openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string fileName = this.openFileDialog.FileName;

            try
            {
                Race raceParameters = new Race(fileName);

                // TODO (priority 6) - This level of security is not good enough as the race is stored un-encrypted.
                ControlLibrary.CheckPassword password =
                   new ControlLibrary.CheckPassword(raceParameters);

                password.ShowDialog();
                if (password.DialogResult == DialogResult.OK)
                {
                    ReloadRace(raceParameters);
                    ReloadSecondaryTraits(raceParameters);
                    ReloadBuildCosts(raceParameters);
                    ReloadEnvironmentalTolerance(raceParameters);
                    ReloadResearchCosts(raceParameters);
                }

                password.Dispose();
            }
            catch (Exception ex)
            {
                Report.Error("Failed to load file: \r\n" + ex.Message);
            }
        }

        #endregion

        #region Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Brute force and ignorance reload of a file. There must be a bettter way of
        /// doing this.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void ReloadRace(Race raceParameters)
        {

            this.selectedRace = raceParameters.Traits.Primary;
            this.raceName.Text = raceParameters.Name;
            this.pluralRaceName.Text = raceParameters.PluralName;

            switch (this.selectedRace.Code)
            {
                case "HE":
                    this.hyperExpansion.Checked = true;
                    break;
                case "SS":
                    this.superStealth.Checked = true;
                    break;
                case "WM":
                    this.warMonger.Checked = true;
                    break;
                case "CA":
                    this.claimAdjuster.Checked = true;
                    break;
                case "IS":
                    this.innerStrength.Checked = true;
                    break;
                case "SD":
                    this.spaceDemolition.Checked = true;
                    break;
                case "PP":
                    this.packetPhysics.Checked = true;
                    break;
                case "IT":
                    this.interStellarTraveller.Checked = true;
                    break;
                case "AR":
                    this.alternateReality.Checked = true;
                    break;
                case "JOAT":
                    this.jackOfAllTrades.Checked = true;
                    break;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reload seconday traits
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void ReloadSecondaryTraits(Race raceParameters)
        {
            this.improvedFuelEfficiency.Checked = raceParameters.Traits.Contains("IFE");
            this.noRamEngines.Checked           = raceParameters.Traits.Contains("NRS");
            this.totalTerraforming.Checked      = raceParameters.Traits.Contains("TT");
            this.cheapEngines.Checked           = raceParameters.Traits.Contains("CE");
            this.advancedRemoteMining.Checked   = raceParameters.Traits.Contains("ARM");
            this.basicRemoteMining.Checked      = raceParameters.Traits.Contains("OBRM");
            this.improvedStarbases.Checked      = raceParameters.Traits.Contains("ISB");
            this.noAdvancedScanners.Checked     = raceParameters.Traits.Contains("NAS");
            this.generalisedResearch.Checked    = raceParameters.Traits.Contains("GR");
            this.lowStartingPopulation.Checked  = raceParameters.Traits.Contains("LSP");
            this.ultimateRecycling.Checked      = raceParameters.Traits.Contains("UR");
            this.bleedingEdgeTechnology.Checked = raceParameters.Traits.Contains("BET");
            this.mineralAlchemy.Checked         = raceParameters.Traits.Contains("MA");
            this.regeneratingShields.Checked    = raceParameters.Traits.Contains("RS");
            this.cheapFactories.Checked         = raceParameters.Traits.Contains("CF");
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reload build cost parameters
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void ReloadBuildCosts(Race raceParameters)
        {
            this.colonistProduction.Value = (decimal)raceParameters.ColonistsPerResource;
            this.operableFactories.Value  = (decimal)raceParameters.OperableFactories;
            this.mineralProduction.Value  = (decimal)raceParameters.MineProductionRate;
            this.operableMines.Value      = (decimal)raceParameters.OperableMines;
            this.factoryBuildCost.Value   = (decimal)raceParameters.FactoryBuildCost;
            this.resourcesPerMine.Value   = (decimal)raceParameters.MineBuildCost;
            this.resourceProduction.Value = (decimal)raceParameters.FactoryProduction;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reload Environmental Tolerance
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void ReloadEnvironmentalTolerance(Race raceParameters)
        {
            this.gravityTolerance.MinimumValue = raceParameters.GravityTolerance.MinimumValue;
            this.gravityTolerance.MaximumValue = raceParameters.GravityTolerance.MaximumValue;
            this.gravityTolerance.Immune = raceParameters.GravityTolerance.Immune;
            this.gravityTolerance.ActivateRangeChange();

            this.radiationTolerance.MinimumValue = raceParameters.RadiationTolerance.MinimumValue;
            this.radiationTolerance.MaximumValue = raceParameters.RadiationTolerance.MaximumValue;
            this.radiationTolerance.Immune = raceParameters.RadiationTolerance.Immune;
            this.radiationTolerance.ActivateRangeChange();

            this.temperatureTolerance.MinimumValue = raceParameters.TemperatureTolerance.MinimumValue;
            this.temperatureTolerance.MaximumValue = raceParameters.TemperatureTolerance.MaximumValue;
            this.temperatureTolerance.Immune = raceParameters.TemperatureTolerance.Immune;
            this.temperatureTolerance.ActivateRangeChange();

            this.maxGrowth.Value = (decimal)raceParameters.GrowthRate;
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Reload Research Costs
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void ReloadResearchCosts(Race raceParameters)
        {
            this.energyResearch.Cost        = (int)raceParameters.ResearchCosts[TechLevel.ResearchField.Energy];
            this.weaponsResearch.Cost       = (int)raceParameters.ResearchCosts[TechLevel.ResearchField.Weapons];
            this.propulsionResearch.Cost    = (int)raceParameters.ResearchCosts[TechLevel.ResearchField.Propulsion];
            this.constructionResearch.Cost  = (int)raceParameters.ResearchCosts[TechLevel.ResearchField.Construction];
            this.electronicsResearch.Cost   = (int)raceParameters.ResearchCosts[TechLevel.ResearchField.Electronics];
            this.biotechnologyResearch.Cost = (int)raceParameters.ResearchCosts[TechLevel.ResearchField.Biotechnology];
        }

        #endregion

       

    }
}


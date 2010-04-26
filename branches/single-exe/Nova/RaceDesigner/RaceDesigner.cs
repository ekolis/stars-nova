// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010 stars-nova
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// Race Designer: Create (or examine) the race parameters to be fed into the
// Nova main game.
// ============================================================================

using Microsoft.Win32;
using NovaCommon;
using ControlLibrary;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.IO.Compression;
using System.Xml;
using System.Web.Security;
using System.Windows.Forms;


namespace Nova.RaceDesigner
{

// ============================================================================
// Race designer main (and only) application form
// ============================================================================

   [Serializable]
   public class RaceDesignerForm : System.Windows.Forms.Form
   {
      RaceIcon CurrentRaceIcon = null; 

      //---------------------------------------------------------------------------- 
      //  Non-designer generated variables
      //---------------------------------------------------------------------------- 
      private int AdvantagePoints = 53;   
      private TraitEntry          SelectedRace      = AllTraits.Data.All["JOAT"];
      private bool            ParametersChanged = false;

      //---------------------------------------------------------------------------- 
      //    Designer generated variables
      //---------------------------------------------------------------------------- 
      #region Designer Generated Variables
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.TabControl TabConrol;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Button Finish;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.TextBox RaceName;
      private System.Windows.Forms.TextBox PluralRaceName;
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
      private System.Windows.Forms.NumericUpDown OperableFactories;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label PrimaryTraitDescription;
      private System.Windows.Forms.GroupBox groupBox9;
      private System.Windows.Forms.RadioButton JackOfAllTrades;
      private System.Windows.Forms.RadioButton HyperExpansion;
      private System.Windows.Forms.RadioButton SpaceDemolition;
      private System.Windows.Forms.RadioButton PacketPhysics;
      private System.Windows.Forms.RadioButton InterStellarTraveller;
      private System.Windows.Forms.RadioButton AlternateReality;
      private System.Windows.Forms.RadioButton SuperStealth;
      private System.Windows.Forms.RadioButton WarMonger;
      private System.Windows.Forms.RadioButton ClaimAdjuster;
      private System.Windows.Forms.RadioButton InnerStrength;
      private System.Windows.Forms.CheckBox CheapFactories;
      private System.Windows.Forms.CheckBox RegeneratingShields;
      private System.Windows.Forms.CheckBox BleedingEdgeTechnology;
      private System.Windows.Forms.CheckBox LowStartingPopulation;
      private System.Windows.Forms.CheckBox NoAdvancedScanners;
      private System.Windows.Forms.CheckBox BasicRemoteMining;
      private System.Windows.Forms.CheckBox CheapEngines;
      private System.Windows.Forms.CheckBox NoRAMEngines;
      private System.Windows.Forms.CheckBox MineralAlchemy;
      private System.Windows.Forms.CheckBox UltimateRecycling;
      private System.Windows.Forms.CheckBox GeneralisedResearch;
      private System.Windows.Forms.CheckBox AdvancedRemoteMining;
      private System.Windows.Forms.CheckBox TotalTerraforming;
      private System.Windows.Forms.CheckBox ImprovedFuelEfficiency;
      private System.Windows.Forms.TabPage TraitsTab;
      private System.Windows.Forms.TabPage RaceTab;
      private System.Windows.Forms.TabPage ProductionTab;
      private System.Windows.Forms.TabPage ResearchTab;
      private System.Windows.Forms.TabPage EnvironmentTab;
      private System.Windows.Forms.Label SecondaryTraitDescription;
      private System.Windows.Forms.CheckBox ImprovedStarbases;
      private System.Windows.Forms.Label AvailablePoints;
      private System.Windows.Forms.NumericUpDown FactoryBuildCost;
      private System.Windows.Forms.NumericUpDown MineralProduction;
      private System.Windows.Forms.NumericUpDown ResourcesPerMine;
      private System.Windows.Forms.NumericUpDown OperableMines;
      private ControlLibrary.ResearchCost EnergyResearch;
      private ControlLibrary.ResearchCost WeaponsResearch;
      private ControlLibrary.ResearchCost PropulsionResearch;
      private ControlLibrary.ResearchCost ConstructionResearch;
      private ControlLibrary.ResearchCost ElectronicsResearch;
      private ControlLibrary.ResearchCost BiotechnologyResearch;
      private ControlLibrary.Range GravityTolerance;
      private ControlLibrary.Range TemperatureTolerance;
      private ControlLibrary.Range RadiationTolerance;
      private System.Windows.Forms.GroupBox groupBox10;
      private System.Windows.Forms.ComboBox UnusedPointsTarget;
      private System.Windows.Forms.NumericUpDown ResourceProduction;
       private System.Windows.Forms.NumericUpDown ColonistProduction;
      private System.Windows.Forms.Button Exit;
      private GroupBox groupBox12;
      private GroupBox groupBox11;
      private MenuStrip MainMenu;
      private ToolStripMenuItem fileToolStripMenuItem;
      private ToolStripMenuItem exitToolStripMenuItem;
      private ToolStripMenuItem helpToolStripMenuItem;
      private ToolStripMenuItem aboutToolStripMenuItem;
      private TextBox Password;
      private Label label12;
//      private ImageList RaceIcons;
      private Button PreviousImage;
      private Button NextImage;
      private PictureBox PictureBox;
      private Label IconIndex;
      private ToolStripMenuItem LoadRaceFile;
      private OpenFileDialog OpenFileDialog;
       private NumericUpDown MaxGrowth;
       private Label label4;
      private IContainer components;

      #endregion Designer Generated Variables

      /// <summary>
      /// Construction and dynamic initialisation which consists of ensuring
      /// that there is a default primary racial trait selected and that what to
      /// spend unused advantage points on has a default.
      /// </summary>
      public RaceDesignerForm()
      {
          InitializeComponent();

          // Initialize the Nova registry keys
          FileSearcher.SetKeys();

          JackOfAllTrades.Checked = true;
          SelectedRace = AllTraits.Data.Primary["JOAT"];
          PrimaryTraitDescription.Text = SelectedRace.Description;

          ParametersChanged = false;
          UnusedPointsTarget.SelectedIndex = 0;
      }


      /// <summary>
      /// Called when the form is loaded. 
      /// Load the race icon data. Load the first race icon into
      /// its image box, record the index of the current image (0) and make a note
      /// of the number of images so that we can just cycle through them.
      /// </summary>
      private void OnLoad(object sender, EventArgs e)
      {
          AllRaceIcons.Restore();
          CurrentRaceIcon = (RaceIcon)AllRaceIcons.Data.IconList[0];
          PictureBox.Image = CurrentRaceIcon.Image;
          IconIndex.Text = Path.GetFileNameWithoutExtension(CurrentRaceIcon.Source);

          // Can't trust the windows designer generate code to set the environment range before setting the environment value, so set it here to be sure.
          // TODO (priority 3) - put all these literal values somewhere sensible (EnvironmentTolerance object?)
          TemperatureTolerance.RangeMinimum = -200;
          TemperatureTolerance.RangeMaximum = 200;
          TemperatureTolerance.BarLower = -140;
          TemperatureTolerance.BarUpper = 140;
          GravityTolerance.RangeMinimum = 0;
          GravityTolerance.RangeMaximum = 10;
          GravityTolerance.BarLower = 1.5;
          GravityTolerance.BarUpper = 8.5;
          RadiationTolerance.RangeMinimum = 0;
          RadiationTolerance.RangeMaximum = 100;
          RadiationTolerance.BarLower = 15;
          RadiationTolerance.BarUpper = 85;

      }

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing"></param>
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

       
// ============================================================================
// Form Designer generated code
// ============================================================================
#region Windows Form Designer generated code
       /// <summary>
       /// Required method for Designer support - do not modify
       /// the contents of this method with the code editor.
       /// </summary>
         private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RaceDesignerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AvailablePoints = new System.Windows.Forms.Label();
            this.TabConrol = new System.Windows.Forms.TabControl();
            this.RaceTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PluralRaceName = new System.Windows.Forms.TextBox();
            this.PreviousImage = new System.Windows.Forms.Button();
            this.NextImage = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.RaceName = new System.Windows.Forms.TextBox();
            this.IconIndex = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PrimaryTraitDescription = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.JackOfAllTrades = new System.Windows.Forms.RadioButton();
            this.AlternateReality = new System.Windows.Forms.RadioButton();
            this.InterStellarTraveller = new System.Windows.Forms.RadioButton();
            this.PacketPhysics = new System.Windows.Forms.RadioButton();
            this.SpaceDemolition = new System.Windows.Forms.RadioButton();
            this.InnerStrength = new System.Windows.Forms.RadioButton();
            this.ClaimAdjuster = new System.Windows.Forms.RadioButton();
            this.WarMonger = new System.Windows.Forms.RadioButton();
            this.SuperStealth = new System.Windows.Forms.RadioButton();
            this.HyperExpansion = new System.Windows.Forms.RadioButton();
            this.TraitsTab = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.SecondaryTraitDescription = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CheapFactories = new System.Windows.Forms.CheckBox();
            this.RegeneratingShields = new System.Windows.Forms.CheckBox();
            this.BleedingEdgeTechnology = new System.Windows.Forms.CheckBox();
            this.LowStartingPopulation = new System.Windows.Forms.CheckBox();
            this.NoAdvancedScanners = new System.Windows.Forms.CheckBox();
            this.BasicRemoteMining = new System.Windows.Forms.CheckBox();
            this.CheapEngines = new System.Windows.Forms.CheckBox();
            this.NoRAMEngines = new System.Windows.Forms.CheckBox();
            this.MineralAlchemy = new System.Windows.Forms.CheckBox();
            this.UltimateRecycling = new System.Windows.Forms.CheckBox();
            this.GeneralisedResearch = new System.Windows.Forms.CheckBox();
            this.ImprovedStarbases = new System.Windows.Forms.CheckBox();
            this.AdvancedRemoteMining = new System.Windows.Forms.CheckBox();
            this.TotalTerraforming = new System.Windows.Forms.CheckBox();
            this.ImprovedFuelEfficiency = new System.Windows.Forms.CheckBox();
            this.ProductionTab = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.MineralProduction = new System.Windows.Forms.NumericUpDown();
            this.OperableMines = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.ResourcesPerMine = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ResourceProduction = new System.Windows.Forms.NumericUpDown();
            this.FactoryBuildCost = new System.Windows.Forms.NumericUpDown();
            this.OperableFactories = new System.Windows.Forms.NumericUpDown();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.UnusedPointsTarget = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.ColonistProduction = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.EnvironmentTab = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.MaxGrowth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.ResearchTab = new System.Windows.Forms.TabPage();
            this.Finish = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadRaceFile = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.RadiationTolerance = new ControlLibrary.Range();
            this.TemperatureTolerance = new ControlLibrary.Range();
            this.GravityTolerance = new ControlLibrary.Range();
            this.BiotechnologyResearch = new ControlLibrary.ResearchCost();
            this.ElectronicsResearch = new ControlLibrary.ResearchCost();
            this.ConstructionResearch = new ControlLibrary.ResearchCost();
            this.PropulsionResearch = new ControlLibrary.ResearchCost();
            this.WeaponsResearch = new ControlLibrary.ResearchCost();
            this.EnergyResearch = new ControlLibrary.ResearchCost();
            this.groupBox1.SuspendLayout();
            this.TabConrol.SuspendLayout();
            this.RaceTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TraitsTab.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.ProductionTab.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MineralProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperableMines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesPerMine)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResourceProduction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FactoryBuildCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperableFactories)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColonistProduction)).BeginInit();
            this.EnvironmentTab.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxGrowth)).BeginInit();
            this.ResearchTab.SuspendLayout();
            this.MainMenu.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.AvailablePoints);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(15, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 40);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // AvailablePoints
            // 
            this.AvailablePoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailablePoints.Location = new System.Drawing.Point(112, 10);
            this.AvailablePoints.Name = "AvailablePoints";
            this.AvailablePoints.Size = new System.Drawing.Size(104, 24);
            this.AvailablePoints.TabIndex = 99;
            this.AvailablePoints.Text = "53";
            this.AvailablePoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabConrol
            // 
            this.TabConrol.Controls.Add(this.RaceTab);
            this.TabConrol.Controls.Add(this.TraitsTab);
            this.TabConrol.Controls.Add(this.ProductionTab);
            this.TabConrol.Controls.Add(this.EnvironmentTab);
            this.TabConrol.Controls.Add(this.ResearchTab);
            this.TabConrol.ItemSize = new System.Drawing.Size(0, 18);
            this.TabConrol.Location = new System.Drawing.Point(16, 78);
            this.TabConrol.Name = "TabConrol";
            this.TabConrol.SelectedIndex = 0;
            this.TabConrol.Size = new System.Drawing.Size(400, 418);
            this.TabConrol.TabIndex = 0;
            // 
            // RaceTab
            // 
            this.RaceTab.Controls.Add(this.label2);
            this.RaceTab.Controls.Add(this.label3);
            this.RaceTab.Controls.Add(this.groupBox2);
            this.RaceTab.Controls.Add(this.groupBox4);
            this.RaceTab.Controls.Add(this.groupBox3);
            this.RaceTab.Location = new System.Drawing.Point(4, 22);
            this.RaceTab.Name = "RaceTab";
            this.RaceTab.Size = new System.Drawing.Size(392, 392);
            this.RaceTab.TabIndex = 0;
            this.RaceTab.Text = "Race";
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
            this.groupBox2.Controls.Add(this.PluralRaceName);
            this.groupBox2.Controls.Add(this.PreviousImage);
            this.groupBox2.Controls.Add(this.NextImage);
            this.groupBox2.Controls.Add(this.PictureBox);
            this.groupBox2.Controls.Add(this.Password);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.RaceName);
            this.groupBox2.Controls.Add(this.IconIndex);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(8, -2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 86);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // PluralRaceName
            // 
            this.PluralRaceName.Location = new System.Drawing.Point(104, 36);
            this.PluralRaceName.Name = "PluralRaceName";
            this.PluralRaceName.Size = new System.Drawing.Size(132, 20);
            this.PluralRaceName.TabIndex = 2;
            this.PluralRaceName.Text = "Humanoids";
            // 
            // PreviousImage
            // 
            this.PreviousImage.Location = new System.Drawing.Point(325, 36);
            this.PreviousImage.Name = "PreviousImage";
            this.PreviousImage.Size = new System.Drawing.Size(44, 23);
            this.PreviousImage.TabIndex = 6;
            this.PreviousImage.Text = "-";
            this.PreviousImage.UseVisualStyleBackColor = true;
            this.PreviousImage.Click += new System.EventHandler(this.PreviousImage_Click);
            // 
            // NextImage
            // 
            this.NextImage.Location = new System.Drawing.Point(325, 13);
            this.NextImage.Name = "NextImage";
            this.NextImage.Size = new System.Drawing.Size(44, 23);
            this.NextImage.TabIndex = 5;
            this.NextImage.Text = "+";
            this.NextImage.UseVisualStyleBackColor = true;
            this.NextImage.Click += new System.EventHandler(this.NextImage_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox.Location = new System.Drawing.Point(255, 14);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(64, 64);
            this.PictureBox.TabIndex = 4;
            this.PictureBox.TabStop = false;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(103, 60);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(132, 20);
            this.Password.TabIndex = 3;
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
            this.RaceName.Location = new System.Drawing.Point(104, 13);
            this.RaceName.Name = "RaceName";
            this.RaceName.Size = new System.Drawing.Size(132, 20);
            this.RaceName.TabIndex = 1;
            this.RaceName.Text = "Humanoid";
            // 
            // IconIndex
            // 
            this.IconIndex.Location = new System.Drawing.Point(324, 62);
            this.IconIndex.Name = "IconIndex";
            this.IconIndex.Size = new System.Drawing.Size(43, 17);
            this.IconIndex.TabIndex = 7;
            this.IconIndex.Text = "31";
            this.IconIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.PrimaryTraitDescription);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(7, 244);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(376, 133);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trait Description";
            // 
            // PrimaryTraitDescription
            // 
            this.PrimaryTraitDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrimaryTraitDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PrimaryTraitDescription.Location = new System.Drawing.Point(3, 16);
            this.PrimaryTraitDescription.Name = "PrimaryTraitDescription";
            this.PrimaryTraitDescription.Size = new System.Drawing.Size(370, 114);
            this.PrimaryTraitDescription.TabIndex = 0;
            this.PrimaryTraitDescription.Text = "Will hold race description.";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.JackOfAllTrades);
            this.groupBox3.Controls.Add(this.AlternateReality);
            this.groupBox3.Controls.Add(this.InterStellarTraveller);
            this.groupBox3.Controls.Add(this.PacketPhysics);
            this.groupBox3.Controls.Add(this.SpaceDemolition);
            this.groupBox3.Controls.Add(this.InnerStrength);
            this.groupBox3.Controls.Add(this.ClaimAdjuster);
            this.groupBox3.Controls.Add(this.WarMonger);
            this.groupBox3.Controls.Add(this.SuperStealth);
            this.groupBox3.Controls.Add(this.HyperExpansion);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Location = new System.Drawing.Point(7, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(374, 144);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Primary Racial Traits";
            // 
            // JackOfAllTrades
            // 
            this.JackOfAllTrades.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.JackOfAllTrades.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.JackOfAllTrades.Location = new System.Drawing.Point(239, 112);
            this.JackOfAllTrades.Name = "JackOfAllTrades";
            this.JackOfAllTrades.Size = new System.Drawing.Size(126, 24);
            this.JackOfAllTrades.TabIndex = 9;
            this.JackOfAllTrades.Tag = "JOAT";
            this.JackOfAllTrades.Text = "Jack of all Trades";
            this.JackOfAllTrades.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // AlternateReality
            // 
            this.AlternateReality.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AlternateReality.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AlternateReality.Location = new System.Drawing.Point(239, 88);
            this.AlternateReality.Name = "AlternateReality";
            this.AlternateReality.Size = new System.Drawing.Size(126, 24);
            this.AlternateReality.TabIndex = 8;
            this.AlternateReality.Tag = "AR";
            this.AlternateReality.Text = "Alternate Reality";
            this.AlternateReality.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // InterStellarTraveller
            // 
            this.InterStellarTraveller.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.InterStellarTraveller.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.InterStellarTraveller.Location = new System.Drawing.Point(239, 64);
            this.InterStellarTraveller.Name = "InterStellarTraveller";
            this.InterStellarTraveller.Size = new System.Drawing.Size(126, 24);
            this.InterStellarTraveller.TabIndex = 7;
            this.InterStellarTraveller.Tag = "IT";
            this.InterStellarTraveller.Text = "Inter-stellar Traveller";
            this.InterStellarTraveller.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // PacketPhysics
            // 
            this.PacketPhysics.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PacketPhysics.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PacketPhysics.Location = new System.Drawing.Point(239, 40);
            this.PacketPhysics.Name = "PacketPhysics";
            this.PacketPhysics.Size = new System.Drawing.Size(126, 24);
            this.PacketPhysics.TabIndex = 6;
            this.PacketPhysics.Tag = "PP";
            this.PacketPhysics.Text = "Packet Physics";
            this.PacketPhysics.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // SpaceDemolition
            // 
            this.SpaceDemolition.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SpaceDemolition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SpaceDemolition.Location = new System.Drawing.Point(239, 20);
            this.SpaceDemolition.Name = "SpaceDemolition";
            this.SpaceDemolition.Size = new System.Drawing.Size(126, 24);
            this.SpaceDemolition.TabIndex = 5;
            this.SpaceDemolition.Tag = "SD";
            this.SpaceDemolition.Text = "Space Demolition";
            this.SpaceDemolition.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // InnerStrength
            // 
            this.InnerStrength.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.InnerStrength.Location = new System.Drawing.Point(8, 112);
            this.InnerStrength.Name = "InnerStrength";
            this.InnerStrength.Size = new System.Drawing.Size(104, 24);
            this.InnerStrength.TabIndex = 4;
            this.InnerStrength.Tag = "IS";
            this.InnerStrength.Text = "Inner Strength";
            this.InnerStrength.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // ClaimAdjuster
            // 
            this.ClaimAdjuster.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ClaimAdjuster.Location = new System.Drawing.Point(8, 88);
            this.ClaimAdjuster.Name = "ClaimAdjuster";
            this.ClaimAdjuster.Size = new System.Drawing.Size(104, 24);
            this.ClaimAdjuster.TabIndex = 3;
            this.ClaimAdjuster.Tag = "CA";
            this.ClaimAdjuster.Text = "Claim Adjuster";
            this.ClaimAdjuster.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // WarMonger
            // 
            this.WarMonger.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.WarMonger.Location = new System.Drawing.Point(8, 64);
            this.WarMonger.Name = "WarMonger";
            this.WarMonger.Size = new System.Drawing.Size(104, 24);
            this.WarMonger.TabIndex = 2;
            this.WarMonger.Tag = "WM";
            this.WarMonger.Text = "War Monger";
            this.WarMonger.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // SuperStealth
            // 
            this.SuperStealth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SuperStealth.Location = new System.Drawing.Point(8, 40);
            this.SuperStealth.Name = "SuperStealth";
            this.SuperStealth.Size = new System.Drawing.Size(104, 24);
            this.SuperStealth.TabIndex = 1;
            this.SuperStealth.Tag = "SS";
            this.SuperStealth.Text = "Super Stealth";
            this.SuperStealth.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // HyperExpansion
            // 
            this.HyperExpansion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.HyperExpansion.Location = new System.Drawing.Point(8, 20);
            this.HyperExpansion.Name = "HyperExpansion";
            this.HyperExpansion.Size = new System.Drawing.Size(110, 24);
            this.HyperExpansion.TabIndex = 0;
            this.HyperExpansion.Tag = "HE";
            this.HyperExpansion.Text = "Hyper-Expansion";
            this.HyperExpansion.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // TraitsTab
            // 
            this.TraitsTab.Controls.Add(this.groupBox7);
            this.TraitsTab.Controls.Add(this.groupBox5);
            this.TraitsTab.Location = new System.Drawing.Point(4, 22);
            this.TraitsTab.Name = "TraitsTab";
            this.TraitsTab.Size = new System.Drawing.Size(392, 392);
            this.TraitsTab.TabIndex = 1;
            this.TraitsTab.Text = "Traits";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.SecondaryTraitDescription);
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
            this.SecondaryTraitDescription.Location = new System.Drawing.Point(10, 16);
            this.SecondaryTraitDescription.Name = "SecondaryTraitDescription";
            this.SecondaryTraitDescription.Size = new System.Drawing.Size(346, 59);
            this.SecondaryTraitDescription.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.CheapFactories);
            this.groupBox5.Controls.Add(this.RegeneratingShields);
            this.groupBox5.Controls.Add(this.BleedingEdgeTechnology);
            this.groupBox5.Controls.Add(this.LowStartingPopulation);
            this.groupBox5.Controls.Add(this.NoAdvancedScanners);
            this.groupBox5.Controls.Add(this.BasicRemoteMining);
            this.groupBox5.Controls.Add(this.CheapEngines);
            this.groupBox5.Controls.Add(this.NoRAMEngines);
            this.groupBox5.Controls.Add(this.MineralAlchemy);
            this.groupBox5.Controls.Add(this.UltimateRecycling);
            this.groupBox5.Controls.Add(this.GeneralisedResearch);
            this.groupBox5.Controls.Add(this.ImprovedStarbases);
            this.groupBox5.Controls.Add(this.AdvancedRemoteMining);
            this.groupBox5.Controls.Add(this.TotalTerraforming);
            this.groupBox5.Controls.Add(this.ImprovedFuelEfficiency);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox5.Location = new System.Drawing.Point(8, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(374, 226);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Secondary Racial Traits";
            // 
            // CheapFactories
            // 
            this.CheapFactories.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheapFactories.Location = new System.Drawing.Point(16, 193);
            this.CheapFactories.Name = "CheapFactories";
            this.CheapFactories.Size = new System.Drawing.Size(186, 24);
            this.CheapFactories.TabIndex = 14;
            this.CheapFactories.Tag = "CF";
            this.CheapFactories.Text = "Factories cost 1kT less Germanium";
            this.CheapFactories.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // RegeneratingShields
            // 
            this.RegeneratingShields.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RegeneratingShields.Location = new System.Drawing.Point(211, 168);
            this.RegeneratingShields.Name = "RegeneratingShields";
            this.RegeneratingShields.Size = new System.Drawing.Size(131, 24);
            this.RegeneratingShields.TabIndex = 13;
            this.RegeneratingShields.Tag = "RS";
            this.RegeneratingShields.Text = "Regenerating shields";
            this.RegeneratingShields.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // BleedingEdgeTechnology
            // 
            this.BleedingEdgeTechnology.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BleedingEdgeTechnology.Location = new System.Drawing.Point(211, 144);
            this.BleedingEdgeTechnology.Name = "BleedingEdgeTechnology";
            this.BleedingEdgeTechnology.Size = new System.Drawing.Size(145, 24);
            this.BleedingEdgeTechnology.TabIndex = 12;
            this.BleedingEdgeTechnology.Tag = "BET";
            this.BleedingEdgeTechnology.Text = "Bleeding edge technology";
            this.BleedingEdgeTechnology.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // LowStartingPopulation
            // 
            this.LowStartingPopulation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LowStartingPopulation.Location = new System.Drawing.Point(211, 120);
            this.LowStartingPopulation.Name = "LowStartingPopulation";
            this.LowStartingPopulation.Size = new System.Drawing.Size(130, 24);
            this.LowStartingPopulation.TabIndex = 11;
            this.LowStartingPopulation.Tag = "LSP";
            this.LowStartingPopulation.Text = "Low starting population";
            this.LowStartingPopulation.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // NoAdvancedScanners
            // 
            this.NoAdvancedScanners.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.NoAdvancedScanners.Location = new System.Drawing.Point(211, 96);
            this.NoAdvancedScanners.Name = "NoAdvancedScanners";
            this.NoAdvancedScanners.Size = new System.Drawing.Size(133, 24);
            this.NoAdvancedScanners.TabIndex = 10;
            this.NoAdvancedScanners.Tag = "NAS";
            this.NoAdvancedScanners.Text = "No advanced scanners";
            this.NoAdvancedScanners.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // BasicRemoteMining
            // 
            this.BasicRemoteMining.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BasicRemoteMining.Location = new System.Drawing.Point(211, 72);
            this.BasicRemoteMining.Name = "BasicRemoteMining";
            this.BasicRemoteMining.Size = new System.Drawing.Size(141, 24);
            this.BasicRemoteMining.TabIndex = 9;
            this.BasicRemoteMining.Tag = "OBRM";
            this.BasicRemoteMining.Text = "Only basic remote mining";
            this.BasicRemoteMining.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // CheapEngines
            // 
            this.CheapEngines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheapEngines.Location = new System.Drawing.Point(211, 48);
            this.CheapEngines.Name = "CheapEngines";
            this.CheapEngines.Size = new System.Drawing.Size(131, 24);
            this.CheapEngines.TabIndex = 8;
            this.CheapEngines.Tag = "CE";
            this.CheapEngines.Text = "Cheap engines";
            this.CheapEngines.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // NoRAMEngines
            // 
            this.NoRAMEngines.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.NoRAMEngines.Location = new System.Drawing.Point(211, 24);
            this.NoRAMEngines.Name = "NoRAMEngines";
            this.NoRAMEngines.Size = new System.Drawing.Size(134, 24);
            this.NoRAMEngines.TabIndex = 7;
            this.NoRAMEngines.Tag = "NRS";
            this.NoRAMEngines.Text = "No RAM scoop engines";
            this.NoRAMEngines.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // MineralAlchemy
            // 
            this.MineralAlchemy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MineralAlchemy.Location = new System.Drawing.Point(16, 168);
            this.MineralAlchemy.Name = "MineralAlchemy";
            this.MineralAlchemy.Size = new System.Drawing.Size(152, 24);
            this.MineralAlchemy.TabIndex = 6;
            this.MineralAlchemy.Tag = "MA";
            this.MineralAlchemy.Text = "Mineral alchemy";
            this.MineralAlchemy.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // UltimateRecycling
            // 
            this.UltimateRecycling.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UltimateRecycling.Location = new System.Drawing.Point(16, 144);
            this.UltimateRecycling.Name = "UltimateRecycling";
            this.UltimateRecycling.Size = new System.Drawing.Size(152, 24);
            this.UltimateRecycling.TabIndex = 5;
            this.UltimateRecycling.Tag = "UR";
            this.UltimateRecycling.Text = "Ultimate recycling";
            this.UltimateRecycling.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // GeneralisedResearch
            // 
            this.GeneralisedResearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GeneralisedResearch.Location = new System.Drawing.Point(16, 120);
            this.GeneralisedResearch.Name = "GeneralisedResearch";
            this.GeneralisedResearch.Size = new System.Drawing.Size(152, 24);
            this.GeneralisedResearch.TabIndex = 4;
            this.GeneralisedResearch.Tag = "GR";
            this.GeneralisedResearch.Text = "Generalised reseach";
            this.GeneralisedResearch.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // ImprovedStarbases
            // 
            this.ImprovedStarbases.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ImprovedStarbases.Location = new System.Drawing.Point(16, 96);
            this.ImprovedStarbases.Name = "ImprovedStarbases";
            this.ImprovedStarbases.Size = new System.Drawing.Size(152, 24);
            this.ImprovedStarbases.TabIndex = 3;
            this.ImprovedStarbases.Tag = "ISB";
            this.ImprovedStarbases.Text = "Improved starbases";
            this.ImprovedStarbases.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // AdvancedRemoteMining
            // 
            this.AdvancedRemoteMining.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AdvancedRemoteMining.Location = new System.Drawing.Point(16, 72);
            this.AdvancedRemoteMining.Name = "AdvancedRemoteMining";
            this.AdvancedRemoteMining.Size = new System.Drawing.Size(152, 24);
            this.AdvancedRemoteMining.TabIndex = 2;
            this.AdvancedRemoteMining.Tag = "ARM";
            this.AdvancedRemoteMining.Text = "Advanced remote mining";
            this.AdvancedRemoteMining.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // TotalTerraforming
            // 
            this.TotalTerraforming.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.TotalTerraforming.Location = new System.Drawing.Point(16, 48);
            this.TotalTerraforming.Name = "TotalTerraforming";
            this.TotalTerraforming.Size = new System.Drawing.Size(152, 24);
            this.TotalTerraforming.TabIndex = 1;
            this.TotalTerraforming.Tag = "TT";
            this.TotalTerraforming.Text = "Total terraforming";
            this.TotalTerraforming.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // ImprovedFuelEfficiency
            // 
            this.ImprovedFuelEfficiency.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ImprovedFuelEfficiency.Location = new System.Drawing.Point(16, 24);
            this.ImprovedFuelEfficiency.Name = "ImprovedFuelEfficiency";
            this.ImprovedFuelEfficiency.Size = new System.Drawing.Size(152, 24);
            this.ImprovedFuelEfficiency.TabIndex = 0;
            this.ImprovedFuelEfficiency.Tag = "IFE";
            this.ImprovedFuelEfficiency.Text = "Improved fuel efficiency";
            this.ImprovedFuelEfficiency.CheckedChanged += new System.EventHandler(this.SecondaryTraits_CheckedChanged);
            // 
            // ProductionTab
            // 
            this.ProductionTab.Controls.Add(this.groupBox12);
            this.ProductionTab.Controls.Add(this.groupBox11);
            this.ProductionTab.Controls.Add(this.groupBox10);
            this.ProductionTab.Controls.Add(this.groupBox8);
            this.ProductionTab.Location = new System.Drawing.Point(4, 22);
            this.ProductionTab.Name = "ProductionTab";
            this.ProductionTab.Size = new System.Drawing.Size(392, 392);
            this.ProductionTab.TabIndex = 2;
            this.ProductionTab.Text = "Production";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label9);
            this.groupBox12.Controls.Add(this.MineralProduction);
            this.groupBox12.Controls.Add(this.OperableMines);
            this.groupBox12.Controls.Add(this.label10);
            this.groupBox12.Controls.Add(this.ResourcesPerMine);
            this.groupBox12.Controls.Add(this.label11);
            this.groupBox12.Location = new System.Drawing.Point(11, 161);
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
            this.MineralProduction.Location = new System.Drawing.Point(292, 16);
            this.MineralProduction.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.MineralProduction.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MineralProduction.Name = "MineralProduction";
            this.MineralProduction.Size = new System.Drawing.Size(56, 20);
            this.MineralProduction.TabIndex = 10;
            this.MineralProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MineralProduction.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MineralProduction.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // OperableMines
            // 
            this.OperableMines.Location = new System.Drawing.Point(292, 41);
            this.OperableMines.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.OperableMines.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.OperableMines.Name = "OperableMines";
            this.OperableMines.Size = new System.Drawing.Size(56, 20);
            this.OperableMines.TabIndex = 14;
            this.OperableMines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OperableMines.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.OperableMines.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
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
            this.ResourcesPerMine.Location = new System.Drawing.Point(293, 66);
            this.ResourcesPerMine.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.ResourcesPerMine.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ResourcesPerMine.Name = "ResourcesPerMine";
            this.ResourcesPerMine.Size = new System.Drawing.Size(56, 20);
            this.ResourcesPerMine.TabIndex = 12;
            this.ResourcesPerMine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ResourcesPerMine.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ResourcesPerMine.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
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
            this.groupBox11.Controls.Add(this.label8);
            this.groupBox11.Controls.Add(this.label7);
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Controls.Add(this.ResourceProduction);
            this.groupBox11.Controls.Add(this.FactoryBuildCost);
            this.groupBox11.Controls.Add(this.OperableFactories);
            this.groupBox11.Location = new System.Drawing.Point(11, 60);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(363, 95);
            this.groupBox11.TabIndex = 15;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Factories";
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
            this.ResourceProduction.Location = new System.Drawing.Point(292, 19);
            this.ResourceProduction.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.ResourceProduction.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ResourceProduction.Name = "ResourceProduction";
            this.ResourceProduction.Size = new System.Drawing.Size(56, 20);
            this.ResourceProduction.TabIndex = 3;
            this.ResourceProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ResourceProduction.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ResourceProduction.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // FactoryBuildCost
            // 
            this.FactoryBuildCost.Location = new System.Drawing.Point(292, 69);
            this.FactoryBuildCost.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.FactoryBuildCost.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FactoryBuildCost.Name = "FactoryBuildCost";
            this.FactoryBuildCost.Size = new System.Drawing.Size(56, 20);
            this.FactoryBuildCost.TabIndex = 5;
            this.FactoryBuildCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FactoryBuildCost.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.FactoryBuildCost.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // OperableFactories
            // 
            this.OperableFactories.Location = new System.Drawing.Point(292, 44);
            this.OperableFactories.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.OperableFactories.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.OperableFactories.Name = "OperableFactories";
            this.OperableFactories.Size = new System.Drawing.Size(56, 20);
            this.OperableFactories.TabIndex = 8;
            this.OperableFactories.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OperableFactories.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.OperableFactories.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.UnusedPointsTarget);
            this.groupBox10.Location = new System.Drawing.Point(11, 262);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(363, 58);
            this.groupBox10.TabIndex = 1;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Spend Unused Allocation Points On:";
            // 
            // UnusedPointsTarget
            // 
            this.UnusedPointsTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnusedPointsTarget.Items.AddRange(new object[] {
            "Minerals",
            "Mines",
            "Resources"});
            this.UnusedPointsTarget.Location = new System.Drawing.Point(14, 21);
            this.UnusedPointsTarget.Name = "UnusedPointsTarget";
            this.UnusedPointsTarget.Size = new System.Drawing.Size(323, 21);
            this.UnusedPointsTarget.Sorted = true;
            this.UnusedPointsTarget.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.ColonistProduction);
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
            this.ColonistProduction.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ColonistProduction.Location = new System.Drawing.Point(292, 18);
            this.ColonistProduction.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.ColonistProduction.Minimum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.ColonistProduction.Name = "ColonistProduction";
            this.ColonistProduction.Size = new System.Drawing.Size(56, 20);
            this.ColonistProduction.TabIndex = 1;
            this.ColonistProduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColonistProduction.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ColonistProduction.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(277, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Colonists required to generate one resource each year";
            // 
            // EnvironmentTab
            // 
            this.EnvironmentTab.Controls.Add(this.groupBox9);
            this.EnvironmentTab.Location = new System.Drawing.Point(4, 22);
            this.EnvironmentTab.Name = "EnvironmentTab";
            this.EnvironmentTab.Size = new System.Drawing.Size(392, 392);
            this.EnvironmentTab.TabIndex = 4;
            this.EnvironmentTab.Text = "Environment";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.MaxGrowth);
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.RadiationTolerance);
            this.groupBox9.Controls.Add(this.TemperatureTolerance);
            this.groupBox9.Controls.Add(this.GravityTolerance);
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
            this.MaxGrowth.Location = new System.Drawing.Point(235, 320);
            this.MaxGrowth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.MaxGrowth.Name = "MaxGrowth";
            this.MaxGrowth.Size = new System.Drawing.Size(94, 20);
            this.MaxGrowth.TabIndex = 4;
            this.MaxGrowth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaxGrowth.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.MaxGrowth.ValueChanged += new System.EventHandler(this.UpDown_ValueChanged);
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
            // ResearchTab
            // 
            this.ResearchTab.Controls.Add(this.BiotechnologyResearch);
            this.ResearchTab.Controls.Add(this.ElectronicsResearch);
            this.ResearchTab.Controls.Add(this.ConstructionResearch);
            this.ResearchTab.Controls.Add(this.PropulsionResearch);
            this.ResearchTab.Controls.Add(this.WeaponsResearch);
            this.ResearchTab.Controls.Add(this.EnergyResearch);
            this.ResearchTab.Location = new System.Drawing.Point(4, 22);
            this.ResearchTab.Name = "ResearchTab";
            this.ResearchTab.Size = new System.Drawing.Size(392, 392);
            this.ResearchTab.TabIndex = 3;
            this.ResearchTab.Text = "Research";
            // 
            // Finish
            // 
            this.Finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Finish.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Finish.Location = new System.Drawing.Point(20, 501);
            this.Finish.Name = "Finish";
            this.Finish.Size = new System.Drawing.Size(80, 24);
            this.Finish.TabIndex = 2;
            this.Finish.Text = "Generate";
            this.Finish.Click += new System.EventHandler(this.Finish_Click);
            // 
            // Exit
            // 
            this.Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Exit.Location = new System.Drawing.Point(332, 499);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(80, 26);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(426, 24);
            this.MainMenu.TabIndex = 5;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.LoadRaceFile});
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
            this.LoadRaceFile.Name = "LoadRaceFile";
            this.LoadRaceFile.Size = new System.Drawing.Size(149, 22);
            this.LoadRaceFile.Text = "Load Race File";
            this.LoadRaceFile.Click += new System.EventHandler(this.LoadRaceFile_Click);
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
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog";
            this.OpenFileDialog.Filter = "Race Definition File | *.race";
            this.OpenFileDialog.Title = "Nova - Specify Race Definition File";
            // 
            // RadiationTolerance
            // 
            this.RadiationTolerance.Location = new System.Drawing.Point(14, 220);
            this.RadiationTolerance.Name = "RadiationTolerance";
            this.RadiationTolerance.RangeBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.RadiationTolerance.RangeMaximum = 100;
            this.RadiationTolerance.RangeMinimum = 0;
            this.RadiationTolerance.BarLower = 15;
            this.RadiationTolerance.BarUpper = 85;
            this.RadiationTolerance.EnvironmentValues = new NovaCommon.EnvironmentTolerance(15, 85);
            this.RadiationTolerance.RangeTitle = "Radiation";
            this.RadiationTolerance.RangeUnits = "mR";
            this.RadiationTolerance.Size = new System.Drawing.Size(324, 95);
            this.RadiationTolerance.TabIndex = 2;
            this.RadiationTolerance.RangeChanged += new ControlLibrary.Range.RangeChangedHandler(this.Tolerance_RangeChanged);
            // 
            // TemperatureTolerance
            // 
            this.TemperatureTolerance.Location = new System.Drawing.Point(14, 118);
            this.TemperatureTolerance.Name = "TemperatureTolerance";
            this.TemperatureTolerance.RangeBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TemperatureTolerance.RangeMaximum = 200;
            this.TemperatureTolerance.RangeMinimum = -200;
            this.TemperatureTolerance.BarLower = -140;
            this.TemperatureTolerance.BarUpper = 140;
            this.TemperatureTolerance.EnvironmentValues = new NovaCommon.EnvironmentTolerance(-140, 140);
            this.TemperatureTolerance.RangeTitle = "Temperature";
            this.TemperatureTolerance.RangeUnits = "C";
            this.TemperatureTolerance.Size = new System.Drawing.Size(324, 95);
            this.TemperatureTolerance.TabIndex = 1;
            this.TemperatureTolerance.RangeChanged += new ControlLibrary.Range.RangeChangedHandler(this.Tolerance_RangeChanged);
            // 
            // GravityTolerance
            // 
            this.GravityTolerance.Location = new System.Drawing.Point(14, 19);
            this.GravityTolerance.Name = "GravityTolerance";
            this.GravityTolerance.RangeBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.GravityTolerance.RangeMaximum = 10;
            this.GravityTolerance.RangeMinimum = 0;
            this.GravityTolerance.BarLower = 1.5;
            this.GravityTolerance.BarUpper = 8.5;
            this.GravityTolerance.EnvironmentValues = new NovaCommon.EnvironmentTolerance(1.5, 8.5);
            this.GravityTolerance.RangeTitle = "Gravity";
            this.GravityTolerance.RangeUnits = "g";
            this.GravityTolerance.Size = new System.Drawing.Size(324, 95);
            this.GravityTolerance.TabIndex = 0;
            this.GravityTolerance.RangeChanged += new ControlLibrary.Range.RangeChangedHandler(this.Tolerance_RangeChanged);
            // 
            // BiotechnologyResearch
            // 
            this.BiotechnologyResearch.Cost = 100;
            this.BiotechnologyResearch.Location = new System.Drawing.Point(193, 259);
            this.BiotechnologyResearch.Name = "BiotechnologyResearch";
            this.BiotechnologyResearch.Size = new System.Drawing.Size(200, 128);
            this.BiotechnologyResearch.TabIndex = 5;
            this.BiotechnologyResearch.Title = "Biotechnology Research";
            this.BiotechnologyResearch.SelectionChanged += new ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // ElectronicsResearch
            // 
            this.ElectronicsResearch.Cost = 100;
            this.ElectronicsResearch.Location = new System.Drawing.Point(193, 133);
            this.ElectronicsResearch.Name = "ElectronicsResearch";
            this.ElectronicsResearch.Size = new System.Drawing.Size(200, 128);
            this.ElectronicsResearch.TabIndex = 4;
            this.ElectronicsResearch.Title = "Electronics Research";
            this.ElectronicsResearch.SelectionChanged += new ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // ConstructionResearch
            // 
            this.ConstructionResearch.Cost = 100;
            this.ConstructionResearch.Location = new System.Drawing.Point(193, 9);
            this.ConstructionResearch.Name = "ConstructionResearch";
            this.ConstructionResearch.Size = new System.Drawing.Size(200, 128);
            this.ConstructionResearch.TabIndex = 3;
            this.ConstructionResearch.Title = "Construction Research";
            this.ConstructionResearch.SelectionChanged += new ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // PropulsionResearch
            // 
            this.PropulsionResearch.Cost = 100;
            this.PropulsionResearch.Location = new System.Drawing.Point(3, 259);
            this.PropulsionResearch.Name = "PropulsionResearch";
            this.PropulsionResearch.Size = new System.Drawing.Size(200, 128);
            this.PropulsionResearch.TabIndex = 2;
            this.PropulsionResearch.Title = "Propulsion Research";
            this.PropulsionResearch.SelectionChanged += new ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // WeaponsResearch
            // 
            this.WeaponsResearch.Cost = 100;
            this.WeaponsResearch.Location = new System.Drawing.Point(3, 133);
            this.WeaponsResearch.Name = "WeaponsResearch";
            this.WeaponsResearch.Size = new System.Drawing.Size(200, 128);
            this.WeaponsResearch.TabIndex = 1;
            this.WeaponsResearch.Title = "Weapons Research";
            this.WeaponsResearch.SelectionChanged += new ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // EnergyResearch
            // 
            this.EnergyResearch.Cost = 100;
            this.EnergyResearch.Location = new System.Drawing.Point(3, 9);
            this.EnergyResearch.Name = "EnergyResearch";
            this.EnergyResearch.Size = new System.Drawing.Size(200, 128);
            this.EnergyResearch.TabIndex = 0;
            this.EnergyResearch.Title = "Energy Research";
            this.EnergyResearch.SelectionChanged += new ControlLibrary.ResearchCost.SelectionChangedHandler(this.ResearchCost_SelectionChanged);
            // 
            // RaceDesignerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(426, 539);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.TabConrol);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Finish);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(432, 544);
            this.Name = "RaceDesignerForm";
            this.Text = "Nova Race Designer";
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.TabConrol.ResumeLayout(false);
            this.RaceTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.TraitsTab.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ProductionTab.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MineralProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperableMines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResourcesPerMine)).EndInit();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResourceProduction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FactoryBuildCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OperableFactories)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColonistProduction)).EndInit();
            this.EnvironmentTab.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxGrowth)).EndInit();
            this.ResearchTab.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

         }
#endregion

         /// <summary>
         /// The main entry point for the application.
         /// </summary>
         [STAThread]
         static void Main()
         {
             Application.EnableVisualStyles();
             Application.Run(new RaceDesignerForm());
         }


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
         private void radioButton_CheckedChanged(object sender, EventArgs e)
         {
             RadioButton radioButton = (RadioButton)sender;

             foreach (DictionaryEntry dict in AllTraits.Data.Primary)
             {
                 TraitEntry trait = (TraitEntry)dict.Value;
                 if (trait.Code == radioButton.Tag.ToString())
                 {
                     if (radioButton.Checked)
                     {
                         SelectedRace = AllTraits.Data.Primary[radioButton.Tag.ToString()];
                         AdvantagePoints -= trait.Cost;
                         PrimaryTraitDescription.Text = trait.Description;
                     }
                     else
                     {
                         AdvantagePoints += trait.Cost;
                     }

                     ParametersChanged = true;
                     AvailablePoints.Text = AdvantagePoints.ToString(System.Globalization.CultureInfo.InvariantCulture);
                     break;
                 }
             }
         }


         /// <summary>
         /// This funtion is invoked whenever a secondary racial traits radio button is
         /// selected. Based on the value in the Name property, the description and
         /// advantage point value will be adjusted to suit the selected race (in a
         /// similar manner is as done for the primary racial trait above). 
         ///
         /// All secondary racial traits are defined in the file SecondaryTraits.cs.
         /// </summary>

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
                         AdvantagePoints -= trait.Cost;
                         SecondaryTraitDescription.Text = trait.Description;
                     }
                     else
                     {
                         AdvantagePoints += trait.Cost;
                     }

                     AvailablePoints.Text = AdvantagePoints.ToString(System.Globalization.CultureInfo.InvariantCulture);
                     ParametersChanged = true;
                     break;
                 }
             }
         }


         /// <summary>
         /// Called when an up/down counter has changed. Put back the previous advantage
         /// point value taken and now use the newly selected value. 
         /// 
         /// Explicitly check for ColonistProduction as array indeces need dividing by
         /// 100 to match its units. 
         ///
         /// Note that element zero of each entry in the parameter definition array is
         /// used to hold the previous valueof the up-down counter (so that we can "pay
         /// back" advantage points on a parameter change)
         ///
         /// The actual advantage point costs are defined in the file ParameterCosts.cs.
         /// </summary>
         private void UpDown_ValueChanged(object sender, EventArgs e)
         {
             NumericUpDown upDown = (NumericUpDown)sender;

             foreach (ParameterEntry parameter in ParameterCosts.Parameters)
             {
                 if (parameter.ParameterName == upDown.Name)
                 {
                     int newValue = (int)upDown.Value;
                     int oldValue = parameter.Cost[0];

                     if (upDown.Name == "ColonistProduction")
                     {
                         AdvantagePoints += parameter.Cost[oldValue / 100];
                         AdvantagePoints -= parameter.Cost[newValue / 100];
                     }
                     else
                     {
                         AdvantagePoints += parameter.Cost[oldValue];
                         AdvantagePoints -= parameter.Cost[newValue];
                     }

                     parameter.Cost[0] = newValue;
                     AvailablePoints.Text = AdvantagePoints.ToString(System.Globalization.CultureInfo.InvariantCulture);
                     ParametersChanged = true;
                     break;
                 }
             }
         }//UpDown_ValueChanged


         /// <summary>
         /// Called when a research cost has changed. Note that the ResearchCost control
         /// retuns the appropriate advantage points adjustment for both the button going
         /// off and for the new selection.
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="value"></param>
         private void ResearchCost_SelectionChanged(object sender, int value)
         {
             AdvantagePoints += value;
             AvailablePoints.Text = AdvantagePoints.ToString(System.Globalization.CultureInfo.InvariantCulture);
             ParametersChanged = true;
         }

         /// <summary>
         /// Called when a tolerance range has changed. The new width of the range and
         /// its position (based on the middle of the bar) both impact the advantage
         /// point value.
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="newLeftPos"></param>
         /// <param name="newRightPos"></param>
         /// <param name="oldLeftPos"></param>
         /// <param name="oldRightPos"></param>
         private void Tolerance_RangeChanged(object sender,
                                             int newLeftPos,
                                             int newRightPos,
                                             int oldLeftPos,
                                             int oldRightPos)
         {

             AdvantagePoints -= Utilities.BarWidthCost(oldLeftPos, oldRightPos);
             AdvantagePoints += Utilities.BarWidthCost(newLeftPos, newRightPos);

             AdvantagePoints -= Utilities.BarPositionCost(oldLeftPos, oldRightPos);
             AdvantagePoints += Utilities.BarPositionCost(newLeftPos, newRightPos);

             AvailablePoints.Text = AdvantagePoints.ToString(System.Globalization.CultureInfo.InvariantCulture);
             ParametersChanged = true;
         }


         /// <summary>
         /// This function is called when the Exit button is pressed. Provide a warning
         /// that this will discard the race definition and see if he really wants
         /// to do this.
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void Exit_Click(object sender, EventArgs e)
         {
             if (ParametersChanged)
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


         /// <summary>
         /// This function is called when the Generate button is pressed. Providing that
         /// the advantage points total is not negative we then populate the race details
         /// structure and use it to generate the race definition file.
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void Finish_Click(object sender, System.EventArgs e)
         {
             if (AdvantagePoints < 0)
             {
                 Report.Error("You are not allowed to generate a race file" +
                              "when you have less than zero Advantage Points");
                 return;
             }

             // ----------------------------------------------------------------------------
             // Primary Racial Traits
             // ----------------------------------------------------------------------------

             if (RaceName.Text == "" || PluralRaceName.Text == "")
             {
                 Report.Error("The race name canonot be blank when generating" +
                              "a new race file - nor can the plural name");
                 return;
             }

             if (Password.Text == "")
             {
                 Report.Error("The password field cannot be blank when " +
                              "generating a new race file. You will need it to " +
                              "access your turn during a game. Note, passwords " +
                              "are case-sensitive.");
                 return;
             }
             
             Race            RaceParameters    = new Race();
             RaceParameters.Traits.SetPrimary(SelectedRace);
             RaceParameters.Name = RaceName.Text;
             RaceParameters.PluralName = PluralRaceName.Text;

             string passwordHash = FormsAuthentication.
                HashPasswordForStoringInConfigFile(Password.Text, "MD5");

             RaceParameters.Password = passwordHash;
             RaceParameters.Icon = CurrentRaceIcon;

             // ----------------------------------------------------------------------------
             // Secondary Racial Traits
             // ----------------------------------------------------------------------------
             if (ImprovedFuelEfficiency.Checked) RaceParameters.Traits.Add("IFE");
             if (NoRAMEngines.Checked) RaceParameters.Traits.Add("NRS");
             if (TotalTerraforming.Checked) RaceParameters.Traits.Add("TT");
             if (CheapEngines.Checked) RaceParameters.Traits.Add("CE");
             if (AdvancedRemoteMining.Checked) RaceParameters.Traits.Add("ARM");
             if (BasicRemoteMining.Checked) RaceParameters.Traits.Add("OBRM");
             if (ImprovedStarbases.Checked) RaceParameters.Traits.Add("ISB");
             if (NoAdvancedScanners.Checked) RaceParameters.Traits.Add("NAS");
             if (GeneralisedResearch.Checked) RaceParameters.Traits.Add("GR");
             if (LowStartingPopulation.Checked) RaceParameters.Traits.Add("LSP");
             if (UltimateRecycling.Checked) RaceParameters.Traits.Add("UR");
             if (BleedingEdgeTechnology.Checked) RaceParameters.Traits.Add("BET");
             if (MineralAlchemy.Checked) RaceParameters.Traits.Add("MA");
             if (RegeneratingShields.Checked) RaceParameters.Traits.Add("RS");
             if (CheapFactories.Checked) RaceParameters.Traits.Add("CF");

             // ----------------------------------------------------------------------------
             // Production Costs and Rates
             // ----------------------------------------------------------------------------

             RaceParameters.ColonistsPerResource = (double)ColonistProduction.Value;
             RaceParameters.OperableFactories = (double)OperableFactories.Value;
             RaceParameters.MineProductionRate = (double)MineralProduction.Value;
             RaceParameters.OperableMines = (double)OperableMines.Value;
             RaceParameters.FactoryBuildCost = (int)FactoryBuildCost.Value;
             RaceParameters.MineBuildCost = (int)ResourcesPerMine.Value;
             RaceParameters.FactoryProduction = (double)ResourceProduction.Value;

             // ----------------------------------------------------------------------------
             // Environmental Tolerance
             // ----------------------------------------------------------------------------

             RaceParameters.GravityTolerance = GravityTolerance.EnvironmentValues;
             RaceParameters.RadiationTolerance = RadiationTolerance.EnvironmentValues;
             RaceParameters.TemperatureTolerance = TemperatureTolerance.EnvironmentValues;
             RaceParameters.GrowthRate = (double)MaxGrowth.Value;

             // ----------------------------------------------------------------------------
             // Research Costs
             // ----------------------------------------------------------------------------

             RaceParameters.ResearchCosts[TechLevel.ResearchField.Energy] = EnergyResearch.Cost;
             RaceParameters.ResearchCosts[TechLevel.ResearchField.Weapons] = WeaponsResearch.Cost;
             RaceParameters.ResearchCosts[TechLevel.ResearchField.Propulsion] = PropulsionResearch.Cost;
             RaceParameters.ResearchCosts[TechLevel.ResearchField.Construction] = ConstructionResearch.Cost;
             RaceParameters.ResearchCosts[TechLevel.ResearchField.Electronics] = ElectronicsResearch.Cost;
             RaceParameters.ResearchCosts[TechLevel.ResearchField.Biotechnology] = BiotechnologyResearch.Cost;

             // ----------------------------------------------------------------------------
             // Generate the race definition file
             //
             // Note if the same folder on the same machine is chosen for all Nova GUI and
             // Nova Console files it is possible to play a game with multiple races using
             // the same log-in name. This is a useful debugging aid.
             // ----------------------------------------------------------------------------
             
             try
             {

                 String RaceFilePath = FileSearcher.GetFolder(Global.RaceFolderKey, Global.RaceFolderName);

                 SaveFileDialog fd = new SaveFileDialog();
                 fd.Title = "Save Race - " + RaceParameters.Name;
                 fd.FileName = RaceParameters.Name + ".race";
                 fd.InitialDirectory = RaceFilePath;
                 DialogResult result = fd.ShowDialog();
                 if( result == DialogResult.OK )
                 {
                     RaceFilePath = fd.FileName;
                 }
                 else
                 {
                     Report.Error( "Race has not been saved." );
                     return;
                 }

                 FileStream saveFile = new FileStream(RaceFilePath, FileMode.Create);

                 // Setup the XML document
                 XmlDocument xmldoc = new XmlDocument();
                 XmlElement xmlRoot = Global.InitializeXmlDocument( xmldoc );

                 // add the components to the document
                 xmldoc.ChildNodes.Item( 1 ).AppendChild( RaceParameters.ToXml( xmldoc ) );

                 xmldoc.Save( saveFile );
                 saveFile.Close();

                 Report.Information("The " + RaceParameters.PluralName + " have been saved to " + RaceFilePath);

                 FileSearcher.SetNovaRegistryValue(Global.RaceFolderKey, System.IO.Path.GetDirectoryName(RaceFilePath));

                 // Remove the warning message for exiting with unsaved changes.
                 ParametersChanged = false;

                 return;
             }
             catch( System.IO.FileNotFoundException )
             {

                 Report.Error( "File path not specified." );
                 return;

             }
             catch( Exception exception )
             {
                 Report.Error( "Failed to save race file. " + exception.Message );
                 return;
             }

         }//Finish_Click (save&close the race)


         /// <summary>
         /// Display the about box
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
         {
             AboutBox about = new AboutBox();
             about.ShowDialog();
             about.Dispose();
         }


         /// <summary>
         /// Called when the next image is to be selected
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void NextImage_Click(object sender, EventArgs e)
         {
             ++CurrentRaceIcon;
             PictureBox.Image = CurrentRaceIcon.Image;
             IconIndex.Text = Path.GetFileNameWithoutExtension(CurrentRaceIcon.Source);

         }


         /// <summary>
         /// Called when the previous image is to be selected
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void PreviousImage_Click(object sender, EventArgs e)
         {
             --CurrentRaceIcon;
             PictureBox.Image = CurrentRaceIcon.Image;
             IconIndex.Text = Path.GetFileNameWithoutExtension(CurrentRaceIcon.Source);
         }






         /// <summary>
         /// Load an existing race file for examination or modification (however, once
         /// submitted there is no point in changing the race definition. It's too late
         /// and it's "gone into the system").
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void LoadRaceFile_Click(object sender, EventArgs e)
         {


             OpenFileDialog.CheckFileExists = true;
             OpenFileDialog.FileName = "Humanoid.race";
             if (OpenFileDialog.ShowDialog() != DialogResult.OK)
             {
                 return;
             }

             string fileName = OpenFileDialog.FileName;

             Race RaceParameters;
             try
             {
                 RaceParameters = new Race(fileName);

                 // TODO (priority 4) - This level of security is not good enough as the race is stored un-encrypted.
                 ControlLibrary.CheckPassword password =
                    new ControlLibrary.CheckPassword(RaceParameters);

                 password.ShowDialog();
                 if (password.DialogResult == DialogResult.OK)
                 {
                     reloadRace(RaceParameters);
                     reloadSecondaryTraits(RaceParameters);
                     reloadBuildCosts(RaceParameters);
                     reloadEnvironmentalTolerance(RaceParameters);
                     reloadResearchCosts(RaceParameters);
                 }

                 password.Dispose();
             }
             catch (Exception ex)
             {
                 Report.Error("Failed to load file: \r\n" + ex.Message);
             }
         }//LoadRaceFile_Click


         /// <summary>
         /// Brute force and ignorance reload of a file. There must be a bettter way of
         /// doing this.
         /// </summary>
         void reloadRace(Race RaceParameters)
         {

             SelectedRace = RaceParameters.Traits.Primary;
             RaceName.Text = RaceParameters.Name;
             PluralRaceName.Text = RaceParameters.PluralName;

             switch (SelectedRace.Code)
             {
                 case "HE":
                     HyperExpansion.Checked = true;
                     break;
                 case "SS":
                     SuperStealth.Checked = true;
                     break;
                 case "WM":
                     WarMonger.Checked = true;
                     break;
                 case "CA":
                     ClaimAdjuster.Checked = true;
                     break;
                 case "IS":
                     InnerStrength.Checked = true;
                     break;
                 case "SD":
                     SpaceDemolition.Checked = true;
                     break;
                 case "PP":
                     PacketPhysics.Checked = true;
                     break;
                 case "IT":
                     InterStellarTraveller.Checked = true;
                     break;
                 case "AR":
                     AlternateReality.Checked = true;
                     break;
                 case "JOAT":
                     JackOfAllTrades.Checked = true;
                     break;
             }
         }

         /// <summary>
         /// Reload seconday traits
         /// </summary>
         void reloadSecondaryTraits(Race RaceParameters)
         {
             ImprovedFuelEfficiency.Checked  = RaceParameters.Traits.Contains("IFE");
             NoRAMEngines.Checked            = RaceParameters.Traits.Contains("NRS");
             TotalTerraforming.Checked       = RaceParameters.Traits.Contains("TT");
             CheapEngines.Checked            = RaceParameters.Traits.Contains("CE");
             AdvancedRemoteMining.Checked    = RaceParameters.Traits.Contains("ARM");
             BasicRemoteMining.Checked       = RaceParameters.Traits.Contains("OBRM");
             ImprovedStarbases.Checked       = RaceParameters.Traits.Contains("ISB");
             NoAdvancedScanners.Checked      = RaceParameters.Traits.Contains("NAS");
             GeneralisedResearch.Checked     = RaceParameters.Traits.Contains("GR");
             LowStartingPopulation.Checked   = RaceParameters.Traits.Contains("LSP");
             UltimateRecycling.Checked       = RaceParameters.Traits.Contains("UR");
             BleedingEdgeTechnology.Checked  = RaceParameters.Traits.Contains("BET");
             MineralAlchemy.Checked          = RaceParameters.Traits.Contains("MA");
             RegeneratingShields.Checked     = RaceParameters.Traits.Contains("RS");
             CheapFactories.Checked          = RaceParameters.Traits.Contains("CF");
         }


         /// <summary>
         /// Reload build cost parameters
         /// </summary>
         void reloadBuildCosts(Race RaceParameters)
         {
             ColonistProduction.Value  = (decimal)RaceParameters.ColonistsPerResource;
             OperableFactories.Value   = (decimal)RaceParameters.OperableFactories;
             MineralProduction.Value   = (decimal)RaceParameters.MineProductionRate;
             OperableMines.Value       = (decimal)RaceParameters.OperableMines;
             FactoryBuildCost.Value    = (decimal)RaceParameters.FactoryBuildCost;
             ResourcesPerMine.Value    = (decimal)RaceParameters.MineBuildCost;
             ResourceProduction.Value  = (decimal)RaceParameters.FactoryProduction;
         }


         /// <summary>
         /// Reload Environmental Tolerance
         /// </summary>
         void reloadEnvironmentalTolerance(Race RaceParameters)
         {

             GravityTolerance.EnvironmentValues = RaceParameters.GravityTolerance;
             RadiationTolerance.EnvironmentValues = RaceParameters.RadiationTolerance;
             TemperatureTolerance.EnvironmentValues = RaceParameters.TemperatureTolerance;
             MaxGrowth.Value              = (decimal)RaceParameters.GrowthRate;
         }


         /// <summary>
         /// Reload Research Costs
         /// </summary>
         void reloadResearchCosts(Race RaceParameters)
         {
             EnergyResearch.Cost = (int)RaceParameters.ResearchCosts[TechLevel.ResearchField.Energy];
             WeaponsResearch.Cost = (int)RaceParameters.ResearchCosts[TechLevel.ResearchField.Weapons];
             PropulsionResearch.Cost = (int)RaceParameters.ResearchCosts[TechLevel.ResearchField.Propulsion];
             ConstructionResearch.Cost = (int)RaceParameters.ResearchCosts[TechLevel.ResearchField.Construction];
             ElectronicsResearch.Cost = (int)RaceParameters.ResearchCosts[TechLevel.ResearchField.Electronics];
             BiotechnologyResearch.Cost = (int)RaceParameters.ResearchCosts[TechLevel.ResearchField.Biotechnology];
         }

   }//RaceDesignerForm
}//namespace RaceDesigner


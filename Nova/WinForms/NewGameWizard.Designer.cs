using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms
{
   public partial class NewGameWizard
   {
      /// <Summary>
      /// Required designer variable.
      /// </Summary>
      private System.ComponentModel.IContainer components = null;

      /// <Summary>
      /// Clean up any resources being used.
      /// </Summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <Summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </Summary>
      private void InitializeComponent()
      {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGameWizard));
          this.label2 = new System.Windows.Forms.Label();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.totalScore = new Nova.ControlLibrary.EnabledCounter();
          this.exceedSecondPlace = new Nova.ControlLibrary.EnabledCounter();
          this.highestScore = new Nova.ControlLibrary.EnabledCounter();
          this.capitalShips = new Nova.ControlLibrary.EnabledCounter();
          this.productionCapacity = new Nova.ControlLibrary.EnabledCounter();
          this.numberOfFields = new Nova.ControlLibrary.EnabledCounter();
          this.techLevels = new Nova.ControlLibrary.EnabledCounter();
          this.planetsOwned = new Nova.ControlLibrary.EnabledCounter();
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.minimumGameTime = new System.Windows.Forms.NumericUpDown();
          this.label5 = new System.Windows.Forms.Label();
          this.targetsToMeet = new System.Windows.Forms.NumericUpDown();
          this.label4 = new System.Windows.Forms.Label();
          this.cancelButton = new System.Windows.Forms.Button();
          this.tutorialButton = new System.Windows.Forms.Button();
          this.okButton = new System.Windows.Forms.Button();
          this.tabControl1 = new System.Windows.Forms.TabControl();
          this.tabGameOptions = new System.Windows.Forms.TabPage();
          this.groupBox6 = new System.Windows.Forms.GroupBox();
          this.gameName = new System.Windows.Forms.TextBox();
          this.groupBox3 = new System.Windows.Forms.GroupBox();
          this.label16 = new System.Windows.Forms.Label();
          this.label15 = new System.Windows.Forms.Label();
          this.label14 = new System.Windows.Forms.Label();
          this.label13 = new System.Windows.Forms.Label();
          this.starUniformity = new System.Windows.Forms.NumericUpDown();
          this.label12 = new System.Windows.Forms.Label();
          this.starDensity = new System.Windows.Forms.NumericUpDown();
          this.label11 = new System.Windows.Forms.Label();
          this.starSeparation = new System.Windows.Forms.NumericUpDown();
          this.label10 = new System.Windows.Forms.Label();
          this.label9 = new System.Windows.Forms.Label();
          this.numberOfStars = new System.Windows.Forms.NumericUpDown();
          this.mapWidth = new System.Windows.Forms.NumericUpDown();
          this.mapHeight = new System.Windows.Forms.NumericUpDown();
          this.label6 = new System.Windows.Forms.Label();
          this.label3 = new System.Windows.Forms.Label();
          this.tabPlayers = new System.Windows.Forms.TabPage();
          this.newRaceButton = new System.Windows.Forms.Button();
          this.groupBox5 = new System.Windows.Forms.GroupBox();
          this.playerList = new System.Windows.Forms.ListView();
          this.playerNumber = new System.Windows.Forms.ColumnHeader();
          this.race = new System.Windows.Forms.ColumnHeader();
          this.ai = new System.Windows.Forms.ColumnHeader();
          this.playerDeleteButton = new System.Windows.Forms.Button();
          this.playerDownButton = new System.Windows.Forms.Button();
          this.playerUpButton = new System.Windows.Forms.Button();
          this.addPlayerButton = new System.Windows.Forms.Button();
          this.groupBox4 = new System.Windows.Forms.GroupBox();
          this.playerNumberLabel = new System.Windows.Forms.Label();
          this.aiBrowseButton = new System.Windows.Forms.Button();
          this.raceBrowseButton = new System.Windows.Forms.Button();
          this.aiSelectionBox = new System.Windows.Forms.ComboBox();
          this.raceSelectionBox = new System.Windows.Forms.ComboBox();
          this.label8 = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.tabVictoryConditions = new System.Windows.Forms.TabPage();
          this.groupBox7 = new System.Windows.Forms.GroupBox();
          this.acceleratedStart = new System.Windows.Forms.CheckBox();
          this.groupBox1.SuspendLayout();
          this.groupBox2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.minimumGameTime)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.targetsToMeet)).BeginInit();
          this.tabControl1.SuspendLayout();
          this.tabGameOptions.SuspendLayout();
          this.groupBox6.SuspendLayout();
          this.groupBox3.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.starUniformity)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.starDensity)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.starSeparation)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.numberOfStars)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).BeginInit();
          this.tabPlayers.SuspendLayout();
          this.groupBox5.SuspendLayout();
          this.groupBox4.SuspendLayout();
          this.tabVictoryConditions.SuspendLayout();
          this.groupBox7.SuspendLayout();
          this.SuspendLayout();
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Enabled = false;
          this.label2.Location = new System.Drawing.Point(69, 57);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(0, 13);
          this.label2.TabIndex = 3;
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.totalScore);
          this.groupBox1.Controls.Add(this.exceedSecondPlace);
          this.groupBox1.Controls.Add(this.highestScore);
          this.groupBox1.Controls.Add(this.capitalShips);
          this.groupBox1.Controls.Add(this.productionCapacity);
          this.groupBox1.Controls.Add(this.numberOfFields);
          this.groupBox1.Controls.Add(this.techLevels);
          this.groupBox1.Controls.Add(this.planetsOwned);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Controls.Add(this.label2);
          this.groupBox1.Location = new System.Drawing.Point(14, 20);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(352, 285);
          this.groupBox1.TabIndex = 20;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Victory Conditions";
          // 
          // TotalScore
          // 
          this.totalScore.ControlCounter = 100;
          this.totalScore.ControlSelected = false;
          this.totalScore.ControlText = "Exceeds a score of";
          this.totalScore.Location = new System.Drawing.Point(7, 135);
          this.totalScore.Maximum = 10000;
          this.totalScore.Minimum = 0;
          this.totalScore.Name = "totalScore";
          this.totalScore.Size = new System.Drawing.Size(338, 23);
          this.totalScore.TabIndex = 11;
          this.totalScore.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // ExceedSecondPlace
          // 
          this.exceedSecondPlace.ControlCounter = 100;
          this.exceedSecondPlace.ControlSelected = false;
          this.exceedSecondPlace.ControlText = "Exceed second place score by (%)";
          this.exceedSecondPlace.Location = new System.Drawing.Point(6, 254);
          this.exceedSecondPlace.Maximum = 10000;
          this.exceedSecondPlace.Minimum = 0;
          this.exceedSecondPlace.Name = "exceedSecondPlace";
          this.exceedSecondPlace.Size = new System.Drawing.Size(339, 23);
          this.exceedSecondPlace.TabIndex = 10;
          this.exceedSecondPlace.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // HighestScore
          // 
          this.highestScore.ControlCounter = 100;
          this.highestScore.ControlSelected = false;
          this.highestScore.ControlText = "Has the highest score after (years)";
          this.highestScore.Location = new System.Drawing.Point(6, 224);
          this.highestScore.Maximum = 10000;
          this.highestScore.Minimum = 0;
          this.highestScore.Name = "highestScore";
          this.highestScore.Size = new System.Drawing.Size(339, 23);
          this.highestScore.TabIndex = 9;
          this.highestScore.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // CapitalShips
          // 
          this.capitalShips.ControlCounter = 100;
          this.capitalShips.ControlSelected = false;
          this.capitalShips.ControlText = "Number of capital ships";
          this.capitalShips.Location = new System.Drawing.Point(6, 194);
          this.capitalShips.Maximum = 10000;
          this.capitalShips.Minimum = 0;
          this.capitalShips.Name = "capitalShips";
          this.capitalShips.Size = new System.Drawing.Size(339, 23);
          this.capitalShips.TabIndex = 8;
          this.capitalShips.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // ProductionCapacity
          // 
          this.productionCapacity.ControlCounter = 100;
          this.productionCapacity.ControlSelected = false;
          this.productionCapacity.ControlText = "Has  production capacity of (in K resources)";
          this.productionCapacity.Location = new System.Drawing.Point(6, 164);
          this.productionCapacity.Maximum = 10000;
          this.productionCapacity.Minimum = 0;
          this.productionCapacity.Name = "productionCapacity";
          this.productionCapacity.Size = new System.Drawing.Size(339, 23);
          this.productionCapacity.TabIndex = 7;
          this.productionCapacity.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // NumberOfFields
          // 
          this.numberOfFields.ControlCounter = 4;
          this.numberOfFields.ControlSelected = false;
          this.numberOfFields.ControlText = "In the following number of fields";
          this.numberOfFields.Location = new System.Drawing.Point(7, 105);
          this.numberOfFields.Maximum = 6;
          this.numberOfFields.Minimum = 0;
          this.numberOfFields.Name = "numberOfFields";
          this.numberOfFields.Size = new System.Drawing.Size(339, 23);
          this.numberOfFields.TabIndex = 6;
          this.numberOfFields.Value = new Nova.Common.EnabledValue(false, 4);
          // 
          // TechLevels
          // 
          this.techLevels.ControlCounter = 22;
          this.techLevels.ControlSelected = false;
          this.techLevels.ControlText = "Attains the following tech-level";
          this.techLevels.Location = new System.Drawing.Point(6, 73);
          this.techLevels.Maximum = 10000;
          this.techLevels.Minimum = 0;
          this.techLevels.Name = "techLevels";
          this.techLevels.Size = new System.Drawing.Size(339, 23);
          this.techLevels.TabIndex = 5;
          this.techLevels.Value = new Nova.Common.EnabledValue(false, 22);
          // 
          // PlanetsOwned
          // 
          this.planetsOwned.ControlCounter = 60;
          this.planetsOwned.ControlSelected = false;
          this.planetsOwned.ControlText = "Owns the following number of planets (%)";
          this.planetsOwned.Location = new System.Drawing.Point(7, 45);
          this.planetsOwned.Maximum = 10000;
          this.planetsOwned.Minimum = 0;
          this.planetsOwned.Name = "planetsOwned";
          this.planetsOwned.Size = new System.Drawing.Size(339, 23);
          this.planetsOwned.TabIndex = 4;
          this.planetsOwned.Value = new Nova.Common.EnabledValue(false, 60);
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Enabled = false;
          this.label1.Location = new System.Drawing.Point(14, 28);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(165, 13);
          this.label1.TabIndex = 2;
          this.label1.Text = "Victory is declared when a player:";
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.minimumGameTime);
          this.groupBox2.Controls.Add(this.label5);
          this.groupBox2.Controls.Add(this.targetsToMeet);
          this.groupBox2.Controls.Add(this.label4);
          this.groupBox2.Location = new System.Drawing.Point(14, 321);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(352, 77);
          this.groupBox2.TabIndex = 24;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Main criteria";
          // 
          // MinimumGameTime
          // 
          this.minimumGameTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
          this.minimumGameTime.Location = new System.Drawing.Point(277, 47);
          this.minimumGameTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
          this.minimumGameTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
          this.minimumGameTime.Name = "minimumGameTime";
          this.minimumGameTime.Size = new System.Drawing.Size(69, 20);
          this.minimumGameTime.TabIndex = 34;
          this.minimumGameTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.minimumGameTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
          // 
          // label5
          // 
          this.label5.Location = new System.Drawing.Point(17, 47);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(195, 20);
          this.label5.TabIndex = 33;
          this.label5.Text = "Minimum game time (years)";
          this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // TargetsToMeet
          // 
          this.targetsToMeet.Anchor = System.Windows.Forms.AnchorStyles.Right;
          this.targetsToMeet.Location = new System.Drawing.Point(277, 17);
          this.targetsToMeet.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
          this.targetsToMeet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.targetsToMeet.Name = "targetsToMeet";
          this.targetsToMeet.Size = new System.Drawing.Size(69, 20);
          this.targetsToMeet.TabIndex = 32;
          this.targetsToMeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.targetsToMeet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
          // 
          // label4
          // 
          this.label4.Location = new System.Drawing.Point(17, 17);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(195, 20);
          this.label4.TabIndex = 31;
          this.label4.Text = "Number of targets to meet";
          this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // cancelButton
          // 
          this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.cancelButton.Location = new System.Drawing.Point(4, 446);
          this.cancelButton.Name = "cancelButton";
          this.cancelButton.Size = new System.Drawing.Size(75, 23);
          this.cancelButton.TabIndex = 1;
          this.cancelButton.Text = "Cancel";
          this.cancelButton.UseVisualStyleBackColor = true;
          // 
          // tutorialButton
          // 
          this.tutorialButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.tutorialButton.CausesValidation = false;
          this.tutorialButton.Location = new System.Drawing.Point(201, 446);
          this.tutorialButton.Name = "tutorialButton";
          this.tutorialButton.Size = new System.Drawing.Size(75, 23);
          this.tutorialButton.TabIndex = 2;
          this.tutorialButton.Text = "Start Tutorial";
          this.tutorialButton.UseVisualStyleBackColor = true;
          this.tutorialButton.Click += new System.EventHandler(this.TutorialButton_Click);
          // 
          // okButton
          // 
          this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.okButton.Location = new System.Drawing.Point(282, 446);
          this.okButton.Name = "okButton";
          this.okButton.Size = new System.Drawing.Size(99, 23);
          this.okButton.TabIndex = 0;
          this.okButton.Text = "Create Game";
          this.okButton.UseVisualStyleBackColor = true;
          this.okButton.Click += new System.EventHandler(this.OkButton_Click);
          // 
          // tabControl1
          // 
          this.tabControl1.Controls.Add(this.tabGameOptions);
          this.tabControl1.Controls.Add(this.tabPlayers);
          this.tabControl1.Controls.Add(this.tabVictoryConditions);
          this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
          this.tabControl1.Location = new System.Drawing.Point(0, 0);
          this.tabControl1.Name = "tabControl1";
          this.tabControl1.SelectedIndex = 0;
          this.tabControl1.Size = new System.Drawing.Size(385, 439);
          this.tabControl1.TabIndex = 3;
          // 
          // tabGameOptions
          // 
          this.tabGameOptions.Controls.Add(this.groupBox7);
          this.tabGameOptions.Controls.Add(this.groupBox6);
          this.tabGameOptions.Controls.Add(this.groupBox3);
          this.tabGameOptions.Location = new System.Drawing.Point(4, 22);
          this.tabGameOptions.Name = "tabGameOptions";
          this.tabGameOptions.Padding = new System.Windows.Forms.Padding(3);
          this.tabGameOptions.Size = new System.Drawing.Size(377, 413);
          this.tabGameOptions.TabIndex = 0;
          this.tabGameOptions.Text = "Game Options";
          this.tabGameOptions.UseVisualStyleBackColor = true;
          // 
          // groupBox6
          // 
          this.groupBox6.Controls.Add(this.gameName);
          this.groupBox6.Location = new System.Drawing.Point(8, 6);
          this.groupBox6.Name = "groupBox6";
          this.groupBox6.Size = new System.Drawing.Size(365, 53);
          this.groupBox6.TabIndex = 1;
          this.groupBox6.TabStop = false;
          this.groupBox6.Text = "Game Name";
          // 
          // gameName
          // 
          this.gameName.Location = new System.Drawing.Point(6, 19);
          this.gameName.Name = "gameName";
          this.gameName.Size = new System.Drawing.Size(351, 20);
          this.gameName.TabIndex = 0;
          this.gameName.Text = "Feel the Nova";
          // 
          // groupBox3
          // 
          this.groupBox3.Controls.Add(this.label16);
          this.groupBox3.Controls.Add(this.label15);
          this.groupBox3.Controls.Add(this.label14);
          this.groupBox3.Controls.Add(this.label13);
          this.groupBox3.Controls.Add(this.starUniformity);
          this.groupBox3.Controls.Add(this.label12);
          this.groupBox3.Controls.Add(this.starDensity);
          this.groupBox3.Controls.Add(this.label11);
          this.groupBox3.Controls.Add(this.starSeparation);
          this.groupBox3.Controls.Add(this.label10);
          this.groupBox3.Controls.Add(this.label9);
          this.groupBox3.Controls.Add(this.numberOfStars);
          this.groupBox3.Controls.Add(this.mapWidth);
          this.groupBox3.Controls.Add(this.mapHeight);
          this.groupBox3.Controls.Add(this.label6);
          this.groupBox3.Controls.Add(this.label3);
          this.groupBox3.Location = new System.Drawing.Point(8, 65);
          this.groupBox3.Name = "groupBox3";
          this.groupBox3.Size = new System.Drawing.Size(365, 200);
          this.groupBox3.TabIndex = 0;
          this.groupBox3.TabStop = false;
          this.groupBox3.Text = "Map";
          // 
          // label16
          // 
          this.label16.AutoSize = true;
          this.label16.Location = new System.Drawing.Point(181, 176);
          this.label16.Name = "label16";
          this.label16.Size = new System.Drawing.Size(133, 13);
          this.label16.TabIndex = 15;
          this.label16.Text = "Unavailable in this version.";
          // 
          // label15
          // 
          this.label15.AutoSize = true;
          this.label15.Location = new System.Drawing.Point(181, 73);
          this.label15.Name = "label15";
          this.label15.Size = new System.Drawing.Size(14, 13);
          this.label15.TabIndex = 14;
          this.label15.Text = "ly";
          // 
          // label14
          // 
          this.label14.AutoSize = true;
          this.label14.Location = new System.Drawing.Point(181, 47);
          this.label14.Name = "label14";
          this.label14.Size = new System.Drawing.Size(14, 13);
          this.label14.TabIndex = 13;
          this.label14.Text = "ly";
          // 
          // label13
          // 
          this.label13.AutoSize = true;
          this.label13.Location = new System.Drawing.Point(181, 21);
          this.label13.Name = "label13";
          this.label13.Size = new System.Drawing.Size(14, 13);
          this.label13.TabIndex = 12;
          this.label13.Text = "ly";
          // 
          // starUniformity
          // 
          this.starUniformity.Location = new System.Drawing.Point(101, 123);
          this.starUniformity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.starUniformity.Name = "starUniformity";
          this.starUniformity.Size = new System.Drawing.Size(74, 20);
          this.starUniformity.TabIndex = 11;
          this.starUniformity.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
          // 
          // label12
          // 
          this.label12.AutoSize = true;
          this.label12.Location = new System.Drawing.Point(11, 125);
          this.label12.Name = "label12";
          this.label12.Size = new System.Drawing.Size(56, 13);
          this.label12.TabIndex = 10;
          this.label12.Text = "Uniformity:";
          // 
          // starDensity
          // 
          this.starDensity.Location = new System.Drawing.Point(101, 97);
          this.starDensity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.starDensity.Name = "starDensity";
          this.starDensity.Size = new System.Drawing.Size(74, 20);
          this.starDensity.TabIndex = 9;
          this.starDensity.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
          this.starDensity.ValueChanged += new System.EventHandler(this.MapDensity_ValueChanged);
          // 
          // label11
          // 
          this.label11.AutoSize = true;
          this.label11.Location = new System.Drawing.Point(11, 99);
          this.label11.Name = "label11";
          this.label11.Size = new System.Drawing.Size(45, 13);
          this.label11.TabIndex = 8;
          this.label11.Text = "Density:";
          // 
          // starSeparation
          // 
          this.starSeparation.Location = new System.Drawing.Point(101, 71);
          this.starSeparation.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
          this.starSeparation.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
          this.starSeparation.Name = "starSeparation";
          this.starSeparation.Size = new System.Drawing.Size(74, 20);
          this.starSeparation.TabIndex = 7;
          this.starSeparation.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
          // 
          // label10
          // 
          this.label10.AutoSize = true;
          this.label10.Location = new System.Drawing.Point(11, 73);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(79, 13);
          this.label10.TabIndex = 6;
          this.label10.Text = "Min separation:";
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Location = new System.Drawing.Point(11, 176);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(84, 13);
          this.label9.TabIndex = 5;
          this.label9.Text = "Number of stars:";
          // 
          // numberOfStars
          // 
          this.numberOfStars.Enabled = false;
          this.numberOfStars.Location = new System.Drawing.Point(101, 174);
          this.numberOfStars.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
          this.numberOfStars.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
          this.numberOfStars.Name = "numberOfStars";
          this.numberOfStars.Size = new System.Drawing.Size(74, 20);
          this.numberOfStars.TabIndex = 4;
          this.numberOfStars.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
          this.numberOfStars.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
          // 
          // mapWidth
          // 
          this.mapWidth.Location = new System.Drawing.Point(101, 45);
          this.mapWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
          this.mapWidth.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
          this.mapWidth.Name = "mapWidth";
          this.mapWidth.Size = new System.Drawing.Size(74, 20);
          this.mapWidth.TabIndex = 3;
          this.mapWidth.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
          // 
          // mapHeight
          // 
          this.mapHeight.Location = new System.Drawing.Point(101, 19);
          this.mapHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
          this.mapHeight.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
          this.mapHeight.Name = "mapHeight";
          this.mapHeight.Size = new System.Drawing.Size(74, 20);
          this.mapHeight.TabIndex = 2;
          this.mapHeight.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
          // 
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(11, 47);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(38, 13);
          this.label6.TabIndex = 1;
          this.label6.Text = "Width:";
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(11, 21);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(41, 13);
          this.label3.TabIndex = 0;
          this.label3.Text = "Height:";
          // 
          // tabPlayers
          // 
          this.tabPlayers.Controls.Add(this.newRaceButton);
          this.tabPlayers.Controls.Add(this.groupBox5);
          this.tabPlayers.Controls.Add(this.addPlayerButton);
          this.tabPlayers.Controls.Add(this.groupBox4);
          this.tabPlayers.Location = new System.Drawing.Point(4, 22);
          this.tabPlayers.Name = "tabPlayers";
          this.tabPlayers.Padding = new System.Windows.Forms.Padding(3);
          this.tabPlayers.Size = new System.Drawing.Size(377, 413);
          this.tabPlayers.TabIndex = 1;
          this.tabPlayers.Text = "Players";
          this.tabPlayers.UseVisualStyleBackColor = true;
          // 
          // newRaceButton
          // 
          this.newRaceButton.Location = new System.Drawing.Point(264, 282);
          this.newRaceButton.Name = "newRaceButton";
          this.newRaceButton.Size = new System.Drawing.Size(101, 23);
          this.newRaceButton.TabIndex = 7;
          this.newRaceButton.Text = "Race Designer";
          this.newRaceButton.UseVisualStyleBackColor = true;
          this.newRaceButton.Click += new System.EventHandler(this.NewRaceButton_Click);
          // 
          // groupBox5
          // 
          this.groupBox5.Controls.Add(this.playerList);
          this.groupBox5.Controls.Add(this.playerDeleteButton);
          this.groupBox5.Controls.Add(this.playerDownButton);
          this.groupBox5.Controls.Add(this.playerUpButton);
          this.groupBox5.Location = new System.Drawing.Point(3, 6);
          this.groupBox5.Name = "groupBox5";
          this.groupBox5.Size = new System.Drawing.Size(370, 267);
          this.groupBox5.TabIndex = 9;
          this.groupBox5.TabStop = false;
          this.groupBox5.Text = "Player List";
          // 
          // playerList
          // 
          this.playerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerNumber,
            this.race,
            this.ai});
          this.playerList.FullRowSelect = true;
          this.playerList.HideSelection = false;
          this.playerList.Location = new System.Drawing.Point(6, 19);
          this.playerList.MultiSelect = false;
          this.playerList.Name = "playerList";
          this.playerList.Size = new System.Drawing.Size(300, 242);
          this.playerList.TabIndex = 4;
          this.playerList.UseCompatibleStateImageBehavior = false;
          this.playerList.View = System.Windows.Forms.View.Details;
          this.playerList.SelectedIndexChanged += new System.EventHandler(this.PlayerList_SelectedIndexChanged);
          // 
          // PlayerNumber
          // 
          this.playerNumber.Text = "Player #";
          // 
          // Race
          // 
          this.race.Text = "Race";
          this.race.Width = 119;
          // 
          // Ai
          // 
          this.ai.Text = "AI / Human";
          this.ai.Width = 104;
          // 
          // playerDeleteButton
          // 
          this.playerDeleteButton.Location = new System.Drawing.Point(312, 93);
          this.playerDeleteButton.Name = "playerDeleteButton";
          this.playerDeleteButton.Size = new System.Drawing.Size(50, 23);
          this.playerDeleteButton.TabIndex = 8;
          this.playerDeleteButton.Text = "Delete";
          this.playerDeleteButton.UseVisualStyleBackColor = true;
          this.playerDeleteButton.Click += new System.EventHandler(this.PlayerDeleteButton_Click);
          // 
          // playerDownButton
          // 
          this.playerDownButton.Location = new System.Drawing.Point(312, 131);
          this.playerDownButton.Name = "playerDownButton";
          this.playerDownButton.Size = new System.Drawing.Size(50, 23);
          this.playerDownButton.TabIndex = 7;
          this.playerDownButton.Text = "Down";
          this.playerDownButton.UseVisualStyleBackColor = true;
          this.playerDownButton.Click += new System.EventHandler(this.PlayerDownButton_Click);
          // 
          // playerUpButton
          // 
          this.playerUpButton.Location = new System.Drawing.Point(312, 55);
          this.playerUpButton.Name = "playerUpButton";
          this.playerUpButton.Size = new System.Drawing.Size(50, 23);
          this.playerUpButton.TabIndex = 6;
          this.playerUpButton.Text = "Up";
          this.playerUpButton.UseVisualStyleBackColor = true;
          this.playerUpButton.Click += new System.EventHandler(this.PlayerUpButton_Click);
          // 
          // addPlayerButton
          // 
          this.addPlayerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.addPlayerButton.Location = new System.Drawing.Point(9, 279);
          this.addPlayerButton.Name = "addPlayerButton";
          this.addPlayerButton.Size = new System.Drawing.Size(75, 23);
          this.addPlayerButton.TabIndex = 6;
          this.addPlayerButton.Text = "Add Player";
          this.addPlayerButton.UseVisualStyleBackColor = true;
          this.addPlayerButton.Click += new System.EventHandler(this.AddPlayerButton_Click);
          // 
          // groupBox4
          // 
          this.groupBox4.Controls.Add(this.playerNumberLabel);
          this.groupBox4.Controls.Add(this.aiBrowseButton);
          this.groupBox4.Controls.Add(this.raceBrowseButton);
          this.groupBox4.Controls.Add(this.aiSelectionBox);
          this.groupBox4.Controls.Add(this.raceSelectionBox);
          this.groupBox4.Controls.Add(this.label8);
          this.groupBox4.Controls.Add(this.label7);
          this.groupBox4.Location = new System.Drawing.Point(6, 311);
          this.groupBox4.Name = "groupBox4";
          this.groupBox4.Size = new System.Drawing.Size(369, 96);
          this.groupBox4.TabIndex = 5;
          this.groupBox4.TabStop = false;
          this.groupBox4.Text = "Modify Player";
          // 
          // playerNumberLabel
          // 
          this.playerNumberLabel.AutoSize = true;
          this.playerNumberLabel.Location = new System.Drawing.Point(6, 16);
          this.playerNumberLabel.Name = "playerNumberLabel";
          this.playerNumberLabel.Size = new System.Drawing.Size(46, 13);
          this.playerNumberLabel.TabIndex = 7;
          this.playerNumberLabel.Text = "Player #";
          // 
          // aiBrowseButton
          // 
          this.aiBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.aiBrowseButton.Location = new System.Drawing.Point(284, 63);
          this.aiBrowseButton.Name = "aiBrowseButton";
          this.aiBrowseButton.Size = new System.Drawing.Size(75, 23);
          this.aiBrowseButton.TabIndex = 5;
          this.aiBrowseButton.Text = "Browse";
          this.aiBrowseButton.UseVisualStyleBackColor = true;
          this.aiBrowseButton.Click += new System.EventHandler(this.AiBrowseButton_Click);
          // 
          // raceBrowseButton
          // 
          this.raceBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.raceBrowseButton.Location = new System.Drawing.Point(284, 36);
          this.raceBrowseButton.Name = "raceBrowseButton";
          this.raceBrowseButton.Size = new System.Drawing.Size(75, 23);
          this.raceBrowseButton.TabIndex = 4;
          this.raceBrowseButton.Text = "Browse";
          this.raceBrowseButton.UseVisualStyleBackColor = true;
          this.raceBrowseButton.Click += new System.EventHandler(this.RaceBrowseButton_Click);
          // 
          // aiSelectionBox
          // 
          this.aiSelectionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.aiSelectionBox.FormattingEnabled = true;
          this.aiSelectionBox.Items.AddRange(new object[] {
            "Human",
            "Default AI"});
          this.aiSelectionBox.Location = new System.Drawing.Point(76, 65);
          this.aiSelectionBox.Name = "aiSelectionBox";
          this.aiSelectionBox.Size = new System.Drawing.Size(202, 21);
          this.aiSelectionBox.TabIndex = 3;
          this.aiSelectionBox.Text = "Human";
          this.aiSelectionBox.SelectedIndexChanged += new System.EventHandler(this.AiSelectionBox_SelectedIndexChanged);
          // 
          // raceSelectionBox
          // 
          this.raceSelectionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.raceSelectionBox.FormattingEnabled = true;
          this.raceSelectionBox.Location = new System.Drawing.Point(76, 38);
          this.raceSelectionBox.Name = "raceSelectionBox";
          this.raceSelectionBox.Size = new System.Drawing.Size(202, 21);
          this.raceSelectionBox.TabIndex = 2;
          this.raceSelectionBox.SelectedIndexChanged += new System.EventHandler(this.RaceSelectionBox_SelectedIndexChanged);
          // 
          // label8
          // 
          this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.label8.AutoSize = true;
          this.label8.Location = new System.Drawing.Point(6, 68);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(62, 13);
          this.label8.TabIndex = 1;
          this.label8.Text = "Human / AI";
          // 
          // label7
          // 
          this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.label7.AutoSize = true;
          this.label7.Location = new System.Drawing.Point(6, 41);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(64, 13);
          this.label7.TabIndex = 0;
          this.label7.Text = "Race Name";
          // 
          // tabVictoryConditions
          // 
          this.tabVictoryConditions.Controls.Add(this.groupBox1);
          this.tabVictoryConditions.Controls.Add(this.groupBox2);
          this.tabVictoryConditions.Location = new System.Drawing.Point(4, 22);
          this.tabVictoryConditions.Name = "tabVictoryConditions";
          this.tabVictoryConditions.Size = new System.Drawing.Size(377, 413);
          this.tabVictoryConditions.TabIndex = 2;
          this.tabVictoryConditions.Text = "Victory Conditions";
          this.tabVictoryConditions.UseVisualStyleBackColor = true;
          // 
          // groupBox7
          // 
          this.groupBox7.Controls.Add(this.acceleratedStart);
          this.groupBox7.Location = new System.Drawing.Point(8, 271);
          this.groupBox7.Name = "groupBox7";
          this.groupBox7.Size = new System.Drawing.Size(364, 141);
          this.groupBox7.TabIndex = 2;
          this.groupBox7.TabStop = false;
          this.groupBox7.Text = "Game Options";
          // 
          // acceleratedStart
          // 
          this.acceleratedStart.AutoSize = true;
          this.acceleratedStart.Location = new System.Drawing.Point(6, 19);
          this.acceleratedStart.Name = "acceleratedStart";
          this.acceleratedStart.Size = new System.Drawing.Size(108, 17);
          this.acceleratedStart.TabIndex = 1;
          this.acceleratedStart.Text = "Accelerated Start";
          this.acceleratedStart.UseVisualStyleBackColor = true;
          // 
          // NewGameWizard
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(385, 481);
          this.Controls.Add(this.tutorialButton);
          this.Controls.Add(this.tabControl1);
          this.Controls.Add(this.okButton);
          this.Controls.Add(this.cancelButton);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "NewGameWizard";
          this.Text = "New Game";
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.groupBox2.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.minimumGameTime)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.targetsToMeet)).EndInit();
          this.tabControl1.ResumeLayout(false);
          this.tabGameOptions.ResumeLayout(false);
          this.groupBox6.ResumeLayout(false);
          this.groupBox6.PerformLayout();
          this.groupBox3.ResumeLayout(false);
          this.groupBox3.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.starUniformity)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.starDensity)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.starSeparation)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.numberOfStars)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).EndInit();
          this.tabPlayers.ResumeLayout(false);
          this.groupBox5.ResumeLayout(false);
          this.groupBox4.ResumeLayout(false);
          this.groupBox4.PerformLayout();
          this.tabVictoryConditions.ResumeLayout(false);
          this.groupBox7.ResumeLayout(false);
          this.groupBox7.PerformLayout();
          this.ResumeLayout(false);

        }

      #endregion

      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Button cancelButton;
      private System.Windows.Forms.Button tutorialButton;
      private System.Windows.Forms.Button okButton;
       private System.Windows.Forms.NumericUpDown targetsToMeet;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.NumericUpDown minimumGameTime;
       private System.Windows.Forms.TabControl tabControl1;
       private System.Windows.Forms.TabPage tabGameOptions;
       private System.Windows.Forms.TabPage tabPlayers;
       private System.Windows.Forms.TabPage tabVictoryConditions;
       private System.Windows.Forms.GroupBox groupBox3;
       private System.Windows.Forms.NumericUpDown mapWidth;
       private System.Windows.Forms.NumericUpDown mapHeight;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label3;
       private EnabledCounter planetsOwned;
       private EnabledCounter exceedSecondPlace;
       private EnabledCounter highestScore;
       private EnabledCounter capitalShips;
       private EnabledCounter productionCapacity;
       private EnabledCounter numberOfFields;
       private EnabledCounter techLevels;
       private EnabledCounter totalScore;
       private System.Windows.Forms.ListView playerList;
       private System.Windows.Forms.ColumnHeader playerNumber;
       private System.Windows.Forms.ColumnHeader race;
       private System.Windows.Forms.ColumnHeader ai;
       private System.Windows.Forms.Button playerDeleteButton;
       private System.Windows.Forms.Button playerDownButton;
       private System.Windows.Forms.Button playerUpButton;
       private System.Windows.Forms.GroupBox groupBox4;
       private System.Windows.Forms.Button addPlayerButton;
       private System.Windows.Forms.Button aiBrowseButton;
       private System.Windows.Forms.Button raceBrowseButton;
       private System.Windows.Forms.ComboBox aiSelectionBox;
       private System.Windows.Forms.ComboBox raceSelectionBox;
       private System.Windows.Forms.Label label8;
       private System.Windows.Forms.Label label7;
       private System.Windows.Forms.GroupBox groupBox5;
       private System.Windows.Forms.Button newRaceButton;
       private System.Windows.Forms.GroupBox groupBox6;
       private System.Windows.Forms.TextBox gameName;
       private System.Windows.Forms.Label label9;
       private System.Windows.Forms.NumericUpDown numberOfStars;
       private System.Windows.Forms.Label playerNumberLabel;
       private System.Windows.Forms.NumericUpDown starUniformity;
       private System.Windows.Forms.Label label12;
       private System.Windows.Forms.NumericUpDown starDensity;
       private System.Windows.Forms.Label label11;
       private System.Windows.Forms.NumericUpDown starSeparation;
       private System.Windows.Forms.Label label10;
       private System.Windows.Forms.Label label15;
       private System.Windows.Forms.Label label14;
       private System.Windows.Forms.Label label13;
       private System.Windows.Forms.Label label16;
       private System.Windows.Forms.GroupBox groupBox7;
       private System.Windows.Forms.CheckBox acceleratedStart;
   }
}
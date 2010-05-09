using Nova.ControlLibrary;
using Nova.Common;

namespace Nova.WinForms.NewGame
{
   partial class NewGameWizard
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
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

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGameWizard));
          this.label2 = new System.Windows.Forms.Label();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.MinimumGameTime = new System.Windows.Forms.NumericUpDown();
          this.label5 = new System.Windows.Forms.Label();
          this.TargetsToMeet = new System.Windows.Forms.NumericUpDown();
          this.label4 = new System.Windows.Forms.Label();
          this.cancelButton = new System.Windows.Forms.Button();
          this.tutorialButton = new System.Windows.Forms.Button();
          this.okButton = new System.Windows.Forms.Button();
          this.tabControl1 = new System.Windows.Forms.TabControl();
          this.tabGameOptions = new System.Windows.Forms.TabPage();
          this.groupBox6 = new System.Windows.Forms.GroupBox();
          this.gameName = new System.Windows.Forms.TextBox();
          this.groupBox3 = new System.Windows.Forms.GroupBox();
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
          this.PlayerNumber = new System.Windows.Forms.ColumnHeader();
          this.Race = new System.Windows.Forms.ColumnHeader();
          this.Ai = new System.Windows.Forms.ColumnHeader();
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
          this.starSeparation = new System.Windows.Forms.NumericUpDown();
          this.label10 = new System.Windows.Forms.Label();
          this.starDensity = new System.Windows.Forms.NumericUpDown();
          this.label11 = new System.Windows.Forms.Label();
          this.starUniformity = new System.Windows.Forms.NumericUpDown();
          this.label12 = new System.Windows.Forms.Label();
          this.TotalScore = new ControlLibrary.EnabledCounter();
          this.ExceedSecondPlace = new ControlLibrary.EnabledCounter();
          this.HighestScore = new ControlLibrary.EnabledCounter();
          this.CapitalShips = new ControlLibrary.EnabledCounter();
          this.ProductionCapacity = new ControlLibrary.EnabledCounter();
          this.NumberOfFields = new ControlLibrary.EnabledCounter();
          this.TechLevels = new ControlLibrary.EnabledCounter();
          this.PlanetsOwned = new ControlLibrary.EnabledCounter();
          this.label13 = new System.Windows.Forms.Label();
          this.label14 = new System.Windows.Forms.Label();
          this.label15 = new System.Windows.Forms.Label();
          this.label16 = new System.Windows.Forms.Label();
          this.groupBox1.SuspendLayout();
          this.groupBox2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.MinimumGameTime)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.TargetsToMeet)).BeginInit();
          this.tabControl1.SuspendLayout();
          this.tabGameOptions.SuspendLayout();
          this.groupBox6.SuspendLayout();
          this.groupBox3.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.numberOfStars)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).BeginInit();
          this.tabPlayers.SuspendLayout();
          this.groupBox5.SuspendLayout();
          this.groupBox4.SuspendLayout();
          this.tabVictoryConditions.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.starSeparation)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.starDensity)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.starUniformity)).BeginInit();
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
          // TotalScore
          // 
          this.TotalScore.ControlCounter = 100;
          this.TotalScore.ControlSelected = false;
          this.TotalScore.ControlText = "Exceeds a score of";
          this.TotalScore.Location = new System.Drawing.Point(7, 135);
          this.TotalScore.Maximum = 10000;
          this.TotalScore.Minimum = 0;
          this.TotalScore.Name = "TotalScore";
          this.TotalScore.Size = new System.Drawing.Size(338, 23);
          this.TotalScore.TabIndex = 11;
          this.TotalScore.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // ExceedSecondPlace
          // 
          this.ExceedSecondPlace.ControlCounter = 100;
          this.ExceedSecondPlace.ControlSelected = false;
          this.ExceedSecondPlace.ControlText = "Exceed second place score by (%)";
          this.ExceedSecondPlace.Location = new System.Drawing.Point(6, 254);
          this.ExceedSecondPlace.Maximum = 10000;
          this.ExceedSecondPlace.Minimum = 0;
          this.ExceedSecondPlace.Name = "ExceedSecondPlace";
          this.ExceedSecondPlace.Size = new System.Drawing.Size(339, 23);
          this.ExceedSecondPlace.TabIndex = 10;
          this.ExceedSecondPlace.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // HighestScore
          // 
          this.HighestScore.ControlCounter = 100;
          this.HighestScore.ControlSelected = false;
          this.HighestScore.ControlText = "Has the highest score after (years)";
          this.HighestScore.Location = new System.Drawing.Point(6, 224);
          this.HighestScore.Maximum = 10000;
          this.HighestScore.Minimum = 0;
          this.HighestScore.Name = "HighestScore";
          this.HighestScore.Size = new System.Drawing.Size(339, 23);
          this.HighestScore.TabIndex = 9;
          this.HighestScore.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // CapitalShips
          // 
          this.CapitalShips.ControlCounter = 100;
          this.CapitalShips.ControlSelected = false;
          this.CapitalShips.ControlText = "Number of capital ships";
          this.CapitalShips.Location = new System.Drawing.Point(6, 194);
          this.CapitalShips.Maximum = 10000;
          this.CapitalShips.Minimum = 0;
          this.CapitalShips.Name = "CapitalShips";
          this.CapitalShips.Size = new System.Drawing.Size(339, 23);
          this.CapitalShips.TabIndex = 8;
          this.CapitalShips.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // ProductionCapacity
          // 
          this.ProductionCapacity.ControlCounter = 100;
          this.ProductionCapacity.ControlSelected = false;
          this.ProductionCapacity.ControlText = "Has  production capacity of (in K resources)";
          this.ProductionCapacity.Location = new System.Drawing.Point(6, 164);
          this.ProductionCapacity.Maximum = 10000;
          this.ProductionCapacity.Minimum = 0;
          this.ProductionCapacity.Name = "ProductionCapacity";
          this.ProductionCapacity.Size = new System.Drawing.Size(339, 23);
          this.ProductionCapacity.TabIndex = 7;
          this.ProductionCapacity.Value = new Nova.Common.EnabledValue(false, 100);
          // 
          // NumberOfFields
          // 
          this.NumberOfFields.ControlCounter = 4;
          this.NumberOfFields.ControlSelected = false;
          this.NumberOfFields.ControlText = "In the following number of fields";
          this.NumberOfFields.Location = new System.Drawing.Point(7, 105);
          this.NumberOfFields.Maximum = 6;
          this.NumberOfFields.Minimum = 0;
          this.NumberOfFields.Name = "NumberOfFields";
          this.NumberOfFields.Size = new System.Drawing.Size(339, 23);
          this.NumberOfFields.TabIndex = 6;
          this.NumberOfFields.Value = new Nova.Common.EnabledValue(false, 4);
          // 
          // TechLevels
          // 
          this.TechLevels.ControlCounter = 22;
          this.TechLevels.ControlSelected = false;
          this.TechLevels.ControlText = "Attains the following tech-level";
          this.TechLevels.Location = new System.Drawing.Point(6, 73);
          this.TechLevels.Maximum = 10000;
          this.TechLevels.Minimum = 0;
          this.TechLevels.Name = "TechLevels";
          this.TechLevels.Size = new System.Drawing.Size(339, 23);
          this.TechLevels.TabIndex = 5;
          this.TechLevels.Value = new Nova.Common.EnabledValue(false, 22);
          // 
          // PlanetsOwned
          // 
          this.PlanetsOwned.ControlCounter = 60;
          this.PlanetsOwned.ControlSelected = false;
          this.PlanetsOwned.ControlText = "Owns the following number of planets (%)";
          this.PlanetsOwned.Location = new System.Drawing.Point(7, 45);
          this.PlanetsOwned.Maximum = 10000;
          this.PlanetsOwned.Minimum = 0;
          this.PlanetsOwned.Name = "PlanetsOwned";
          this.PlanetsOwned.Size = new System.Drawing.Size(339, 23);
          this.PlanetsOwned.TabIndex = 4;
          this.PlanetsOwned.Value = new Nova.Common.EnabledValue(false, 60);
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
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.TotalScore);
          this.groupBox1.Controls.Add(this.ExceedSecondPlace);
          this.groupBox1.Controls.Add(this.HighestScore);
          this.groupBox1.Controls.Add(this.CapitalShips);
          this.groupBox1.Controls.Add(this.ProductionCapacity);
          this.groupBox1.Controls.Add(this.NumberOfFields);
          this.groupBox1.Controls.Add(this.TechLevels);
          this.groupBox1.Controls.Add(this.PlanetsOwned);
          this.groupBox1.Controls.Add(this.label1);
          this.groupBox1.Controls.Add(this.label2);
          this.groupBox1.Location = new System.Drawing.Point(14, 20);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(352, 285);
          this.groupBox1.TabIndex = 20;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Victory Conditions";
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.MinimumGameTime);
          this.groupBox2.Controls.Add(this.label5);
          this.groupBox2.Controls.Add(this.TargetsToMeet);
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
          this.MinimumGameTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
          this.MinimumGameTime.Location = new System.Drawing.Point(277, 47);
          this.MinimumGameTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
          this.MinimumGameTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
          this.MinimumGameTime.Name = "MinimumGameTime";
          this.MinimumGameTime.Size = new System.Drawing.Size(69, 20);
          this.MinimumGameTime.TabIndex = 34;
          this.MinimumGameTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.MinimumGameTime.Value = new decimal(new int[] {
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
          this.TargetsToMeet.Anchor = System.Windows.Forms.AnchorStyles.Right;
          this.TargetsToMeet.Location = new System.Drawing.Point(277, 17);
          this.TargetsToMeet.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
          this.TargetsToMeet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.TargetsToMeet.Name = "TargetsToMeet";
          this.TargetsToMeet.Size = new System.Drawing.Size(69, 20);
          this.TargetsToMeet.TabIndex = 32;
          this.TargetsToMeet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.TargetsToMeet.Value = new decimal(new int[] {
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
          this.okButton.Click += new System.EventHandler(this.okButton_Click);
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
          this.numberOfStars.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
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
          this.newRaceButton.Click += new System.EventHandler(this.newRaceButton_Click);
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
            this.PlayerNumber,
            this.Race,
            this.Ai});
          this.playerList.FullRowSelect = true;
          this.playerList.HideSelection = false;
          this.playerList.Location = new System.Drawing.Point(6, 19);
          this.playerList.MultiSelect = false;
          this.playerList.Name = "playerList";
          this.playerList.Size = new System.Drawing.Size(300, 242);
          this.playerList.TabIndex = 4;
          this.playerList.UseCompatibleStateImageBehavior = false;
          this.playerList.View = System.Windows.Forms.View.Details;
          this.playerList.SelectedIndexChanged += new System.EventHandler(this.playerList_SelectedIndexChanged);
          // 
          // PlayerNumber
          // 
          this.PlayerNumber.Text = "Player #";
          // 
          // Race
          // 
          this.Race.Text = "Race";
          this.Race.Width = 119;
          // 
          // Ai
          // 
          this.Ai.Text = "AI / Human";
          this.Ai.Width = 104;
          // 
          // playerDeleteButton
          // 
          this.playerDeleteButton.Location = new System.Drawing.Point(312, 93);
          this.playerDeleteButton.Name = "playerDeleteButton";
          this.playerDeleteButton.Size = new System.Drawing.Size(50, 23);
          this.playerDeleteButton.TabIndex = 8;
          this.playerDeleteButton.Text = "Delete";
          this.playerDeleteButton.UseVisualStyleBackColor = true;
          this.playerDeleteButton.Click += new System.EventHandler(this.playerDeleteButton_Click);
          // 
          // playerDownButton
          // 
          this.playerDownButton.Location = new System.Drawing.Point(312, 131);
          this.playerDownButton.Name = "playerDownButton";
          this.playerDownButton.Size = new System.Drawing.Size(50, 23);
          this.playerDownButton.TabIndex = 7;
          this.playerDownButton.Text = "Down";
          this.playerDownButton.UseVisualStyleBackColor = true;
          this.playerDownButton.Click += new System.EventHandler(this.playerDownButton_Click);
          // 
          // playerUpButton
          // 
          this.playerUpButton.Location = new System.Drawing.Point(312, 55);
          this.playerUpButton.Name = "playerUpButton";
          this.playerUpButton.Size = new System.Drawing.Size(50, 23);
          this.playerUpButton.TabIndex = 6;
          this.playerUpButton.Text = "Up";
          this.playerUpButton.UseVisualStyleBackColor = true;
          this.playerUpButton.Click += new System.EventHandler(this.playerUpButton_Click);
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
          this.addPlayerButton.Click += new System.EventHandler(this.addPlayerButton_Click);
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
          this.aiBrowseButton.Click += new System.EventHandler(this.aiBrowseButton_Click);
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
          this.raceBrowseButton.Click += new System.EventHandler(this.raceBrowseButton_Click);
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
          this.aiSelectionBox.SelectedIndexChanged += new System.EventHandler(this.aiSelectionBox_SelectedIndexChanged);
          // 
          // raceSelectionBox
          // 
          this.raceSelectionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.raceSelectionBox.FormattingEnabled = true;
          this.raceSelectionBox.Location = new System.Drawing.Point(76, 38);
          this.raceSelectionBox.Name = "raceSelectionBox";
          this.raceSelectionBox.Size = new System.Drawing.Size(202, 21);
          this.raceSelectionBox.TabIndex = 2;
          this.raceSelectionBox.SelectedIndexChanged += new System.EventHandler(this.raceSelectionBox_SelectedIndexChanged);
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
          // starSeparation
          // 
          this.starSeparation.Location = new System.Drawing.Point(101, 71);
          this.starSeparation.Maximum = new decimal(new int[] {
            90,
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
          this.starDensity.ValueChanged += new System.EventHandler(this.mapDensity_ValueChanged);
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
          // TotalScore
          // 
          this.TotalScore.ControlCounter = 100;
          this.TotalScore.ControlSelected = false;
          this.TotalScore.ControlText = "Exceeds a score of";
          this.TotalScore.Location = new System.Drawing.Point(7, 135);
          this.TotalScore.Maximum = 10000;
          this.TotalScore.Minimum = 0;
          this.TotalScore.Name = "TotalScore";
          this.TotalScore.Size = new System.Drawing.Size(338, 23);
          this.TotalScore.TabIndex = 11;
          // 
          // ExceedSecondPlace
          // 
          this.ExceedSecondPlace.ControlCounter = 100;
          this.ExceedSecondPlace.ControlSelected = false;
          this.ExceedSecondPlace.ControlText = "Exceed second place score by (%)";
          this.ExceedSecondPlace.Location = new System.Drawing.Point(6, 254);
          this.ExceedSecondPlace.Maximum = 10000;
          this.ExceedSecondPlace.Minimum = 0;
          this.ExceedSecondPlace.Name = "ExceedSecondPlace";
          this.ExceedSecondPlace.Size = new System.Drawing.Size(339, 23);
          this.ExceedSecondPlace.TabIndex = 10;
          // 
          // HighestScore
          // 
          this.HighestScore.ControlCounter = 100;
          this.HighestScore.ControlSelected = false;
          this.HighestScore.ControlText = "Has the highest score after (years)";
          this.HighestScore.Location = new System.Drawing.Point(6, 224);
          this.HighestScore.Maximum = 10000;
          this.HighestScore.Minimum = 0;
          this.HighestScore.Name = "HighestScore";
          this.HighestScore.Size = new System.Drawing.Size(339, 23);
          this.HighestScore.TabIndex = 9;
          // 
          // CapitalShips
          // 
          this.CapitalShips.ControlCounter = 100;
          this.CapitalShips.ControlSelected = false;
          this.CapitalShips.ControlText = "Number of capital ships";
          this.CapitalShips.Location = new System.Drawing.Point(6, 194);
          this.CapitalShips.Maximum = 10000;
          this.CapitalShips.Minimum = 0;
          this.CapitalShips.Name = "CapitalShips";
          this.CapitalShips.Size = new System.Drawing.Size(339, 23);
          this.CapitalShips.TabIndex = 8;
          // 
          // ProductionCapacity
          // 
          this.ProductionCapacity.ControlCounter = 100;
          this.ProductionCapacity.ControlSelected = false;
          this.ProductionCapacity.ControlText = "Has  production capacity of (in K resources)";
          this.ProductionCapacity.Location = new System.Drawing.Point(6, 164);
          this.ProductionCapacity.Maximum = 10000;
          this.ProductionCapacity.Minimum = 0;
          this.ProductionCapacity.Name = "ProductionCapacity";
          this.ProductionCapacity.Size = new System.Drawing.Size(339, 23);
          this.ProductionCapacity.TabIndex = 7;
          // 
          // NumberOfFields
          // 
          this.NumberOfFields.ControlCounter = 4;
          this.NumberOfFields.ControlSelected = false;
          this.NumberOfFields.ControlText = "In the following number of fields";
          this.NumberOfFields.Location = new System.Drawing.Point(7, 105);
          this.NumberOfFields.Maximum = 6;
          this.NumberOfFields.Minimum = 0;
          this.NumberOfFields.Name = "NumberOfFields";
          this.NumberOfFields.Size = new System.Drawing.Size(339, 23);
          this.NumberOfFields.TabIndex = 6;
          // 
          // TechLevels
          // 
          this.TechLevels.ControlCounter = 22;
          this.TechLevels.ControlSelected = false;
          this.TechLevels.ControlText = "Attains the following tech-level";
          this.TechLevels.Location = new System.Drawing.Point(6, 73);
          this.TechLevels.Maximum = 10000;
          this.TechLevels.Minimum = 0;
          this.TechLevels.Name = "TechLevels";
          this.TechLevels.Size = new System.Drawing.Size(339, 23);
          this.TechLevels.TabIndex = 5;
          // 
          // PlanetsOwned
          // 
          this.PlanetsOwned.ControlCounter = 60;
          this.PlanetsOwned.ControlSelected = false;
          this.PlanetsOwned.ControlText = "Owns the following number of planets (%)";
          this.PlanetsOwned.Location = new System.Drawing.Point(7, 45);
          this.PlanetsOwned.Maximum = 10000;
          this.PlanetsOwned.Minimum = 0;
          this.PlanetsOwned.Name = "PlanetsOwned";
          this.PlanetsOwned.Size = new System.Drawing.Size(339, 23);
          this.PlanetsOwned.TabIndex = 4;
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
          // label14
          // 
          this.label14.AutoSize = true;
          this.label14.Location = new System.Drawing.Point(181, 47);
          this.label14.Name = "label14";
          this.label14.Size = new System.Drawing.Size(14, 13);
          this.label14.TabIndex = 13;
          this.label14.Text = "ly";
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
          // label16
          // 
          this.label16.AutoSize = true;
          this.label16.Location = new System.Drawing.Point(181, 176);
          this.label16.Name = "label16";
          this.label16.Size = new System.Drawing.Size(133, 13);
          this.label16.TabIndex = 15;
          this.label16.Text = "Unavailable in this version.";
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
          ((System.ComponentModel.ISupportInitialize)(this.MinimumGameTime)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.TargetsToMeet)).EndInit();
          this.tabControl1.ResumeLayout(false);
          this.tabGameOptions.ResumeLayout(false);
          this.groupBox6.ResumeLayout(false);
          this.groupBox6.PerformLayout();
          this.groupBox3.ResumeLayout(false);
          this.groupBox3.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.numberOfStars)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapWidth)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.mapHeight)).EndInit();
          this.tabPlayers.ResumeLayout(false);
          this.groupBox5.ResumeLayout(false);
          this.groupBox4.ResumeLayout(false);
          this.groupBox4.PerformLayout();
          this.tabVictoryConditions.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.starSeparation)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.starDensity)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.starUniformity)).EndInit();
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
       private System.Windows.Forms.NumericUpDown TargetsToMeet;
       private System.Windows.Forms.Label label4;
       private System.Windows.Forms.Label label5;
       private System.Windows.Forms.NumericUpDown MinimumGameTime;
       private System.Windows.Forms.TabControl tabControl1;
       private System.Windows.Forms.TabPage tabGameOptions;
       private System.Windows.Forms.TabPage tabPlayers;
       private System.Windows.Forms.TabPage tabVictoryConditions;
       private System.Windows.Forms.GroupBox groupBox3;
       private System.Windows.Forms.NumericUpDown mapWidth;
       private System.Windows.Forms.NumericUpDown mapHeight;
       private System.Windows.Forms.Label label6;
       private System.Windows.Forms.Label label3;
       private EnabledCounter PlanetsOwned;
       private EnabledCounter ExceedSecondPlace;
       private EnabledCounter HighestScore;
       private EnabledCounter CapitalShips;
       private EnabledCounter ProductionCapacity;
       private EnabledCounter NumberOfFields;
       private EnabledCounter TechLevels;
       private EnabledCounter TotalScore;
       private System.Windows.Forms.ListView playerList;
       private System.Windows.Forms.ColumnHeader PlayerNumber;
       private System.Windows.Forms.ColumnHeader Race;
       private System.Windows.Forms.ColumnHeader Ai;
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
   }
}
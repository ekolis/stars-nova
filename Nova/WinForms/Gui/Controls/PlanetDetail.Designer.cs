using System;
using System.Drawing;
using System.Windows.Forms;

using Nova.Client;
using Nova.Common;
using Nova.ControlLibrary;

namespace Nova.WinForms.Gui
{
    public partial class PlanetDetail
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanetDetail));
            this.mines = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.productionQueue = new System.Windows.Forms.ListView();
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.changeProductionQueue = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.population = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.factories = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.defenseCoverage = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resourceDisplay = new Nova.ControlLibrary.ResourcesOnHandDisplay();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.scannerRange = new System.Windows.Forms.Label();
            this.scannerType = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.defenseType = new System.Windows.Forms.Label();
            this.defenses = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.starbasePanel = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.targetButton = new System.Windows.Forms.Button();
            this.starbaseCapacity = new System.Windows.Forms.Label();
            this.massDriverDestination = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.starbaseDamage = new System.Windows.Forms.Label();
            this.massDriverType = new System.Windows.Forms.Label();
            this.starbaseShields = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.starbaseArmor = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.nextPlanet = new System.Windows.Forms.Button();
            this.previousPlanet = new System.Windows.Forms.Button();
            this.groupPlanetSelect = new System.Windows.Forms.GroupBox();
            this.comboFleetsInOrbit = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonGoto = new System.Windows.Forms.Button();
            this.buttonCargo = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.meterCargo = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.meterFuel = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.fuel = new Nova.ControlLibrary.Gauge();
            this.cargoMeter1 = new Nova.WinForms.Gui.Controls.CargoMeter();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.starbasePanel.SuspendLayout();
            this.groupPlanetSelect.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // mines
            // 
            this.mines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mines.Location = new System.Drawing.Point(67, 40);
            this.mines.Name = "mines";
            this.mines.Size = new System.Drawing.Size(89, 16);
            this.mines.TabIndex = 14;
            this.mines.Text = "0 of 0";
            this.mines.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Population";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Mines";
            // 
            // productionQueue
            // 
            this.productionQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.productionQueue.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.productionQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productionQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.description,
            this.quantity});
            this.productionQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.productionQueue.Location = new System.Drawing.Point(6, 22);
            this.productionQueue.MultiSelect = false;
            this.productionQueue.Name = "productionQueue";
            this.productionQueue.Scrollable = false;
            this.productionQueue.Size = new System.Drawing.Size(166, 57);
            this.productionQueue.TabIndex = 13;
            this.productionQueue.UseCompatibleStateImageBehavior = false;
            this.productionQueue.View = System.Windows.Forms.View.Details;
            // 
            // description
            // 
            this.description.Text = "Description";
            this.description.Width = 92;
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity";
            this.quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.quantity.Width = 59;
            // 
            // changeProductionQueue
            // 
            this.changeProductionQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeProductionQueue.Location = new System.Drawing.Point(6, 85);
            this.changeProductionQueue.Name = "changeProductionQueue";
            this.changeProductionQueue.Size = new System.Drawing.Size(75, 23);
            this.changeProductionQueue.TabIndex = 14;
            this.changeProductionQueue.Text = "Change";
            this.changeProductionQueue.UseVisualStyleBackColor = true;
            this.changeProductionQueue.Click += new System.EventHandler(this.ChangeProductionQueue_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.changeProductionQueue);
            this.groupBox1.Controls.Add(this.productionQueue);
            this.groupBox1.Location = new System.Drawing.Point(177, 117);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 114);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Production Queue";
            // 
            // population
            // 
            this.population.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.population.Location = new System.Drawing.Point(78, 89);
            this.population.Name = "population";
            this.population.Size = new System.Drawing.Size(64, 16);
            this.population.TabIndex = 12;
            this.population.Text = "0";
            this.population.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Factories";
            // 
            // factories
            // 
            this.factories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.factories.Location = new System.Drawing.Point(64, 24);
            this.factories.Name = "factories";
            this.factories.Size = new System.Drawing.Size(92, 16);
            this.factories.TabIndex = 17;
            this.factories.Text = "0 of 0";
            this.factories.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Defense Coverage (%)";
            // 
            // defenseCoverage
            // 
            this.defenseCoverage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenseCoverage.Location = new System.Drawing.Point(128, 23);
            this.defenseCoverage.Name = "defenseCoverage";
            this.defenseCoverage.Size = new System.Drawing.Size(28, 16);
            this.defenseCoverage.TabIndex = 24;
            this.defenseCoverage.Text = "0";
            this.defenseCoverage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.population);
            this.groupBox2.Controls.Add(this.resourceDisplay);
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 109);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resources on Hand";
            // 
            // resourceDisplay
            // 
            this.resourceDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resourceDisplay.Location = new System.Drawing.Point(6, 18);
            this.resourceDisplay.Name = "resourceDisplay";
            this.resourceDisplay.Size = new System.Drawing.Size(150, 68);
            this.resourceDisplay.TabIndex = 20;
            this.resourceDisplay.Value = new Nova.Common.Resources(0, 0, 0, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.scannerRange);
            this.groupBox3.Controls.Add(this.scannerType);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(-1, 326);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 61);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scanner";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(142, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "ly";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scannerRange
            // 
            this.scannerRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerRange.Location = new System.Drawing.Point(95, 38);
            this.scannerRange.Name = "scannerRange";
            this.scannerRange.Size = new System.Drawing.Size(47, 16);
            this.scannerRange.TabIndex = 32;
            this.scannerRange.Text = "0";
            this.scannerRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scannerType
            // 
            this.scannerType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scannerType.Location = new System.Drawing.Point(90, 21);
            this.scannerType.Name = "scannerType";
            this.scannerType.Size = new System.Drawing.Size(68, 16);
            this.scannerType.TabIndex = 31;
            this.scannerType.Text = "None";
            this.scannerType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Scanner Type";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Scanner Range";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Defenses";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Defense Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // defenseType
            // 
            this.defenseType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenseType.Location = new System.Drawing.Point(93, 58);
            this.defenseType.Name = "defenseType";
            this.defenseType.Size = new System.Drawing.Size(65, 13);
            this.defenseType.TabIndex = 29;
            this.defenseType.Text = "None";
            this.defenseType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // defenses
            // 
            this.defenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defenses.Location = new System.Drawing.Point(128, 40);
            this.defenses.Name = "defenses";
            this.defenses.Size = new System.Drawing.Size(28, 16);
            this.defenses.TabIndex = 30;
            this.defenses.Text = "0";
            this.defenses.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.defenseCoverage);
            this.groupBox4.Controls.Add(this.defenseType);
            this.groupBox4.Controls.Add(this.defenses);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(0, 242);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 79);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Defenses";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.mines);
            this.groupBox5.Controls.Add(this.factories);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(2, 170);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(162, 67);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Production";
            // 
            // starbasePanel
            // 
            this.starbasePanel.Controls.Add(this.label21);
            this.starbasePanel.Controls.Add(this.label26);
            this.starbasePanel.Controls.Add(this.label27);
            this.starbasePanel.Controls.Add(this.label12);
            this.starbasePanel.Controls.Add(this.targetButton);
            this.starbasePanel.Controls.Add(this.starbaseCapacity);
            this.starbasePanel.Controls.Add(this.massDriverDestination);
            this.starbasePanel.Controls.Add(this.label18);
            this.starbasePanel.Controls.Add(this.starbaseDamage);
            this.starbasePanel.Controls.Add(this.massDriverType);
            this.starbasePanel.Controls.Add(this.starbaseShields);
            this.starbasePanel.Controls.Add(this.label17);
            this.starbasePanel.Controls.Add(this.starbaseArmor);
            this.starbasePanel.Controls.Add(this.label16);
            this.starbasePanel.Controls.Add(this.label15);
            this.starbasePanel.Controls.Add(this.label14);
            this.starbasePanel.Controls.Add(this.label13);
            this.starbasePanel.Location = new System.Drawing.Point(177, 236);
            this.starbasePanel.Margin = new System.Windows.Forms.Padding(0);
            this.starbasePanel.Name = "starbasePanel";
            this.starbasePanel.Size = new System.Drawing.Size(178, 151);
            this.starbasePanel.TabIndex = 31;
            this.starbasePanel.TabStop = false;
            this.starbasePanel.Text = "Starbase";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(156, 70);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(20, 13);
            this.label21.TabIndex = 37;
            this.label21.Text = "kT";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(158, 50);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 13);
            this.label26.TabIndex = 35;
            this.label26.Text = "%";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(158, 14);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(19, 13);
            this.label27.TabIndex = 36;
            this.label27.Text = "dp";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(158, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "dp";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetButton
            // 
            this.targetButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.targetButton.Location = new System.Drawing.Point(6, 125);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(75, 23);
            this.targetButton.TabIndex = 15;
            this.targetButton.Text = "Target";
            this.targetButton.UseVisualStyleBackColor = true;
            // 
            // starbaseCapacity
            // 
            this.starbaseCapacity.Location = new System.Drawing.Point(83, 72);
            this.starbaseCapacity.Name = "starbaseCapacity";
            this.starbaseCapacity.Size = new System.Drawing.Size(62, 13);
            this.starbaseCapacity.TabIndex = 9;
            this.starbaseCapacity.Text = "0";
            this.starbaseCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // massDriverDestination
            // 
            this.massDriverDestination.Location = new System.Drawing.Point(100, 90);
            this.massDriverDestination.Name = "massDriverDestination";
            this.massDriverDestination.Size = new System.Drawing.Size(72, 13);
            this.massDriverDestination.TabIndex = 11;
            this.massDriverDestination.Text = "None";
            this.massDriverDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 108);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Destination";
            // 
            // starbaseDamage
            // 
            this.starbaseDamage.Location = new System.Drawing.Point(74, 53);
            this.starbaseDamage.Name = "starbaseDamage";
            this.starbaseDamage.Size = new System.Drawing.Size(71, 13);
            this.starbaseDamage.TabIndex = 8;
            this.starbaseDamage.Text = "0";
            this.starbaseDamage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // massDriverType
            // 
            this.massDriverType.Location = new System.Drawing.Point(92, 108);
            this.massDriverType.Name = "massDriverType";
            this.massDriverType.Size = new System.Drawing.Size(79, 13);
            this.massDriverType.TabIndex = 10;
            this.massDriverType.Tag = "";
            this.massDriverType.Text = "None";
            this.massDriverType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // starbaseShields
            // 
            this.starbaseShields.Location = new System.Drawing.Point(80, 35);
            this.starbaseShields.Name = "starbaseShields";
            this.starbaseShields.Size = new System.Drawing.Size(65, 13);
            this.starbaseShields.TabIndex = 7;
            this.starbaseShields.Text = "0";
            this.starbaseShields.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 90);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Mass Driver";
            // 
            // starbaseArmor
            // 
            this.starbaseArmor.Location = new System.Drawing.Point(87, 17);
            this.starbaseArmor.Name = "starbaseArmor";
            this.starbaseArmor.Size = new System.Drawing.Size(58, 13);
            this.starbaseArmor.TabIndex = 6;
            this.starbaseArmor.Text = "0";
            this.starbaseArmor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Dock Capacity";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Damage";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Shields";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Armor";
            // 
            // nextPlanet
            // 
            this.nextPlanet.Location = new System.Drawing.Point(9, 19);
            this.nextPlanet.Name = "nextPlanet";
            this.nextPlanet.Size = new System.Drawing.Size(59, 23);
            this.nextPlanet.TabIndex = 32;
            this.nextPlanet.Text = "Next";
            this.nextPlanet.UseVisualStyleBackColor = true;
            this.nextPlanet.Click += new System.EventHandler(this.NextPlanet_Click);
            // 
            // previousPlanet
            // 
            this.previousPlanet.Location = new System.Drawing.Point(93, 19);
            this.previousPlanet.Name = "previousPlanet";
            this.previousPlanet.Size = new System.Drawing.Size(59, 23);
            this.previousPlanet.TabIndex = 33;
            this.previousPlanet.Text = "Previous";
            this.previousPlanet.UseVisualStyleBackColor = true;
            this.previousPlanet.Click += new System.EventHandler(this.PreviousPlanet_Click);
            // 
            // groupPlanetSelect
            // 
            this.groupPlanetSelect.Controls.Add(this.previousPlanet);
            this.groupPlanetSelect.Controls.Add(this.nextPlanet);
            this.groupPlanetSelect.Location = new System.Drawing.Point(0, 0);
            this.groupPlanetSelect.Margin = new System.Windows.Forms.Padding(0);
            this.groupPlanetSelect.Name = "groupPlanetSelect";
            this.groupPlanetSelect.Size = new System.Drawing.Size(162, 51);
            this.groupPlanetSelect.TabIndex = 32;
            this.groupPlanetSelect.TabStop = false;
            this.groupPlanetSelect.Text = "Planet Selection";
            // 
            // comboFleetsInOrbit
            // 
            this.comboFleetsInOrbit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFleetsInOrbit.FormattingEnabled = true;
            this.comboFleetsInOrbit.Location = new System.Drawing.Point(9, 19);
            this.comboFleetsInOrbit.Name = "comboFleetsInOrbit";
            this.comboFleetsInOrbit.Size = new System.Drawing.Size(162, 21);
            this.comboFleetsInOrbit.TabIndex = 0;
            this.comboFleetsInOrbit.SelectedIndexChanged += new System.EventHandler(this.comboFleetsInOrbit_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(21, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 16);
            this.label10.TabIndex = 88;
            this.label10.Text = "Fuel";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(13, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 91;
            this.label9.Text = "Cargo";
            // 
            // buttonGoto
            // 
            this.buttonGoto.Location = new System.Drawing.Point(10, 81);
            this.buttonGoto.Name = "buttonGoto";
            this.buttonGoto.Size = new System.Drawing.Size(59, 23);
            this.buttonGoto.TabIndex = 92;
            this.buttonGoto.Text = "Goto";
            this.buttonGoto.UseVisualStyleBackColor = true;
            this.buttonGoto.Click += new System.EventHandler(this.buttonGoto_Click);
            // 
            // buttonCargo
            // 
            this.buttonCargo.Location = new System.Drawing.Point(112, 81);
            this.buttonCargo.Name = "buttonCargo";
            this.buttonCargo.Size = new System.Drawing.Size(59, 23);
            this.buttonCargo.TabIndex = 93;
            this.buttonCargo.Text = "Cargo";
            this.buttonCargo.UseVisualStyleBackColor = true;
            this.buttonCargo.Click += new System.EventHandler(this.buttonCargo_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.meterCargo);
            this.groupBox7.Controls.Add(this.meterFuel);
            this.groupBox7.Controls.Add(this.buttonCargo);
            this.groupBox7.Controls.Add(this.buttonGoto);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.comboFleetsInOrbit);
            this.groupBox7.Location = new System.Drawing.Point(177, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(178, 112);
            this.groupBox7.TabIndex = 33;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Fleets in Orbit";
            // 
            // meterCargo
            // 
            this.meterCargo.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Multi;
            this.meterCargo.CargoLevels = ((Nova.Common.Cargo)(resources.GetObject("meterCargo.CargoLevels")));
            this.meterCargo.Location = new System.Drawing.Point(64, 62);
            this.meterCargo.Name = "meterCargo";
            this.meterCargo.Size = new System.Drawing.Size(108, 15);
            this.meterCargo.TabIndex = 94;
            this.meterCargo.Text = "cargoMeter2";
            // 
            // meterFuel
            // 
            this.meterFuel.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.meterFuel.CargoLevels = ((Nova.Common.Cargo)(resources.GetObject("meterFuel.CargoLevels")));
            this.meterFuel.Location = new System.Drawing.Point(64, 45);
            this.meterFuel.Name = "meterFuel";
            this.meterFuel.Size = new System.Drawing.Size(108, 15);
            this.meterFuel.TabIndex = 38;
            this.meterFuel.Text = "cargoMeter1";
            // 
            // fuel
            // 
            this.fuel.BarColour = System.Drawing.Color.LightGreen;
            this.fuel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fuel.BottomValue = 0D;
            this.fuel.Enabled = false;
            this.fuel.Location = new System.Drawing.Point(63, 47);
            this.fuel.Marker = 0;
            this.fuel.MarkerColour = System.Drawing.Color.Green;
            this.fuel.Maximum = 0D;
            this.fuel.Minimum = 0D;
            this.fuel.Name = "fuel";
            this.fuel.ShowText = true;
            this.fuel.Size = new System.Drawing.Size(100, 10);
            this.fuel.TabIndex = 89;
            this.fuel.TopValue = 0D;
            this.fuel.Units = "mg";
            this.fuel.Value = 0D;
            // 
            // cargoMeter1
            // 
            this.cargoMeter1.Cargo = Nova.WinForms.Gui.Controls.CargoMeter.CargoType.Fuel;
            this.cargoMeter1.CargoLevels = ((Nova.Common.Cargo)(resources.GetObject("cargoMeter1.CargoLevels")));
            this.cargoMeter1.Location = new System.Drawing.Point(64, 45);
            this.cargoMeter1.Name = "cargoMeter1";
            this.cargoMeter1.Size = new System.Drawing.Size(108, 15);
            this.cargoMeter1.TabIndex = 38;
            this.cargoMeter1.Text = "cargoMeter1";
            // 
            // PlanetDetail
            // 
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupPlanetSelect);
            this.Controls.Add(this.starbasePanel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PlanetDetail";
            this.Size = new System.Drawing.Size(355, 390);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.starbasePanel.ResumeLayout(false);
            this.starbasePanel.PerformLayout();
            this.groupPlanetSelect.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

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

        private Label mines;
        private Label label3;
        private Label label5;
        private ListView productionQueue;
        private ColumnHeader description;
        private ColumnHeader quantity;
        private Button changeProductionQueue;
        private GroupBox groupBox1;
        private Label population;
        private Label label6;
        private Label factories;
        private Label label4;
        private Label defenseCoverage;
        private ResourcesOnHandDisplay resourceDisplay;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label scannerRange;
        private Label scannerType;
        private Label defenses;
        private Label defenseType;
        private Label label8;
        private Label label7;
        private Label label2;
        private Label label1;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox starbasePanel;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label massDriverDestination;
        private Label massDriverType;
        private Label starbaseCapacity;
        private Label starbaseDamage;
        private Label starbaseShields;
        private Label starbaseArmor;
        private Button targetButton;
        private Label label11;
        private Label label26;
        private Label label27;
        private Label label12;
        private Label label21;
        private Button nextPlanet;
        private Button previousPlanet;
        private GroupBox groupPlanetSelect;
        private System.ComponentModel.Container components = null;
        private ComboBox comboFleetsInOrbit;
        private Label label10;
        private Label label9;
        private Button buttonGoto;
        private Button buttonCargo;
        private GroupBox groupBox7;
        private Gauge fuel;
        private Controls.CargoMeter meterCargo;
        private Controls.CargoMeter meterFuel;
        private Controls.CargoMeter cargoMeter1;

    }
}
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file defines the main GUI for the component designer.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

namespace Nova.WinForms.ComponentEditor
{
    public partial class ComponentEditorWindow
    {
        /// <Summary>
        /// Required designer variable.
        /// </Summary>
        private System.ComponentModel.IContainer components = null;

        /// <Summary>
        /// Clean up any resources being used.
        /// </Summary>
        /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentEditorWindow));
            this.resetFileLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuStripComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.newComponentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editComponentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardComponentChangesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveComponentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteComponentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.raceRestrictionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.armorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bombToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.electricalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capacitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.energyDampenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuelGenerationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jammerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tachyonDetectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.engineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hullAffinityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radiationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transportOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.massDriverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mechanicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.battleMovementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beamDeflectorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cargoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colonizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mineLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerEfficencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miningRobotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orbitalAdjusterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planetaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planetaryScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terraformingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weaponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedPropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restrictionSummary = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.description = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.componentList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.componentType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.componentName = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.propertyTabs = new System.Windows.Forms.TabControl();
            this.tabArmor = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.armor = new System.Windows.Forms.NumericUpDown();
            this.tabMovement = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.battleMovement = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.tabBomb = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.smartBomb = new System.Windows.Forms.CheckBox();
            this.minimumPopKill = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.installationsDestroyed = new System.Windows.Forms.NumericUpDown();
            this.populationKill = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tabCapacitor = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.beamDamage = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.tabCargo = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cargoCapacity = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.tabCloak = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.cloaking = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tabColonization = new System.Windows.Forms.TabPage();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.orbitalColonizationModule = new System.Windows.Forms.RadioButton();
            this.colonizationModule = new System.Windows.Forms.RadioButton();
            this.tabComputer = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.initiative = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.accuracy = new System.Windows.Forms.NumericUpDown();
            this.tabDefense = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.defenseCover100 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.defenseCover80 = new System.Windows.Forms.Label();
            this.defenseCover40 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.defenseCover1 = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tabDeflector = new System.Windows.Forms.TabPage();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.label92 = new System.Windows.Forms.Label();
            this.beamDeflector = new System.Windows.Forms.NumericUpDown();
            this.label91 = new System.Windows.Forms.Label();
            this.tabEnergyDampener = new System.Windows.Forms.TabPage();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.energyDampener = new System.Windows.Forms.NumericUpDown();
            this.label90 = new System.Windows.Forms.Label();
            this.tabEngine = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.labelFreeWarpValue = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.engineOptimalSpeed = new System.Windows.Forms.NumericUpDown();
            this.label80 = new System.Windows.Forms.Label();
            this.engineFastestSafeSpeed = new System.Windows.Forms.NumericUpDown();
            this.label39 = new System.Windows.Forms.Label();
            this.ramScoopCheckBox = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.warp2Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp3Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp4Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp5Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp6Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp7Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp8Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp9Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp10Fuel = new System.Windows.Forms.NumericUpDown();
            this.warp1Fuel = new System.Windows.Forms.NumericUpDown();
            this.tabFuel = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.fuelCapacity = new System.Windows.Forms.NumericUpDown();
            this.labelFuelGeneration = new System.Windows.Forms.Label();
            this.fuelGeneration = new System.Windows.Forms.NumericUpDown();
            this.lableFuelCapcity = new System.Windows.Forms.Label();
            this.tabGate = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.gateMassInfinite = new System.Windows.Forms.CheckBox();
            this.gateRangeInfinite = new System.Windows.Forms.CheckBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.safeRange = new System.Windows.Forms.NumericUpDown();
            this.safeHullMass = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tabHull = new System.Windows.Forms.TabPage();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.infiniteDock = new System.Windows.Forms.CheckBox();
            this.label86 = new System.Windows.Forms.Label();
            this.alternateRealityMaxPop = new System.Windows.Forms.NumericUpDown();
            this.label87 = new System.Windows.Forms.Label();
            this.buttonEditHull = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.hullCargoCapacity = new System.Windows.Forms.NumericUpDown();
            this.label48 = new System.Windows.Forms.Label();
            this.labelBaseCargoKT = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.labelBaseCargo = new System.Windows.Forms.Label();
            this.hullFuelCapacity = new System.Windows.Forms.NumericUpDown();
            this.label44 = new System.Windows.Forms.Label();
            this.hullInitiative = new System.Windows.Forms.NumericUpDown();
            this.hullDockCapacity = new System.Windows.Forms.NumericUpDown();
            this.hullArmor = new System.Windows.Forms.NumericUpDown();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.tabHullAffinity = new System.Windows.Forms.TabPage();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.componentHullAffinity = new System.Windows.Forms.ComboBox();
            this.tabJammer = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.deflection = new System.Windows.Forms.NumericUpDown();
            this.tabLayerEfficiency = new System.Windows.Forms.TabPage();
            this.improvedMineLayingEfficiency = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.mineLayerEfficiency = new System.Windows.Forms.NumericUpDown();
            this.label93 = new System.Windows.Forms.Label();
            this.tabDriver = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.massDriverSpeed = new System.Windows.Forms.NumericUpDown();
            this.label53 = new System.Windows.Forms.Label();
            this.tabMineLayer = new System.Windows.Forms.TabPage();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.mineMinRamScoopDamage = new System.Windows.Forms.NumericUpDown();
            this.mineMinFleetDamage = new System.Windows.Forms.NumericUpDown();
            this.mineDamagePerRamScoop = new System.Windows.Forms.NumericUpDown();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.mineDamagePerEngine = new System.Windows.Forms.NumericUpDown();
            this.mineHitChance = new System.Windows.Forms.NumericUpDown();
            this.mineSafeSpeed = new System.Windows.Forms.NumericUpDown();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.mineLayingRate = new System.Windows.Forms.NumericUpDown();
            this.label60 = new System.Windows.Forms.Label();
            this.tabRadiation = new System.Windows.Forms.TabPage();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.radiation = new System.Windows.Forms.NumericUpDown();
            this.label78 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.tabRobot = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.miningRate = new System.Windows.Forms.NumericUpDown();
            this.label61 = new System.Windows.Forms.Label();
            this.tabOrbitalAdjuster = new System.Windows.Forms.TabPage();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.label64 = new System.Windows.Forms.Label();
            this.adjusterRate = new System.Windows.Forms.NumericUpDown();
            this.label63 = new System.Windows.Forms.Label();
            this.tabScanner = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.normalRange = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.penetratingRange = new System.Windows.Forms.NumericUpDown();
            this.label34 = new System.Windows.Forms.Label();
            this.tabShield = new System.Windows.Forms.TabPage();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.label65 = new System.Windows.Forms.Label();
            this.shield = new System.Windows.Forms.NumericUpDown();
            this.label66 = new System.Windows.Forms.Label();
            this.tabTachyonDetector = new System.Windows.Forms.TabPage();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.tachyonDetector = new System.Windows.Forms.NumericUpDown();
            this.label89 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.tabTerraforming = new System.Windows.Forms.TabPage();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.radiationMod = new System.Windows.Forms.NumericUpDown();
            this.temperatureMod = new System.Windows.Forms.NumericUpDown();
            this.gravityMod = new System.Windows.Forms.NumericUpDown();
            this.label69 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.tabTransportShipsOnly = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabWeapon = new System.Windows.Forms.TabPage();
            this.groupBoxWeaponType = new System.Windows.Forms.GroupBox();
            this.isMissile = new System.Windows.Forms.RadioButton();
            this.isTorpedo = new System.Windows.Forms.RadioButton();
            this.isGattling = new System.Windows.Forms.RadioButton();
            this.isSapper = new System.Windows.Forms.RadioButton();
            this.isStandardBeam = new System.Windows.Forms.RadioButton();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.minesSwept = new System.Windows.Forms.Label();
            this.weaponAccuracy = new System.Windows.Forms.NumericUpDown();
            this.weaponRange = new System.Windows.Forms.NumericUpDown();
            this.weaponInitiative = new System.Windows.Forms.NumericUpDown();
            this.weaponPower = new System.Windows.Forms.NumericUpDown();
            this.label70 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.componentImage = new Nova.WinForms.ComponentEditor.ImageDisplay();
            this.techRequirements = new Nova.WinForms.ComponentEditor.TechRequirements();
            this.basicProperties = new Nova.WinForms.ComponentEditor.BasicProperties();
            this.mainMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.propertyTabs.SuspendLayout();
            this.tabArmor.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.armor)).BeginInit();
            this.tabMovement.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.battleMovement)).BeginInit();
            this.tabBomb.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimumPopKill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.installationsDestroyed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationKill)).BeginInit();
            this.tabCapacitor.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beamDamage)).BeginInit();
            this.tabCargo.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cargoCapacity)).BeginInit();
            this.tabCloak.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cloaking)).BeginInit();
            this.tabColonization.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabComputer.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.initiative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracy)).BeginInit();
            this.tabDefense.SuspendLayout();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defenseCover1)).BeginInit();
            this.tabDeflector.SuspendLayout();
            this.groupBox32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beamDeflector)).BeginInit();
            this.tabEnergyDampener.SuspendLayout();
            this.groupBox31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.energyDampener)).BeginInit();
            this.tabEngine.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.engineOptimalSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.engineFastestSafeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp2Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp3Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp4Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp5Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp6Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp7Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp8Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp9Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp10Fuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp1Fuel)).BeginInit();
            this.tabFuel.SuspendLayout();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fuelCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuelGeneration)).BeginInit();
            this.tabGate.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.safeRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.safeHullMass)).BeginInit();
            this.tabHull.SuspendLayout();
            this.groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alternateRealityMaxPop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullCargoCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullFuelCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullInitiative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullDockCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullArmor)).BeginInit();
            this.tabHullAffinity.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.tabJammer.SuspendLayout();
            this.groupBox20.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deflection)).BeginInit();
            this.tabLayerEfficiency.SuspendLayout();
            this.improvedMineLayingEfficiency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mineLayerEfficiency)).BeginInit();
            this.tabDriver.SuspendLayout();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.massDriverSpeed)).BeginInit();
            this.tabMineLayer.SuspendLayout();
            this.groupBox22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mineMinRamScoopDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineMinFleetDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineDamagePerRamScoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineDamagePerEngine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineHitChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineSafeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineLayingRate)).BeginInit();
            this.tabRadiation.SuspendLayout();
            this.groupBox28.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiation)).BeginInit();
            this.tabRobot.SuspendLayout();
            this.groupBox23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.miningRate)).BeginInit();
            this.tabOrbitalAdjuster.SuspendLayout();
            this.groupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adjusterRate)).BeginInit();
            this.tabScanner.SuspendLayout();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.normalRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penetratingRange)).BeginInit();
            this.tabShield.SuspendLayout();
            this.groupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shield)).BeginInit();
            this.tabTachyonDetector.SuspendLayout();
            this.groupBox30.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tachyonDetector)).BeginInit();
            this.tabTerraforming.SuspendLayout();
            this.groupBox26.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiationMod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureMod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gravityMod)).BeginInit();
            this.tabTransportShipsOnly.SuspendLayout();
            this.tabWeapon.SuspendLayout();
            this.groupBoxWeaponType.SuspendLayout();
            this.groupBox27.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponInitiative)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponPower)).BeginInit();
            this.SuspendLayout();
            // 
            // resetFileLocation
            // 
            this.resetFileLocation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileMenuItem,
            this.openFileMenuItem,
            this.saveFileMenuItem,
            this.saveFileAsMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.resetFileLocation.Name = "resetFileLocation";
            this.resetFileLocation.Size = new System.Drawing.Size(37, 20);
            this.resetFileLocation.Text = "&File";
            // 
            // newFileMenuItem
            // 
            this.newFileMenuItem.Name = "newFileMenuItem";
            this.newFileMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newFileMenuItem.Text = "&New";
            this.newFileMenuItem.Click += new System.EventHandler(this.MenuItem_NewFile_Click);
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openFileMenuItem.Text = "&Open";
            this.openFileMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveFileMenuItem.Text = "&Save";
            this.saveFileMenuItem.Click += new System.EventHandler(this.MenuItem_SaveFile_Click);
            // 
            // saveFileAsMenuItem
            // 
            this.saveFileAsMenuItem.Name = "saveFileAsMenuItem";
            this.saveFileAsMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveFileAsMenuItem.Text = "Save &As";
            this.saveFileAsMenuItem.Click += new System.EventHandler(this.MenuItem_SaveFileAs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(111, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutMenuClick);
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetFileLocation,
            this.menuStripComponent,
            this.propertyToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(904, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menuStripComponent
            // 
            this.menuStripComponent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newComponentMenuItem,
            this.editComponentMenuItem,
            this.copyToolStripMenuItem,
            this.discardComponentChangesMenuItem,
            this.saveComponentMenuItem,
            this.toolStripMenuItem2,
            this.reloadToolStripMenuItem,
            this.deleteComponentMenuItem,
            this.toolStripMenuItem1,
            this.raceRestrictionsMenuItem});
            this.menuStripComponent.Name = "menuStripComponent";
            this.menuStripComponent.Size = new System.Drawing.Size(83, 20);
            this.menuStripComponent.Text = "&Component";
            // 
            // newComponentMenuItem
            // 
            this.newComponentMenuItem.Name = "newComponentMenuItem";
            this.newComponentMenuItem.Size = new System.Drawing.Size(163, 22);
            this.newComponentMenuItem.Text = "&New";
            this.newComponentMenuItem.Click += new System.EventHandler(this.MenuItem_NewComponent_Click);
            // 
            // editComponentMenuItem
            // 
            this.editComponentMenuItem.Name = "editComponentMenuItem";
            this.editComponentMenuItem.Size = new System.Drawing.Size(163, 22);
            this.editComponentMenuItem.Text = "&Edit";
            this.editComponentMenuItem.Click += new System.EventHandler(this.MenuItem_EditComponent_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // discardComponentChangesMenuItem
            // 
            this.discardComponentChangesMenuItem.Name = "discardComponentChangesMenuItem";
            this.discardComponentChangesMenuItem.Size = new System.Drawing.Size(163, 22);
            this.discardComponentChangesMenuItem.Text = "Discard Changes";
            this.discardComponentChangesMenuItem.Click += new System.EventHandler(this.MenuItem_DiscardComponentChanges_Click);
            // 
            // saveComponentMenuItem
            // 
            this.saveComponentMenuItem.Name = "saveComponentMenuItem";
            this.saveComponentMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveComponentMenuItem.Text = "&Save";
            this.saveComponentMenuItem.Click += new System.EventHandler(this.SaveComponent_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(160, 6);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            // 
            // deleteComponentMenuItem
            // 
            this.deleteComponentMenuItem.Name = "deleteComponentMenuItem";
            this.deleteComponentMenuItem.Size = new System.Drawing.Size(163, 22);
            this.deleteComponentMenuItem.Text = "Delete";
            this.deleteComponentMenuItem.Click += new System.EventHandler(this.MenuItem_DeleteComponent_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(160, 6);
            // 
            // raceRestrictionsMenuItem
            // 
            this.raceRestrictionsMenuItem.Name = "raceRestrictionsMenuItem";
            this.raceRestrictionsMenuItem.Size = new System.Drawing.Size(163, 22);
            this.raceRestrictionsMenuItem.Text = "&Race Restrictions";
            this.raceRestrictionsMenuItem.Click += new System.EventHandler(this.MenuItem_RaceRestrictions_Click);
            // 
            // propertyToolStripMenuItem
            // 
            this.propertyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPropertyToolStripMenuItem,
            this.deleteSelectedPropertyToolStripMenuItem});
            this.propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            this.propertyToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.propertyToolStripMenuItem.Text = "&Property";
            // 
            // addPropertyToolStripMenuItem
            // 
            this.addPropertyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.armorToolStripMenuItem,
            this.bombToolStripMenuItem,
            this.cloakToolStripMenuItem,
            this.electricalToolStripMenuItem,
            this.engineToolStripMenuItem,
            this.fuelToolStripMenuItem,
            this.gateToolStripMenuItem,
            this.hullToolStripMenuItem,
            this.limitationToolStripMenuItem,
            this.massDriverToolStripMenuItem,
            this.mechanicalToolStripMenuItem,
            this.mineLayerToolStripMenuItem,
            this.layerEfficencyToolStripMenuItem,
            this.miningRobotToolStripMenuItem,
            this.orbitalAdjusterToolStripMenuItem,
            this.planetaryToolStripMenuItem,
            this.scannerToolStripMenuItem,
            this.shieldToolStripMenuItem,
            this.weaponToolStripMenuItem});
            this.addPropertyToolStripMenuItem.Name = "addPropertyToolStripMenuItem";
            this.addPropertyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addPropertyToolStripMenuItem.Text = "Add";
            // 
            // armorToolStripMenuItem
            // 
            this.armorToolStripMenuItem.Name = "armorToolStripMenuItem";
            this.armorToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.armorToolStripMenuItem.Text = "&Armor";
            this.armorToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // bombToolStripMenuItem
            // 
            this.bombToolStripMenuItem.Name = "bombToolStripMenuItem";
            this.bombToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.bombToolStripMenuItem.Text = "&Bomb";
            this.bombToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // cloakToolStripMenuItem
            // 
            this.cloakToolStripMenuItem.Name = "cloakToolStripMenuItem";
            this.cloakToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.cloakToolStripMenuItem.Text = "&Cloak";
            this.cloakToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // electricalToolStripMenuItem
            // 
            this.electricalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.capacitorToolStripMenuItem,
            this.computerToolStripMenuItem,
            this.energyDampenerToolStripMenuItem,
            this.fuelGenerationToolStripMenuItem,
            this.jammerToolStripMenuItem,
            this.tachyonDetectorToolStripMenuItem});
            this.electricalToolStripMenuItem.Name = "electricalToolStripMenuItem";
            this.electricalToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.electricalToolStripMenuItem.Text = "Electrical";
            // 
            // capacitorToolStripMenuItem
            // 
            this.capacitorToolStripMenuItem.Name = "capacitorToolStripMenuItem";
            this.capacitorToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.capacitorToolStripMenuItem.Text = "Capacitor";
            this.capacitorToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // computerToolStripMenuItem
            // 
            this.computerToolStripMenuItem.Name = "computerToolStripMenuItem";
            this.computerToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.computerToolStripMenuItem.Text = "Computer";
            this.computerToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // energyDampenerToolStripMenuItem
            // 
            this.energyDampenerToolStripMenuItem.Name = "energyDampenerToolStripMenuItem";
            this.energyDampenerToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.energyDampenerToolStripMenuItem.Text = "Energy Dampener";
            this.energyDampenerToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // fuelGenerationToolStripMenuItem
            // 
            this.fuelGenerationToolStripMenuItem.Name = "fuelGenerationToolStripMenuItem";
            this.fuelGenerationToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.fuelGenerationToolStripMenuItem.Text = "Fuel";
            this.fuelGenerationToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // jammerToolStripMenuItem
            // 
            this.jammerToolStripMenuItem.Name = "jammerToolStripMenuItem";
            this.jammerToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.jammerToolStripMenuItem.Text = "Jammer";
            this.jammerToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // tachyonDetectorToolStripMenuItem
            // 
            this.tachyonDetectorToolStripMenuItem.Name = "tachyonDetectorToolStripMenuItem";
            this.tachyonDetectorToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.tachyonDetectorToolStripMenuItem.Text = "Tachyon Detector";
            this.tachyonDetectorToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // engineToolStripMenuItem
            // 
            this.engineToolStripMenuItem.Name = "engineToolStripMenuItem";
            this.engineToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.engineToolStripMenuItem.Text = "&Engine";
            this.engineToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // fuelToolStripMenuItem
            // 
            this.fuelToolStripMenuItem.Name = "fuelToolStripMenuItem";
            this.fuelToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.fuelToolStripMenuItem.Text = "&Fuel";
            this.fuelToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // gateToolStripMenuItem
            // 
            this.gateToolStripMenuItem.Name = "gateToolStripMenuItem";
            this.gateToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.gateToolStripMenuItem.Text = "&Gate";
            this.gateToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // hullToolStripMenuItem
            // 
            this.hullToolStripMenuItem.Name = "hullToolStripMenuItem";
            this.hullToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.hullToolStripMenuItem.Text = "&Hull";
            this.hullToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // limitationToolStripMenuItem
            // 
            this.limitationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hullAffinityToolStripMenuItem,
            this.radiationToolStripMenuItem,
            this.transportOnlyToolStripMenuItem});
            this.limitationToolStripMenuItem.Name = "limitationToolStripMenuItem";
            this.limitationToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.limitationToolStripMenuItem.Text = "Limitation";
            // 
            // hullAffinityToolStripMenuItem
            // 
            this.hullAffinityToolStripMenuItem.Name = "hullAffinityToolStripMenuItem";
            this.hullAffinityToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.hullAffinityToolStripMenuItem.Text = "Hull Affinity";
            this.hullAffinityToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // radiationToolStripMenuItem
            // 
            this.radiationToolStripMenuItem.Name = "radiationToolStripMenuItem";
            this.radiationToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.radiationToolStripMenuItem.Text = "Radiation";
            this.radiationToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // transportOnlyToolStripMenuItem
            // 
            this.transportOnlyToolStripMenuItem.Name = "transportOnlyToolStripMenuItem";
            this.transportOnlyToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.transportOnlyToolStripMenuItem.Text = "Transport Only";
            this.transportOnlyToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // massDriverToolStripMenuItem
            // 
            this.massDriverToolStripMenuItem.Name = "massDriverToolStripMenuItem";
            this.massDriverToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.massDriverToolStripMenuItem.Text = "Mass &Driver";
            this.massDriverToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // mechanicalToolStripMenuItem
            // 
            this.mechanicalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.battleMovementToolStripMenuItem,
            this.beamDeflectorToolStripMenuItem1,
            this.cargoToolStripMenuItem,
            this.colonizationToolStripMenuItem,
            this.fuelToolStripMenuItem1});
            this.mechanicalToolStripMenuItem.Name = "mechanicalToolStripMenuItem";
            this.mechanicalToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.mechanicalToolStripMenuItem.Text = "Mechanical";
            // 
            // battleMovementToolStripMenuItem
            // 
            this.battleMovementToolStripMenuItem.Name = "battleMovementToolStripMenuItem";
            this.battleMovementToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.battleMovementToolStripMenuItem.Text = "Battle Movement";
            this.battleMovementToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // beamDeflectorToolStripMenuItem1
            // 
            this.beamDeflectorToolStripMenuItem1.Name = "beamDeflectorToolStripMenuItem1";
            this.beamDeflectorToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.beamDeflectorToolStripMenuItem1.Text = "Beam Deflector";
            this.beamDeflectorToolStripMenuItem1.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // cargoToolStripMenuItem
            // 
            this.cargoToolStripMenuItem.Name = "cargoToolStripMenuItem";
            this.cargoToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.cargoToolStripMenuItem.Text = "Cargo";
            this.cargoToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // colonizationToolStripMenuItem
            // 
            this.colonizationToolStripMenuItem.Name = "colonizationToolStripMenuItem";
            this.colonizationToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.colonizationToolStripMenuItem.Text = "Colonization";
            this.colonizationToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // fuelToolStripMenuItem1
            // 
            this.fuelToolStripMenuItem1.Name = "fuelToolStripMenuItem1";
            this.fuelToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.fuelToolStripMenuItem1.Text = "Fuel";
            this.fuelToolStripMenuItem1.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // mineLayerToolStripMenuItem
            // 
            this.mineLayerToolStripMenuItem.Name = "mineLayerToolStripMenuItem";
            this.mineLayerToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.mineLayerToolStripMenuItem.Text = "Mine &Layer";
            this.mineLayerToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // layerEfficencyToolStripMenuItem
            // 
            this.layerEfficencyToolStripMenuItem.Name = "layerEfficencyToolStripMenuItem";
            this.layerEfficencyToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.layerEfficencyToolStripMenuItem.Text = "Mine Layer Efficiency";
            this.layerEfficencyToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // miningRobotToolStripMenuItem
            // 
            this.miningRobotToolStripMenuItem.Name = "miningRobotToolStripMenuItem";
            this.miningRobotToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.miningRobotToolStripMenuItem.Text = "Mining &Robot";
            this.miningRobotToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // orbitalAdjusterToolStripMenuItem
            // 
            this.orbitalAdjusterToolStripMenuItem.Name = "orbitalAdjusterToolStripMenuItem";
            this.orbitalAdjusterToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.orbitalAdjusterToolStripMenuItem.Text = "Orbital Adjuster";
            this.orbitalAdjusterToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // planetaryToolStripMenuItem
            // 
            this.planetaryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defenseToolStripMenuItem,
            this.planetaryScannerToolStripMenuItem,
            this.terraformingToolStripMenuItem});
            this.planetaryToolStripMenuItem.Name = "planetaryToolStripMenuItem";
            this.planetaryToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.planetaryToolStripMenuItem.Text = "Planetary";
            // 
            // defenseToolStripMenuItem
            // 
            this.defenseToolStripMenuItem.Name = "defenseToolStripMenuItem";
            this.defenseToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.defenseToolStripMenuItem.Text = "Defense";
            this.defenseToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // planetaryScannerToolStripMenuItem
            // 
            this.planetaryScannerToolStripMenuItem.Name = "planetaryScannerToolStripMenuItem";
            this.planetaryScannerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.planetaryScannerToolStripMenuItem.Text = "Scanner";
            this.planetaryScannerToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // terraformingToolStripMenuItem
            // 
            this.terraformingToolStripMenuItem.Name = "terraformingToolStripMenuItem";
            this.terraformingToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.terraformingToolStripMenuItem.Text = "Terraforming";
            this.terraformingToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // scannerToolStripMenuItem
            // 
            this.scannerToolStripMenuItem.Name = "scannerToolStripMenuItem";
            this.scannerToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.scannerToolStripMenuItem.Text = "&Scanner";
            this.scannerToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // shieldToolStripMenuItem
            // 
            this.shieldToolStripMenuItem.Name = "shieldToolStripMenuItem";
            this.shieldToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.shieldToolStripMenuItem.Text = "Shield";
            this.shieldToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // weaponToolStripMenuItem
            // 
            this.weaponToolStripMenuItem.Name = "weaponToolStripMenuItem";
            this.weaponToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.weaponToolStripMenuItem.Text = "&Weapon";
            this.weaponToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_AddProperty);
            // 
            // deleteSelectedPropertyToolStripMenuItem
            // 
            this.deleteSelectedPropertyToolStripMenuItem.Name = "deleteSelectedPropertyToolStripMenuItem";
            this.deleteSelectedPropertyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.deleteSelectedPropertyToolStripMenuItem.Text = "&Delete Selected Property";
            this.deleteSelectedPropertyToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedPropertyToolStripMenuItem_Click);
            // 
            // restrictionSummary
            // 
            this.restrictionSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.restrictionSummary, 2);
            this.restrictionSummary.ForeColor = System.Drawing.Color.Maroon;
            this.restrictionSummary.Location = new System.Drawing.Point(403, 178);
            this.restrictionSummary.Multiline = true;
            this.restrictionSummary.Name = "restrictionSummary";
            this.restrictionSummary.ReadOnly = true;
            this.restrictionSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.restrictionSummary.Size = new System.Drawing.Size(498, 82);
            this.restrictionSummary.TabIndex = 19;
            this.restrictionSummary.Text = "This component only available to the primary racial trait \'Inner Strength\'.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.restrictionSummary, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.componentImage, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.techRequirements, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.basicProperties, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(904, 528);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(904, 528);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.description);
            this.groupBox3.Location = new System.Drawing.Point(403, 3);
            this.groupBox3.Name = "groupBox3";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox3, 2);
            this.groupBox3.Size = new System.Drawing.Size(373, 169);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Description";
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.description.Location = new System.Drawing.Point(7, 16);
            this.description.Multiline = true;
            this.description.Name = "description";
            this.description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.description.Size = new System.Drawing.Size(360, 147);
            this.description.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.componentList);
            this.groupBox4.Location = new System.Drawing.Point(3, 53);
            this.groupBox4.Name = "groupBox4";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox4, 3);
            this.groupBox4.Size = new System.Drawing.Size(194, 472);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Component List";
            // 
            // componentList
            // 
            this.componentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.componentList.FormattingEnabled = true;
            this.componentList.Location = new System.Drawing.Point(3, 16);
            this.componentList.Name = "componentList";
            this.componentList.Size = new System.Drawing.Size(188, 446);
            this.componentList.Sorted = true;
            this.componentList.TabIndex = 8;
            this.componentList.SelectedIndexChanged += new System.EventHandler(this.ComponentList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.componentType);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 44);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Component Type";
            // 
            // componentType
            // 
            this.componentType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.componentType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.componentType.FormattingEnabled = true;
            this.componentType.Items.AddRange(new object[] {
            "Armor",
            "Beam Weapons",
            "Bomb",
            "Electrical",
            "Engine",
            "Hull",
            "Mechanical",
            "Mine Layer",
            "Mining Robot",
            "Orbital",
            "Planetary Installations",
            "Scanner",
            "Shield",
            "Terraforming",
            "Torpedoes"});
            this.componentType.Location = new System.Drawing.Point(14, 15);
            this.componentType.MaxDropDownItems = 15;
            this.componentType.Name = "componentType";
            this.componentType.Size = new System.Drawing.Size(174, 21);
            this.componentType.Sorted = true;
            this.componentType.TabIndex = 0;
            this.componentType.SelectedIndexChanged += new System.EventHandler(this.ComponentType_Changed);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.componentName);
            this.groupBox2.Location = new System.Drawing.Point(203, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 44);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Name";
            // 
            // componentName
            // 
            this.componentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.componentName.Location = new System.Drawing.Point(3, 16);
            this.componentName.Name = "componentName";
            this.componentName.Size = new System.Drawing.Size(188, 20);
            this.componentName.TabIndex = 7;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox5, 2);
            this.groupBox5.Controls.Add(this.propertyTabs);
            this.groupBox5.Location = new System.Drawing.Point(403, 266);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(498, 259);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Component Properties";
            // 
            // propertyTabs
            // 
            this.propertyTabs.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.propertyTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyTabs.Controls.Add(this.tabArmor);
            this.propertyTabs.Controls.Add(this.tabMovement);
            this.propertyTabs.Controls.Add(this.tabBomb);
            this.propertyTabs.Controls.Add(this.tabCapacitor);
            this.propertyTabs.Controls.Add(this.tabCargo);
            this.propertyTabs.Controls.Add(this.tabCloak);
            this.propertyTabs.Controls.Add(this.tabColonization);
            this.propertyTabs.Controls.Add(this.tabComputer);
            this.propertyTabs.Controls.Add(this.tabDefense);
            this.propertyTabs.Controls.Add(this.tabDeflector);
            this.propertyTabs.Controls.Add(this.tabEnergyDampener);
            this.propertyTabs.Controls.Add(this.tabEngine);
            this.propertyTabs.Controls.Add(this.tabFuel);
            this.propertyTabs.Controls.Add(this.tabGate);
            this.propertyTabs.Controls.Add(this.tabHull);
            this.propertyTabs.Controls.Add(this.tabHullAffinity);
            this.propertyTabs.Controls.Add(this.tabJammer);
            this.propertyTabs.Controls.Add(this.tabLayerEfficiency);
            this.propertyTabs.Controls.Add(this.tabDriver);
            this.propertyTabs.Controls.Add(this.tabMineLayer);
            this.propertyTabs.Controls.Add(this.tabRadiation);
            this.propertyTabs.Controls.Add(this.tabRobot);
            this.propertyTabs.Controls.Add(this.tabOrbitalAdjuster);
            this.propertyTabs.Controls.Add(this.tabScanner);
            this.propertyTabs.Controls.Add(this.tabShield);
            this.propertyTabs.Controls.Add(this.tabTachyonDetector);
            this.propertyTabs.Controls.Add(this.tabTerraforming);
            this.propertyTabs.Controls.Add(this.tabTransportShipsOnly);
            this.propertyTabs.Controls.Add(this.tabWeapon);
            this.propertyTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.propertyTabs.ItemSize = new System.Drawing.Size(25, 100);
            this.propertyTabs.Location = new System.Drawing.Point(3, 19);
            this.propertyTabs.Multiline = true;
            this.propertyTabs.Name = "propertyTabs";
            this.propertyTabs.SelectedIndex = 0;
            this.propertyTabs.Size = new System.Drawing.Size(776, 238);
            this.propertyTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.propertyTabs.TabIndex = 4;
            this.propertyTabs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.PropertyTabs_DrawItem);
            // 
            // tabArmor
            // 
            this.tabArmor.BackColor = System.Drawing.SystemColors.Control;
            this.tabArmor.Controls.Add(this.groupBox8);
            this.tabArmor.Location = new System.Drawing.Point(4, 4);
            this.tabArmor.Name = "tabArmor";
            this.tabArmor.Padding = new System.Windows.Forms.Padding(3);
            this.tabArmor.Size = new System.Drawing.Size(368, 230);
            this.tabArmor.TabIndex = 0;
            this.tabArmor.Text = "Armor";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox8.Controls.Add(this.label79);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.armor);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(362, 224);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Armor Properties";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(160, 36);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(19, 13);
            this.label79.TabIndex = 2;
            this.label79.Text = "dp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Armor Value";
            // 
            // armor
            // 
            this.armor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.armor.Location = new System.Drawing.Point(77, 34);
            this.armor.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.armor.Name = "armor";
            this.armor.Size = new System.Drawing.Size(77, 20);
            this.armor.TabIndex = 1;
            this.armor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabMovement
            // 
            this.tabMovement.BackColor = System.Drawing.Color.Transparent;
            this.tabMovement.Controls.Add(this.groupBox9);
            this.tabMovement.Location = new System.Drawing.Point(4, 4);
            this.tabMovement.Name = "tabMovement";
            this.tabMovement.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovement.Size = new System.Drawing.Size(368, 230);
            this.tabMovement.TabIndex = 1;
            this.tabMovement.Text = "Battle Movement";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.battleMovement);
            this.groupBox9.Controls.Add(this.label14);
            this.groupBox9.Location = new System.Drawing.Point(11, 13);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(346, 207);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Battle Movement Property";
            // 
            // battleMovement
            // 
            this.battleMovement.DecimalPlaces = 2;
            this.battleMovement.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.battleMovement.Location = new System.Drawing.Point(112, 26);
            this.battleMovement.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.battleMovement.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.battleMovement.Name = "battleMovement";
            this.battleMovement.Size = new System.Drawing.Size(57, 20);
            this.battleMovement.TabIndex = 1;
            this.battleMovement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Battle Movement";
            // 
            // tabBomb
            // 
            this.tabBomb.BackColor = System.Drawing.SystemColors.Control;
            this.tabBomb.Controls.Add(this.groupBox10);
            this.tabBomb.Location = new System.Drawing.Point(4, 4);
            this.tabBomb.Name = "tabBomb";
            this.tabBomb.Padding = new System.Windows.Forms.Padding(3);
            this.tabBomb.Size = new System.Drawing.Size(368, 230);
            this.tabBomb.TabIndex = 2;
            this.tabBomb.Text = "Bomb";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.smartBomb);
            this.groupBox10.Controls.Add(this.minimumPopKill);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.installationsDestroyed);
            this.groupBox10.Controls.Add(this.populationKill);
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Location = new System.Drawing.Point(11, 13);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(346, 202);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Bomb Properties";
            // 
            // smartBomb
            // 
            this.smartBomb.AutoSize = true;
            this.smartBomb.Location = new System.Drawing.Point(9, 106);
            this.smartBomb.Name = "smartBomb";
            this.smartBomb.Size = new System.Drawing.Size(83, 17);
            this.smartBomb.TabIndex = 10;
            this.smartBomb.Text = "Smart Bomb";
            this.smartBomb.UseVisualStyleBackColor = true;
            // 
            // minimumPopKill
            // 
            this.minimumPopKill.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.minimumPopKill.Location = new System.Drawing.Point(112, 53);
            this.minimumPopKill.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minimumPopKill.Name = "minimumPopKill";
            this.minimumPopKill.Size = new System.Drawing.Size(56, 20);
            this.minimumPopKill.TabIndex = 8;
            this.minimumPopKill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(64, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Minimum Kill";
            // 
            // installationsDestroyed
            // 
            this.installationsDestroyed.Location = new System.Drawing.Point(112, 79);
            this.installationsDestroyed.Name = "installationsDestroyed";
            this.installationsDestroyed.Size = new System.Drawing.Size(56, 20);
            this.installationsDestroyed.TabIndex = 9;
            this.installationsDestroyed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // populationKill
            // 
            this.populationKill.DecimalPlaces = 1;
            this.populationKill.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.populationKill.Location = new System.Drawing.Point(112, 27);
            this.populationKill.Name = "populationKill";
            this.populationKill.Size = new System.Drawing.Size(56, 20);
            this.populationKill.TabIndex = 7;
            this.populationKill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Installations";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 5;
            this.label19.Text = "Population Kill %";
            // 
            // tabCapacitor
            // 
            this.tabCapacitor.BackColor = System.Drawing.SystemColors.Control;
            this.tabCapacitor.Controls.Add(this.groupBox11);
            this.tabCapacitor.Location = new System.Drawing.Point(4, 4);
            this.tabCapacitor.Name = "tabCapacitor";
            this.tabCapacitor.Size = new System.Drawing.Size(368, 230);
            this.tabCapacitor.TabIndex = 3;
            this.tabCapacitor.Text = "Capacitor";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label21);
            this.groupBox11.Controls.Add(this.beamDamage);
            this.groupBox11.Controls.Add(this.label20);
            this.groupBox11.Location = new System.Drawing.Point(8, 13);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(349, 207);
            this.groupBox11.TabIndex = 0;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Capacitor Properties";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(166, 29);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "%";
            // 
            // beamDamage
            // 
            this.beamDamage.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.beamDamage.Location = new System.Drawing.Point(98, 27);
            this.beamDamage.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.beamDamage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.beamDamage.Name = "beamDamage";
            this.beamDamage.Size = new System.Drawing.Size(62, 20);
            this.beamDamage.TabIndex = 1;
            this.beamDamage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(77, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Beam Damage";
            // 
            // tabCargo
            // 
            this.tabCargo.BackColor = System.Drawing.SystemColors.Control;
            this.tabCargo.Controls.Add(this.groupBox12);
            this.tabCargo.Location = new System.Drawing.Point(4, 4);
            this.tabCargo.Name = "tabCargo";
            this.tabCargo.Size = new System.Drawing.Size(368, 230);
            this.tabCargo.TabIndex = 4;
            this.tabCargo.Text = "Cargo";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label23);
            this.groupBox12.Controls.Add(this.cargoCapacity);
            this.groupBox12.Controls.Add(this.label22);
            this.groupBox12.Location = new System.Drawing.Point(10, 13);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(347, 206);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Cargo Properties";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(184, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(20, 13);
            this.label23.TabIndex = 2;
            this.label23.Text = "kT";
            // 
            // cargoCapacity
            // 
            this.cargoCapacity.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.cargoCapacity.Location = new System.Drawing.Point(100, 27);
            this.cargoCapacity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cargoCapacity.Name = "cargoCapacity";
            this.cargoCapacity.Size = new System.Drawing.Size(78, 20);
            this.cargoCapacity.TabIndex = 1;
            this.cargoCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 29);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "Cargo Capacity";
            // 
            // tabCloak
            // 
            this.tabCloak.BackColor = System.Drawing.SystemColors.Control;
            this.tabCloak.Controls.Add(this.groupBox13);
            this.tabCloak.Location = new System.Drawing.Point(4, 4);
            this.tabCloak.Name = "tabCloak";
            this.tabCloak.Size = new System.Drawing.Size(368, 230);
            this.tabCloak.TabIndex = 5;
            this.tabCloak.Text = "Cloak";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.cloaking);
            this.groupBox13.Controls.Add(this.label25);
            this.groupBox13.Controls.Add(this.label24);
            this.groupBox13.Location = new System.Drawing.Point(7, 7);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(350, 214);
            this.groupBox13.TabIndex = 5;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Cloaking Properties";
            // 
            // cloaking
            // 
            this.cloaking.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cloaking.Location = new System.Drawing.Point(76, 33);
            this.cloaking.Name = "cloaking";
            this.cloaking.Size = new System.Drawing.Size(62, 20);
            this.cloaking.TabIndex = 3;
            this.cloaking.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(144, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "%";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 2;
            this.label24.Text = "Cloaking";
            // 
            // tabColonization
            // 
            this.tabColonization.BackColor = System.Drawing.SystemColors.Control;
            this.tabColonization.Controls.Add(this.groupBox14);
            this.tabColonization.Location = new System.Drawing.Point(4, 4);
            this.tabColonization.Name = "tabColonization";
            this.tabColonization.Size = new System.Drawing.Size(368, 230);
            this.tabColonization.TabIndex = 6;
            this.tabColonization.Text = "Colonization";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.orbitalColonizationModule);
            this.groupBox14.Controls.Add(this.colonizationModule);
            this.groupBox14.Location = new System.Drawing.Point(9, 9);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(348, 214);
            this.groupBox14.TabIndex = 0;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Colonization Properties";
            // 
            // orbitalColonizationModule
            // 
            this.orbitalColonizationModule.AutoSize = true;
            this.orbitalColonizationModule.Location = new System.Drawing.Point(6, 52);
            this.orbitalColonizationModule.Name = "orbitalColonizationModule";
            this.orbitalColonizationModule.Size = new System.Drawing.Size(153, 17);
            this.orbitalColonizationModule.TabIndex = 1;
            this.orbitalColonizationModule.Text = "Orbital Colonization Module";
            this.orbitalColonizationModule.UseVisualStyleBackColor = true;
            // 
            // colonizationModule
            // 
            this.colonizationModule.AutoSize = true;
            this.colonizationModule.Checked = true;
            this.colonizationModule.Location = new System.Drawing.Point(6, 29);
            this.colonizationModule.Name = "colonizationModule";
            this.colonizationModule.Size = new System.Drawing.Size(120, 17);
            this.colonizationModule.TabIndex = 0;
            this.colonizationModule.TabStop = true;
            this.colonizationModule.Text = "Colonization Module";
            this.colonizationModule.UseVisualStyleBackColor = true;
            // 
            // tabComputer
            // 
            this.tabComputer.BackColor = System.Drawing.SystemColors.Control;
            this.tabComputer.Controls.Add(this.groupBox15);
            this.tabComputer.Location = new System.Drawing.Point(4, 4);
            this.tabComputer.Name = "tabComputer";
            this.tabComputer.Size = new System.Drawing.Size(368, 230);
            this.tabComputer.TabIndex = 7;
            this.tabComputer.Text = "Computer";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label26);
            this.groupBox15.Controls.Add(this.label2);
            this.groupBox15.Controls.Add(this.initiative);
            this.groupBox15.Controls.Add(this.label3);
            this.groupBox15.Controls.Add(this.accuracy);
            this.groupBox15.Location = new System.Drawing.Point(8, 11);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(352, 209);
            this.groupBox15.TabIndex = 3;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Computer Properties";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(163, 31);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(15, 13);
            this.label26.TabIndex = 3;
            this.label26.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Accuracy";
            // 
            // initiative
            // 
            this.initiative.Location = new System.Drawing.Point(80, 55);
            this.initiative.Name = "initiative";
            this.initiative.Size = new System.Drawing.Size(76, 20);
            this.initiative.TabIndex = 2;
            this.initiative.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Initiative";
            // 
            // accuracy
            // 
            this.accuracy.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.accuracy.Location = new System.Drawing.Point(80, 29);
            this.accuracy.Name = "accuracy";
            this.accuracy.Size = new System.Drawing.Size(77, 20);
            this.accuracy.TabIndex = 1;
            this.accuracy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabDefense
            // 
            this.tabDefense.BackColor = System.Drawing.SystemColors.Control;
            this.tabDefense.Controls.Add(this.groupBox16);
            this.tabDefense.Location = new System.Drawing.Point(4, 4);
            this.tabDefense.Name = "tabDefense";
            this.tabDefense.Size = new System.Drawing.Size(368, 230);
            this.tabDefense.TabIndex = 8;
            this.tabDefense.Text = "Defense";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.defenseCover100);
            this.groupBox16.Controls.Add(this.label35);
            this.groupBox16.Controls.Add(this.label36);
            this.groupBox16.Controls.Add(this.defenseCover80);
            this.groupBox16.Controls.Add(this.defenseCover40);
            this.groupBox16.Controls.Add(this.label32);
            this.groupBox16.Controls.Add(this.label31);
            this.groupBox16.Controls.Add(this.label30);
            this.groupBox16.Controls.Add(this.defenseCover1);
            this.groupBox16.Controls.Add(this.label29);
            this.groupBox16.Controls.Add(this.label28);
            this.groupBox16.Controls.Add(this.label27);
            this.groupBox16.Location = new System.Drawing.Point(9, 10);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(348, 211);
            this.groupBox16.TabIndex = 0;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Defense Properties";
            // 
            // defenseCover100
            // 
            this.defenseCover100.AutoSize = true;
            this.defenseCover100.Location = new System.Drawing.Point(138, 115);
            this.defenseCover100.Name = "defenseCover100";
            this.defenseCover100.Size = new System.Drawing.Size(28, 13);
            this.defenseCover100.TabIndex = 11;
            this.defenseCover100.Text = "0.00";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(207, 115);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(15, 13);
            this.label35.TabIndex = 10;
            this.label35.Text = "%";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(16, 115);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(104, 13);
            this.label36.TabIndex = 9;
            this.label36.Text = "100 Defenses Cover";
            // 
            // defenseCover80
            // 
            this.defenseCover80.AutoSize = true;
            this.defenseCover80.Location = new System.Drawing.Point(138, 91);
            this.defenseCover80.Name = "defenseCover80";
            this.defenseCover80.Size = new System.Drawing.Size(28, 13);
            this.defenseCover80.TabIndex = 8;
            this.defenseCover80.Text = "0.00";
            // 
            // defenseCover40
            // 
            this.defenseCover40.AutoSize = true;
            this.defenseCover40.Location = new System.Drawing.Point(138, 66);
            this.defenseCover40.Name = "defenseCover40";
            this.defenseCover40.Size = new System.Drawing.Size(28, 13);
            this.defenseCover40.TabIndex = 7;
            this.defenseCover40.Text = "0.00";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(207, 91);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(15, 13);
            this.label32.TabIndex = 6;
            this.label32.Text = "%";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(207, 66);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(15, 13);
            this.label31.TabIndex = 5;
            this.label31.Text = "%";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(207, 35);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(15, 13);
            this.label30.TabIndex = 4;
            this.label30.Text = "%";
            // 
            // defenseCover1
            // 
            this.defenseCover1.DecimalPlaces = 2;
            this.defenseCover1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.defenseCover1.Location = new System.Drawing.Point(134, 30);
            this.defenseCover1.Name = "defenseCover1";
            this.defenseCover1.Size = new System.Drawing.Size(57, 20);
            this.defenseCover1.TabIndex = 3;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(16, 91);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(98, 13);
            this.label29.TabIndex = 2;
            this.label29.Text = "80 Defenses Cover";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(16, 66);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(98, 13);
            this.label28.TabIndex = 1;
            this.label28.Text = "40 Defenses Cover";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(16, 32);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(92, 13);
            this.label27.TabIndex = 0;
            this.label27.Text = "1 Defense Covers";
            // 
            // tabDeflector
            // 
            this.tabDeflector.BackColor = System.Drawing.SystemColors.Control;
            this.tabDeflector.Controls.Add(this.groupBox32);
            this.tabDeflector.Location = new System.Drawing.Point(4, 4);
            this.tabDeflector.Name = "tabDeflector";
            this.tabDeflector.Size = new System.Drawing.Size(368, 230);
            this.tabDeflector.TabIndex = 28;
            this.tabDeflector.Text = "Deflector";
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.label92);
            this.groupBox32.Controls.Add(this.beamDeflector);
            this.groupBox32.Controls.Add(this.label91);
            this.groupBox32.Location = new System.Drawing.Point(11, 11);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Size = new System.Drawing.Size(346, 211);
            this.groupBox32.TabIndex = 0;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "Beam Deflector";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(149, 31);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(15, 13);
            this.label92.TabIndex = 2;
            this.label92.Text = "%";
            // 
            // beamDeflector
            // 
            this.beamDeflector.Location = new System.Drawing.Point(80, 29);
            this.beamDeflector.Name = "beamDeflector";
            this.beamDeflector.Size = new System.Drawing.Size(63, 20);
            this.beamDeflector.TabIndex = 1;
            this.beamDeflector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(19, 31);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(55, 13);
            this.label91.TabIndex = 0;
            this.label91.Text = "Deflection";
            // 
            // tabEnergyDampener
            // 
            this.tabEnergyDampener.BackColor = System.Drawing.SystemColors.Control;
            this.tabEnergyDampener.Controls.Add(this.groupBox31);
            this.tabEnergyDampener.Location = new System.Drawing.Point(4, 4);
            this.tabEnergyDampener.Name = "tabEnergyDampener";
            this.tabEnergyDampener.Size = new System.Drawing.Size(368, 230);
            this.tabEnergyDampener.TabIndex = 29;
            this.tabEnergyDampener.Text = "Energy Dampener";
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.textBox6);
            this.groupBox31.Controls.Add(this.energyDampener);
            this.groupBox31.Controls.Add(this.label90);
            this.groupBox31.Location = new System.Drawing.Point(8, 11);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Size = new System.Drawing.Size(349, 211);
            this.groupBox31.TabIndex = 0;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Energy Dampener";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(65, 79);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(89, 20);
            this.textBox6.TabIndex = 2;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "Affects all ships.";
            // 
            // energyDampener
            // 
            this.energyDampener.DecimalPlaces = 2;
            this.energyDampener.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.energyDampener.Location = new System.Drawing.Point(132, 29);
            this.energyDampener.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.energyDampener.Name = "energyDampener";
            this.energyDampener.Size = new System.Drawing.Size(65, 20);
            this.energyDampener.TabIndex = 1;
            this.energyDampener.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(17, 31);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(109, 13);
            this.label90.TabIndex = 0;
            this.label90.Text = "Movement Reduction";
            // 
            // tabEngine
            // 
            this.tabEngine.BackColor = System.Drawing.SystemColors.Control;
            this.tabEngine.Controls.Add(this.groupBox6);
            this.tabEngine.Location = new System.Drawing.Point(4, 4);
            this.tabEngine.Name = "tabEngine";
            this.tabEngine.Size = new System.Drawing.Size(368, 230);
            this.tabEngine.TabIndex = 9;
            this.tabEngine.Text = "Engine";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.labelFreeWarpValue);
            this.groupBox6.Controls.Add(this.label85);
            this.groupBox6.Controls.Add(this.label84);
            this.groupBox6.Controls.Add(this.label83);
            this.groupBox6.Controls.Add(this.label82);
            this.groupBox6.Controls.Add(this.label81);
            this.groupBox6.Controls.Add(this.engineOptimalSpeed);
            this.groupBox6.Controls.Add(this.label80);
            this.groupBox6.Controls.Add(this.engineFastestSafeSpeed);
            this.groupBox6.Controls.Add(this.label39);
            this.groupBox6.Controls.Add(this.ramScoopCheckBox);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.warp2Fuel);
            this.groupBox6.Controls.Add(this.warp3Fuel);
            this.groupBox6.Controls.Add(this.warp4Fuel);
            this.groupBox6.Controls.Add(this.warp5Fuel);
            this.groupBox6.Controls.Add(this.warp6Fuel);
            this.groupBox6.Controls.Add(this.warp7Fuel);
            this.groupBox6.Controls.Add(this.warp8Fuel);
            this.groupBox6.Controls.Add(this.warp9Fuel);
            this.groupBox6.Controls.Add(this.warp10Fuel);
            this.groupBox6.Controls.Add(this.warp1Fuel);
            this.groupBox6.Location = new System.Drawing.Point(8, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(349, 213);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Engine Parameters";
            // 
            // labelFreeWarpValue
            // 
            this.labelFreeWarpValue.AutoSize = true;
            this.labelFreeWarpValue.Location = new System.Drawing.Point(159, 73);
            this.labelFreeWarpValue.Name = "labelFreeWarpValue";
            this.labelFreeWarpValue.Size = new System.Drawing.Size(13, 13);
            this.labelFreeWarpValue.TabIndex = 30;
            this.labelFreeWarpValue.Text = "0";
            this.labelFreeWarpValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(11, 72);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(119, 13);
            this.label85.TabIndex = 29;
            this.label85.Text = "Fastest Free Speed   W";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(249, 189);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(16, 13);
            this.label84.TabIndex = 27;
            this.label84.Text = "---";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(151, 189);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(16, 13);
            this.label83.TabIndex = 26;
            this.label83.Text = "---";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(201, 189);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(30, 13);
            this.label82.TabIndex = 25;
            this.label82.Text = "W12";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(103, 189);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(30, 13);
            this.label81.TabIndex = 24;
            this.label81.Text = "W11";
            // 
            // engineOptimalSpeed
            // 
            this.engineOptimalSpeed.Location = new System.Drawing.Point(139, 20);
            this.engineOptimalSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.engineOptimalSpeed.Name = "engineOptimalSpeed";
            this.engineOptimalSpeed.Size = new System.Drawing.Size(48, 20);
            this.engineOptimalSpeed.TabIndex = 22;
            this.engineOptimalSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(11, 22);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(117, 13);
            this.label80.TabIndex = 23;
            this.label80.Text = "Optimal Speed          W";
            // 
            // engineFastestSafeSpeed
            // 
            this.engineFastestSafeSpeed.Location = new System.Drawing.Point(139, 45);
            this.engineFastestSafeSpeed.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.engineFastestSafeSpeed.Name = "engineFastestSafeSpeed";
            this.engineFastestSafeSpeed.Size = new System.Drawing.Size(48, 20);
            this.engineFastestSafeSpeed.TabIndex = 10;
            this.engineFastestSafeSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(11, 47);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(120, 13);
            this.label39.TabIndex = 21;
            this.label39.Text = "Fastest Safe Speed   W";
            // 
            // ramScoopCheckBox
            // 
            this.ramScoopCheckBox.AutoSize = true;
            this.ramScoopCheckBox.Location = new System.Drawing.Point(213, 19);
            this.ramScoopCheckBox.Name = "ramScoopCheckBox";
            this.ramScoopCheckBox.Size = new System.Drawing.Size(118, 17);
            this.ramScoopCheckBox.TabIndex = 11;
            this.ramScoopCheckBox.Text = "Ram Scoop Engine";
            this.ramScoopCheckBox.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(109, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "W2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(207, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "W3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "W4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "W5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "W6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "W7";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "W10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "W9";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(109, 162);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "W8";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "W1";
            // 
            // warp2Fuel
            // 
            this.warp2Fuel.Location = new System.Drawing.Point(139, 109);
            this.warp2Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp2Fuel.Name = "warp2Fuel";
            this.warp2Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp2Fuel.TabIndex = 1;
            this.warp2Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp2Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp3Fuel
            // 
            this.warp3Fuel.Location = new System.Drawing.Point(237, 109);
            this.warp3Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp3Fuel.Name = "warp3Fuel";
            this.warp3Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp3Fuel.TabIndex = 2;
            this.warp3Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp3Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp4Fuel
            // 
            this.warp4Fuel.Location = new System.Drawing.Point(47, 135);
            this.warp4Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp4Fuel.Name = "warp4Fuel";
            this.warp4Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp4Fuel.TabIndex = 3;
            this.warp4Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp4Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp5Fuel
            // 
            this.warp5Fuel.Location = new System.Drawing.Point(139, 135);
            this.warp5Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp5Fuel.Name = "warp5Fuel";
            this.warp5Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp5Fuel.TabIndex = 4;
            this.warp5Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp5Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp6Fuel
            // 
            this.warp6Fuel.Location = new System.Drawing.Point(237, 136);
            this.warp6Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp6Fuel.Name = "warp6Fuel";
            this.warp6Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp6Fuel.TabIndex = 5;
            this.warp6Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp6Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp7Fuel
            // 
            this.warp7Fuel.Location = new System.Drawing.Point(47, 161);
            this.warp7Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp7Fuel.Name = "warp7Fuel";
            this.warp7Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp7Fuel.TabIndex = 6;
            this.warp7Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp7Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp8Fuel
            // 
            this.warp8Fuel.Location = new System.Drawing.Point(139, 161);
            this.warp8Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp8Fuel.Name = "warp8Fuel";
            this.warp8Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp8Fuel.TabIndex = 7;
            this.warp8Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp8Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp9Fuel
            // 
            this.warp9Fuel.Location = new System.Drawing.Point(237, 161);
            this.warp9Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp9Fuel.Name = "warp9Fuel";
            this.warp9Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp9Fuel.TabIndex = 8;
            this.warp9Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp9Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp10Fuel
            // 
            this.warp10Fuel.Location = new System.Drawing.Point(47, 187);
            this.warp10Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp10Fuel.Name = "warp10Fuel";
            this.warp10Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp10Fuel.TabIndex = 9;
            this.warp10Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp10Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // warp1Fuel
            // 
            this.warp1Fuel.Location = new System.Drawing.Point(47, 109);
            this.warp1Fuel.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.warp1Fuel.Name = "warp1Fuel";
            this.warp1Fuel.Size = new System.Drawing.Size(46, 20);
            this.warp1Fuel.TabIndex = 0;
            this.warp1Fuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.warp1Fuel.ValueChanged += new System.EventHandler(this.FuelCostChanged);
            // 
            // tabFuel
            // 
            this.tabFuel.BackColor = System.Drawing.SystemColors.Control;
            this.tabFuel.Controls.Add(this.groupBox18);
            this.tabFuel.Location = new System.Drawing.Point(4, 4);
            this.tabFuel.Name = "tabFuel";
            this.tabFuel.Size = new System.Drawing.Size(368, 230);
            this.tabFuel.TabIndex = 10;
            this.tabFuel.Text = "Fuel";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.label41);
            this.groupBox18.Controls.Add(this.label40);
            this.groupBox18.Controls.Add(this.fuelCapacity);
            this.groupBox18.Controls.Add(this.labelFuelGeneration);
            this.groupBox18.Controls.Add(this.fuelGeneration);
            this.groupBox18.Controls.Add(this.lableFuelCapcity);
            this.groupBox18.Location = new System.Drawing.Point(8, 9);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(349, 211);
            this.groupBox18.TabIndex = 4;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Fuel Properties";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(172, 57);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(52, 13);
            this.label41.TabIndex = 5;
            this.label41.Text = "mg / year";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(172, 27);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(21, 13);
            this.label40.TabIndex = 4;
            this.label40.Text = "mg";
            // 
            // fuelCapacity
            // 
            this.fuelCapacity.Location = new System.Drawing.Point(91, 25);
            this.fuelCapacity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fuelCapacity.Name = "fuelCapacity";
            this.fuelCapacity.Size = new System.Drawing.Size(75, 20);
            this.fuelCapacity.TabIndex = 1;
            this.fuelCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelFuelGeneration
            // 
            this.labelFuelGeneration.AutoSize = true;
            this.labelFuelGeneration.Location = new System.Drawing.Point(8, 57);
            this.labelFuelGeneration.Name = "labelFuelGeneration";
            this.labelFuelGeneration.Size = new System.Drawing.Size(82, 13);
            this.labelFuelGeneration.TabIndex = 3;
            this.labelFuelGeneration.Text = "Fuel Generation";
            // 
            // fuelGeneration
            // 
            this.fuelGeneration.Location = new System.Drawing.Point(91, 55);
            this.fuelGeneration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fuelGeneration.Name = "fuelGeneration";
            this.fuelGeneration.Size = new System.Drawing.Size(75, 20);
            this.fuelGeneration.TabIndex = 2;
            this.fuelGeneration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lableFuelCapcity
            // 
            this.lableFuelCapcity.AutoSize = true;
            this.lableFuelCapcity.Location = new System.Drawing.Point(8, 27);
            this.lableFuelCapcity.Name = "lableFuelCapcity";
            this.lableFuelCapcity.Size = new System.Drawing.Size(71, 13);
            this.lableFuelCapcity.TabIndex = 2;
            this.lableFuelCapcity.Text = "Fuel Capacity";
            // 
            // tabGate
            // 
            this.tabGate.BackColor = System.Drawing.SystemColors.Control;
            this.tabGate.Controls.Add(this.groupBox7);
            this.tabGate.Location = new System.Drawing.Point(4, 4);
            this.tabGate.Name = "tabGate";
            this.tabGate.Size = new System.Drawing.Size(368, 230);
            this.tabGate.TabIndex = 11;
            this.tabGate.Text = "Gate";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.gateMassInfinite);
            this.groupBox7.Controls.Add(this.gateRangeInfinite);
            this.groupBox7.Controls.Add(this.label43);
            this.groupBox7.Controls.Add(this.label42);
            this.groupBox7.Controls.Add(this.safeRange);
            this.groupBox7.Controls.Add(this.safeHullMass);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Location = new System.Drawing.Point(9, 10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(351, 215);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Gate Properties";
            // 
            // gateMassInfinite
            // 
            this.gateMassInfinite.AutoSize = true;
            this.gateMassInfinite.Location = new System.Drawing.Point(209, 29);
            this.gateMassInfinite.Name = "gateMassInfinite";
            this.gateMassInfinite.Size = new System.Drawing.Size(85, 17);
            this.gateMassInfinite.TabIndex = 7;
            this.gateMassInfinite.Text = "Infinite Mass";
            this.gateMassInfinite.UseVisualStyleBackColor = true;
            // 
            // gateRangeInfinite
            // 
            this.gateRangeInfinite.AutoSize = true;
            this.gateRangeInfinite.Location = new System.Drawing.Point(209, 57);
            this.gateRangeInfinite.Name = "gateRangeInfinite";
            this.gateRangeInfinite.Size = new System.Drawing.Size(92, 17);
            this.gateRangeInfinite.TabIndex = 6;
            this.gateRangeInfinite.Text = "Infinite Range";
            this.gateRangeInfinite.UseVisualStyleBackColor = true;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(179, 58);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(14, 13);
            this.label43.TabIndex = 4;
            this.label43.Text = "ly";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(179, 30);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(20, 13);
            this.label42.TabIndex = 3;
            this.label42.Text = "kT";
            // 
            // safeRange
            // 
            this.safeRange.Location = new System.Drawing.Point(102, 54);
            this.safeRange.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.safeRange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.safeRange.Name = "safeRange";
            this.safeRange.Size = new System.Drawing.Size(71, 20);
            this.safeRange.TabIndex = 2;
            this.safeRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // safeHullMass
            // 
            this.safeHullMass.Location = new System.Drawing.Point(102, 28);
            this.safeHullMass.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.safeHullMass.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.safeHullMass.Name = "safeHullMass";
            this.safeHullMass.Size = new System.Drawing.Size(71, 20);
            this.safeHullMass.TabIndex = 1;
            this.safeHullMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Safe Range";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Safe Hull Mass";
            // 
            // tabHull
            // 
            this.tabHull.BackColor = System.Drawing.SystemColors.Control;
            this.tabHull.Controls.Add(this.groupBox19);
            this.tabHull.Location = new System.Drawing.Point(4, 4);
            this.tabHull.Name = "tabHull";
            this.tabHull.Size = new System.Drawing.Size(368, 230);
            this.tabHull.TabIndex = 12;
            this.tabHull.Text = "Hull";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.infiniteDock);
            this.groupBox19.Controls.Add(this.label86);
            this.groupBox19.Controls.Add(this.alternateRealityMaxPop);
            this.groupBox19.Controls.Add(this.label87);
            this.groupBox19.Controls.Add(this.buttonEditHull);
            this.groupBox19.Controls.Add(this.label50);
            this.groupBox19.Controls.Add(this.label49);
            this.groupBox19.Controls.Add(this.hullCargoCapacity);
            this.groupBox19.Controls.Add(this.label48);
            this.groupBox19.Controls.Add(this.labelBaseCargoKT);
            this.groupBox19.Controls.Add(this.label47);
            this.groupBox19.Controls.Add(this.labelBaseCargo);
            this.groupBox19.Controls.Add(this.hullFuelCapacity);
            this.groupBox19.Controls.Add(this.label44);
            this.groupBox19.Controls.Add(this.hullInitiative);
            this.groupBox19.Controls.Add(this.hullDockCapacity);
            this.groupBox19.Controls.Add(this.hullArmor);
            this.groupBox19.Controls.Add(this.label45);
            this.groupBox19.Controls.Add(this.label46);
            this.groupBox19.Location = new System.Drawing.Point(6, 7);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(357, 216);
            this.groupBox19.TabIndex = 27;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Hull Properties";
            // 
            // infiniteDock
            // 
            this.infiniteDock.AutoSize = true;
            this.infiniteDock.Location = new System.Drawing.Point(181, 133);
            this.infiniteDock.Name = "infiniteDock";
            this.infiniteDock.Size = new System.Drawing.Size(130, 17);
            this.infiniteDock.TabIndex = 20;
            this.infiniteDock.Text = "Infinite Dock Capacity";
            this.infiniteDock.UseVisualStyleBackColor = true;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(145, 160);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(25, 13);
            this.label86.TabIndex = 31;
            this.label86.Text = "000";
            // 
            // alternateRealityMaxPop
            // 
            this.alternateRealityMaxPop.Location = new System.Drawing.Point(89, 158);
            this.alternateRealityMaxPop.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.alternateRealityMaxPop.Name = "alternateRealityMaxPop";
            this.alternateRealityMaxPop.Size = new System.Drawing.Size(50, 20);
            this.alternateRealityMaxPop.TabIndex = 21;
            this.alternateRealityMaxPop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 160);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(70, 13);
            this.label87.TabIndex = 30;
            this.label87.Text = "Max AR Pop.";
            // 
            // buttonEditHull
            // 
            this.buttonEditHull.Location = new System.Drawing.Point(253, 187);
            this.buttonEditHull.Name = "buttonEditHull";
            this.buttonEditHull.Size = new System.Drawing.Size(98, 23);
            this.buttonEditHull.TabIndex = 22;
            this.buttonEditHull.Text = "Hull Map";
            this.buttonEditHull.UseVisualStyleBackColor = true;
            this.buttonEditHull.Click += new System.EventHandler(this.ButtonEditHull_Click);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(145, 56);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(19, 13);
            this.label50.TabIndex = 27;
            this.label50.Text = "dp";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(7, 30);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(71, 13);
            this.label49.TabIndex = 14;
            this.label49.Text = "Fuel Capacity";
            // 
            // hullCargoCapacity
            // 
            this.hullCargoCapacity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.hullCargoCapacity.Location = new System.Drawing.Point(89, 106);
            this.hullCargoCapacity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.hullCargoCapacity.Name = "hullCargoCapacity";
            this.hullCargoCapacity.Size = new System.Drawing.Size(50, 20);
            this.hullCargoCapacity.TabIndex = 18;
            this.hullCargoCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 56);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(77, 13);
            this.label48.TabIndex = 16;
            this.label48.Text = "Armor Strength";
            // 
            // labelBaseCargoKT
            // 
            this.labelBaseCargoKT.AutoSize = true;
            this.labelBaseCargoKT.Location = new System.Drawing.Point(145, 108);
            this.labelBaseCargoKT.Name = "labelBaseCargoKT";
            this.labelBaseCargoKT.Size = new System.Drawing.Size(20, 13);
            this.labelBaseCargoKT.TabIndex = 26;
            this.labelBaseCargoKT.Text = "kT";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 84);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(76, 13);
            this.label47.TabIndex = 18;
            this.label47.Text = "Battle Initiative";
            // 
            // labelBaseCargo
            // 
            this.labelBaseCargo.AutoSize = true;
            this.labelBaseCargo.Location = new System.Drawing.Point(6, 108);
            this.labelBaseCargo.Name = "labelBaseCargo";
            this.labelBaseCargo.Size = new System.Drawing.Size(62, 13);
            this.labelBaseCargo.TabIndex = 25;
            this.labelBaseCargo.Text = "Base Cargo";
            // 
            // hullFuelCapacity
            // 
            this.hullFuelCapacity.Location = new System.Drawing.Point(89, 28);
            this.hullFuelCapacity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.hullFuelCapacity.Name = "hullFuelCapacity";
            this.hullFuelCapacity.Size = new System.Drawing.Size(50, 20);
            this.hullFuelCapacity.TabIndex = 15;
            this.hullFuelCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(145, 134);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(20, 13);
            this.label44.TabIndex = 24;
            this.label44.Text = "kT";
            // 
            // hullInitiative
            // 
            this.hullInitiative.Location = new System.Drawing.Point(89, 80);
            this.hullInitiative.Name = "hullInitiative";
            this.hullInitiative.Size = new System.Drawing.Size(50, 20);
            this.hullInitiative.TabIndex = 17;
            this.hullInitiative.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hullDockCapacity
            // 
            this.hullDockCapacity.Location = new System.Drawing.Point(89, 132);
            this.hullDockCapacity.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.hullDockCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.hullDockCapacity.Name = "hullDockCapacity";
            this.hullDockCapacity.Size = new System.Drawing.Size(50, 20);
            this.hullDockCapacity.TabIndex = 19;
            this.hullDockCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hullArmor
            // 
            this.hullArmor.Location = new System.Drawing.Point(89, 54);
            this.hullArmor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.hullArmor.Name = "hullArmor";
            this.hullArmor.Size = new System.Drawing.Size(50, 20);
            this.hullArmor.TabIndex = 16;
            this.hullArmor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(7, 134);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(77, 13);
            this.label45.TabIndex = 23;
            this.label45.Text = "Dock Capacity";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(145, 30);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(21, 13);
            this.label46.TabIndex = 22;
            this.label46.Text = "mg";
            // 
            // tabHullAffinity
            // 
            this.tabHullAffinity.BackColor = System.Drawing.SystemColors.Control;
            this.tabHullAffinity.Controls.Add(this.groupBox29);
            this.tabHullAffinity.Location = new System.Drawing.Point(4, 4);
            this.tabHullAffinity.Name = "tabHullAffinity";
            this.tabHullAffinity.Size = new System.Drawing.Size(368, 230);
            this.tabHullAffinity.TabIndex = 24;
            this.tabHullAffinity.Text = "Hull Affinity";
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.textBox3);
            this.groupBox29.Controls.Add(this.componentHullAffinity);
            this.groupBox29.Location = new System.Drawing.Point(11, 12);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Size = new System.Drawing.Size(346, 210);
            this.groupBox29.TabIndex = 0;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Hull Affinity*";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(19, 69);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(187, 54);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "*This component may only be fitted to the hull specified above.";
            // 
            // componentHullAffinity
            // 
            this.componentHullAffinity.FormattingEnabled = true;
            this.componentHullAffinity.Location = new System.Drawing.Point(19, 31);
            this.componentHullAffinity.Name = "componentHullAffinity";
            this.componentHullAffinity.Size = new System.Drawing.Size(187, 21);
            this.componentHullAffinity.Sorted = true;
            this.componentHullAffinity.TabIndex = 0;
            // 
            // tabJammer
            // 
            this.tabJammer.BackColor = System.Drawing.SystemColors.Control;
            this.tabJammer.Controls.Add(this.groupBox20);
            this.tabJammer.Location = new System.Drawing.Point(4, 4);
            this.tabJammer.Name = "tabJammer";
            this.tabJammer.Size = new System.Drawing.Size(368, 230);
            this.tabJammer.TabIndex = 13;
            this.tabJammer.Text = "Jammer";
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.label51);
            this.groupBox20.Controls.Add(this.label52);
            this.groupBox20.Controls.Add(this.deflection);
            this.groupBox20.Location = new System.Drawing.Point(9, 13);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(348, 207);
            this.groupBox20.TabIndex = 0;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Jammer Properties";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(146, 29);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(15, 13);
            this.label51.TabIndex = 6;
            this.label51.Text = "%";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(6, 29);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(55, 13);
            this.label52.TabIndex = 4;
            this.label52.Text = "Deflection";
            // 
            // deflection
            // 
            this.deflection.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.deflection.Location = new System.Drawing.Point(80, 27);
            this.deflection.Name = "deflection";
            this.deflection.Size = new System.Drawing.Size(60, 20);
            this.deflection.TabIndex = 5;
            this.deflection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabLayerEfficiency
            // 
            this.tabLayerEfficiency.BackColor = System.Drawing.SystemColors.Control;
            this.tabLayerEfficiency.Controls.Add(this.improvedMineLayingEfficiency);
            this.tabLayerEfficiency.Location = new System.Drawing.Point(4, 4);
            this.tabLayerEfficiency.Name = "tabLayerEfficiency";
            this.tabLayerEfficiency.Size = new System.Drawing.Size(368, 230);
            this.tabLayerEfficiency.TabIndex = 27;
            this.tabLayerEfficiency.Text = "Layer Efficiency";
            // 
            // improvedMineLayingEfficiency
            // 
            this.improvedMineLayingEfficiency.Controls.Add(this.textBox5);
            this.improvedMineLayingEfficiency.Controls.Add(this.mineLayerEfficiency);
            this.improvedMineLayingEfficiency.Controls.Add(this.label93);
            this.improvedMineLayingEfficiency.Location = new System.Drawing.Point(10, 11);
            this.improvedMineLayingEfficiency.Name = "improvedMineLayingEfficiency";
            this.improvedMineLayingEfficiency.Size = new System.Drawing.Size(344, 211);
            this.improvedMineLayingEfficiency.TabIndex = 0;
            this.improvedMineLayingEfficiency.TabStop = false;
            this.improvedMineLayingEfficiency.Text = "Improved Mine Laying";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(28, 74);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(163, 45);
            this.textBox5.TabIndex = 3;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "Example: a multiplier of 2.00 doubles the number of mines layed per year.";
            // 
            // mineLayerEfficiency
            // 
            this.mineLayerEfficiency.DecimalPlaces = 2;
            this.mineLayerEfficiency.Location = new System.Drawing.Point(112, 22);
            this.mineLayerEfficiency.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mineLayerEfficiency.Name = "mineLayerEfficiency";
            this.mineLayerEfficiency.Size = new System.Drawing.Size(64, 20);
            this.mineLayerEfficiency.TabIndex = 2;
            this.mineLayerEfficiency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(6, 24);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(97, 13);
            this.label93.TabIndex = 0;
            this.label93.Text = "Efficiency Multiplier";
            // 
            // tabDriver
            // 
            this.tabDriver.BackColor = System.Drawing.SystemColors.Control;
            this.tabDriver.Controls.Add(this.groupBox21);
            this.tabDriver.Location = new System.Drawing.Point(4, 4);
            this.tabDriver.Name = "tabDriver";
            this.tabDriver.Size = new System.Drawing.Size(368, 230);
            this.tabDriver.TabIndex = 14;
            this.tabDriver.Text = "Mass Driver";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.massDriverSpeed);
            this.groupBox21.Controls.Add(this.label53);
            this.groupBox21.Location = new System.Drawing.Point(9, 9);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(404, 211);
            this.groupBox21.TabIndex = 24;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Mass Driver Properties";
            // 
            // massDriverSpeed
            // 
            this.massDriverSpeed.Location = new System.Drawing.Point(134, 31);
            this.massDriverSpeed.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.massDriverSpeed.Name = "massDriverSpeed";
            this.massDriverSpeed.Size = new System.Drawing.Size(48, 20);
            this.massDriverSpeed.TabIndex = 22;
            this.massDriverSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(6, 33);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(120, 13);
            this.label53.TabIndex = 23;
            this.label53.Text = "Fastest Safe Speed   W";
            // 
            // tabMineLayer
            // 
            this.tabMineLayer.BackColor = System.Drawing.SystemColors.Control;
            this.tabMineLayer.Controls.Add(this.groupBox22);
            this.tabMineLayer.Location = new System.Drawing.Point(4, 4);
            this.tabMineLayer.Name = "tabMineLayer";
            this.tabMineLayer.Size = new System.Drawing.Size(368, 230);
            this.tabMineLayer.TabIndex = 16;
            this.tabMineLayer.Text = "Mine Layer";
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.mineMinRamScoopDamage);
            this.groupBox22.Controls.Add(this.mineMinFleetDamage);
            this.groupBox22.Controls.Add(this.mineDamagePerRamScoop);
            this.groupBox22.Controls.Add(this.label54);
            this.groupBox22.Controls.Add(this.label55);
            this.groupBox22.Controls.Add(this.label56);
            this.groupBox22.Controls.Add(this.mineDamagePerEngine);
            this.groupBox22.Controls.Add(this.mineHitChance);
            this.groupBox22.Controls.Add(this.mineSafeSpeed);
            this.groupBox22.Controls.Add(this.label57);
            this.groupBox22.Controls.Add(this.label58);
            this.groupBox22.Controls.Add(this.label59);
            this.groupBox22.Controls.Add(this.mineLayingRate);
            this.groupBox22.Controls.Add(this.label60);
            this.groupBox22.Location = new System.Drawing.Point(8, 6);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(355, 222);
            this.groupBox22.TabIndex = 2;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Laying Parameters";
            // 
            // mineMinRamScoopDamage
            // 
            this.mineMinRamScoopDamage.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mineMinRamScoopDamage.Location = new System.Drawing.Point(128, 183);
            this.mineMinRamScoopDamage.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.mineMinRamScoopDamage.Name = "mineMinRamScoopDamage";
            this.mineMinRamScoopDamage.Size = new System.Drawing.Size(57, 20);
            this.mineMinRamScoopDamage.TabIndex = 7;
            this.mineMinRamScoopDamage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineMinRamScoopDamage.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // mineMinFleetDamage
            // 
            this.mineMinFleetDamage.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mineMinFleetDamage.Location = new System.Drawing.Point(128, 157);
            this.mineMinFleetDamage.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.mineMinFleetDamage.Name = "mineMinFleetDamage";
            this.mineMinFleetDamage.Size = new System.Drawing.Size(57, 20);
            this.mineMinFleetDamage.TabIndex = 6;
            this.mineMinFleetDamage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineMinFleetDamage.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // mineDamagePerRamScoop
            // 
            this.mineDamagePerRamScoop.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mineDamagePerRamScoop.Location = new System.Drawing.Point(128, 131);
            this.mineDamagePerRamScoop.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.mineDamagePerRamScoop.Name = "mineDamagePerRamScoop";
            this.mineDamagePerRamScoop.Size = new System.Drawing.Size(57, 20);
            this.mineDamagePerRamScoop.TabIndex = 5;
            this.mineDamagePerRamScoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineDamagePerRamScoop.Value = new decimal(new int[] {
            125,
            0,
            0,
            0});
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(8, 185);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(108, 13);
            this.label54.TabIndex = 10;
            this.label54.Text = "Min. Ram S. Damage";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(8, 159);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(108, 13);
            this.label55.TabIndex = 9;
            this.label55.Text = "Min. Damage to Fleet";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(8, 133);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(106, 13);
            this.label56.TabIndex = 8;
            this.label56.Text = "Ram Scoop Damage";
            // 
            // mineDamagePerEngine
            // 
            this.mineDamagePerEngine.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.mineDamagePerEngine.Location = new System.Drawing.Point(128, 105);
            this.mineDamagePerEngine.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.mineDamagePerEngine.Name = "mineDamagePerEngine";
            this.mineDamagePerEngine.Size = new System.Drawing.Size(57, 20);
            this.mineDamagePerEngine.TabIndex = 4;
            this.mineDamagePerEngine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineDamagePerEngine.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // mineHitChance
            // 
            this.mineHitChance.DecimalPlaces = 1;
            this.mineHitChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.mineHitChance.Location = new System.Drawing.Point(128, 79);
            this.mineHitChance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mineHitChance.Name = "mineHitChance";
            this.mineHitChance.Size = new System.Drawing.Size(57, 20);
            this.mineHitChance.TabIndex = 3;
            this.mineHitChance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineHitChance.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // mineSafeSpeed
            // 
            this.mineSafeSpeed.Location = new System.Drawing.Point(128, 53);
            this.mineSafeSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.mineSafeSpeed.Name = "mineSafeSpeed";
            this.mineSafeSpeed.Size = new System.Drawing.Size(57, 20);
            this.mineSafeSpeed.TabIndex = 2;
            this.mineSafeSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineSafeSpeed.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(8, 107);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(102, 13);
            this.label57.TabIndex = 4;
            this.label57.Text = "Damage Per Engine";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(8, 81);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(60, 13);
            this.label58.TabIndex = 3;
            this.label58.Text = "Hit Chance";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(8, 55);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(63, 13);
            this.label59.TabIndex = 2;
            this.label59.Text = "Safe Speed";
            // 
            // mineLayingRate
            // 
            this.mineLayingRate.Location = new System.Drawing.Point(128, 27);
            this.mineLayingRate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.mineLayingRate.Name = "mineLayingRate";
            this.mineLayingRate.Size = new System.Drawing.Size(57, 20);
            this.mineLayingRate.TabIndex = 1;
            this.mineLayingRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mineLayingRate.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(8, 29);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(64, 13);
            this.label60.TabIndex = 0;
            this.label60.Text = "Laying Rate";
            // 
            // tabRadiation
            // 
            this.tabRadiation.BackColor = System.Drawing.SystemColors.Control;
            this.tabRadiation.Controls.Add(this.groupBox28);
            this.tabRadiation.Location = new System.Drawing.Point(4, 4);
            this.tabRadiation.Name = "tabRadiation";
            this.tabRadiation.Size = new System.Drawing.Size(368, 230);
            this.tabRadiation.TabIndex = 23;
            this.tabRadiation.Text = "Radiation";
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.textBox2);
            this.groupBox28.Controls.Add(this.radiation);
            this.groupBox28.Controls.Add(this.label78);
            this.groupBox28.Controls.Add(this.label77);
            this.groupBox28.Location = new System.Drawing.Point(7, 9);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(350, 213);
            this.groupBox28.TabIndex = 0;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Dangerous Radiation";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(30, 75);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(270, 97);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // radiation
            // 
            this.radiation.DecimalPlaces = 1;
            this.radiation.Location = new System.Drawing.Point(110, 33);
            this.radiation.Name = "radiation";
            this.radiation.Size = new System.Drawing.Size(58, 20);
            this.radiation.TabIndex = 2;
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(174, 35);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(23, 13);
            this.label78.TabIndex = 1;
            this.label78.Text = "mR";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(27, 35);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(77, 13);
            this.label77.TabIndex = 0;
            this.label77.Text = "Radiation level";
            // 
            // tabRobot
            // 
            this.tabRobot.BackColor = System.Drawing.SystemColors.Control;
            this.tabRobot.Controls.Add(this.groupBox23);
            this.tabRobot.Location = new System.Drawing.Point(4, 4);
            this.tabRobot.Name = "tabRobot";
            this.tabRobot.Size = new System.Drawing.Size(368, 230);
            this.tabRobot.TabIndex = 17;
            this.tabRobot.Text = "Mining Robot";
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.richTextBox1);
            this.groupBox23.Controls.Add(this.label62);
            this.groupBox23.Controls.Add(this.miningRate);
            this.groupBox23.Controls.Add(this.label61);
            this.groupBox23.Location = new System.Drawing.Point(7, 10);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(404, 211);
            this.groupBox23.TabIndex = 0;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Mining Robot Properties";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.CausesValidation = false;
            this.richTextBox1.Location = new System.Drawing.Point(19, 66);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(228, 71);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "Note: The nominated mining rate in kT, of each mineral, will be produced at a con" +
                "centration of 100. Average production is one half this value.";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(157, 32);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(36, 13);
            this.label62.TabIndex = 2;
            this.label62.Text = "kT/yr ";
            // 
            // miningRate
            // 
            this.miningRate.Location = new System.Drawing.Point(85, 30);
            this.miningRate.Name = "miningRate";
            this.miningRate.Size = new System.Drawing.Size(66, 20);
            this.miningRate.TabIndex = 1;
            this.miningRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(6, 32);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(64, 13);
            this.label61.TabIndex = 0;
            this.label61.Text = "Mining Rate";
            // 
            // tabOrbitalAdjuster
            // 
            this.tabOrbitalAdjuster.BackColor = System.Drawing.SystemColors.Control;
            this.tabOrbitalAdjuster.Controls.Add(this.groupBox24);
            this.tabOrbitalAdjuster.Location = new System.Drawing.Point(4, 4);
            this.tabOrbitalAdjuster.Name = "tabOrbitalAdjuster";
            this.tabOrbitalAdjuster.Size = new System.Drawing.Size(368, 230);
            this.tabOrbitalAdjuster.TabIndex = 18;
            this.tabOrbitalAdjuster.Text = "Orbital Adjuster";
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.label64);
            this.groupBox24.Controls.Add(this.adjusterRate);
            this.groupBox24.Controls.Add(this.label63);
            this.groupBox24.Location = new System.Drawing.Point(9, 11);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(348, 210);
            this.groupBox24.TabIndex = 0;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "Orbital Adjuster Properties";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(167, 31);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(59, 13);
            this.label64.TabIndex = 2;
            this.label64.Text = "% per year.";
            // 
            // adjusterRate
            // 
            this.adjusterRate.Location = new System.Drawing.Point(104, 29);
            this.adjusterRate.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.adjusterRate.Name = "adjusterRate";
            this.adjusterRate.Size = new System.Drawing.Size(57, 20);
            this.adjusterRate.TabIndex = 1;
            this.adjusterRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(6, 31);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(92, 13);
            this.label63.TabIndex = 0;
            this.label63.Text = "Terraforming Rate";
            // 
            // tabScanner
            // 
            this.tabScanner.BackColor = System.Drawing.SystemColors.Control;
            this.tabScanner.Controls.Add(this.groupBox17);
            this.tabScanner.Location = new System.Drawing.Point(4, 4);
            this.tabScanner.Name = "tabScanner";
            this.tabScanner.Size = new System.Drawing.Size(368, 230);
            this.tabScanner.TabIndex = 19;
            this.tabScanner.Text = "Scanner";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.label38);
            this.groupBox17.Controls.Add(this.label37);
            this.groupBox17.Controls.Add(this.normalRange);
            this.groupBox17.Controls.Add(this.label33);
            this.groupBox17.Controls.Add(this.penetratingRange);
            this.groupBox17.Controls.Add(this.label34);
            this.groupBox17.Location = new System.Drawing.Point(10, 12);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(347, 209);
            this.groupBox17.TabIndex = 0;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Scanner Properties";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(189, 57);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(14, 13);
            this.label38.TabIndex = 9;
            this.label38.Text = "ly";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(189, 27);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(14, 13);
            this.label37.TabIndex = 8;
            this.label37.Text = "ly";
            // 
            // normalRange
            // 
            this.normalRange.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.normalRange.Location = new System.Drawing.Point(108, 25);
            this.normalRange.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.normalRange.Name = "normalRange";
            this.normalRange.Size = new System.Drawing.Size(75, 20);
            this.normalRange.TabIndex = 4;
            this.normalRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(10, 57);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(96, 13);
            this.label33.TabIndex = 7;
            this.label33.Text = "Penetrating Range";
            // 
            // penetratingRange
            // 
            this.penetratingRange.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.penetratingRange.Location = new System.Drawing.Point(108, 55);
            this.penetratingRange.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.penetratingRange.Name = "penetratingRange";
            this.penetratingRange.Size = new System.Drawing.Size(75, 20);
            this.penetratingRange.TabIndex = 5;
            this.penetratingRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 27);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 13);
            this.label34.TabIndex = 6;
            this.label34.Text = "Normal Range";
            // 
            // tabShield
            // 
            this.tabShield.BackColor = System.Drawing.SystemColors.Control;
            this.tabShield.Controls.Add(this.groupBox25);
            this.tabShield.Location = new System.Drawing.Point(4, 4);
            this.tabShield.Name = "tabShield";
            this.tabShield.Size = new System.Drawing.Size(368, 230);
            this.tabShield.TabIndex = 20;
            this.tabShield.Text = "Shield";
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.label65);
            this.groupBox25.Controls.Add(this.shield);
            this.groupBox25.Controls.Add(this.label66);
            this.groupBox25.Location = new System.Drawing.Point(8, 8);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(350, 214);
            this.groupBox25.TabIndex = 0;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "Shield Properties";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(185, 34);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(19, 13);
            this.label65.TabIndex = 11;
            this.label65.Text = "dp";
            // 
            // shield
            // 
            this.shield.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.shield.Location = new System.Drawing.Point(104, 32);
            this.shield.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.shield.Name = "shield";
            this.shield.Size = new System.Drawing.Size(75, 20);
            this.shield.TabIndex = 9;
            this.shield.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 34);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(79, 13);
            this.label66.TabIndex = 10;
            this.label66.Text = "Shield Strength";
            // 
            // tabTachyonDetector
            // 
            this.tabTachyonDetector.BackColor = System.Drawing.SystemColors.Control;
            this.tabTachyonDetector.Controls.Add(this.groupBox30);
            this.tabTachyonDetector.Location = new System.Drawing.Point(4, 4);
            this.tabTachyonDetector.Name = "tabTachyonDetector";
            this.tabTachyonDetector.Size = new System.Drawing.Size(368, 230);
            this.tabTachyonDetector.TabIndex = 30;
            this.tabTachyonDetector.Text = "Tachyon Detector";
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.tachyonDetector);
            this.groupBox30.Controls.Add(this.label89);
            this.groupBox30.Controls.Add(this.label88);
            this.groupBox30.Location = new System.Drawing.Point(8, 10);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(349, 212);
            this.groupBox30.TabIndex = 0;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "Tachyon Detector";
            // 
            // tachyonDetector
            // 
            this.tachyonDetector.Location = new System.Drawing.Point(73, 25);
            this.tachyonDetector.Name = "tachyonDetector";
            this.tachyonDetector.Size = new System.Drawing.Size(56, 20);
            this.tachyonDetector.TabIndex = 2;
            this.tachyonDetector.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(135, 27);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(15, 13);
            this.label89.TabIndex = 1;
            this.label89.Text = "%";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(14, 27);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(53, 13);
            this.label88.TabIndex = 0;
            this.label88.Text = "Detection";
            // 
            // tabTerraforming
            // 
            this.tabTerraforming.BackColor = System.Drawing.SystemColors.Control;
            this.tabTerraforming.Controls.Add(this.groupBox26);
            this.tabTerraforming.Location = new System.Drawing.Point(4, 4);
            this.tabTerraforming.Name = "tabTerraforming";
            this.tabTerraforming.Size = new System.Drawing.Size(368, 230);
            this.tabTerraforming.TabIndex = 21;
            this.tabTerraforming.Text = "Terraforming";
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.textBox1);
            this.groupBox26.Controls.Add(this.label73);
            this.groupBox26.Controls.Add(this.label72);
            this.groupBox26.Controls.Add(this.label71);
            this.groupBox26.Controls.Add(this.radiationMod);
            this.groupBox26.Controls.Add(this.temperatureMod);
            this.groupBox26.Controls.Add(this.gravityMod);
            this.groupBox26.Controls.Add(this.label69);
            this.groupBox26.Controls.Add(this.label68);
            this.groupBox26.Controls.Add(this.label67);
            this.groupBox26.Location = new System.Drawing.Point(10, 15);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(448, 206);
            this.groupBox26.TabIndex = 0;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Terraforming Properties";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 114);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(202, 73);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Each unit of terraforming built will modify one habitability parameter by 1% up t" +
                "o a maximum of the values indicated.";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(184, 79);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(15, 13);
            this.label73.TabIndex = 9;
            this.label73.Text = "%";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(184, 56);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(15, 13);
            this.label72.TabIndex = 8;
            this.label72.Text = "%";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(184, 27);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(15, 13);
            this.label71.TabIndex = 7;
            this.label71.Text = "%";
            // 
            // radiationMod
            // 
            this.radiationMod.Location = new System.Drawing.Point(118, 77);
            this.radiationMod.Name = "radiationMod";
            this.radiationMod.Size = new System.Drawing.Size(60, 20);
            this.radiationMod.TabIndex = 6;
            this.radiationMod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // temperatureMod
            // 
            this.temperatureMod.Location = new System.Drawing.Point(118, 51);
            this.temperatureMod.Name = "temperatureMod";
            this.temperatureMod.Size = new System.Drawing.Size(60, 20);
            this.temperatureMod.TabIndex = 5;
            this.temperatureMod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gravityMod
            // 
            this.gravityMod.Location = new System.Drawing.Point(118, 25);
            this.gravityMod.Name = "gravityMod";
            this.gravityMod.Size = new System.Drawing.Size(60, 20);
            this.gravityMod.TabIndex = 4;
            this.gravityMod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(6, 79);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(94, 13);
            this.label69.TabIndex = 2;
            this.label69.Text = "Modifies Radiation";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(6, 53);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(109, 13);
            this.label68.TabIndex = 1;
            this.label68.Text = "Modifies Temperature";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 27);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(82, 13);
            this.label67.TabIndex = 0;
            this.label67.Text = "Modifies Gravity";
            // 
            // tabTransportShipsOnly
            // 
            this.tabTransportShipsOnly.BackColor = System.Drawing.SystemColors.Control;
            this.tabTransportShipsOnly.Controls.Add(this.textBox4);
            this.tabTransportShipsOnly.Location = new System.Drawing.Point(4, 4);
            this.tabTransportShipsOnly.Name = "tabTransportShipsOnly";
            this.tabTransportShipsOnly.Size = new System.Drawing.Size(368, 230);
            this.tabTransportShipsOnly.TabIndex = 26;
            this.tabTransportShipsOnly.Text = "Transport Ships Only";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(73, 72);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(228, 79);
            this.textBox4.TabIndex = 0;
            this.textBox4.Text = "This component may only be fitted to transport (un-armed) ships.";
            // 
            // tabWeapon
            // 
            this.tabWeapon.BackColor = System.Drawing.SystemColors.Control;
            this.tabWeapon.Controls.Add(this.groupBoxWeaponType);
            this.tabWeapon.Controls.Add(this.groupBox27);
            this.tabWeapon.Location = new System.Drawing.Point(4, 4);
            this.tabWeapon.Name = "tabWeapon";
            this.tabWeapon.Size = new System.Drawing.Size(368, 230);
            this.tabWeapon.TabIndex = 22;
            this.tabWeapon.Text = "Weapon";
            // 
            // groupBoxWeaponType
            // 
            this.groupBoxWeaponType.Controls.Add(this.isMissile);
            this.groupBoxWeaponType.Controls.Add(this.isTorpedo);
            this.groupBoxWeaponType.Controls.Add(this.isGattling);
            this.groupBoxWeaponType.Controls.Add(this.isSapper);
            this.groupBoxWeaponType.Controls.Add(this.isStandardBeam);
            this.groupBoxWeaponType.Location = new System.Drawing.Point(197, 6);
            this.groupBoxWeaponType.Name = "groupBoxWeaponType";
            this.groupBoxWeaponType.Size = new System.Drawing.Size(162, 216);
            this.groupBoxWeaponType.TabIndex = 5;
            this.groupBoxWeaponType.TabStop = false;
            this.groupBoxWeaponType.Text = "Weapon Type";
            // 
            // isMissile
            // 
            this.isMissile.AutoSize = true;
            this.isMissile.Location = new System.Drawing.Point(6, 129);
            this.isMissile.Name = "isMissile";
            this.isMissile.Size = new System.Drawing.Size(56, 17);
            this.isMissile.TabIndex = 4;
            this.isMissile.Text = "Missile";
            this.isMissile.UseVisualStyleBackColor = true;
            // 
            // isTorpedo
            // 
            this.isTorpedo.AutoSize = true;
            this.isTorpedo.Location = new System.Drawing.Point(6, 106);
            this.isTorpedo.Name = "isTorpedo";
            this.isTorpedo.Size = new System.Drawing.Size(65, 17);
            this.isTorpedo.TabIndex = 3;
            this.isTorpedo.Text = "Torpedo";
            this.isTorpedo.UseVisualStyleBackColor = true;
            // 
            // isGattling
            // 
            this.isGattling.AutoSize = true;
            this.isGattling.Location = new System.Drawing.Point(6, 82);
            this.isGattling.Name = "isGattling";
            this.isGattling.Size = new System.Drawing.Size(105, 17);
            this.isGattling.TabIndex = 2;
            this.isGattling.Text = "Gattling Weapon";
            this.isGattling.UseVisualStyleBackColor = true;
            // 
            // isSapper
            // 
            this.isSapper.AutoSize = true;
            this.isSapper.Location = new System.Drawing.Point(6, 58);
            this.isSapper.Name = "isSapper";
            this.isSapper.Size = new System.Drawing.Size(91, 17);
            this.isSapper.TabIndex = 1;
            this.isSapper.Text = "Shield Sapper";
            this.isSapper.UseVisualStyleBackColor = true;
            // 
            // isStandardBeam
            // 
            this.isStandardBeam.AutoSize = true;
            this.isStandardBeam.Checked = true;
            this.isStandardBeam.Location = new System.Drawing.Point(6, 34);
            this.isStandardBeam.Name = "isStandardBeam";
            this.isStandardBeam.Size = new System.Drawing.Size(98, 17);
            this.isStandardBeam.TabIndex = 0;
            this.isStandardBeam.TabStop = true;
            this.isStandardBeam.Text = "Standard Beam";
            this.isStandardBeam.UseVisualStyleBackColor = true;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.minesSwept);
            this.groupBox27.Controls.Add(this.weaponAccuracy);
            this.groupBox27.Controls.Add(this.weaponRange);
            this.groupBox27.Controls.Add(this.weaponInitiative);
            this.groupBox27.Controls.Add(this.weaponPower);
            this.groupBox27.Controls.Add(this.label70);
            this.groupBox27.Controls.Add(this.label74);
            this.groupBox27.Controls.Add(this.label75);
            this.groupBox27.Controls.Add(this.label76);
            this.groupBox27.Location = new System.Drawing.Point(12, 6);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(179, 216);
            this.groupBox27.TabIndex = 4;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "Weapon Properties";
            // 
            // minesSwept
            // 
            this.minesSwept.AutoSize = true;
            this.minesSwept.Location = new System.Drawing.Point(6, 147);
            this.minesSwept.Name = "minesSwept";
            this.minesSwept.Size = new System.Drawing.Size(66, 13);
            this.minesSwept.TabIndex = 5;
            this.minesSwept.Text = "Mines swept";
            // 
            // weaponAccuracy
            // 
            this.weaponAccuracy.Location = new System.Drawing.Point(96, 106);
            this.weaponAccuracy.Name = "weaponAccuracy";
            this.weaponAccuracy.Size = new System.Drawing.Size(56, 20);
            this.weaponAccuracy.TabIndex = 4;
            this.weaponAccuracy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // weaponRange
            // 
            this.weaponRange.Location = new System.Drawing.Point(96, 58);
            this.weaponRange.Name = "weaponRange";
            this.weaponRange.Size = new System.Drawing.Size(56, 20);
            this.weaponRange.TabIndex = 2;
            this.weaponRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // weaponInitiative
            // 
            this.weaponInitiative.Location = new System.Drawing.Point(96, 82);
            this.weaponInitiative.Name = "weaponInitiative";
            this.weaponInitiative.Size = new System.Drawing.Size(56, 20);
            this.weaponInitiative.TabIndex = 3;
            this.weaponInitiative.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // weaponPower
            // 
            this.weaponPower.Location = new System.Drawing.Point(96, 34);
            this.weaponPower.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.weaponPower.Name = "weaponPower";
            this.weaponPower.Size = new System.Drawing.Size(56, 20);
            this.weaponPower.TabIndex = 1;
            this.weaponPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(6, 108);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(52, 13);
            this.label70.TabIndex = 3;
            this.label70.Text = "Accuracy";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(6, 84);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(46, 13);
            this.label74.TabIndex = 2;
            this.label74.Text = "Initiative";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(6, 60);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(39, 13);
            this.label75.TabIndex = 1;
            this.label75.Text = "Range";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 36);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(37, 13);
            this.label76.TabIndex = 0;
            this.label76.Text = "Power";
            // 
            // componentImage
            // 
            this.componentImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.componentImage.AutoSize = true;
            this.componentImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.componentImage.Image = null;
            this.componentImage.Location = new System.Drawing.Point(783, 3);
            this.componentImage.MaximumSize = new System.Drawing.Size(118, 166);
            this.componentImage.MinimumSize = new System.Drawing.Size(118, 166);
            this.componentImage.Name = "componentImage";
            this.tableLayoutPanel1.SetRowSpan(this.componentImage, 2);
            this.componentImage.Size = new System.Drawing.Size(118, 166);
            this.componentImage.TabIndex = 29;
            // 
            // techRequirements
            // 
            this.techRequirements.Location = new System.Drawing.Point(203, 53);
            this.techRequirements.Name = "techRequirements";
            this.tableLayoutPanel1.SetRowSpan(this.techRequirements, 2);
            this.techRequirements.Size = new System.Drawing.Size(171, 190);
            this.techRequirements.TabIndex = 0;
            this.techRequirements.Value = new Nova.Common.TechLevel(0, 0, 0, 0, 0, 0);
            // 
            // basicProperties
            // 
            this.basicProperties.AutoSize = true;
            this.basicProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.basicProperties.Cost = new Nova.Common.Resources(0, 0, 0, 0);
            this.basicProperties.Location = new System.Drawing.Point(203, 266);
            this.basicProperties.Mass = 0;
            this.basicProperties.Name = "basicProperties";
            this.basicProperties.Size = new System.Drawing.Size(171, 192);
            this.basicProperties.TabIndex = 21;
            // 
            // ComponentEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 564);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ComponentEditorWindow";
            this.Text = "Stars! Nova - Component Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.propertyTabs.ResumeLayout(false);
            this.tabArmor.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.armor)).EndInit();
            this.tabMovement.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.battleMovement)).EndInit();
            this.tabBomb.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimumPopKill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.installationsDestroyed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.populationKill)).EndInit();
            this.tabCapacitor.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beamDamage)).EndInit();
            this.tabCargo.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cargoCapacity)).EndInit();
            this.tabCloak.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cloaking)).EndInit();
            this.tabColonization.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tabComputer.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.initiative)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracy)).EndInit();
            this.tabDefense.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.defenseCover1)).EndInit();
            this.tabDeflector.ResumeLayout(false);
            this.groupBox32.ResumeLayout(false);
            this.groupBox32.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.beamDeflector)).EndInit();
            this.tabEnergyDampener.ResumeLayout(false);
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.energyDampener)).EndInit();
            this.tabEngine.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.engineOptimalSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.engineFastestSafeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp2Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp3Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp4Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp5Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp6Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp7Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp8Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp9Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp10Fuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warp1Fuel)).EndInit();
            this.tabFuel.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fuelCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fuelGeneration)).EndInit();
            this.tabGate.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.safeRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.safeHullMass)).EndInit();
            this.tabHull.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alternateRealityMaxPop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullCargoCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullFuelCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullInitiative)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullDockCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hullArmor)).EndInit();
            this.tabHullAffinity.ResumeLayout(false);
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.tabJammer.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deflection)).EndInit();
            this.tabLayerEfficiency.ResumeLayout(false);
            this.improvedMineLayingEfficiency.ResumeLayout(false);
            this.improvedMineLayingEfficiency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mineLayerEfficiency)).EndInit();
            this.tabDriver.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.massDriverSpeed)).EndInit();
            this.tabMineLayer.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mineMinRamScoopDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineMinFleetDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineDamagePerRamScoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineDamagePerEngine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineHitChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineSafeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mineLayingRate)).EndInit();
            this.tabRadiation.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiation)).EndInit();
            this.tabRobot.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.miningRate)).EndInit();
            this.tabOrbitalAdjuster.ResumeLayout(false);
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adjusterRate)).EndInit();
            this.tabScanner.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.normalRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penetratingRange)).EndInit();
            this.tabShield.ResumeLayout(false);
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shield)).EndInit();
            this.tabTachyonDetector.ResumeLayout(false);
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tachyonDetector)).EndInit();
            this.tabTerraforming.ResumeLayout(false);
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radiationMod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureMod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gravityMod)).EndInit();
            this.tabTransportShipsOnly.ResumeLayout(false);
            this.tabTransportShipsOnly.PerformLayout();
            this.tabWeapon.ResumeLayout(false);
            this.groupBoxWeaponType.ResumeLayout(false);
            this.groupBoxWeaponType.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponInitiative)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponPower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion
        
        private System.Windows.Forms.ToolStripMenuItem resetFileLocation;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem newFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuStripComponent;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveComponentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteComponentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem armorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bombToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem engineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hullToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem massDriverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mineLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miningRobotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orbitalAdjusterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem weaponToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedPropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newComponentMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem raceRestrictionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editComponentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardComponentChangesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.TextBox restrictionSummary;
        private System.Windows.Forms.ToolStripMenuItem layerEfficencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem electricalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capacitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem energyDampenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuelGenerationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jammerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tachyonDetectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mechanicalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beamDeflectorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cargoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colonizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem planetaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hullAffinityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem radiationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transportOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planetaryScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terraformingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem battleMovementToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox componentList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox componentType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox componentName;
        private BasicProperties basicProperties;
        private System.Windows.Forms.GroupBox groupBox5;
        private ImageDisplay componentImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox description;
        private System.Windows.Forms.TabControl propertyTabs;
        private System.Windows.Forms.TabPage tabArmor;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown armor;
        private System.Windows.Forms.TabPage tabMovement;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.NumericUpDown battleMovement;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabPage tabBomb;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.CheckBox smartBomb;
        private System.Windows.Forms.NumericUpDown minimumPopKill;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown installationsDestroyed;
        private System.Windows.Forms.NumericUpDown populationKill;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabCapacitor;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown beamDamage;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage tabCargo;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown cargoCapacity;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TabPage tabCloak;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.NumericUpDown cloaking;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TabPage tabColonization;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.RadioButton orbitalColonizationModule;
        private System.Windows.Forms.RadioButton colonizationModule;
        private System.Windows.Forms.TabPage tabComputer;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown initiative;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown accuracy;
        private System.Windows.Forms.TabPage tabDefense;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label defenseCover100;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label defenseCover80;
        private System.Windows.Forms.Label defenseCover40;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown defenseCover1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TabPage tabDeflector;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.NumericUpDown beamDeflector;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.TabPage tabEnergyDampener;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.NumericUpDown energyDampener;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.TabPage tabEngine;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.NumericUpDown engineOptimalSpeed;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.NumericUpDown engineFastestSafeSpeed;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.CheckBox ramScoopCheckBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown warp2Fuel;
        private System.Windows.Forms.NumericUpDown warp3Fuel;
        private System.Windows.Forms.NumericUpDown warp4Fuel;
        private System.Windows.Forms.NumericUpDown warp5Fuel;
        private System.Windows.Forms.NumericUpDown warp6Fuel;
        private System.Windows.Forms.NumericUpDown warp7Fuel;
        private System.Windows.Forms.NumericUpDown warp8Fuel;
        private System.Windows.Forms.NumericUpDown warp9Fuel;
        private System.Windows.Forms.NumericUpDown warp10Fuel;
        private System.Windows.Forms.NumericUpDown warp1Fuel;
        private System.Windows.Forms.TabPage tabFuel;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.NumericUpDown fuelCapacity;
        private System.Windows.Forms.Label labelFuelGeneration;
        private System.Windows.Forms.NumericUpDown fuelGeneration;
        private System.Windows.Forms.Label lableFuelCapcity;
        private System.Windows.Forms.TabPage tabGate;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox gateMassInfinite;
        private System.Windows.Forms.CheckBox gateRangeInfinite;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown safeRange;
        private System.Windows.Forms.NumericUpDown safeHullMass;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabHull;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.CheckBox infiniteDock;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.NumericUpDown alternateRealityMaxPop;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Button buttonEditHull;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.NumericUpDown hullCargoCapacity;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label labelBaseCargoKT;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label labelBaseCargo;
        private System.Windows.Forms.NumericUpDown hullFuelCapacity;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.NumericUpDown hullInitiative;
        private System.Windows.Forms.NumericUpDown hullDockCapacity;
        private System.Windows.Forms.NumericUpDown hullArmor;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TabPage tabHullAffinity;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox componentHullAffinity;
        private System.Windows.Forms.TabPage tabJammer;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.NumericUpDown deflection;
        private System.Windows.Forms.TabPage tabLayerEfficiency;
        private System.Windows.Forms.GroupBox improvedMineLayingEfficiency;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.NumericUpDown mineLayerEfficiency;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.TabPage tabDriver;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.NumericUpDown massDriverSpeed;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TabPage tabMineLayer;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.NumericUpDown mineMinRamScoopDamage;
        private System.Windows.Forms.NumericUpDown mineMinFleetDamage;
        private System.Windows.Forms.NumericUpDown mineDamagePerRamScoop;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.NumericUpDown mineDamagePerEngine;
        private System.Windows.Forms.NumericUpDown mineHitChance;
        private System.Windows.Forms.NumericUpDown mineSafeSpeed;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.NumericUpDown mineLayingRate;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TabPage tabRadiation;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown radiation;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TabPage tabRobot;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.NumericUpDown miningRate;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TabPage tabOrbitalAdjuster;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.NumericUpDown adjusterRate;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.TabPage tabScanner;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.NumericUpDown normalRange;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.NumericUpDown penetratingRange;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TabPage tabShield;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.NumericUpDown shield;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TabPage tabTachyonDetector;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.NumericUpDown tachyonDetector;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TabPage tabTerraforming;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.NumericUpDown radiationMod;
        private System.Windows.Forms.NumericUpDown temperatureMod;
        private System.Windows.Forms.NumericUpDown gravityMod;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TabPage tabTransportShipsOnly;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TabPage tabWeapon;
        private System.Windows.Forms.GroupBox groupBoxWeaponType;
        private System.Windows.Forms.RadioButton isMissile;
        private System.Windows.Forms.RadioButton isTorpedo;
        private System.Windows.Forms.RadioButton isGattling;
        private System.Windows.Forms.RadioButton isSapper;
        private System.Windows.Forms.RadioButton isStandardBeam;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.Label minesSwept;
        private System.Windows.Forms.NumericUpDown weaponAccuracy;
        private System.Windows.Forms.NumericUpDown weaponRange;
        private System.Windows.Forms.NumericUpDown weaponInitiative;
        private System.Windows.Forms.NumericUpDown weaponPower;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private TechRequirements techRequirements;
        private System.Windows.Forms.Label labelFreeWarpValue;
    }
}


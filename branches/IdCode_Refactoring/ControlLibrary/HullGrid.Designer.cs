namespace Nova.ControlLibrary
{
   public partial class HullGrid
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">Set to true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          this.components = new System.ComponentModel.Container();
          this.hullDisplay = new System.Windows.Forms.Panel();
          this.grid12 = new System.Windows.Forms.Panel();
          this.cellContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
          this.armorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.bombToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.electricalStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.engineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.mechanicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.minelayeystoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.orbitalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.robotMinerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.scannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.shieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.weaponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
          this.decrementStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.incrementModulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.incrementStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.clearCellToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
          this.armorScannerElectMechToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.generalStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.scannerElectricalMechanicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.shieldOrArmorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.shieldElectricalMechanicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.mineLayerElectricalMechanicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.orbitalOrElectricalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
          this.weaponOrShieldToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
          this.cargoBuiltinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.spaceDockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.grid24 = new System.Windows.Forms.Panel();
          this.grid23 = new System.Windows.Forms.Panel();
          this.grid22 = new System.Windows.Forms.Panel();
          this.grid21 = new System.Windows.Forms.Panel();
          this.grid20 = new System.Windows.Forms.Panel();
          this.grid19 = new System.Windows.Forms.Panel();
          this.grid18 = new System.Windows.Forms.Panel();
          this.grid17 = new System.Windows.Forms.Panel();
          this.grid16 = new System.Windows.Forms.Panel();
          this.grid15 = new System.Windows.Forms.Panel();
          this.grid14 = new System.Windows.Forms.Panel();
          this.grid13 = new System.Windows.Forms.Panel();
          this.grid11 = new System.Windows.Forms.Panel();
          this.grid9 = new System.Windows.Forms.Panel();
          this.grid8 = new System.Windows.Forms.Panel();
          this.grid7 = new System.Windows.Forms.Panel();
          this.grid6 = new System.Windows.Forms.Panel();
          this.grid5 = new System.Windows.Forms.Panel();
          this.grid1 = new System.Windows.Forms.Panel();
          this.grid3 = new System.Windows.Forms.Panel();
          this.grid10 = new System.Windows.Forms.Panel();
          this.grid2 = new System.Windows.Forms.Panel();
          this.grid4 = new System.Windows.Forms.Panel();
          this.grid0 = new System.Windows.Forms.Panel();
          this.electricalOrMechanicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.hullDisplay.SuspendLayout();
          this.cellContextMenu.SuspendLayout();
          this.SuspendLayout();
          // 
          // hullDisplay
          // 
          this.hullDisplay.Controls.Add(this.grid12);
          this.hullDisplay.Controls.Add(this.grid24);
          this.hullDisplay.Controls.Add(this.grid23);
          this.hullDisplay.Controls.Add(this.grid22);
          this.hullDisplay.Controls.Add(this.grid21);
          this.hullDisplay.Controls.Add(this.grid20);
          this.hullDisplay.Controls.Add(this.grid19);
          this.hullDisplay.Controls.Add(this.grid18);
          this.hullDisplay.Controls.Add(this.grid17);
          this.hullDisplay.Controls.Add(this.grid16);
          this.hullDisplay.Controls.Add(this.grid15);
          this.hullDisplay.Controls.Add(this.grid14);
          this.hullDisplay.Controls.Add(this.grid13);
          this.hullDisplay.Controls.Add(this.grid11);
          this.hullDisplay.Controls.Add(this.grid9);
          this.hullDisplay.Controls.Add(this.grid8);
          this.hullDisplay.Controls.Add(this.grid7);
          this.hullDisplay.Controls.Add(this.grid6);
          this.hullDisplay.Controls.Add(this.grid5);
          this.hullDisplay.Controls.Add(this.grid1);
          this.hullDisplay.Controls.Add(this.grid3);
          this.hullDisplay.Controls.Add(this.grid10);
          this.hullDisplay.Controls.Add(this.grid2);
          this.hullDisplay.Controls.Add(this.grid4);
          this.hullDisplay.Controls.Add(this.grid0);
          this.hullDisplay.Location = new System.Drawing.Point(3, 3);
          this.hullDisplay.Name = "hullDisplay";
          this.hullDisplay.Size = new System.Drawing.Size(333, 332);
          this.hullDisplay.TabIndex = 3;
          // 
          // grid12
          // 
          this.grid12.AllowDrop = true;
          this.grid12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid12.ContextMenuStrip = this.cellContextMenu;
          this.grid12.Location = new System.Drawing.Point(133, 132);
          this.grid12.Name = "grid12";
          this.grid12.Size = new System.Drawing.Size(64, 64);
          this.grid12.TabIndex = 12;
          this.grid12.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid12.Click += new System.EventHandler(this.GridCell_Click);
          this.grid12.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid12.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // cellContextMenu
          // 
          this.cellContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.armorToolStripMenuItem,
            this.bombToolStripMenuItem,
            this.electricalStripMenuItem,
            this.engineToolStripMenuItem,
            this.mechanicalToolStripMenuItem,
            this.minelayeystoolStripMenuItem,
            this.orbitalToolStripMenuItem,
            this.robotMinerToolStripMenuItem,
            this.scannerToolStripMenuItem,
            this.shieldsToolStripMenuItem,
            this.weaponToolStripMenuItem,
            this.toolStripSeparator1,
            this.decrementStripMenuItem,
            this.incrementModulesToolStripMenuItem,
            this.incrementStripMenuItem,
            this.clearCellToolStripMenuItem1,
            this.toolStripSeparator2,
            this.armorScannerElectMechToolStripMenuItem,
            this.electricalOrMechanicalToolStripMenuItem,
            this.generalStripMenuItem,
            this.scannerElectricalMechanicalToolStripMenuItem,
            this.shieldOrArmorToolStripMenuItem,
            this.shieldElectricalMechanicalToolStripMenuItem,
            this.mineLayerElectricalMechanicalToolStripMenuItem,
            this.orbitalOrElectricalToolStripMenuItem1,
            this.weaponOrShieldToolStripMenuItem1,
            this.toolStripSeparator3,
            this.cargoBuiltinToolStripMenuItem,
            this.spaceDockToolStripMenuItem});
          this.cellContextMenu.Name = "cellContextMenu";
          this.cellContextMenu.ShowImageMargin = false;
          this.cellContextMenu.Size = new System.Drawing.Size(222, 616);
          this.cellContextMenu.Text = "CargoPod";
          this.cellContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CellContextMenuItem);
          // 
          // armorToolStripMenuItem
          // 
          this.armorToolStripMenuItem.Name = "armorToolStripMenuItem";
          this.armorToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.armorToolStripMenuItem.Text = "Armor";
          // 
          // bombToolStripMenuItem
          // 
          this.bombToolStripMenuItem.Name = "bombToolStripMenuItem";
          this.bombToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.bombToolStripMenuItem.Text = "Bomb";
          // 
          // electricalStripMenuItem
          // 
          this.electricalStripMenuItem.Name = "electricalStripMenuItem";
          this.electricalStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.electricalStripMenuItem.Tag = "Electrical";
          this.electricalStripMenuItem.Text = "Electrical";
          // 
          // engineToolStripMenuItem
          // 
          this.engineToolStripMenuItem.Name = "engineToolStripMenuItem";
          this.engineToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.engineToolStripMenuItem.Text = "Engine";
          // 
          // mechanicalToolStripMenuItem
          // 
          this.mechanicalToolStripMenuItem.Name = "mechanicalToolStripMenuItem";
          this.mechanicalToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.mechanicalToolStripMenuItem.Text = "Mechanical";
          // 
          // minelayeystoolStripMenuItem
          // 
          this.minelayeystoolStripMenuItem.Name = "minelayeystoolStripMenuItem";
          this.minelayeystoolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.minelayeystoolStripMenuItem.Tag = "MineLayer";
          this.minelayeystoolStripMenuItem.Text = "Mine Layer";
          // 
          // orbitalToolStripMenuItem
          // 
          this.orbitalToolStripMenuItem.Name = "orbitalToolStripMenuItem";
          this.orbitalToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.orbitalToolStripMenuItem.Text = "Orbital";
          // 
          // robotMinerToolStripMenuItem
          // 
          this.robotMinerToolStripMenuItem.Name = "robotMinerToolStripMenuItem";
          this.robotMinerToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.robotMinerToolStripMenuItem.Text = "Robot Miner";
          // 
          // scannerToolStripMenuItem
          // 
          this.scannerToolStripMenuItem.Name = "scannerToolStripMenuItem";
          this.scannerToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.scannerToolStripMenuItem.Text = "Scanner";
          // 
          // shieldsToolStripMenuItem
          // 
          this.shieldsToolStripMenuItem.Name = "shieldsToolStripMenuItem";
          this.shieldsToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.shieldsToolStripMenuItem.Text = "Shield";
          // 
          // weaponToolStripMenuItem
          // 
          this.weaponToolStripMenuItem.Name = "weaponToolStripMenuItem";
          this.weaponToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.weaponToolStripMenuItem.Text = "Weapon";
          // 
          // toolStripSeparator1
          // 
          this.toolStripSeparator1.Name = "toolStripSeparator1";
          this.toolStripSeparator1.Size = new System.Drawing.Size(218, 6);
          // 
          // decrementStripMenuItem
          // 
          this.decrementStripMenuItem.Name = "decrementStripMenuItem";
          this.decrementStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.decrementStripMenuItem.Text = "Decrement Modules";
          // 
          // incrementModulesToolStripMenuItem
          // 
          this.incrementModulesToolStripMenuItem.Name = "incrementModulesToolStripMenuItem";
          this.incrementModulesToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.incrementModulesToolStripMenuItem.Text = "Increment Modules";
          // 
          // incrementStripMenuItem
          // 
          this.incrementStripMenuItem.Name = "incrementStripMenuItem";
          this.incrementStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.incrementStripMenuItem.Text = "Add 10";
          // 
          // clearCellToolStripMenuItem1
          // 
          this.clearCellToolStripMenuItem1.Name = "clearCellToolStripMenuItem1";
          this.clearCellToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
          this.clearCellToolStripMenuItem1.Text = "Clear Cell";
          // 
          // toolStripSeparator2
          // 
          this.toolStripSeparator2.Name = "toolStripSeparator2";
          this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
          // 
          // armorScannerElectMechToolStripMenuItem
          // 
          this.armorScannerElectMechToolStripMenuItem.Name = "armorScannerElectMechToolStripMenuItem";
          this.armorScannerElectMechToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.armorScannerElectMechToolStripMenuItem.Text = "Armor Scanner Elect Mech";
          // 
          // generalStripMenuItem
          // 
          this.generalStripMenuItem.Name = "generalStripMenuItem";
          this.generalStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.generalStripMenuItem.Text = "General Purpose";
          // 
          // scannerElectricalMechanicalToolStripMenuItem
          // 
          this.scannerElectricalMechanicalToolStripMenuItem.Name = "scannerElectricalMechanicalToolStripMenuItem";
          this.scannerElectricalMechanicalToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.scannerElectricalMechanicalToolStripMenuItem.Text = "Scanner Electrical Mechanical";
          // 
          // shieldOrArmorToolStripMenuItem
          // 
          this.shieldOrArmorToolStripMenuItem.Name = "shieldOrArmorToolStripMenuItem";
          this.shieldOrArmorToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.shieldOrArmorToolStripMenuItem.Text = "Shield or Armor";
          // 
          // shieldElectricalMechanicalToolStripMenuItem
          // 
          this.shieldElectricalMechanicalToolStripMenuItem.Name = "shieldElectricalMechanicalToolStripMenuItem";
          this.shieldElectricalMechanicalToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.shieldElectricalMechanicalToolStripMenuItem.Text = "Shield Electrical Mechanical";
          // 
          // mineLayerElectricalMechanicalToolStripMenuItem
          // 
          this.mineLayerElectricalMechanicalToolStripMenuItem.Name = "mineLayerElectricalMechanicalToolStripMenuItem";
          this.mineLayerElectricalMechanicalToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.mineLayerElectricalMechanicalToolStripMenuItem.Text = "Mine Layer Electrical Mechanical";
          // 
          // orbitalOrElectricalToolStripMenuItem1
          // 
          this.orbitalOrElectricalToolStripMenuItem1.Name = "orbitalOrElectricalToolStripMenuItem1";
          this.orbitalOrElectricalToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
          this.orbitalOrElectricalToolStripMenuItem1.Text = "Orbital or Electrical";
          // 
          // weaponOrShieldToolStripMenuItem1
          // 
          this.weaponOrShieldToolStripMenuItem1.Name = "weaponOrShieldToolStripMenuItem1";
          this.weaponOrShieldToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
          this.weaponOrShieldToolStripMenuItem1.Text = "Weapon or Shield";
          // 
          // toolStripSeparator3
          // 
          this.toolStripSeparator3.Name = "toolStripSeparator3";
          this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
          // 
          // cargoBuiltinToolStripMenuItem
          // 
          this.cargoBuiltinToolStripMenuItem.Name = "cargoBuiltinToolStripMenuItem";
          this.cargoBuiltinToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.cargoBuiltinToolStripMenuItem.Text = "Base Cargo";
          // 
          // spaceDockToolStripMenuItem
          // 
          this.spaceDockToolStripMenuItem.Name = "spaceDockToolStripMenuItem";
          this.spaceDockToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.spaceDockToolStripMenuItem.Text = "Space Dock";
          // 
          // grid24
          // 
          this.grid24.AllowDrop = true;
          this.grid24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid24.ContextMenuStrip = this.cellContextMenu;
          this.grid24.Location = new System.Drawing.Point(263, 262);
          this.grid24.Name = "grid24";
          this.grid24.Size = new System.Drawing.Size(64, 64);
          this.grid24.TabIndex = 24;
          this.grid24.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid24.Click += new System.EventHandler(this.GridCell_Click);
          this.grid24.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid24.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid24.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid23
          // 
          this.grid23.AllowDrop = true;
          this.grid23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid23.ContextMenuStrip = this.cellContextMenu;
          this.grid23.Location = new System.Drawing.Point(198, 262);
          this.grid23.Name = "grid23";
          this.grid23.Size = new System.Drawing.Size(64, 64);
          this.grid23.TabIndex = 23;
          this.grid23.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid23.Click += new System.EventHandler(this.GridCell_Click);
          this.grid23.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid23.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid23.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid22
          // 
          this.grid22.AllowDrop = true;
          this.grid22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid22.ContextMenuStrip = this.cellContextMenu;
          this.grid22.Location = new System.Drawing.Point(133, 262);
          this.grid22.Name = "grid22";
          this.grid22.Size = new System.Drawing.Size(64, 64);
          this.grid22.TabIndex = 22;
          this.grid22.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid22.Click += new System.EventHandler(this.GridCell_Click);
          this.grid22.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid22.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid22.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid21
          // 
          this.grid21.AllowDrop = true;
          this.grid21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid21.ContextMenuStrip = this.cellContextMenu;
          this.grid21.Location = new System.Drawing.Point(68, 262);
          this.grid21.Name = "grid21";
          this.grid21.Size = new System.Drawing.Size(64, 64);
          this.grid21.TabIndex = 21;
          this.grid21.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid21.Click += new System.EventHandler(this.GridCell_Click);
          this.grid21.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid21.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid21.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid20
          // 
          this.grid20.AllowDrop = true;
          this.grid20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid20.ContextMenuStrip = this.cellContextMenu;
          this.grid20.Location = new System.Drawing.Point(3, 263);
          this.grid20.Name = "grid20";
          this.grid20.Size = new System.Drawing.Size(64, 64);
          this.grid20.TabIndex = 20;
          this.grid20.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid20.Click += new System.EventHandler(this.GridCell_Click);
          this.grid20.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid20.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid20.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid19
          // 
          this.grid19.AllowDrop = true;
          this.grid19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid19.ContextMenuStrip = this.cellContextMenu;
          this.grid19.Location = new System.Drawing.Point(263, 197);
          this.grid19.Name = "grid19";
          this.grid19.Size = new System.Drawing.Size(64, 64);
          this.grid19.TabIndex = 19;
          this.grid19.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid19.Click += new System.EventHandler(this.GridCell_Click);
          this.grid19.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid19.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid19.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid18
          // 
          this.grid18.AllowDrop = true;
          this.grid18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid18.ContextMenuStrip = this.cellContextMenu;
          this.grid18.Location = new System.Drawing.Point(198, 197);
          this.grid18.Name = "grid18";
          this.grid18.Size = new System.Drawing.Size(64, 64);
          this.grid18.TabIndex = 18;
          this.grid18.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid18.Click += new System.EventHandler(this.GridCell_Click);
          this.grid18.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid18.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid18.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid17
          // 
          this.grid17.AllowDrop = true;
          this.grid17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid17.ContextMenuStrip = this.cellContextMenu;
          this.grid17.Location = new System.Drawing.Point(133, 197);
          this.grid17.Name = "grid17";
          this.grid17.Size = new System.Drawing.Size(64, 64);
          this.grid17.TabIndex = 17;
          this.grid17.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid17.Click += new System.EventHandler(this.GridCell_Click);
          this.grid17.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid17.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid17.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid16
          // 
          this.grid16.AllowDrop = true;
          this.grid16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid16.ContextMenuStrip = this.cellContextMenu;
          this.grid16.Location = new System.Drawing.Point(68, 197);
          this.grid16.Name = "grid16";
          this.grid16.Size = new System.Drawing.Size(64, 64);
          this.grid16.TabIndex = 16;
          this.grid16.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid16.Click += new System.EventHandler(this.GridCell_Click);
          this.grid16.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid16.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid16.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid15
          // 
          this.grid15.AllowDrop = true;
          this.grid15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid15.ContextMenuStrip = this.cellContextMenu;
          this.grid15.Location = new System.Drawing.Point(3, 197);
          this.grid15.Name = "grid15";
          this.grid15.Size = new System.Drawing.Size(64, 64);
          this.grid15.TabIndex = 15;
          this.grid15.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid15.Click += new System.EventHandler(this.GridCell_Click);
          this.grid15.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid15.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid15.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid14
          // 
          this.grid14.AllowDrop = true;
          this.grid14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid14.ContextMenuStrip = this.cellContextMenu;
          this.grid14.Location = new System.Drawing.Point(263, 132);
          this.grid14.Name = "grid14";
          this.grid14.Size = new System.Drawing.Size(64, 64);
          this.grid14.TabIndex = 14;
          this.grid14.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid14.Click += new System.EventHandler(this.GridCell_Click);
          this.grid14.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid14.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid13
          // 
          this.grid13.AllowDrop = true;
          this.grid13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid13.ContextMenuStrip = this.cellContextMenu;
          this.grid13.Location = new System.Drawing.Point(198, 132);
          this.grid13.Name = "grid13";
          this.grid13.Size = new System.Drawing.Size(64, 64);
          this.grid13.TabIndex = 13;
          this.grid13.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid13.Click += new System.EventHandler(this.GridCell_Click);
          this.grid13.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid13.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid11
          // 
          this.grid11.AllowDrop = true;
          this.grid11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid11.ContextMenuStrip = this.cellContextMenu;
          this.grid11.Location = new System.Drawing.Point(68, 132);
          this.grid11.Name = "grid11";
          this.grid11.Size = new System.Drawing.Size(64, 64);
          this.grid11.TabIndex = 11;
          this.grid11.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid11.Click += new System.EventHandler(this.GridCell_Click);
          this.grid11.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid11.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid9
          // 
          this.grid9.AllowDrop = true;
          this.grid9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid9.ContextMenuStrip = this.cellContextMenu;
          this.grid9.Location = new System.Drawing.Point(263, 67);
          this.grid9.Name = "grid9";
          this.grid9.Size = new System.Drawing.Size(64, 64);
          this.grid9.TabIndex = 9;
          this.grid9.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid9.Click += new System.EventHandler(this.GridCell_Click);
          this.grid9.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid9.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid8
          // 
          this.grid8.AllowDrop = true;
          this.grid8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid8.ContextMenuStrip = this.cellContextMenu;
          this.grid8.Location = new System.Drawing.Point(198, 67);
          this.grid8.Name = "grid8";
          this.grid8.Size = new System.Drawing.Size(64, 64);
          this.grid8.TabIndex = 8;
          this.grid8.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid8.Click += new System.EventHandler(this.GridCell_Click);
          this.grid8.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid8.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid7
          // 
          this.grid7.AllowDrop = true;
          this.grid7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid7.ContextMenuStrip = this.cellContextMenu;
          this.grid7.Location = new System.Drawing.Point(133, 67);
          this.grid7.Name = "grid7";
          this.grid7.Size = new System.Drawing.Size(64, 64);
          this.grid7.TabIndex = 7;
          this.grid7.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid7.Click += new System.EventHandler(this.GridCell_Click);
          this.grid7.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid7.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid6
          // 
          this.grid6.AllowDrop = true;
          this.grid6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid6.ContextMenuStrip = this.cellContextMenu;
          this.grid6.Location = new System.Drawing.Point(68, 67);
          this.grid6.Name = "grid6";
          this.grid6.Size = new System.Drawing.Size(64, 64);
          this.grid6.TabIndex = 6;
          this.grid6.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid6.Click += new System.EventHandler(this.GridCell_Click);
          this.grid6.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid6.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid5
          // 
          this.grid5.AllowDrop = true;
          this.grid5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid5.ContextMenuStrip = this.cellContextMenu;
          this.grid5.Location = new System.Drawing.Point(3, 67);
          this.grid5.Name = "grid5";
          this.grid5.Size = new System.Drawing.Size(64, 64);
          this.grid5.TabIndex = 5;
          this.grid5.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid5.Click += new System.EventHandler(this.GridCell_Click);
          this.grid5.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid5.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid1
          // 
          this.grid1.AllowDrop = true;
          this.grid1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid1.ContextMenuStrip = this.cellContextMenu;
          this.grid1.Location = new System.Drawing.Point(68, 2);
          this.grid1.Name = "grid1";
          this.grid1.Size = new System.Drawing.Size(64, 64);
          this.grid1.TabIndex = 1;
          this.grid1.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid1.Click += new System.EventHandler(this.GridCell_Click);
          this.grid1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid3
          // 
          this.grid3.AllowDrop = true;
          this.grid3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid3.ContextMenuStrip = this.cellContextMenu;
          this.grid3.Location = new System.Drawing.Point(198, 2);
          this.grid3.Name = "grid3";
          this.grid3.Size = new System.Drawing.Size(64, 64);
          this.grid3.TabIndex = 3;
          this.grid3.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid3.Click += new System.EventHandler(this.GridCell_Click);
          this.grid3.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid3.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid10
          // 
          this.grid10.AllowDrop = true;
          this.grid10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid10.ContextMenuStrip = this.cellContextMenu;
          this.grid10.Location = new System.Drawing.Point(3, 132);
          this.grid10.Name = "grid10";
          this.grid10.Size = new System.Drawing.Size(64, 64);
          this.grid10.TabIndex = 10;
          this.grid10.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid10.Click += new System.EventHandler(this.GridCell_Click);
          this.grid10.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid10.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid2
          // 
          this.grid2.AllowDrop = true;
          this.grid2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid2.ContextMenuStrip = this.cellContextMenu;
          this.grid2.Location = new System.Drawing.Point(133, 2);
          this.grid2.Name = "grid2";
          this.grid2.Size = new System.Drawing.Size(64, 64);
          this.grid2.TabIndex = 2;
          this.grid2.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid2.Click += new System.EventHandler(this.GridCell_Click);
          this.grid2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid4
          // 
          this.grid4.AllowDrop = true;
          this.grid4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid4.ContextMenuStrip = this.cellContextMenu;
          this.grid4.Location = new System.Drawing.Point(263, 2);
          this.grid4.Name = "grid4";
          this.grid4.Size = new System.Drawing.Size(64, 64);
          this.grid4.TabIndex = 4;
          this.grid4.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid4.Click += new System.EventHandler(this.GridCell_Click);
          this.grid4.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid4.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // grid0
          // 
          this.grid0.AllowDrop = true;
          this.grid0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
          this.grid0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.grid0.ContextMenuStrip = this.cellContextMenu;
          this.grid0.Location = new System.Drawing.Point(3, 2);
          this.grid0.Name = "grid0";
          this.grid0.Size = new System.Drawing.Size(64, 64);
          this.grid0.TabIndex = 0;
          this.grid0.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid0_Paint);
          this.grid0.Click += new System.EventHandler(this.GridCell_Click);
          this.grid0.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
          this.grid0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_DragBegin);
          this.grid0.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
          // 
          // electricalOrMechanicalToolStripMenuItem
          // 
          this.electricalOrMechanicalToolStripMenuItem.Name = "electricalOrMechanicalToolStripMenuItem";
          this.electricalOrMechanicalToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
          this.electricalOrMechanicalToolStripMenuItem.Text = "Electrical or Mechanical";
          // 
          // HullGrid
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.hullDisplay);
          this.Name = "HullGrid";
          this.Size = new System.Drawing.Size(340, 339);
          this.hullDisplay.ResumeLayout(false);
          this.cellContextMenu.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel hullDisplay;
      private System.Windows.Forms.Panel grid12;
      private System.Windows.Forms.Panel grid24;
      private System.Windows.Forms.Panel grid23;
      private System.Windows.Forms.Panel grid22;
      private System.Windows.Forms.Panel grid21;
      private System.Windows.Forms.Panel grid20;
      private System.Windows.Forms.Panel grid19;
      private System.Windows.Forms.Panel grid18;
      private System.Windows.Forms.Panel grid17;
      private System.Windows.Forms.Panel grid16;
      private System.Windows.Forms.Panel grid15;
      private System.Windows.Forms.Panel grid14;
      private System.Windows.Forms.Panel grid13;
      private System.Windows.Forms.Panel grid11;
      private System.Windows.Forms.Panel grid9;
      private System.Windows.Forms.Panel grid8;
      private System.Windows.Forms.Panel grid7;
      private System.Windows.Forms.Panel grid6;
      private System.Windows.Forms.Panel grid5;
      private System.Windows.Forms.Panel grid1;
      private System.Windows.Forms.Panel grid3;
      private System.Windows.Forms.Panel grid10;
      private System.Windows.Forms.Panel grid2;
      private System.Windows.Forms.Panel grid4;
      private System.Windows.Forms.Panel grid0;
      private System.Windows.Forms.ContextMenuStrip cellContextMenu;
      private System.Windows.Forms.ToolStripMenuItem engineToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mechanicalToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem weaponToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem armorToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem bombToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem scannerToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem shieldsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem orbitalToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem electricalStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem generalStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem decrementStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem incrementStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem minelayeystoolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem clearCellToolStripMenuItem1;
       private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
       private System.Windows.Forms.ToolStripMenuItem incrementModulesToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem cargoBuiltinToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem robotMinerToolStripMenuItem;
       private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
       private System.Windows.Forms.ToolStripMenuItem armorScannerElectMechToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem scannerElectricalMechanicalToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem shieldOrArmorToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem shieldElectricalMechanicalToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem mineLayerElectricalMechanicalToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem orbitalOrElectricalToolStripMenuItem1;
       private System.Windows.Forms.ToolStripMenuItem weaponOrShieldToolStripMenuItem1;
       private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
       private System.Windows.Forms.ToolStripMenuItem spaceDockToolStripMenuItem;
       private System.Windows.Forms.ToolStripMenuItem electricalOrMechanicalToolStripMenuItem;
   }
}

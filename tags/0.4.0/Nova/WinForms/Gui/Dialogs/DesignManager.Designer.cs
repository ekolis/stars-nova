namespace Nova.WinForms.Gui
{
   partial class DesignManager
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignManager));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.label3 = new System.Windows.Forms.Label();
          this.DesignOwner = new System.Windows.Forms.ComboBox();
          this.groupBox7 = new System.Windows.Forms.GroupBox();
          this.DesignList = new System.Windows.Forms.ListView();
          this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
          this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
          this.groupBox6 = new System.Windows.Forms.GroupBox();
          this.ComponentSummary = new System.Windows.Forms.TextBox();
          this.Description = new System.Windows.Forms.TextBox();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.HullImage = new System.Windows.Forms.PictureBox();
          this.HullGrid = new ControlLibrary.HullGrid();
          this.groupBox4 = new System.Windows.Forms.GroupBox();
          this.CapacityType = new System.Windows.Forms.Label();
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
          this.label2 = new System.Windows.Forms.Label();
          this.groupBox3 = new System.Windows.Forms.GroupBox();
          this.DesignResources = new ControlLibrary.ResourceDisplay();
          this.DesignName = new System.Windows.Forms.TextBox();
          this.label1 = new System.Windows.Forms.Label();
          this.Done = new System.Windows.Forms.Button();
          this.Delete = new System.Windows.Forms.Button();
          this.label6 = new System.Windows.Forms.Label();
          this.CargoCapacity = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.groupBox1.SuspendLayout();
          this.groupBox7.SuspendLayout();
          this.groupBox6.SuspendLayout();
          this.groupBox2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.HullImage)).BeginInit();
          this.groupBox4.SuspendLayout();
          this.groupBox3.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.label3);
          this.groupBox1.Controls.Add(this.DesignOwner);
          this.groupBox1.Controls.Add(this.groupBox7);
          this.groupBox1.Controls.Add(this.groupBox6);
          this.groupBox1.Location = new System.Drawing.Point(12, 11);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(355, 550);
          this.groupBox1.TabIndex = 8;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "Available Designs";
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Location = new System.Drawing.Point(6, 18);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(74, 13);
          this.label3.TabIndex = 5;
          this.label3.Text = "Design Owner";
          // 
          // DesignOwner
          // 
          this.DesignOwner.FormattingEnabled = true;
          this.DesignOwner.Location = new System.Drawing.Point(6, 37);
          this.DesignOwner.Name = "DesignOwner";
          this.DesignOwner.Size = new System.Drawing.Size(333, 21);
          this.DesignOwner.TabIndex = 4;
          this.DesignOwner.SelectedIndexChanged += new System.EventHandler(this.DesignOwner_SelectedIndexChanged);
          // 
          // groupBox7
          // 
          this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.groupBox7.Controls.Add(this.DesignList);
          this.groupBox7.Location = new System.Drawing.Point(6, 64);
          this.groupBox7.Name = "groupBox7";
          this.groupBox7.Size = new System.Drawing.Size(333, 357);
          this.groupBox7.TabIndex = 3;
          this.groupBox7.TabStop = false;
          this.groupBox7.Text = "Designs";
          // 
          // DesignList
          // 
          this.DesignList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
          this.DesignList.Dock = System.Windows.Forms.DockStyle.Fill;
          this.DesignList.Location = new System.Drawing.Point(3, 16);
          this.DesignList.Name = "DesignList";
          this.DesignList.Size = new System.Drawing.Size(327, 338);
          this.DesignList.TabIndex = 0;
          this.DesignList.UseCompatibleStateImageBehavior = false;
          this.DesignList.View = System.Windows.Forms.View.Details;
          this.DesignList.SelectedIndexChanged += new System.EventHandler(this.DesignList_SelectedIndexChanged);
          // 
          // columnHeader1
          // 
          this.columnHeader1.Text = "Design Name";
          this.columnHeader1.Width = 230;
          // 
          // columnHeader2
          // 
          this.columnHeader2.Text = "Number Existing";
          this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          this.columnHeader2.Width = 93;
          // 
          // groupBox6
          // 
          this.groupBox6.Controls.Add(this.ComponentSummary);
          this.groupBox6.Controls.Add(this.Description);
          this.groupBox6.Location = new System.Drawing.Point(6, 427);
          this.groupBox6.Name = "groupBox6";
          this.groupBox6.Size = new System.Drawing.Size(333, 112);
          this.groupBox6.TabIndex = 2;
          this.groupBox6.TabStop = false;
          this.groupBox6.Text = "Component Summary";
          // 
          // ComponentSummary
          // 
          this.ComponentSummary.AcceptsTab = true;
          this.ComponentSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.ComponentSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.ComponentSummary.Location = new System.Drawing.Point(3, 16);
          this.ComponentSummary.Multiline = true;
          this.ComponentSummary.Name = "ComponentSummary";
          this.ComponentSummary.ReadOnly = true;
          this.ComponentSummary.Size = new System.Drawing.Size(326, 81);
          this.ComponentSummary.TabIndex = 9;
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
          this.Description.Size = new System.Drawing.Size(315, 81);
          this.Description.TabIndex = 8;
          // 
          // groupBox2
          // 
          this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
          this.groupBox2.Controls.Add(this.HullImage);
          this.groupBox2.Controls.Add(this.HullGrid);
          this.groupBox2.Controls.Add(this.groupBox4);
          this.groupBox2.Controls.Add(this.groupBox3);
          this.groupBox2.Controls.Add(this.DesignName);
          this.groupBox2.Controls.Add(this.label1);
          this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.groupBox2.Location = new System.Drawing.Point(390, 11);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(373, 564);
          this.groupBox2.TabIndex = 7;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "Design Detail";
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
          // groupBox4
          // 
          this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
          this.groupBox4.Controls.Add(this.label11);
          this.groupBox4.Controls.Add(this.label7);
          this.groupBox4.Controls.Add(this.CargoCapacity);
          this.groupBox4.Controls.Add(this.label6);
          this.groupBox4.Controls.Add(this.CapacityType);
          this.groupBox4.Controls.Add(this.ShipCloak);
          this.groupBox4.Controls.Add(this.ShipShields);
          this.groupBox4.Controls.Add(this.ShipArmor);
          this.groupBox4.Controls.Add(this.MaxCapacity);
          this.groupBox4.Controls.Add(this.ShipMass);
          this.groupBox4.Controls.Add(this.label15);
          this.groupBox4.Controls.Add(this.label14);
          this.groupBox4.Controls.Add(this.label13);
          this.groupBox4.Controls.Add(this.CapacityUnits);
          this.groupBox4.Controls.Add(this.label8);
          this.groupBox4.Controls.Add(this.label5);
          this.groupBox4.Controls.Add(this.label4);
          this.groupBox4.Controls.Add(this.label2);
          this.groupBox4.Location = new System.Drawing.Point(196, 435);
          this.groupBox4.Name = "groupBox4";
          this.groupBox4.Size = new System.Drawing.Size(165, 118);
          this.groupBox4.TabIndex = 15;
          this.groupBox4.TabStop = false;
          this.groupBox4.Text = "Primary Characteristics";
          // 
          // CapacityType
          // 
          this.CapacityType.Location = new System.Drawing.Point(6, 34);
          this.CapacityType.Name = "CapacityType";
          this.CapacityType.Size = new System.Drawing.Size(100, 13);
          this.CapacityType.TabIndex = 1;
          this.CapacityType.Text = "Max Fuel Capacity";
          // 
          // ShipCloak
          // 
          this.ShipCloak.Location = new System.Drawing.Point(92, 79);
          this.ShipCloak.Name = "ShipCloak";
          this.ShipCloak.Size = new System.Drawing.Size(50, 13);
          this.ShipCloak.TabIndex = 14;
          this.ShipCloak.Text = "0";
          this.ShipCloak.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // ShipShields
          // 
          this.ShipShields.Location = new System.Drawing.Point(92, 65);
          this.ShipShields.Name = "ShipShields";
          this.ShipShields.Size = new System.Drawing.Size(50, 13);
          this.ShipShields.TabIndex = 13;
          this.ShipShields.Text = "0";
          this.ShipShields.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // ShipArmor
          // 
          this.ShipArmor.Location = new System.Drawing.Point(92, 50);
          this.ShipArmor.Name = "ShipArmor";
          this.ShipArmor.Size = new System.Drawing.Size(50, 13);
          this.ShipArmor.TabIndex = 12;
          this.ShipArmor.Text = "0";
          this.ShipArmor.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // MaxCapacity
          // 
          this.MaxCapacity.Location = new System.Drawing.Point(92, 34);
          this.MaxCapacity.Name = "MaxCapacity";
          this.MaxCapacity.Size = new System.Drawing.Size(50, 13);
          this.MaxCapacity.TabIndex = 11;
          this.MaxCapacity.Text = "0";
          this.MaxCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // ShipMass
          // 
          this.ShipMass.Location = new System.Drawing.Point(92, 19);
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
          this.label11.Location = new System.Drawing.Point(143, 21);
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
          this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
          this.groupBox3.Controls.Add(this.DesignResources);
          this.groupBox3.Location = new System.Drawing.Point(14, 435);
          this.groupBox3.Name = "groupBox3";
          this.groupBox3.Size = new System.Drawing.Size(166, 118);
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
          this.DesignName.Location = new System.Drawing.Point(23, 34);
          this.DesignName.Name = "DesignName";
          this.DesignName.Size = new System.Drawing.Size(238, 20);
          this.DesignName.TabIndex = 1;
          // 
          // label1
          // 
          this.label1.Location = new System.Drawing.Point(20, 18);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(76, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Design Name";
          // 
          // Done
          // 
          this.Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.Done.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.Done.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.Done.Location = new System.Drawing.Point(688, 583);
          this.Done.Name = "Done";
          this.Done.Size = new System.Drawing.Size(75, 23);
          this.Done.TabIndex = 5;
          this.Done.Text = "Done";
          this.Done.Click += new System.EventHandler(this.Done_Click);
          // 
          // Delete
          // 
          this.Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.Delete.Location = new System.Drawing.Point(575, 584);
          this.Delete.Name = "Delete";
          this.Delete.Size = new System.Drawing.Size(75, 23);
          this.Delete.TabIndex = 9;
          this.Delete.Text = "Delete";
          this.Delete.UseVisualStyleBackColor = true;
          this.Delete.Click += new System.EventHandler(this.Delete_Click);
          // 
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Location = new System.Drawing.Point(6, 94);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(79, 13);
          this.label6.TabIndex = 16;
          this.label6.Text = "Cargo Capacity";
          // 
          // CargoCapacity
          // 
          this.CargoCapacity.Location = new System.Drawing.Point(92, 94);
          this.CargoCapacity.Name = "CargoCapacity";
          this.CargoCapacity.Size = new System.Drawing.Size(50, 13);
          this.CargoCapacity.TabIndex = 17;
          this.CargoCapacity.Text = "0";
          this.CargoCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // label7
          // 
          this.label7.AutoSize = true;
          this.label7.Location = new System.Drawing.Point(143, 94);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(20, 13);
          this.label7.TabIndex = 18;
          this.label7.Text = "kT";
          // 
          // DesignManager
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(778, 618);
          this.Controls.Add(this.Delete);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.Done);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.Name = "DesignManager";
          this.Text = "Nova Design Manager";
          this.Load += new System.EventHandler(this.DesignManager_Load);
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          this.groupBox7.ResumeLayout(false);
          this.groupBox6.ResumeLayout(false);
          this.groupBox6.PerformLayout();
          this.groupBox2.ResumeLayout(false);
          this.groupBox2.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.HullImage)).EndInit();
          this.groupBox4.ResumeLayout(false);
          this.groupBox4.PerformLayout();
          this.groupBox3.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox6;
      private System.Windows.Forms.TextBox Description;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.PictureBox HullImage;
      private ControlLibrary.HullGrid HullGrid;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Label ShipCloak;
      private System.Windows.Forms.Label ShipShields;
      private System.Windows.Forms.Label ShipArmor;
      private System.Windows.Forms.Label MaxCapacity;
      private System.Windows.Forms.Label ShipMass;
      private System.Windows.Forms.Label label15;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label CapacityUnits;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label CapacityType;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox groupBox3;
      private ControlLibrary.ResourceDisplay DesignResources;
      private System.Windows.Forms.TextBox DesignName;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button Done;
      private System.Windows.Forms.GroupBox groupBox7;
      private System.Windows.Forms.ListView DesignList;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.Button Delete;
      private System.Windows.Forms.TextBox ComponentSummary;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ComboBox DesignOwner;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label CargoCapacity;
   }
}
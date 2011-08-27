using System.Collections.Generic;
using Nova.Common.Components;

namespace Nova.WinForms.Gui
{
   public partial class DesignManager
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignManager));
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.label3 = new System.Windows.Forms.Label();
          this.comboDesignOwner = new System.Windows.Forms.ComboBox();
          this.groupBox7 = new System.Windows.Forms.GroupBox();
          this.designList = new System.Windows.Forms.ListView();
          this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
          this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
          this.groupBox6 = new System.Windows.Forms.GroupBox();
          this.componentSummary = new System.Windows.Forms.TextBox();
          this.description = new System.Windows.Forms.TextBox();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.hullImage = new System.Windows.Forms.PictureBox();
          this.hullGrid = new ControlLibrary.HullGrid();
          this.groupBox4 = new System.Windows.Forms.GroupBox();
          this.capacityType = new System.Windows.Forms.Label();
          this.shipCloak = new System.Windows.Forms.Label();
          this.shipShields = new System.Windows.Forms.Label();
          this.shipArmor = new System.Windows.Forms.Label();
          this.maxCapacity = new System.Windows.Forms.Label();
          this.shipMass = new System.Windows.Forms.Label();
          this.label15 = new System.Windows.Forms.Label();
          this.label14 = new System.Windows.Forms.Label();
          this.label13 = new System.Windows.Forms.Label();
          this.capacityUnits = new System.Windows.Forms.Label();
          this.label11 = new System.Windows.Forms.Label();
          this.label8 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.label4 = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.groupBox3 = new System.Windows.Forms.GroupBox();
          this.designResources = new ControlLibrary.ResourceDisplay();
          this.designName = new System.Windows.Forms.TextBox();
          this.label1 = new System.Windows.Forms.Label();
          this.done = new System.Windows.Forms.Button();
          this.delete = new System.Windows.Forms.Button();
          this.label6 = new System.Windows.Forms.Label();
          this.cargoCapacity = new System.Windows.Forms.Label();
          this.label7 = new System.Windows.Forms.Label();
          this.groupBox1.SuspendLayout();
          this.groupBox7.SuspendLayout();
          this.groupBox6.SuspendLayout();
          this.groupBox2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.hullImage)).BeginInit();
          this.groupBox4.SuspendLayout();
          this.groupBox3.SuspendLayout();
          this.SuspendLayout();
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.label3);
          this.groupBox1.Controls.Add(this.comboDesignOwner);
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
          this.comboDesignOwner.FormattingEnabled = true;
          this.comboDesignOwner.Location = new System.Drawing.Point(6, 37);
          this.comboDesignOwner.Name = "comboDesignOwner";
          this.comboDesignOwner.Size = new System.Drawing.Size(333, 21);
          this.comboDesignOwner.TabIndex = 4;
          this.comboDesignOwner.SelectedIndexChanged += new System.EventHandler(this.DesignOwner_SelectedIndexChanged);
          // 
          // groupBox7
          // 
          this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.groupBox7.Controls.Add(this.designList);
          this.groupBox7.Location = new System.Drawing.Point(6, 64);
          this.groupBox7.Name = "groupBox7";
          this.groupBox7.Size = new System.Drawing.Size(333, 357);
          this.groupBox7.TabIndex = 3;
          this.groupBox7.TabStop = false;
          this.groupBox7.Text = "Designs";
          // 
          // DesignList
          // 
          this.designList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
          this.designList.Dock = System.Windows.Forms.DockStyle.Fill;
          this.designList.Location = new System.Drawing.Point(3, 16);
          this.designList.Name = "designList";
          this.designList.Size = new System.Drawing.Size(327, 338);
          this.designList.TabIndex = 0;
          this.designList.UseCompatibleStateImageBehavior = false;
          this.designList.View = System.Windows.Forms.View.Details;
          this.designList.SelectedIndexChanged += new System.EventHandler(this.DesignList_SelectedIndexChanged);
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
          this.groupBox6.Controls.Add(this.componentSummary);
          this.groupBox6.Controls.Add(this.description);
          this.groupBox6.Location = new System.Drawing.Point(6, 427);
          this.groupBox6.Name = "groupBox6";
          this.groupBox6.Size = new System.Drawing.Size(333, 112);
          this.groupBox6.TabIndex = 2;
          this.groupBox6.TabStop = false;
          this.groupBox6.Text = "Component Summary";
          // 
          // ComponentSummary
          // 
          this.componentSummary.AcceptsTab = true;
          this.componentSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.componentSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.componentSummary.Location = new System.Drawing.Point(3, 16);
          this.componentSummary.Multiline = true;
          this.componentSummary.Name = "componentSummary";
          this.componentSummary.ReadOnly = true;
          this.componentSummary.Size = new System.Drawing.Size(326, 81);
          this.componentSummary.TabIndex = 9;
          // 
          // Description
          // 
          this.description.AcceptsTab = true;
          this.description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.description.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.description.Location = new System.Drawing.Point(7, 18);
          this.description.Multiline = true;
          this.description.Name = "description";
          this.description.ReadOnly = true;
          this.description.Size = new System.Drawing.Size(315, 81);
          this.description.TabIndex = 8;
          // 
          // groupBox2
          // 
          this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
          this.groupBox2.Controls.Add(this.hullImage);
          this.groupBox2.Controls.Add(this.hullGrid);
          this.groupBox2.Controls.Add(this.groupBox4);
          this.groupBox2.Controls.Add(this.groupBox3);
          this.groupBox2.Controls.Add(this.designName);
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
          this.hullImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.hullImage.Location = new System.Drawing.Point(288, 18);
          this.hullImage.Name = "hullImage";
          this.hullImage.Size = new System.Drawing.Size(64, 64);
          this.hullImage.TabIndex = 19;
          this.hullImage.TabStop = false;
          // 
          // HullGrid
          // 
          this.hullGrid.ActiveModules = new System.Collections.Generic.List<Nova.Common.Components.HullModule>();
          this.hullGrid.HideEmptyModules = true;
          this.hullGrid.Location = new System.Drawing.Point(23, 88);
          this.hullGrid.Name = "hullGrid";
          this.hullGrid.Size = new System.Drawing.Size(338, 338);
          this.hullGrid.TabIndex = 18;
          // 
          // groupBox4
          // 
          this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
          this.groupBox4.Controls.Add(this.label11);
          this.groupBox4.Controls.Add(this.label7);
          this.groupBox4.Controls.Add(this.cargoCapacity);
          this.groupBox4.Controls.Add(this.label6);
          this.groupBox4.Controls.Add(this.capacityType);
          this.groupBox4.Controls.Add(this.shipCloak);
          this.groupBox4.Controls.Add(this.shipShields);
          this.groupBox4.Controls.Add(this.shipArmor);
          this.groupBox4.Controls.Add(this.maxCapacity);
          this.groupBox4.Controls.Add(this.shipMass);
          this.groupBox4.Controls.Add(this.label15);
          this.groupBox4.Controls.Add(this.label14);
          this.groupBox4.Controls.Add(this.label13);
          this.groupBox4.Controls.Add(this.capacityUnits);
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
          this.capacityType.Location = new System.Drawing.Point(6, 34);
          this.capacityType.Name = "capacityType";
          this.capacityType.Size = new System.Drawing.Size(100, 13);
          this.capacityType.TabIndex = 1;
          this.capacityType.Text = "Max Fuel Capacity";
          // 
          // ShipCloak
          // 
          this.shipCloak.Location = new System.Drawing.Point(92, 79);
          this.shipCloak.Name = "shipCloak";
          this.shipCloak.Size = new System.Drawing.Size(50, 13);
          this.shipCloak.TabIndex = 14;
          this.shipCloak.Text = "0";
          this.shipCloak.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // ShipShields
          // 
          this.shipShields.Location = new System.Drawing.Point(92, 65);
          this.shipShields.Name = "shipShields";
          this.shipShields.Size = new System.Drawing.Size(50, 13);
          this.shipShields.TabIndex = 13;
          this.shipShields.Text = "0";
          this.shipShields.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // ShipArmor
          // 
          this.shipArmor.Location = new System.Drawing.Point(92, 50);
          this.shipArmor.Name = "shipArmor";
          this.shipArmor.Size = new System.Drawing.Size(50, 13);
          this.shipArmor.TabIndex = 12;
          this.shipArmor.Text = "0";
          this.shipArmor.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // MaxCapacity
          // 
          this.maxCapacity.Location = new System.Drawing.Point(92, 34);
          this.maxCapacity.Name = "maxCapacity";
          this.maxCapacity.Size = new System.Drawing.Size(50, 13);
          this.maxCapacity.TabIndex = 11;
          this.maxCapacity.Text = "0";
          this.maxCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
          // 
          // ShipMass
          // 
          this.shipMass.Location = new System.Drawing.Point(92, 19);
          this.shipMass.Name = "shipMass";
          this.shipMass.Size = new System.Drawing.Size(50, 13);
          this.shipMass.TabIndex = 10;
          this.shipMass.Text = "0";
          this.shipMass.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
          this.capacityUnits.AutoSize = true;
          this.capacityUnits.Location = new System.Drawing.Point(143, 34);
          this.capacityUnits.Name = "capacityUnits";
          this.capacityUnits.Size = new System.Drawing.Size(21, 13);
          this.capacityUnits.TabIndex = 6;
          this.capacityUnits.Text = "mg";
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
          this.groupBox3.Controls.Add(this.designResources);
          this.groupBox3.Location = new System.Drawing.Point(14, 435);
          this.groupBox3.Name = "groupBox3";
          this.groupBox3.Size = new System.Drawing.Size(166, 118);
          this.groupBox3.TabIndex = 14;
          this.groupBox3.TabStop = false;
          this.groupBox3.Text = "Build Cost";
          // 
          // DesignResources
          // 
          this.designResources.Location = new System.Drawing.Point(9, 19);
          this.designResources.Name = "designResources";
          this.designResources.Size = new System.Drawing.Size(150, 64);
          this.designResources.TabIndex = 10;
          this.designResources.Value = new Nova.Common.Resources(((int)(0)), ((int)(0)), ((int)(0)), ((int)(0)));
          // 
          // DesignName
          // 
          this.designName.Location = new System.Drawing.Point(23, 34);
          this.designName.Name = "designName";
          this.designName.Size = new System.Drawing.Size(238, 20);
          this.designName.TabIndex = 1;
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
          this.done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.done.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.done.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.done.Location = new System.Drawing.Point(688, 583);
          this.done.Name = "done";
          this.done.Size = new System.Drawing.Size(75, 23);
          this.done.TabIndex = 5;
          this.done.Text = "Done";
          this.done.Click += new System.EventHandler(this.Done_Click);
          // 
          // Delete
          // 
          this.delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
          this.delete.Location = new System.Drawing.Point(575, 584);
          this.delete.Name = "delete";
          this.delete.Size = new System.Drawing.Size(75, 23);
          this.delete.TabIndex = 9;
          this.delete.Text = "Delete";
          this.delete.UseVisualStyleBackColor = true;
          this.delete.Click += new System.EventHandler(this.Delete_Click);
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
          this.cargoCapacity.Location = new System.Drawing.Point(92, 94);
          this.cargoCapacity.Name = "cargoCapacity";
          this.cargoCapacity.Size = new System.Drawing.Size(50, 13);
          this.cargoCapacity.TabIndex = 17;
          this.cargoCapacity.Text = "0";
          this.cargoCapacity.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
          this.Controls.Add(this.delete);
          this.Controls.Add(this.groupBox1);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.done);
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
          ((System.ComponentModel.ISupportInitialize)(this.hullImage)).EndInit();
          this.groupBox4.ResumeLayout(false);
          this.groupBox4.PerformLayout();
          this.groupBox3.ResumeLayout(false);
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.GroupBox groupBox6;
      private System.Windows.Forms.TextBox description;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.PictureBox hullImage;
      private ControlLibrary.HullGrid hullGrid;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.Label shipCloak;
      private System.Windows.Forms.Label shipShields;
      private System.Windows.Forms.Label shipArmor;
      private System.Windows.Forms.Label maxCapacity;
      private System.Windows.Forms.Label shipMass;
      private System.Windows.Forms.Label label15;
      private System.Windows.Forms.Label label14;
      private System.Windows.Forms.Label label13;
      private System.Windows.Forms.Label capacityUnits;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label capacityType;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.GroupBox groupBox3;
      private ControlLibrary.ResourceDisplay designResources;
      private System.Windows.Forms.TextBox designName;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button done;
      private System.Windows.Forms.GroupBox groupBox7;
      private System.Windows.Forms.ListView designList;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.Button delete;
      private System.Windows.Forms.TextBox componentSummary;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ComboBox comboDesignOwner;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label cargoCapacity;
   }
}
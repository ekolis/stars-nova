namespace Nova.WinForms.Gui
{
   partial class ManageFleetDialog
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
         if (disposing && (components != null)) {
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
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.RenameButton = new System.Windows.Forms.Button();
         this.FleetName = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.MergeButton = new System.Windows.Forms.Button();
         this.FleetComposition = new System.Windows.Forms.ListView();
         this.DesignHeader = new System.Windows.Forms.ColumnHeader();
         this.NumberHeader = new System.Windows.Forms.ColumnHeader();
         this.CoLocatedFleets = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.RenameButton);
         this.groupBox1.Controls.Add(this.FleetName);
         this.groupBox1.Location = new System.Drawing.Point(13, 13);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(204, 70);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "FleetName";
         // 
         // RenameButton
         // 
         this.RenameButton.Location = new System.Drawing.Point(6, 38);
         this.RenameButton.Name = "RenameButton";
         this.RenameButton.Size = new System.Drawing.Size(60, 24);
         this.RenameButton.TabIndex = 2;
         this.RenameButton.Text = "Rename";
         this.RenameButton.UseVisualStyleBackColor = true;
         this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
         // 
         // FleetName
         // 
         this.FleetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.FleetName.Location = new System.Drawing.Point(6, 17);
         this.FleetName.Name = "FleetName";
         this.FleetName.Size = new System.Drawing.Size(188, 18);
         this.FleetName.TabIndex = 1;
         this.FleetName.Text = "Fleet Name";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.FleetComposition);
         this.groupBox2.Location = new System.Drawing.Point(13, 90);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(204, 233);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Fleet Coposition";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.CoLocatedFleets);
         this.groupBox3.Location = new System.Drawing.Point(276, 90);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(204, 233);
         this.groupBox3.TabIndex = 2;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Co-located Fleets";
         // 
         // MergeButton
         // 
         this.MergeButton.Enabled = false;
         this.MergeButton.Location = new System.Drawing.Point(230, 166);
         this.MergeButton.Name = "MergeButton";
         this.MergeButton.Size = new System.Drawing.Size(33, 29);
         this.MergeButton.TabIndex = 3;
         this.MergeButton.Text = "<-";
         this.MergeButton.UseVisualStyleBackColor = true;
         this.MergeButton.Click += new System.EventHandler(this.MergeButton_Click);
         // 
         // FleetComposition
         // 
         this.FleetComposition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DesignHeader,
            this.NumberHeader});
         this.FleetComposition.Dock = System.Windows.Forms.DockStyle.Fill;
         this.FleetComposition.Location = new System.Drawing.Point(3, 16);
         this.FleetComposition.Name = "FleetComposition";
         this.FleetComposition.Size = new System.Drawing.Size(198, 214);
         this.FleetComposition.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.FleetComposition.TabIndex = 0;
         this.FleetComposition.UseCompatibleStateImageBehavior = false;
         this.FleetComposition.View = System.Windows.Forms.View.Details;
         // 
         // DesignHeader
         // 
         this.DesignHeader.Text = "Design";
         this.DesignHeader.Width = 140;
         // 
         // NumberHeader
         // 
         this.NumberHeader.Text = "Number";
         this.NumberHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.NumberHeader.Width = 54;
         // 
         // CoLocatedFleets
         // 
         this.CoLocatedFleets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
         this.CoLocatedFleets.Dock = System.Windows.Forms.DockStyle.Fill;
         this.CoLocatedFleets.Location = new System.Drawing.Point(3, 16);
         this.CoLocatedFleets.Name = "CoLocatedFleets";
         this.CoLocatedFleets.Size = new System.Drawing.Size(198, 214);
         this.CoLocatedFleets.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.CoLocatedFleets.TabIndex = 1;
         this.CoLocatedFleets.UseCompatibleStateImageBehavior = false;
         this.CoLocatedFleets.View = System.Windows.Forms.View.Details;
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Design";
         this.columnHeader1.Width = 140;
         // 
         // ManageFleetDialog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(492, 332);
         this.Controls.Add(this.MergeButton);
         this.Controls.Add(this.groupBox3);
         this.Controls.Add(this.groupBox2);
         this.Controls.Add(this.groupBox1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.Name = "ManageFleetDialog";
         this.Text = "Nova - Manage Fleet";
         this.groupBox1.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox3.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label FleetName;
      private System.Windows.Forms.Button RenameButton;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.Button MergeButton;
      private System.Windows.Forms.ListView FleetComposition;
      private System.Windows.Forms.ColumnHeader DesignHeader;
      private System.Windows.Forms.ColumnHeader NumberHeader;
      private System.Windows.Forms.ListView CoLocatedFleets;
      private System.Windows.Forms.ColumnHeader columnHeader1;
   }
}
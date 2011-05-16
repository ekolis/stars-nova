namespace Nova.WinForms.Gui
{
   public partial class ManageFleetDialog
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
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.renameButton = new System.Windows.Forms.Button();
         this.fleetName = new System.Windows.Forms.Label();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.mergeButton = new System.Windows.Forms.Button();
         this.fleetComposition = new System.Windows.Forms.ListView();
         this.designHeader = new System.Windows.Forms.ColumnHeader();
         this.numberHeader = new System.Windows.Forms.ColumnHeader();
         this.coLocatedFleets = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.groupBox1.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.SuspendLayout();
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.renameButton);
         this.groupBox1.Controls.Add(this.fleetName);
         this.groupBox1.Location = new System.Drawing.Point(13, 13);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(204, 70);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "FleetName";
         // 
         // RenameButton
         // 
         this.renameButton.Location = new System.Drawing.Point(6, 38);
         this.renameButton.Name = "renameButton";
         this.renameButton.Size = new System.Drawing.Size(60, 24);
         this.renameButton.TabIndex = 2;
         this.renameButton.Text = "Rename";
         this.renameButton.UseVisualStyleBackColor = true;
         this.renameButton.Click += new System.EventHandler(this.RenameButton_Click);
         // 
         // FleetName
         // 
         this.fleetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.fleetName.Location = new System.Drawing.Point(6, 17);
         this.fleetName.Name = "fleetName";
         this.fleetName.Size = new System.Drawing.Size(188, 18);
         this.fleetName.TabIndex = 1;
         this.fleetName.Text = "Fleet Name";
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.fleetComposition);
         this.groupBox2.Location = new System.Drawing.Point(13, 90);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(204, 233);
         this.groupBox2.TabIndex = 1;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Fleet Coposition";
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.coLocatedFleets);
         this.groupBox3.Location = new System.Drawing.Point(276, 90);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(204, 233);
         this.groupBox3.TabIndex = 2;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Co-located Fleets";
         // 
         // MergeButton
         // 
         this.mergeButton.Enabled = false;
         this.mergeButton.Location = new System.Drawing.Point(230, 166);
         this.mergeButton.Name = "mergeButton";
         this.mergeButton.Size = new System.Drawing.Size(33, 29);
         this.mergeButton.TabIndex = 3;
         this.mergeButton.Text = "<-";
         this.mergeButton.UseVisualStyleBackColor = true;
         this.mergeButton.Click += new System.EventHandler(this.MergeButton_Click);
         // 
         // FleetComposition
         // 
         this.fleetComposition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.designHeader,
            this.numberHeader});
         this.fleetComposition.Dock = System.Windows.Forms.DockStyle.Fill;
         this.fleetComposition.Location = new System.Drawing.Point(3, 16);
         this.fleetComposition.Name = "fleetComposition";
         this.fleetComposition.Size = new System.Drawing.Size(198, 214);
         this.fleetComposition.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.fleetComposition.TabIndex = 0;
         this.fleetComposition.UseCompatibleStateImageBehavior = false;
         this.fleetComposition.View = System.Windows.Forms.View.Details;
         // 
         // DesignHeader
         // 
         this.designHeader.Text = "Design";
         this.designHeader.Width = 140;
         // 
         // NumberHeader
         // 
         this.numberHeader.Text = "Number";
         this.numberHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         this.numberHeader.Width = 54;
         // 
         // CoLocatedFleets
         // 
         this.coLocatedFleets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
         this.coLocatedFleets.Dock = System.Windows.Forms.DockStyle.Fill;
         this.coLocatedFleets.Location = new System.Drawing.Point(3, 16);
         this.coLocatedFleets.Name = "coLocatedFleets";
         this.coLocatedFleets.Size = new System.Drawing.Size(198, 214);
         this.coLocatedFleets.Sorting = System.Windows.Forms.SortOrder.Ascending;
         this.coLocatedFleets.TabIndex = 1;
         this.coLocatedFleets.UseCompatibleStateImageBehavior = false;
         this.coLocatedFleets.View = System.Windows.Forms.View.Details;
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
         this.Controls.Add(this.mergeButton);
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
      private System.Windows.Forms.Label fleetName;
      private System.Windows.Forms.Button renameButton;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.Button mergeButton;
      private System.Windows.Forms.ListView fleetComposition;
      private System.Windows.Forms.ColumnHeader designHeader;
      private System.Windows.Forms.ColumnHeader numberHeader;
      private System.Windows.Forms.ListView coLocatedFleets;
      private System.Windows.Forms.ColumnHeader columnHeader1;
   }
}
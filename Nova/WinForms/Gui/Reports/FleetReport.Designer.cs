namespace Nova.WinForms.Gui
{
   public partial class FleetReport
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FleetReport));
          this.fleetGridView = new System.Windows.Forms.DataGridView();
          this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.fleetGridView)).BeginInit();
          this.SuspendLayout();
          // 
          // FleetGridView
          // 
          this.fleetGridView.AllowUserToAddRows = false;
          this.fleetGridView.AllowUserToDeleteRows = false;
          this.fleetGridView.AllowUserToOrderColumns = true;
          this.fleetGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.fleetGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          this.fleetGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.fleetGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.fleetGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.column2,
            this.column3,
            this.column4,
            this.column5,
            this.column6,
            this.column7,
            this.column8,
            this.column9,
            this.column10,
            this.column11});
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
          dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.fleetGridView.DefaultCellStyle = dataGridViewCellStyle2;
          this.fleetGridView.Location = new System.Drawing.Point(0, 0);
          this.fleetGridView.MultiSelect = false;
          this.fleetGridView.Name = "fleetGridView";
          this.fleetGridView.ReadOnly = true;
          this.fleetGridView.Size = new System.Drawing.Size(994, 22);
          this.fleetGridView.TabIndex = 0;
          // 
          // Column1
          // 
          this.column1.HeaderText = "Fleet Name";
          this.column1.Name = "Column1";
          this.column1.ReadOnly = true;
          // 
          // Column2
          // 
          this.column2.HeaderText = "Location";
          this.column2.Name = "Column2";
          this.column2.ReadOnly = true;
          // 
          // Column3
          // 
          this.column3.HeaderText = "Destination";
          this.column3.Name = "Column3";
          this.column3.ReadOnly = true;
          // 
          // Column4
          // 
          this.column4.HeaderText = "ETA (yr)";
          this.column4.Name = "Column4";
          this.column4.ReadOnly = true;
          // 
          // Column5
          // 
          this.column5.HeaderText = "Task";
          this.column5.Name = "Column5";
          this.column5.ReadOnly = true;
          // 
          // Column6
          // 
          this.column6.HeaderText = "Fuel (mg)";
          this.column6.Name = "Column6";
          this.column6.ReadOnly = true;
          // 
          // Column7
          // 
          this.column7.HeaderText = "Cargo";
          this.column7.Name = "Column7";
          this.column7.ReadOnly = true;
          // 
          // Column8
          // 
          this.column8.HeaderText = "Ships";
          this.column8.Name = "Column8";
          this.column8.ReadOnly = true;
          // 
          // Column9
          // 
          this.column9.HeaderText = "Cloak";
          this.column9.Name = "Column9";
          this.column9.ReadOnly = true;
          // 
          // Column10
          // 
          this.column10.HeaderText = "Battle Plan";
          this.column10.Name = "Column10";
          this.column10.ReadOnly = true;
          // 
          // Column11
          // 
          this.column11.HeaderText = "Mass (kT)";
          this.column11.Name = "Column11";
          this.column11.ReadOnly = true;
          // 
          // FleetReport
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSize = true;
          this.ClientSize = new System.Drawing.Size(992, 22);
          this.Controls.Add(this.fleetGridView);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FleetReport";
          this.Text = "Nova Fleet Report";
          this.Load += new System.EventHandler(this.OnLoad);
          ((System.ComponentModel.ISupportInitialize)(this.fleetGridView)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView fleetGridView;
      private System.Windows.Forms.DataGridViewTextBoxColumn column1;
      private System.Windows.Forms.DataGridViewTextBoxColumn column2;
      private System.Windows.Forms.DataGridViewTextBoxColumn column3;
      private System.Windows.Forms.DataGridViewTextBoxColumn column4;
      private System.Windows.Forms.DataGridViewTextBoxColumn column5;
      private System.Windows.Forms.DataGridViewTextBoxColumn column6;
      private System.Windows.Forms.DataGridViewTextBoxColumn column7;
      private System.Windows.Forms.DataGridViewTextBoxColumn column8;
      private System.Windows.Forms.DataGridViewTextBoxColumn column9;
      private System.Windows.Forms.DataGridViewTextBoxColumn column10;
      private System.Windows.Forms.DataGridViewTextBoxColumn column11;
   }
}
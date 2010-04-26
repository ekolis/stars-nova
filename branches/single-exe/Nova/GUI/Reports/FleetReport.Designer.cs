namespace Nova.Gui.Reports
{
   partial class FleetReport
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FleetReport));
          this.FleetGridView = new System.Windows.Forms.DataGridView();
          this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.FleetGridView)).BeginInit();
          this.SuspendLayout();
          // 
          // FleetGridView
          // 
          this.FleetGridView.AllowUserToAddRows = false;
          this.FleetGridView.AllowUserToDeleteRows = false;
          this.FleetGridView.AllowUserToOrderColumns = true;
          this.FleetGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.FleetGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          this.FleetGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.FleetGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.FleetGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
          dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.FleetGridView.DefaultCellStyle = dataGridViewCellStyle2;
          this.FleetGridView.Location = new System.Drawing.Point(0, 0);
          this.FleetGridView.MultiSelect = false;
          this.FleetGridView.Name = "FleetGridView";
          this.FleetGridView.ReadOnly = true;
          this.FleetGridView.Size = new System.Drawing.Size(994, 22);
          this.FleetGridView.TabIndex = 0;
          // 
          // Column1
          // 
          this.Column1.HeaderText = "Fleet Name";
          this.Column1.Name = "Column1";
          this.Column1.ReadOnly = true;
          // 
          // Column2
          // 
          this.Column2.HeaderText = "Location";
          this.Column2.Name = "Column2";
          this.Column2.ReadOnly = true;
          // 
          // Column3
          // 
          this.Column3.HeaderText = "Destination";
          this.Column3.Name = "Column3";
          this.Column3.ReadOnly = true;
          // 
          // Column4
          // 
          this.Column4.HeaderText = "ETA (yr)";
          this.Column4.Name = "Column4";
          this.Column4.ReadOnly = true;
          // 
          // Column5
          // 
          this.Column5.HeaderText = "Task";
          this.Column5.Name = "Column5";
          this.Column5.ReadOnly = true;
          // 
          // Column6
          // 
          this.Column6.HeaderText = "Fuel (mg)";
          this.Column6.Name = "Column6";
          this.Column6.ReadOnly = true;
          // 
          // Column7
          // 
          this.Column7.HeaderText = "Cargo";
          this.Column7.Name = "Column7";
          this.Column7.ReadOnly = true;
          // 
          // Column8
          // 
          this.Column8.HeaderText = "Ships";
          this.Column8.Name = "Column8";
          this.Column8.ReadOnly = true;
          // 
          // Column9
          // 
          this.Column9.HeaderText = "Cloak";
          this.Column9.Name = "Column9";
          this.Column9.ReadOnly = true;
          // 
          // Column10
          // 
          this.Column10.HeaderText = "Battle Plan";
          this.Column10.Name = "Column10";
          this.Column10.ReadOnly = true;
          // 
          // Column11
          // 
          this.Column11.HeaderText = "Mass (kT)";
          this.Column11.Name = "Column11";
          this.Column11.ReadOnly = true;
          // 
          // FleetReport
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSize = true;
          this.ClientSize = new System.Drawing.Size(992, 22);
          this.Controls.Add(this.FleetGridView);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "FleetReport";
          this.Text = "Nova Fleet Report";
          this.Load += new System.EventHandler(this.OnLoad);
          ((System.ComponentModel.ISupportInitialize)(this.FleetGridView)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView FleetGridView;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
   }
}
namespace Nova.WinForms.Gui
{
   partial class PlanetReport
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
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanetReport));
         this.PlanetGridView = new System.Windows.Forms.DataGridView();
         this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
         ((System.ComponentModel.ISupportInitialize)(this.PlanetGridView)).BeginInit();
         this.SuspendLayout();
         // 
         // PlanetGridView
         // 
         this.PlanetGridView.AllowUserToAddRows = false;
         this.PlanetGridView.AllowUserToDeleteRows = false;
         this.PlanetGridView.AllowUserToOrderColumns = true;
         this.PlanetGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.PlanetGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
         dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
         dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         this.PlanetGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
         this.PlanetGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.PlanetGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column11,
            this.Column12});
         dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
         dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
         dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
         dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
         this.PlanetGridView.DefaultCellStyle = dataGridViewCellStyle2;
         this.PlanetGridView.Location = new System.Drawing.Point(-6, -3);
         this.PlanetGridView.MultiSelect = false;
         this.PlanetGridView.Name = "PlanetGridView";
         this.PlanetGridView.ReadOnly = true;
         dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
         dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         this.PlanetGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
         this.PlanetGridView.Size = new System.Drawing.Size(1000, 23);
         this.PlanetGridView.TabIndex = 0;
         // 
         // Column1
         // 
         this.Column1.HeaderText = "Planet Name";
         this.Column1.Name = "Column1";
         this.Column1.ReadOnly = true;
         // 
         // Column2
         // 
         this.Column2.HeaderText = "Starbase";
         this.Column2.Name = "Column2";
         this.Column2.ReadOnly = true;
         // 
         // Column3
         // 
         this.Column3.HeaderText = "Population";
         this.Column3.Name = "Column3";
         this.Column3.ReadOnly = true;
         // 
         // Column4
         // 
         this.Column4.HeaderText = "Capacity (%)";
         this.Column4.Name = "Column4";
         this.Column4.ReadOnly = true;
         // 
         // Column5
         // 
         this.Column5.HeaderText = "Value (%)";
         this.Column5.Name = "Column5";
         this.Column5.ReadOnly = true;
         // 
         // Column6
         // 
         this.Column6.HeaderText = "Mines";
         this.Column6.Name = "Column6";
         this.Column6.ReadOnly = true;
         // 
         // Column7
         // 
         this.Column7.HeaderText = "Factories";
         this.Column7.Name = "Column7";
         this.Column7.ReadOnly = true;
         // 
         // Column8
         // 
         this.Column8.HeaderText = "Defenses";
         this.Column8.Name = "Column8";
         this.Column8.ReadOnly = true;
         // 
         // Column9
         // 
         this.Column9.HeaderText = "Minerals";
         this.Column9.Name = "Column9";
         this.Column9.ReadOnly = true;
         // 
         // Column11
         // 
         this.Column11.HeaderText = "Concentration (%)";
         this.Column11.Name = "Column11";
         this.Column11.ReadOnly = true;
         // 
         // Column12
         // 
         this.Column12.HeaderText = "Resources";
         this.Column12.Name = "Column12";
         this.Column12.ReadOnly = true;
         // 
         // PlanetReport
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoSize = true;
         this.ClientSize = new System.Drawing.Size(999, 16);
         this.Controls.Add(this.PlanetGridView);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "PlanetReport";
         this.Text = "Nova Planet Report";
         this.Load += new System.EventHandler(this.OnLoad);
         ((System.ComponentModel.ISupportInitialize)(this.PlanetGridView)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView PlanetGridView;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
   }
}
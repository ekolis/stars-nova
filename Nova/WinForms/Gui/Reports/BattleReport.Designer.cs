namespace Nova.WinForms.Gui
{
   public partial class BattleReportDialog
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleReportDialog));
          this.BattleGridView = new System.Windows.Forms.DataGridView();
          this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.BattleGridView)).BeginInit();
          this.SuspendLayout();
          // 
          // BattleGridView
          // 
          this.BattleGridView.AllowUserToAddRows = false;
          this.BattleGridView.AllowUserToDeleteRows = false;
          this.BattleGridView.AllowUserToOrderColumns = true;
          this.BattleGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.BattleGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          this.BattleGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.BattleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.BattleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
          dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.BattleGridView.DefaultCellStyle = dataGridViewCellStyle2;
          this.BattleGridView.Location = new System.Drawing.Point(-1, -2);
          this.BattleGridView.MultiSelect = false;
          this.BattleGridView.Name = "BattleGridView";
          this.BattleGridView.ReadOnly = true;
          this.BattleGridView.Size = new System.Drawing.Size(645, 21);
          this.BattleGridView.TabIndex = 0;
          this.BattleGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BattleGridView_CellContentDoubleClick);
          // 
          // Column1
          // 
          this.Column1.HeaderText = "Location";
          this.Column1.Name = "Column1";
          this.Column1.ReadOnly = true;
          // 
          // Column2
          // 
          this.Column2.HeaderText = "Sides";
          this.Column2.Name = "Column2";
          this.Column2.ReadOnly = true;
          // 
          // Column3
          // 
          this.Column3.HeaderText = "Ours";
          this.Column3.Name = "Column3";
          this.Column3.ReadOnly = true;
          // 
          // Column4
          // 
          this.Column4.HeaderText = "Theirs";
          this.Column4.Name = "Column4";
          this.Column4.ReadOnly = true;
          // 
          // Column5
          // 
          this.Column5.HeaderText = "Our Dead";
          this.Column5.Name = "Column5";
          this.Column5.ReadOnly = true;
          // 
          // Column6
          // 
          this.Column6.HeaderText = "Their Dead";
          this.Column6.Name = "Column6";
          this.Column6.ReadOnly = true;
          // 
          // BattleReportDialog
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSize = true;
          this.ClientSize = new System.Drawing.Size(643, 19);
          this.Controls.Add(this.BattleGridView);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "BattleReportDialog";
          this.Text = "Nova Battle Report";
          this.Load += new System.EventHandler(this.OnLoad);
          ((System.ComponentModel.ISupportInitialize)(this.BattleGridView)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView BattleGridView;
      private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
       private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
   }
}
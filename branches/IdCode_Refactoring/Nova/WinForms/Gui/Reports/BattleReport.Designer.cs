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
          this.battleGridView = new System.Windows.Forms.DataGridView();
          this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.battleGridView)).BeginInit();
          this.SuspendLayout();
          // 
          // BattleGridView
          // 
          this.battleGridView.AllowUserToAddRows = false;
          this.battleGridView.AllowUserToDeleteRows = false;
          this.battleGridView.AllowUserToOrderColumns = true;
          this.battleGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.battleGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          this.battleGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.battleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.battleGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.column2,
            this.column3,
            this.column4,
            this.column5,
            this.column6});
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
          dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.battleGridView.DefaultCellStyle = dataGridViewCellStyle2;
          this.battleGridView.Location = new System.Drawing.Point(-1, -2);
          this.battleGridView.MultiSelect = false;
          this.battleGridView.Name = "battleGridView";
          this.battleGridView.ReadOnly = true;
          this.battleGridView.Size = new System.Drawing.Size(645, 21);
          this.battleGridView.TabIndex = 0;
          this.battleGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BattleGridView_CellContentDoubleClick);
          // 
          // Column1
          // 
          this.column1.HeaderText = "Location";
          this.column1.Name = "Column1";
          this.column1.ReadOnly = true;
          // 
          // Column2
          // 
          this.column2.HeaderText = "Sides";
          this.column2.Name = "Column2";
          this.column2.ReadOnly = true;
          // 
          // Column3
          // 
          this.column3.HeaderText = "Ours";
          this.column3.Name = "Column3";
          this.column3.ReadOnly = true;
          // 
          // Column4
          // 
          this.column4.HeaderText = "Theirs";
          this.column4.Name = "Column4";
          this.column4.ReadOnly = true;
          // 
          // Column5
          // 
          this.column5.HeaderText = "Our Dead";
          this.column5.Name = "Column5";
          this.column5.ReadOnly = true;
          // 
          // Column6
          // 
          this.column6.HeaderText = "Their Dead";
          this.column6.Name = "Column6";
          this.column6.ReadOnly = true;
          // 
          // BattleReportDialog
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.AutoSize = true;
          this.ClientSize = new System.Drawing.Size(643, 19);
          this.Controls.Add(this.battleGridView);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "BattleReportDialog";
          this.Text = "Nova Battle Report";
          this.Load += new System.EventHandler(this.OnLoad);
          ((System.ComponentModel.ISupportInitialize)(this.battleGridView)).EndInit();
          this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.DataGridView battleGridView;
      private System.Windows.Forms.DataGridViewTextBoxColumn column1;
       private System.Windows.Forms.DataGridViewTextBoxColumn column2;
       private System.Windows.Forms.DataGridViewTextBoxColumn column3;
       private System.Windows.Forms.DataGridViewTextBoxColumn column4;
       private System.Windows.Forms.DataGridViewTextBoxColumn column5;
       private System.Windows.Forms.DataGridViewTextBoxColumn column6;
   }
}
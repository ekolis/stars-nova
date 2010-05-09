namespace Nova.WinForms.Gui
{
    partial class ScoreReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreReport));
            this.ScoreGridView = new System.Windows.Forms.DataGridView();
            this.Race = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Planets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Starbases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnarmedShips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EscortShips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapitalShips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TechLevels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resources = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ScoreGridView
            // 
            this.ScoreGridView.AllowUserToAddRows = false;
            this.ScoreGridView.AllowUserToDeleteRows = false;
            this.ScoreGridView.AllowUserToOrderColumns = true;
            this.ScoreGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ScoreGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ScoreGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScoreGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Race,
            this.Rank,
            this.Score,
            this.Planets,
            this.Starbases,
            this.UnarmedShips,
            this.EscortShips,
            this.CapitalShips,
            this.TechLevels,
            this.Resources});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ScoreGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ScoreGridView.Location = new System.Drawing.Point(0, 0);
            this.ScoreGridView.Name = "ScoreGridView";
            this.ScoreGridView.ReadOnly = true;
            this.ScoreGridView.Size = new System.Drawing.Size(916, 24);
            this.ScoreGridView.TabIndex = 0;
            // 
            // Race
            // 
            this.Race.HeaderText = "Race";
            this.Race.Name = "Race";
            this.Race.ReadOnly = true;
            // 
            // Rank
            // 
            this.Rank.HeaderText = "Rank";
            this.Rank.Name = "Rank";
            this.Rank.ReadOnly = true;
            // 
            // Score
            // 
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            // 
            // Planets
            // 
            this.Planets.HeaderText = "Planets";
            this.Planets.Name = "Planets";
            this.Planets.ReadOnly = true;
            // 
            // Starbases
            // 
            this.Starbases.HeaderText = "Starbases";
            this.Starbases.Name = "Starbases";
            this.Starbases.ReadOnly = true;
            // 
            // UnarmedShips
            // 
            this.UnarmedShips.HeaderText = "Unarmed Ships";
            this.UnarmedShips.Name = "UnarmedShips";
            this.UnarmedShips.ReadOnly = true;
            // 
            // EscortShips
            // 
            this.EscortShips.HeaderText = "Escort Ships";
            this.EscortShips.Name = "EscortShips";
            this.EscortShips.ReadOnly = true;
            // 
            // CapitalShips
            // 
            this.CapitalShips.HeaderText = "Capital Ships";
            this.CapitalShips.Name = "CapitalShips";
            this.CapitalShips.ReadOnly = true;
            // 
            // TechLevels
            // 
            this.TechLevels.HeaderText = "Tech Levels";
            this.TechLevels.Name = "TechLevels";
            this.TechLevels.ReadOnly = true;
            // 
            // Resources
            // 
            this.Resources.HeaderText = "Resources";
            this.Resources.Name = "Resources";
            this.Resources.ReadOnly = true;
            // 
            // ScoreReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(916, 24);
            this.Controls.Add(this.ScoreGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScoreReport";
            this.Text = "Score Report";
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ScoreGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ScoreGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Race;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rank;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn Planets;
        private System.Windows.Forms.DataGridViewTextBoxColumn Starbases;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnarmedShips;
        private System.Windows.Forms.DataGridViewTextBoxColumn EscortShips;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapitalShips;
        private System.Windows.Forms.DataGridViewTextBoxColumn TechLevels;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resources;


    }
}
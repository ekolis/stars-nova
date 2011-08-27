namespace Nova.WinForms.Gui
{
    public partial class ScoreReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoreReport));
            this.scoreGridView = new System.Windows.Forms.DataGridView();
            this.race = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planets = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starbases = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unarmedShips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.escortShips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capitalShips = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.techLevels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resources = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scoreGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ScoreGridView
            // 
            this.scoreGridView.AllowUserToAddRows = false;
            this.scoreGridView.AllowUserToDeleteRows = false;
            this.scoreGridView.AllowUserToOrderColumns = true;
            this.scoreGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.scoreGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.scoreGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scoreGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.race,
            this.rank,
            this.score,
            this.planets,
            this.starbases,
            this.unarmedShips,
            this.escortShips,
            this.capitalShips,
            this.techLevels,
            this.resources});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.scoreGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.scoreGridView.Location = new System.Drawing.Point(0, 0);
            this.scoreGridView.Name = "scoreGridView";
            this.scoreGridView.ReadOnly = true;
            this.scoreGridView.Size = new System.Drawing.Size(916, 24);
            this.scoreGridView.TabIndex = 0;
            // 
            // Race
            // 
            this.race.HeaderText = "Race";
            this.race.Name = "Race";
            this.race.ReadOnly = true;
            // 
            // Rank
            // 
            this.rank.HeaderText = "Rank";
            this.rank.Name = "Rank";
            this.rank.ReadOnly = true;
            // 
            // Score
            // 
            this.score.HeaderText = "Score";
            this.score.Name = "Score";
            this.score.ReadOnly = true;
            // 
            // Planets
            // 
            this.planets.HeaderText = "Planets";
            this.planets.Name = "Planets";
            this.planets.ReadOnly = true;
            // 
            // Starbases
            // 
            this.starbases.HeaderText = "Starbases";
            this.starbases.Name = "Starbases";
            this.starbases.ReadOnly = true;
            // 
            // UnarmedShips
            // 
            this.unarmedShips.HeaderText = "Unarmed Ships";
            this.unarmedShips.Name = "UnarmedShips";
            this.unarmedShips.ReadOnly = true;
            // 
            // EscortShips
            // 
            this.escortShips.HeaderText = "Escort Ships";
            this.escortShips.Name = "EscortShips";
            this.escortShips.ReadOnly = true;
            // 
            // CapitalShips
            // 
            this.capitalShips.HeaderText = "Capital Ships";
            this.capitalShips.Name = "CapitalShips";
            this.capitalShips.ReadOnly = true;
            // 
            // TechLevels
            // 
            this.techLevels.HeaderText = "Tech Levels";
            this.techLevels.Name = "TechLevels";
            this.techLevels.ReadOnly = true;
            // 
            // Resources
            // 
            this.resources.HeaderText = "Resources";
            this.resources.Name = "Resources";
            this.resources.ReadOnly = true;
            // 
            // ScoreReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(916, 24);
            this.Controls.Add(this.scoreGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScoreReport";
            this.Text = "Score Report";
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.scoreGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView scoreGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn race;
        private System.Windows.Forms.DataGridViewTextBoxColumn rank;
        private System.Windows.Forms.DataGridViewTextBoxColumn score;
        private System.Windows.Forms.DataGridViewTextBoxColumn planets;
        private System.Windows.Forms.DataGridViewTextBoxColumn starbases;
        private System.Windows.Forms.DataGridViewTextBoxColumn unarmedShips;
        private System.Windows.Forms.DataGridViewTextBoxColumn escortShips;
        private System.Windows.Forms.DataGridViewTextBoxColumn capitalShips;
        private System.Windows.Forms.DataGridViewTextBoxColumn techLevels;
        private System.Windows.Forms.DataGridViewTextBoxColumn resources;


    }
}
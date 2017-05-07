namespace Nova.WinForms.Launcher
{
    public partial class NovaLauncher
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.versionNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newGameButton = new System.Windows.Forms.Button();
            this.openGameButton = new System.Windows.Forms.Button();
            this.webLink = new System.Windows.Forms.LinkLabel();
            this.continueGameButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.raceDesignerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(216, 127);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(45, 13);
            this.versionLabel.TabIndex = 0;
            this.versionLabel.Text = "Version:";
            // 
            // versionNumber
            // 
            this.versionNumber.AutoSize = true;
            this.versionNumber.BackColor = System.Drawing.Color.Transparent;
            this.versionNumber.ForeColor = System.Drawing.Color.White;
            this.versionNumber.Location = new System.Drawing.Point(267, 127);
            this.versionNumber.Name = "versionNumber";
            this.versionNumber.Size = new System.Drawing.Size(240, 13);
            this.versionNumber.TabIndex = 1;
            this.versionNumber.Text = "Major.Milestone.Minor.Revision - MMM DD YYYY";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(212, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stars! Nova";
            // 
            // newGameButton
            // 
            this.newGameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.newGameButton.ForeColor = System.Drawing.Color.White;
            this.newGameButton.Location = new System.Drawing.Point(12, 301);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(106, 23);
            this.newGameButton.TabIndex = 1;
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = false;
            this.newGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // openGameButton
            // 
            this.openGameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.openGameButton.ForeColor = System.Drawing.Color.White;
            this.openGameButton.Location = new System.Drawing.Point(134, 301);
            this.openGameButton.Name = "openGameButton";
            this.openGameButton.Size = new System.Drawing.Size(106, 23);
            this.openGameButton.TabIndex = 5;
            this.openGameButton.Text = "Open Game";
            this.openGameButton.UseVisualStyleBackColor = false;
            this.openGameButton.Click += new System.EventHandler(this.OpenGameButton_Click);
            // 
            // webLink
            // 
            this.webLink.AutoSize = true;
            this.webLink.BackColor = System.Drawing.Color.Transparent;
            this.webLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webLink.ForeColor = System.Drawing.Color.White;
            this.webLink.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.webLink.Location = new System.Drawing.Point(361, 206);
            this.webLink.Name = "webLink";
            this.webLink.Size = new System.Drawing.Size(146, 27);
            this.webLink.TabIndex = 6;
            this.webLink.TabStop = true;
            this.webLink.Text = "Nova Webpage";
            this.webLink.UseCompatibleTextRendering = true;
            this.webLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebLink_LinkClicked);
            // 
            // continueGameButton
            // 
            this.continueGameButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.continueGameButton.ForeColor = System.Drawing.Color.White;
            this.continueGameButton.Location = new System.Drawing.Point(261, 301);
            this.continueGameButton.Name = "continueGameButton";
            this.continueGameButton.Size = new System.Drawing.Size(106, 23);
            this.continueGameButton.TabIndex = 7;
            this.continueGameButton.Text = "Continue Game";
            this.continueGameButton.UseVisualStyleBackColor = false;
            this.continueGameButton.Click += new System.EventHandler(this.ContinueGameButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Location = new System.Drawing.Point(517, 301);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(106, 23);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // raceDesignerButton
            // 
            this.raceDesignerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.raceDesignerButton.ForeColor = System.Drawing.Color.White;
            this.raceDesignerButton.Location = new System.Drawing.Point(387, 301);
            this.raceDesignerButton.Name = "raceDesignerButton";
            this.raceDesignerButton.Size = new System.Drawing.Size(106, 23);
            this.raceDesignerButton.TabIndex = 9;
            this.raceDesignerButton.Text = "Race Designer";
            this.raceDesignerButton.UseVisualStyleBackColor = false;
            this.raceDesignerButton.Click += new System.EventHandler(this.RaceDesignerButton_Click);
            // 
            // NovaLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Nova.Properties.Resources.Plasma;
            this.ClientSize = new System.Drawing.Size(635, 336);
            this.Controls.Add(this.raceDesignerButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.continueGameButton);
            this.Controls.Add(this.webLink);
            this.Controls.Add(this.openGameButton);
            this.Controls.Add(this.newGameButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.versionNumber);
            this.Controls.Add(this.versionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "NovaLauncher";
            this.Text = "Nova Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label versionNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Button openGameButton;
        private System.Windows.Forms.Button continueGameButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button raceDesignerButton;
        private System.Windows.Forms.LinkLabel webLink;
    }
}


namespace Nova.WinForms.Gui
{
    using System.Windows.Forms;
    
    public partial class SelectionSummary
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
        
        #region Component Designer generated code
        
        /// <Summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </Summary>
        private void InitializeComponent()
        {
            this.planetSummary = new Nova.WinForms.Gui.PlanetSummary(empireState);
            this.fleetSummary = new Nova.WinForms.Gui.FleetSummary(empireState);
            this.summaryFrame = new System.Windows.Forms.GroupBox();
            this.summaryFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectedItem
            //
            this.summaryFrame.Controls.Add(this.planetSummary);
            this.summaryFrame.Controls.Add(this.fleetSummary);            
            this.summaryFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryFrame.Location = new System.Drawing.Point(50, 50);
            this.summaryFrame.Name = "summaryFrame";
            this.summaryFrame.Size = new System.Drawing.Size(359, 177);
            this.summaryFrame.TabIndex = 0;
            this.summaryFrame.TabStop = false;
            this.summaryFrame.Text = "Summary of Selected Item";
            // 
            // SelectionSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.summaryFrame);
            this.Name = "SelectionSummary";
            this.Size = new System.Drawing.Size(359, 177);
            this.summaryFrame.ResumeLayout();
            this.ResumeLayout();
        }
        
        #endregion
        
        private System.Windows.Forms.GroupBox summaryFrame;
        private Nova.WinForms.Gui.PlanetSummary planetSummary;
        private Nova.WinForms.Gui.FleetSummary fleetSummary;
    }
}

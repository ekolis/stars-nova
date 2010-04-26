namespace Nova.Gui.Controls
{
   public class MyPanel : System.Windows.Forms.Panel
   {
      /*public override bool PreProcessMessage(ref System.Windows.Forms.Message msg)
      {
         const int WM_ERASEBKGND = 0x3c;
         //int i = 10;
         if (msg.Msg == WM_ERASEBKGND)
         {
            base.PreProcessMessage(ref msg);
            return true;
         }
         base.PreProcessMessage(ref msg);
         return true;
      }*/

      protected override void WndProc(ref System.Windows.Forms.Message m)
      {
         const int WM_ERASEBKGND = 0x14;
         //int i = 10;
         if (m.Msg == WM_ERASEBKGND)
         {
            //base.WndProc(ref m);
            //do nothing...
            return;
         }
         base.WndProc(ref m);

      } 

   };

   partial class StarMap
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
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          this.components = new System.ComponentModel.Container();
          //this.MapPanel = new System.Windows.Forms.Panel();
          this.MapPanel = new MyPanel();
          this.VScrollBar = new System.Windows.Forms.VScrollBar();
          this.HScrollBar = new System.Windows.Forms.HScrollBar();
          this.ZoomIn = new System.Windows.Forms.Button();
          this.ZoomOut = new System.Windows.Forms.Button();
          this.ToggleNames = new System.Windows.Forms.CheckBox();
          this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
          this.SuspendLayout();
          // 
          // MapPanel
          // 
          this.MapPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.MapPanel.BackColor = System.Drawing.Color.Black;
          this.MapPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.MapPanel.Cursor = System.Windows.Forms.Cursors.Cross;
          this.MapPanel.Location = new System.Drawing.Point(3, 30);
          this.MapPanel.Name = "MapPanel";
          this.MapPanel.Size = new System.Drawing.Size(535, 620);
          this.MapPanel.TabIndex = 6;
          this.MapPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
          this.MapPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StarMapMouse);

        
          // 
          // VScrollBar
          // 
          this.VScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.VScrollBar.Enabled = false; //added true replaced false
          this.VScrollBar.LargeChange = 1;
          this.VScrollBar.Location = new System.Drawing.Point(537, 32);
          this.VScrollBar.Name = "VScrollBar";
          this.VScrollBar.Size = new System.Drawing.Size(16, 611);
          this.VScrollBar.TabIndex = 5;
          this.VScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MapScrollV);
          // 
          // HScrollBar
          // 
          this.HScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.HScrollBar.Enabled = false;//added true replaced false
          this.HScrollBar.LargeChange = 1;
          this.HScrollBar.Location = new System.Drawing.Point(3, 652);
          this.HScrollBar.Name = "HScrollBar";
          this.HScrollBar.Size = new System.Drawing.Size(533, 16);
          this.HScrollBar.TabIndex = 3;
          this.HScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MapScrollH);
          // 
          // ZoomIn
          // 
          this.ZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.ZoomIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ZoomIn.Location = new System.Drawing.Point(3, 4);
          //this.ZoomIn.Enabled = true;//added completely 
          this.ZoomIn.Name = "ZoomIn";
          this.ZoomIn.Size = new System.Drawing.Size(32, 24);
          this.ZoomIn.TabIndex = 7;
          this.ZoomIn.TabStop = false;
          this.ZoomIn.Text = "+";
          this.toolTip1.SetToolTip(this.ZoomIn, "Zoom In\r\n");
          this.ZoomIn.Click += new System.EventHandler(this.ZoomInClick);
          // 
          // ZoomOut
          // 
          //this.ZoomOut.Enabled = false;
          this.ZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.ZoomOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ZoomOut.Location = new System.Drawing.Point(35, 4);
          //this.ZoomOut.Enabled = true;//added completely 
          this.ZoomOut.Name = "ZoomOut";
          this.ZoomOut.Size = new System.Drawing.Size(32, 24);
          this.ZoomOut.TabIndex = 4;
          this.ZoomOut.TabStop = false;
          this.ZoomOut.Text = "-";
          this.toolTip1.SetToolTip(this.ZoomOut, "Zoom Out");
          this.ZoomOut.Click += new System.EventHandler(this.ZoomOutClick);
          // 
          // ToggleNames
          // 
          this.ToggleNames.Appearance = System.Windows.Forms.Appearance.Button;
          this.ToggleNames.Checked = true;
          this.ToggleNames.CheckState = System.Windows.Forms.CheckState.Checked;
          this.ToggleNames.Location = new System.Drawing.Point(88, 4);
          this.ToggleNames.Name = "ToggleNames";
          this.ToggleNames.Size = new System.Drawing.Size(32, 24);
          this.ToggleNames.TabIndex = 8;
          this.ToggleNames.Text = "A";
          this.ToggleNames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          this.toolTip1.SetToolTip(this.ToggleNames, "Toggle star names");
          this.ToggleNames.UseVisualStyleBackColor = true;
          this.ToggleNames.CheckedChanged += new System.EventHandler(this.ToggleNames_CheckedChanged);
          // 
          // StarMap
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.ToggleNames);          
          this.Controls.Add(this.VScrollBar);
          this.Controls.Add(this.HScrollBar);
          this.Controls.Add(this.ZoomIn);
          this.Controls.Add(this.ZoomOut);
          this.Controls.Add(this.MapPanel);
          this.Name = "StarMap";
          this.Size = new System.Drawing.Size(555, 670);
          this.ResumeLayout(true);//from false
          this.PerformLayout();
          this.Refresh();

      }

      #endregion

      //public System.Windows.Forms.Panel MapPanel;      
      public MyPanel MapPanel;      
      private System.Windows.Forms.VScrollBar VScrollBar;
      private System.Windows.Forms.HScrollBar HScrollBar;
      private System.Windows.Forms.Button ZoomIn;
      private System.Windows.Forms.Button ZoomOut;
      private System.Windows.Forms.CheckBox ToggleNames;
      private System.Windows.Forms.ToolTip toolTip1;

   }
}

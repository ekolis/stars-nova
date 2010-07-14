namespace Nova.WinForms.Gui
{
   public partial class StarMap
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          this.components = new System.ComponentModel.Container();
          this.MapPanel = new Nova.WinForms.Gui.StarMapPanel();
          this.verticalScrollBar = new System.Windows.Forms.VScrollBar();
          this.horizontalScrollBar = new System.Windows.Forms.HScrollBar();
          this.zoomIn = new System.Windows.Forms.Button();
          this.zoomOut = new System.Windows.Forms.Button();
          this.toggleNames = new System.Windows.Forms.CheckBox();
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
          this.verticalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.verticalScrollBar.Enabled = false;
          this.verticalScrollBar.LargeChange = 1;
          this.verticalScrollBar.Location = new System.Drawing.Point(537, 32);
          this.verticalScrollBar.Name = "verticalScrollBar";
          this.verticalScrollBar.Size = new System.Drawing.Size(16, 611);
          this.verticalScrollBar.TabIndex = 5;
          this.verticalScrollBar.Value = 50;
          this.verticalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MapScrollV);
          // 
          // HScrollBar
          // 
          this.horizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.horizontalScrollBar.Enabled = false;
          this.horizontalScrollBar.LargeChange = 1;
          this.horizontalScrollBar.Location = new System.Drawing.Point(3, 652);
          this.horizontalScrollBar.Name = "horizontalScrollBar";
          this.horizontalScrollBar.Size = new System.Drawing.Size(533, 16);
          this.horizontalScrollBar.TabIndex = 3;
          this.horizontalScrollBar.Value = 50;
          this.horizontalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MapScrollH);
          // 
          // ZoomIn
          // 
          this.zoomIn.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.zoomIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.zoomIn.Location = new System.Drawing.Point(3, 4);
          this.zoomIn.Name = "zoomIn";
          this.zoomIn.Size = new System.Drawing.Size(32, 24);
          this.zoomIn.TabIndex = 7;
          this.zoomIn.TabStop = false;
          this.zoomIn.Text = "+";
          this.toolTip1.SetToolTip(this.zoomIn, "Zoom In\r\n");
          this.zoomIn.Click += new System.EventHandler(this.ZoomInClick);
          // 
          // ZoomOut
          // 
          this.zoomOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
          this.zoomOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.zoomOut.Location = new System.Drawing.Point(35, 4);
          this.zoomOut.Name = "zoomOut";
          this.zoomOut.Size = new System.Drawing.Size(32, 24);
          this.zoomOut.TabIndex = 4;
          this.zoomOut.TabStop = false;
          this.zoomOut.Text = "-";
          this.toolTip1.SetToolTip(this.zoomOut, "Zoom Out");
          this.zoomOut.Click += new System.EventHandler(this.ZoomOutClick);
          // 
          // ToggleNames
          // 
          this.toggleNames.Appearance = System.Windows.Forms.Appearance.Button;
          this.toggleNames.Checked = true;
          this.toggleNames.CheckState = System.Windows.Forms.CheckState.Checked;
          this.toggleNames.Location = new System.Drawing.Point(88, 4);
          this.toggleNames.Name = "toggleNames";
          this.toggleNames.Size = new System.Drawing.Size(32, 24);
          this.toggleNames.TabIndex = 8;
          this.toggleNames.Text = "A";
          this.toggleNames.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          this.toolTip1.SetToolTip(this.toggleNames, "Toggle star names");
          this.toggleNames.UseVisualStyleBackColor = true;
          this.toggleNames.CheckedChanged += new System.EventHandler(this.ToggleNames_CheckedChanged);
          // 
          // StarMap
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.toggleNames);
          this.Controls.Add(this.verticalScrollBar);
          this.Controls.Add(this.horizontalScrollBar);
          this.Controls.Add(this.zoomIn);
          this.Controls.Add(this.zoomOut);
          this.Controls.Add(this.MapPanel);
          this.Name = "StarMap";
          this.Size = new System.Drawing.Size(555, 670);
          this.ResumeLayout(false);

      }

      #endregion

      public StarMapPanel MapPanel;      
      private System.Windows.Forms.VScrollBar verticalScrollBar;
      private System.Windows.Forms.HScrollBar horizontalScrollBar;
      private System.Windows.Forms.Button zoomIn;
      private System.Windows.Forms.Button zoomOut;
      private System.Windows.Forms.CheckBox toggleNames;
      private System.Windows.Forms.ToolTip toolTip1;

   }
}

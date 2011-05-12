namespace Nova.WinForms.Gui
{
    public class StarMapPanel : System.Windows.Forms.Panel
    {
        public StarMapPanel()
        {
            // This goes here instead of StarMap. All drawing code affets the
            // MapPanel and not the StarMap. Double buffering eliminates the
            // flickering issues.
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();    
        }    
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
            if (m.Msg == WM_ERASEBKGND)
            {
                // do nothing...
                return;
            }
            base.WndProc(ref m);

        } 

    }
}
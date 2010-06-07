namespace Nova.WinForms.Gui
{
    public class StarMapPanel : System.Windows.Forms.Panel
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
}
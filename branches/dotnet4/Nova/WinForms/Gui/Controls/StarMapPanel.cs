#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

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
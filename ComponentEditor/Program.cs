// ============================================================================
// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// Component editor main entry point.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NovaCommon;

namespace ComponentEditor
{
   static class Program
   {
       [STAThread]
       static void Main()
       {
           // ensure registry keys are initialised
           FileSearcher.SetKeys();

           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new ComponentEditorWindow());
       }
   }
}

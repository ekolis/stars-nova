#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 stars-nova
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

#region Module Description
// ===========================================================================
// Component editor main entry point.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NovaCommon;

namespace Nova.WinForms.ComponentEditor
{
   static class Program
   {
       [STAThread]
       public static void Main()
       {
           // ensure registry keys are initialised
           FileSearcher.SetKeys();

           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new ComponentEditorWindow());
       }
   }
}

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
// The main window.
// ===========================================================================
#endregion

namespace Nova.WinForms.Gui
{
    using System;
    using System.Windows.Forms;

    using Nova.Client;
    using Nova.Common;

    /// ----------------------------------------------------------------------------
    /// <Summary>
    /// The main window.
    /// </Summary>
    /// ----------------------------------------------------------------------------
    public static class MainWindow
    {
        [STAThread]
        public static void Main(string[] args)
        {
            NovaGUI gui = new NovaGUI();
            
            Application.EnableVisualStyles();
            ClientState.Initialize(args);
            gui.Text = "Nova - " + ClientState.Data.PlayerRace.PluralName;
            gui.InitialiseControls();
            Application.Run(gui);
        }        
    }
}
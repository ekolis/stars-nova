// ============================================================================
// Nova. 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// A simple control for selection of a player's race and AI/Human status. Also
// contains a label for the player number. Used by the NewGameWizard.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ===========================================================================

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class PlayerSelector : UserControl
    {
        public PlayerSelector()
        {
            InitializeComponent();
        }
    }
}

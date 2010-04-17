﻿#region Copyright Notice
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
// This module holds the settings for a player.
// ===========================================================================
#endregion


using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
    [Serializable]
    public class PlayerSettings
    {
        public String RaceName = null; // The path & file name of the race.
        public String AiProgram = null; // The path & file name of the AI application or "Human"
        public int PlayerNumber = 0; // The order number of the player from 1 - Global.MaxPlayers
        

    }
}

// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This module holds the settings for a player.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
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

#region Copyright Notice
// ============================================================================
// Copyright (C) 2011, 2012 The Stars-Nova Project
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

namespace Nova.Server.TurnSteps
{
    using System;
    using System.Collections.Generic;
    
    using Nova.Common;
    using Nova.Common.Components;
    
    /// <summary>
    /// Manages any pre-turn generation data setup.
    /// </summary>
    public class FirstStep : ITurnStep
    {
        private ServerData serverState;
        
        public FirstStep()
        {
        }
        
        public void Process(ServerData serverState)
        {
            this.serverState = serverState;
            
            foreach (EmpireData empire in serverState.AllEmpires.Values)
            {               
                foreach (Design design in serverState.AllDesigns.Values)
                {
                    if (design.Owner == empire.Id || design.Owner == Global.Everyone)
                    {
                        empire.Designs[design.Key] = design;
                    }
                }
            }  
        }
    }
}

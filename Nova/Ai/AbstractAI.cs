#region Copyright Notice
// ============================================================================
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nova.Client;
using Nova.Common;

namespace Nova.Ai
{
    public abstract class AbstractAI
    {
        protected string raceName;
        protected int turnNumber = -1;

        public void Initialize(CommandArguments commandArguments)
        {
            raceName = commandArguments[CommandArguments.Option.RaceName];
            turnNumber = int.Parse(commandArguments[CommandArguments.Option.Turn], System.Globalization.CultureInfo.InvariantCulture);
            ClientState.Initialize(commandArguments.ToArray()); 
        }

        public abstract void DoMove();
    }
}

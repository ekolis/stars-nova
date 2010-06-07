#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This module is invoked when a planet is to be invaded
// ===========================================================================
#endregion

using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

using Nova.Common;
using Nova.Server;

namespace Nova.WinForms.Console
{

    /// <summary>
    /// Class invade a planet
    /// </summary>
    public static class Invade
    {
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Invade a star system.
        /// </summary>
        /// <param name="fleet">The invading fleet.</param>
        /// ----------------------------------------------------------------------------
        public static void Planet(Fleet fleet)
        {
            // First check that we are actuallly in orbit around a planet.

            if (fleet.InOrbit == null)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade but the waypoint is not a planet";
                ServerState.Data.AllMessages.Add(message);
                return;
            }

            // and that we have troops.

            double troops = fleet.Cargo.Colonists;
            Star star = fleet.InOrbit;

            if (troops == 0)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Name + " has waypoint orders to "
                   + "invade " + star.Name + " but there are no troops on board";
                ServerState.Data.AllMessages.Add(message);
                return;
            }

            // Take into account the Defenses

            Defenses.ComputeDefenseCoverage(star);
            troops *= Defenses.InvasionCoverage;
            double colonists = star.Colonists;

            colonists -= troops;
            if (colonists > 0)
            {
                Message message = new Message();
                message.Audience = fleet.Owner;
                message.Text = "Fleet " + fleet.Owner + " has killed "
                   + troops.ToString(System.Globalization.CultureInfo.InvariantCulture) + " colonists on " + star.Name
                   + " but did not manage to capture the planet";
                ServerState.Data.AllMessages.Add(message);
                star.Colonists -= (int)troops;
                return;
            }

            Message captureMessage = new Message();
            captureMessage.Audience = fleet.Owner;
            captureMessage.Text = "Fleet " + fleet.Owner + " has killed "
               + "all the colonists on " + star.Name
               + " and has captured the planet";
            ServerState.Data.AllMessages.Add(captureMessage);

            star.Owner = fleet.Owner;
            star.Colonists = (int)(troops - star.Colonists);
        }

    }
}



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
// This module provides a control to display the production queue for the 
// currently selected star system.
// ===========================================================================
#endregion

using NovaCommon;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Nova
{
    /// <summary>
    /// Populate a list control with the details of a production queue.
    /// </summary>
    public static class QueueList
    {

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate a list control with the details of a production queue.
        /// </summary>
        /// <param name="listView">The <see cref="ListView"/> to display the queue in.</param>
        /// <param name="toMake">The <see cref="ProductionQueue"/> to display.</param>
        /// ----------------------------------------------------------------------------
        public static void Populate(ListView listView, ProductionQueue toMake)
        {
            listView.Items.Clear();
            listView.BeginUpdate();

            foreach (ProductionQueue.Item i in toMake.Queue)
            {
                ListViewItem item = new ListViewItem();
                item.Text = i.Name;
                item.SubItems.Add(i.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));

                listView.Items.Add(item);
            }

            listView.EndUpdate();
        }
    }
}

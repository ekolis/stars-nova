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
// currently selected Star system.
// ===========================================================================
#endregion


namespace Nova.WinForms.Gui
{
    using System.Windows.Forms;

    using Nova.Common;

    /// <Summary>
    /// Populate a list control with the details of a production queue.
    /// </Summary>
    public static class QueueList
    {
        /// <Summary>
        /// Populate a list control with the details of a production queue.
        /// </Summary>
        /// <param name="listView">The <see cref="ListView"/> to display the queue in.</param>
        /// <param name="toMake">The <see cref="ProductionQueue"/> to display.</param>
        public static void Populate(ListView listView, ProductionQueue toMake)
        {
            listView.Items.Clear();
            listView.BeginUpdate();

            foreach (ProductionQueue.Item itemToMake in toMake.Queue)
            {
                ListViewItem item = new ListViewItem();
                item.Text = itemToMake.Name;
                item.SubItems.Add(itemToMake.Quantity.ToString(System.Globalization.CultureInfo.InvariantCulture));
                // add the BuildState to the ListViewItem.Tag
                item.Tag = itemToMake.BuildState;
                // either set the Checked stat here for Starbases or in the OnLoad Method of ProductionDialog.cs

                listView.Items.Add(item);
            }

            listView.EndUpdate();
        }
    }
}

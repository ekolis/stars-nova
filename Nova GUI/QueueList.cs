// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Nova
{

   public static class QueueList
   {

// ============================================================================
// Populate a list control with the details of a production queue.
// ============================================================================

      public static void Populate(ListView listView, ProductionQueue toMake)
      {
         listView.Items.Clear();
         listView.BeginUpdate();

         foreach (ProductionQueue.Item i in toMake.Queue) {
            ListViewItem item = new ListViewItem();
            item.Text         = i.Name;
            item.SubItems.Add(i.Quantity.ToString());

            listView.Items.Add(item);
         }

         listView.EndUpdate();
      }
   }
}

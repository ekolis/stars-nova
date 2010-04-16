// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Control to hold the summary of a selected item.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using NovaCommon;
using NovaClient;

namespace Nova
{
   public partial class SelectionSummary : UserControl
   {

      private Item          summaryItem  = null;
      private PlanetSummary planetSummary = new PlanetSummary();
      private FleetSummary  fleetSummary  = new FleetSummary();


// ============================================================================
// Construction
// ============================================================================

      public SelectionSummary()
      {
         InitializeComponent();
      }


// ============================================================================
// Display a planet summary
// ============================================================================

      private void DisplayPlanet(Item item)
      {
         if (ClientState.Data.StarReports.Contains(item.Name) == false) {
            SelectedItem.Text = item.Name + " is unexplored";
            summaryItem       = null;
            SelectedItem.Controls.Clear();
            return;
         }

         SelectedItem.Text      = "Summary of " + item.Name;
         planetSummary.Location = new Point(5, 15);
         planetSummary.Value    = item as Star;

         // If we are displaying a fleet clear it out and add the planet
         // summary display.

         if (summaryItem is Fleet || summaryItem == null) {
            SelectedItem.Controls.Clear();
            SelectedItem.Controls.Add(planetSummary);
         }

         summaryItem = item;
      }


// ============================================================================
// Display a fleet summary
// ============================================================================

      private void DisplayFleet(Item item)
      {
         SelectedItem.Text      = "Summary of " + item.Name;
         fleetSummary.Location = new Point(5, 15);
         fleetSummary.Value    = item as Fleet;

         if (summaryItem is Star || summaryItem == null) {
            SelectedItem.Controls.Clear();
            SelectedItem.Controls.Add(fleetSummary);
         }

         summaryItem = item;
      }


// ============================================================================
// Set the content of the summary control. Depending on the type of the item
// selected this may either be a planet (in which case the planet summary
// control is displayed) or a fleet (which will cause the fleet summary control
// to be displayed).
// ============================================================================

      private void SetItem(Item item) 
      {
         if (item == null) {
            SelectedItem.Text = "Nothing Selected";
            SelectedItem.Controls.Clear();
            return;
         }

         if (item is Fleet) {
            DisplayFleet(item);
         }
         else {
            DisplayPlanet(item);
         }
      }


// ============================================================================
// Property to access the displayed item
// ============================================================================

      public Item Value 
      {
         set { SetItem(value);     }
         get { return summaryItem; }
      }
   }
}

// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// Control to acter as a container to hold the appropriate detail control of a
// selected item.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nova.Controls
{
   public partial class SelectionDetail : UserControl
   {

      private Item         selectedItem    = null;
      private UserControl  selectedControl = null;
      private PlanetDetail planetDetail    = new PlanetDetail();
      private FleetDetail  fleetDetail     = new FleetDetail();


// ============================================================================
// Construction
// ============================================================================

      public SelectionDetail()
      {
         InitializeComponent();
      }


// ============================================================================
// Display planet detail
// ============================================================================

      private void DisplayPlanet(Item item)
      {
         planetDetail.Location = new Point(5, 15);
         planetDetail.Value    = item as Star;
         DetailPanel.Text      = "System " + item.Name;

         if (selectedItem is Fleet || selectedItem == null) {
            selectedControl = planetDetail;
            DetailPanel.Controls.Clear();
            DetailPanel.Controls.Add(planetDetail);
         }

         selectedItem = item;
      }


// ============================================================================
// Display fleet detail
// ============================================================================

      private void DisplayFleet(Item item)
      {
         fleetDetail.Location = new Point(5, 15);
         fleetDetail.Value    = item as Fleet;
         DetailPanel.Text     = "Fleet " + item.Name;

         //  if (selectedItem is Star || selectedItem == null) {
            selectedControl = fleetDetail;
            DetailPanel.Controls.Clear();
            DetailPanel.Controls.Add(fleetDetail);
         //}

         selectedItem = item;
      }


// ============================================================================
// Set the content of the detail control. Depending on the type of the item
// selected this may either be a planet (in which case the planet detail
// control is displayed) or a fleet (which will cause the fleet detail control
// to be displayed).
// ============================================================================

      private void SetItem(Item item) 
      {
         if (item == null) {
            selectedItem     = null;
            selectedControl  = null;
            DetailPanel.Text = "Nothing Selected";
            DetailPanel.Controls.Clear();
            return;
         }

         // To avoid confusion when another race's fleet or planet is selected
         // grey out (disable) the detail panel.

         if (item.Owner == GUIstate.Data.RaceName) {
            DetailPanel.Enabled = true;
         }
         else {
            DetailPanel.Enabled = false;
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
// Property to access the selected item (Fleet or Star).
// ============================================================================

      public Item Value 
      {
         set { SetItem(value);      }
         get { return selectedItem; }
      }


// ============================================================================
// Property to access the actual detail control being displayed.
// ============================================================================

      public UserControl Control
      {
         get {return selectedControl; }
      }

   }
}

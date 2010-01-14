// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// 
// A component for displaying or editing a ship hull design.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using NovaCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ControlLibrary
{

// ============================================================================
// Ship component layout grid
// ============================================================================

   public partial class HullGrid : UserControl
   {
      public event EventHandler ModuleSelected;
 
      private Panel[]      panelMap            = new Panel[25];
      private bool         emptyModulesHidden  = false;
      private StringFormat format              = new StringFormat();
      private Font         font                = null;
      private RectangleF   textRect            = new RectangleF(0, 0, 58, 58);


// ============================================================================
// Data dragged and (possibly) dropped while designing a ship.
// ============================================================================

      public class DragDropData
      {
         public NovaCommon.Component SelectedComponent;
         public int                  ComponentCount;
         public string               HullName;
      }


// ============================================================================
// Construction
// ============================================================================

      public HullGrid()
      {
         InitializeComponent();

         font = new Font("Arial", (float) 7.5, FontStyle.Regular, 
                         GraphicsUnit.Point);

         // Initialise the panel map so that we can have a convienient way of
         // identifying each cell in the grid just from its index.

         panelMap[0]  = Grid0;  panelMap[1]  = Grid1;  panelMap[2]  = Grid2;
         panelMap[3]  = Grid3;  panelMap[4]  = Grid4;  panelMap[5]  = Grid5;
         panelMap[6]  = Grid6;  panelMap[7]  = Grid7;  panelMap[8]  = Grid8;
         panelMap[9]  = Grid9;  panelMap[10] = Grid10; panelMap[11] = Grid11;
         panelMap[12] = Grid12; panelMap[13] = Grid13; panelMap[14] = Grid14;
         panelMap[15] = Grid15; panelMap[16] = Grid16; panelMap[17] = Grid17;
         panelMap[18] = Grid18; panelMap[19] = Grid19; panelMap[20] = Grid20;
         panelMap[21] = Grid21; panelMap[22] = Grid22; panelMap[23] = Grid23;
         panelMap[24] = Grid24;
      }


// ============================================================================
// Right-click context menu has selected an item (only available when cell
// editing is enabled). 
// ============================================================================

      private void CellContextMenuItem(object sender, 
                                       ToolStripItemClickedEventArgs e)
      {
         ContextMenuStrip contextMenu   = sender as ContextMenuStrip;
         Panel            hullCell      = contextMenu.SourceControl as Panel;
         ToolStripItem    selection     = e.ClickedItem;

         // -------------------------------------------------------------------
         // Decrement the count of the number of modules that may be added to
         // this cell. If the count reaches zero then the cell becomes empty.
         // -------------------------------------------------------------------


         if (selection.Text == "Decrement Modules")
         {
             if (hullCell.Tag != null)
             {
                 HullModule cell = hullCell.Tag as HullModule;
                 if (cell.ComponentMaximum > 0)
                 {
                     cell.ComponentMaximum--;
                 }

                 if (cell.ComponentMaximum == 0)
                 {
                     hullCell.Tag = null;
                 }

                 hullCell.Invalidate();
             }
             return;
         }

         // -------------------------------------------------------------------
         // Increment the count of the number of modules that may be added to
         // this cell. 
         // -------------------------------------------------------------------

         if (selection.Text == "Increment Modules")
         {
             if (hullCell.Tag != null)
             {
                 HullModule cell = hullCell.Tag as HullModule;
                 if (cell.ComponentMaximum > 0)
                 {
                     cell.ComponentMaximum++;
                 }

                 hullCell.Invalidate();
             }
             return;
         }

         // -------------------------------------------------------------------
         // Clear the contents of a cell
         // -------------------------------------------------------------------

         if (selection.Text == "Clear Cell") {
            hullCell.Tag = null;
            hullCell.Invalidate();
            return;
         }

         // -------------------------------------------------------------------
         // Add 10 more modules of the same type
         // -------------------------------------------------------------------

         if (selection.Text == "Add 10") {
            if (hullCell.Tag != null) {
               HullModule cell = hullCell.Tag as HullModule;
               cell.ComponentMaximum += 10;
               hullCell.Invalidate();
            }
            return;
         }

         // -------------------------------------------------------------------
         // Add a fixed cargo space to a cell.
         // -------------------------------------------------------------------
         if (selection.Text.Contains("Base Cargo"))
         {
             contextMenu.Close();
             if (hullCell.Tag != null)
             {
                 HullModule cell = hullCell.Tag as HullModule;
                 cell.ComponentType = "Base Cargo";
             }
             else
             {
                 HullModule cell = new HullModule();
                 cell.ComponentType = "Base Cargo";
                 hullCell.Tag = cell;
             }
             hullCell.Invalidate();
             return;
         }

         // -------------------------------------------------------------------
         // Add a component to a cell.
         // -------------------------------------------------------------------

         if (hullCell.Tag == null) {
            HullModule cell    = new HullModule();
            cell.ComponentType = selection.Text;
            hullCell.Tag       = cell;
         }
         else {
            HullModule cell = hullCell.Tag as HullModule;
            if (cell.ComponentType == selection.Text)
            {
                cell.ComponentMaximum++;
            }
            else
            {
                cell.ComponentType = selection.Text;
                hullCell.Invalidate();
            }
         }

         hullCell.Invalidate();
      }


// ============================================================================
// Clear out the grid
// ============================================================================

      public void Clear(bool panelVisible)
      {
         foreach (Panel panel in panelMap) {
            panel.Tag             = null;
            panel.BackgroundImage = null;
            panel.BackColor = Color.FromName(KnownColor.Control.ToString());
            panel.Visible         = panelVisible;
            panel.Invalidate();
         }
      }


// ============================================================================
// Return the details of the hull modules that have been selected to receive
// a component.
// ============================================================================

      public ArrayList ActiveModules {
         get {
            ArrayList activeModules = new ArrayList();

            for (int i = 0; i < panelMap.Length; i++) {
               if (panelMap[i].Tag != null) {
                  HullModule module = panelMap[i].Tag as HullModule;
                  module.CellNumber = i;
                  activeModules.Add(module);
               }
            }

            return activeModules;
         }


// ============================================================================
// Populate the grid cells from a list of Modules.
// ============================================================================

         set 
         {
            ArrayList hullModules = value;

            foreach (Panel panel in panelMap) 
            {
               panel.Tag       = null;
               panel.BackColor = Color.FromName(KnownColor.Control.ToString());

               if (emptyModulesHidden) 
               {
                  panel.Visible = false;
               }

               panel.Invalidate();
            }

            if (hullModules != null)
            {
                foreach (HullModule module in hullModules)
                {
                    Panel currentCell = panelMap[module.CellNumber];
                    currentCell.Tag = module;
                    currentCell.Visible = true;
                    currentCell.Invalidate();
                }
            }
         } // set
      }


// ============================================================================
// Property to hide (Nova GUI case) or show (Component Designer case) empty
// hull modules. For the Nova GUI case we also shut down the editing context
// menu for each cell.
// ============================================================================

      public bool HideEmptyModules {
         get { return emptyModulesHidden;  }
         set {
            emptyModulesHidden = value;
            if (emptyModulesHidden) {
               foreach (Panel panel in panelMap) {
                  panel.ContextMenuStrip = null;
               }
            }
         }
      }


// ============================================================================
// Process a drag entering a cell and see if we are willing to accept the
// data. The component must be of the correct type and must be allowed on the
// particular hull (Settler's Delight can only be mounted on the
// Mini-Coloniser).
// ============================================================================

      private void Grid_DragEnter(object sender, DragEventArgs e)
      {
         e.Effect = DragDropEffects.None; 

         if (e.Data.GetDataPresent(typeof(DragDropData)) == false) {
            return;
         }

         Panel      panel = sender as Panel;
         HullModule cell  = panel.Tag as HullModule;
       
         DragDropData data = e.Data.GetData(typeof(DragDropData)) 
                             as DragDropData;

         
         // No room for any more to be added to this cell.
         if (cell.ComponentCount == cell.ComponentMaximum) 
         {
            return;
         }

         // Not the right component for this cell.
         if (  ! cell.ComponentType.Contains(data.SelectedComponent.Type) &&
               !(cell.ComponentType.Contains("Weapon") && (data.SelectedComponent.Type == "Beam Weapons" || data.SelectedComponent.Type == "Torpedoes")) &&
                 cell.ComponentType != "General Purpose" ) 
         {
            return;
         }

          // General Purpose dosen't allow engines
         if (cell.ComponentType == "General Purpose" && data.SelectedComponent.Type == "Engine")
         {
             return;
         }


         // Wrong (additional) component for this cell

         if (cell.ComponentCount > 0) 
         {
            if (cell.AllocatedComponent != data.SelectedComponent) 
            {
               return;
            }
         }

         // The Settler's Delight engine is only allowed on a min-coloniser
         // hull.
         if (data.SelectedComponent.Properties.ContainsKey("Hull Affinity"))
         {
             HullAffinity affinity = data.SelectedComponent.Properties["Hull Affinity"] as HullAffinity;
             if (affinity.Value != data.HullName) return;
         }

        // Don't allow a CargoPod to be dropped into a general purpose slot as
        // that could allow "freighters" to be built from hulls not meant to
        // be freighters.
        // This is perfectly legal in Stars! e.g. a cargo pod on a scout hull - Daniel Apr 09
        /*
        if (data.SelectedComponent.Name == "CargoPod") {
            if (cell.ComponentType == "General Purpose") {
                return;
            }
        }
        */

          // The Transport Cloak cannot be fitted to armed ships.
         if (data.SelectedComponent.Properties.ContainsKey("Transport Ships Only"))
         {
             foreach (Panel currentCell in panelMap)
             {
                 if (((HullModule)currentCell.Tag).AllocatedComponent.Properties.ContainsKey("Weapon")) return;
             }
         }
          

         e.Effect = DragDropEffects.Copy;
      }

 
// ============================================================================
// Process a drop event
// ============================================================================

      private void Grid_DragDrop(object sender, DragEventArgs e)
      {
         DragDropData data = e.Data.GetData(typeof(DragDropData))
            as DragDropData;

         Panel       panel = sender as Panel;
         HullModule  cell  = panel.Tag as HullModule;

         if (cell.ComponentCount == 0) {
            cell.AllocatedComponent = data.SelectedComponent;
         }

         cell.ComponentCount += data.ComponentCount;
         if (cell.ComponentCount > cell.ComponentMaximum) {
            cell.ComponentCount  = cell.ComponentMaximum;
         }

         panel.Invalidate();
      }


// ============================================================================
// Draw a cell. There are three cases as shown in the code.
// ============================================================================

      private void Grid0_Paint(object sender, PaintEventArgs e)
      {
         Panel panel = sender as Panel;

         // Case (1) : The cell is at it's default (unallocated) state. Do
         // nothing.

         if (panel.Tag == null) {
            return;
         }

         // Case (2) : The cell has been defined to accept one or more of a
         // particular component type. In this case just shade the cell
         // background and annotate it with its component type and how many of
         // that type it will hold.

         if ((panel.Tag as HullModule).ComponentCount == 0) {
            DrawEmptyCell(panel, e.Graphics);
            return;
         }
         
         // Case (3) : As for case (2) but an actual component type has been
         // assigned. Let the cell draw it's own image and then overlay the
         // quantity on top of that image.

         DrawAllocatedCell(panel, e.Graphics);
      }


// ============================================================================
// Draw a defined but empty cell.
// TODO (priority 4) - for an engine it should say Requires N instead of Up to. Needs to be enforced also.
// TODO (priority 1) - if a cell is cleared change it back to the empty module color.
// ============================================================================

      private void DrawEmptyCell(Panel panel, Graphics graphics)
      {
         panel.BackColor       = Color.Silver;
         panel.BackgroundImage = null;

         HullModule cell     = panel.Tag as HullModule;
         string     text     = cell.ComponentType;
         int        capacity = cell.ComponentMaximum;

         if (cell.ComponentMaximum > 1) {
            text += "\nUp to " + cell.ComponentMaximum;
         }

         format.Alignment     = StringAlignment.Center;
         format.LineAlignment = StringAlignment.Center;

         graphics.DrawString(text, font, Brushes.Blue, textRect, format);
      }


// ============================================================================
// Draw an allocated cell.
// ============================================================================

      private void DrawAllocatedCell(Panel panel, Graphics graphics)
      {
         HullModule cell       = panel.Tag as HullModule;
         panel.BackColor       = Color.FromName(KnownColor.Control.ToString());
         panel.BackgroundImage = cell.AllocatedComponent.ComponentImage;
         
         if (cell.ComponentMaximum > 1) {
            format.Alignment     = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Far;

            string text = cell.ComponentCount + " of " + cell.ComponentMaximum;
            graphics.DrawString(text, font, Brushes.Blue, textRect, format);
         }
      }


// ============================================================================
// A grid cell has been clicked. If anyone has registered an interest tell
// them.
// ============================================================================

      public void GridCell_Click(object sender, EventArgs e)
      {
         if (ModuleSelected != null) {
            ModuleSelected(sender, e);
         }
      }

   }
}

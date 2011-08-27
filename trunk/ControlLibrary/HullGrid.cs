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

namespace Nova.ControlLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Nova.Common;
    using Nova.Common.Components;

    /// <summary>
    /// A component for displaying or editing a ship hull design.
    /// </summary>
    public partial class HullGrid : UserControl
    {
        public event EventHandler ModuleSelected;
        public event EventHandler ModuleUpdated;

        private Panel[] panelMap        = new Panel[25];
        private bool emptyModulesHidden = false;
        private StringFormat format     = new StringFormat();
        private Font font               = null;
        private RectangleF textRect     = new RectangleF(0, 0, 58, 58);

        /// <summary>
        /// Data dragged and (possibly) dropped while designing a ship.
        /// </summary>
        public class DragDropData
        {
            public Nova.Common.Components.Component SelectedComponent;
            public int ComponentCount;
            public string HullName;
            public DragDropEffects Operation = DragDropEffects.Copy;
            public HullModule SourceHullModule;
            public HullModule TargetHullModule;
        }

        /// <summary>
        /// Initializes a new instance of the HullGrid class.
        /// </summary>
        public HullGrid()
        {
            InitializeComponent();

            font = new Font("Arial", (float)7.5, FontStyle.Regular, GraphicsUnit.Point);

            // Initialise the panel map so that we can have a convienient way of
            // identifying each cell in the grid just from its index.
            panelMap[0] = this.grid0;
            panelMap[1] = this.grid1;
            panelMap[2] = this.grid2;
            panelMap[3] = this.grid3;
            panelMap[4] = this.grid4;
            panelMap[5] = this.grid5;
            panelMap[6] = this.grid6;
            panelMap[7] = this.grid7;
            panelMap[8] = this.grid8;
            panelMap[9] = this.grid9;
            panelMap[10] = this.grid10;
            panelMap[11] = this.grid11;
            panelMap[12] = this.grid12;
            panelMap[13] = this.grid13;
            panelMap[14] = this.grid14;
            panelMap[15] = this.grid15;
            panelMap[16] = this.grid16;
            panelMap[17] = this.grid17;
            panelMap[18] = this.grid18;
            panelMap[19] = this.grid19;
            panelMap[20] = this.grid20;
            panelMap[21] = this.grid21;
            panelMap[22] = this.grid22;
            panelMap[23] = this.grid23;
            panelMap[24] = this.grid24;
        }

        /// <summary>
        /// Right-click context menu has selected an item (only available when cell
        /// editing is enabled). 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void CellContextMenuItem(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip contextMenu = sender as ContextMenuStrip;
            Panel hullCell = contextMenu.SourceControl as Panel;
            ToolStripItem selection = e.ClickedItem;

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

            if (selection.Text == "Clear Cell")
            {
                hullCell.Tag = null;
                hullCell.Invalidate();
                return;
            }

            // -------------------------------------------------------------------
            // Add 10 more modules of the same type
            // -------------------------------------------------------------------

            if (selection.Text == "Add 10")
            {
                if (hullCell.Tag != null)
                {
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

            if (hullCell.Tag == null)
            {
                HullModule cell = new HullModule();
                cell.ComponentType = selection.Text;
                hullCell.Tag = cell;
            }
            else
            {
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

        /// <summary>
        /// Instigate Drag and Drop of the selected grid item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Grid_DragBegin(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;           
            if (panel == null)
            {
                return;
            }
            
            HullModule cell = panel.Tag as HullModule;
            if (cell == null)
            {
                return;
            }

            Component component = cell.AllocatedComponent;
            if (component == null || cell.ComponentCount == 0)
            {
                return;
            }

            DragDropData dragData = new DragDropData();
            dragData.HullName = HullName;
            dragData.ComponentCount = 1;
            dragData.SelectedComponent = component;
            dragData.Operation = DragDropEffects.Move;
            dragData.SourceHullModule = cell;

            if ((ModifierKeys & Keys.Shift) != 0)
            {
                dragData.ComponentCount = Math.Min(10, cell.ComponentCount);
            }

            DragDropEffects result = DoDragDrop(dragData, DragDropEffects.Move);

            if (dragData.SourceHullModule == dragData.TargetHullModule && dragData.SourceHullModule != null)
            {
                return;
            }
            if (result == DragDropEffects.Move || result == DragDropEffects.None)
            {
                cell.ComponentCount -= dragData.ComponentCount;
                if (cell.ComponentCount <= 0)
                {
                    cell.AllocatedComponent = null;
                }
                DoModuleUpdated(sender, new EventArgs());
                panel.Invalidate();
            }
        }

        protected virtual void DoModuleUpdated(object sender, EventArgs eventArgs)
        {
            if (ModuleUpdated == null)
            {
                return;
            }
            ModuleUpdated(sender, eventArgs);
        }

        /// <summary>
        /// Process a drag entering a cell and see if we are willing to accept the
        /// data. The component must be of the correct type and must be allowed on the
        /// particular hull (Settler's Delight can only be mounted on the
        /// Mini-Coloniser).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            if (e.Data.GetDataPresent(typeof(DragDropData)) == false)
            {
                return;
            }

            Panel panel = sender as Panel;
            HullModule cell = panel.Tag as HullModule;

            DragDropData data = e.Data.GetData(typeof(DragDropData))
                                as DragDropData;

            // Not the right component for this cell.
            if (!cell.ComponentType.Contains(data.SelectedComponent.Type.ToDescription()) &&
                !(cell.ComponentType.Contains("Weapon") && (data.SelectedComponent.Type == ItemType.BeamWeapons || data.SelectedComponent.Type == ItemType.Torpedoes)) &&
                    cell.ComponentType != "General Purpose")
            {
                return;
            }

            // General Purpose dosen't allow engines
            if (cell.ComponentType == "General Purpose" && data.SelectedComponent.Type == ItemType.Engine)
            {
                return;
            }


            // The Settler's Delight engine is only allowed on a min-coloniser
            // hull.
            if (data.SelectedComponent.Properties.ContainsKey("Hull Affinity"))
            {
                HullAffinity affinity = data.SelectedComponent.Properties["Hull Affinity"] as HullAffinity;
                if (affinity.Value != data.HullName)
                {
                    return;
                }
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
                    if (((HullModule)currentCell.Tag).AllocatedComponent.Properties.ContainsKey("Weapon"))
                    {
                        return;
                    }
                }
            }

            e.Effect = data.Operation;
        }

        /// <summary>
        /// Process a drop event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Grid_DragDrop(object sender, DragEventArgs e)
        {
            DragDropData data = e.Data.GetData(typeof(DragDropData))
               as DragDropData;

            Panel panel = sender as Panel;
            HullModule cell = panel.Tag as HullModule;
            data.TargetHullModule = cell;

            if (data.SourceHullModule == data.TargetHullModule && data.SourceHullModule != null)
            {
                return;
            }

            if (cell.AllocatedComponent != data.SelectedComponent)
            {
                cell.AllocatedComponent = data.SelectedComponent;
                cell.ComponentCount = 0;
            }

            cell.ComponentCount += data.ComponentCount;
            if (cell.ComponentCount > cell.ComponentMaximum)
            {
                cell.ComponentCount = cell.ComponentMaximum;
            }

            panel.Invalidate();
        }

        /// <summary>
        /// Draw a cell. 
        /// </summary>
        /// <remarks>There are three cases as shown in the code.</remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Grid0_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            // Case (1) : The cell is at it's default (unallocated) state. Do
            // nothing.

            if (panel.Tag == null)
            {
                return;
            }

            // Case (2) : The cell has been defined to accept one or more of a
            // particular component type. In this case just shade the cell
            // background and annotate it with its component type and how many of
            // that type it will hold.

            if ((panel.Tag as HullModule).ComponentCount == 0)
            {
                DrawEmptyCell(panel, e.Graphics);
                return;
            }

            // Case (3) : As for case (2) but an actual component type has been
            // assigned. Let the cell draw it's own image and then overlay the
            // quantity on top of that image.

            DrawAllocatedCell(panel, e.Graphics);
        }

        /// <summary>
        /// A grid cell has been clicked. If anyone has registered an interest tell
        /// them.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        public void GridCell_Click(object sender, EventArgs e)
        {
            if (ModuleSelected != null)
            {
                ModuleSelected(sender, e);
            }
        }

        /// <summary>
        /// Draw a defined but empty cell.
        /// </summary>
        /// <remarks>
        /// TODO (priority 4) - for an engine it should say Requires N instead of Up to. Needs to be enforced also.
        /// TODO (priority 1) - if a cell is cleared change it back to the empty module color.
        /// </remarks>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void DrawEmptyCell(Panel panel, Graphics graphics)
        {
            panel.BackColor = Color.Silver;
            panel.BackgroundImage = null;

            HullModule cell = panel.Tag as HullModule;
            string text = cell.ComponentType;

            if (cell.ComponentMaximum > 1)
            {
                text += "\nUp to " + cell.ComponentMaximum;
            }

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            graphics.DrawString(text, font, Brushes.Blue, textRect, format);
        }

        /// <summary>
        /// Clear out the grid.
        /// </summary>
        /// <param name="panelVisible">True if this panel is visible for this hull type.</param>
        public void Clear(bool panelVisible)
        {
            foreach (Panel panel in panelMap)
            {
                panel.Tag = null;
                panel.BackgroundImage = null;
                panel.BackColor = Color.FromName(KnownColor.Control.ToString());
                panel.Visible = panelVisible;
                panel.Invalidate();
            }
        }

        /// <summary>
        /// Draw an allocated cell.
        /// </summary>
        /// <param name="panel">The <see cref="Panel"/> to draw on.</param>
        /// <param name="graphics">The <see cref="Graphics"/> to place on the panel.</param>
        private void DrawAllocatedCell(Panel panel, Graphics graphics)
        {
            HullModule cell = panel.Tag as HullModule;
            panel.BackColor = Color.FromName(KnownColor.Control.ToString());
            panel.BackgroundImage = cell.AllocatedComponent.ComponentImage;

            if (cell.ComponentMaximum > 1)
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Far;

                string text = cell.ComponentCount + " of " + cell.ComponentMaximum;
                graphics.DrawString(text, font, Brushes.Blue, textRect, format);
            }
        }

        /// <summary>
        /// Get or Set the active modules.
        /// </summary>
        public List<HullModule> ActiveModules
        {
            // Return the details of the hull modules that have been selected to receive
            // a component.

            get
            {
                List<HullModule> activeModules = new List<HullModule>();

                for (int i = 0; i < panelMap.Length; i++)
                {
                    if (panelMap[i].Tag != null)
                    {
                        HullModule module = panelMap[i].Tag as HullModule;
                        module.CellNumber = i;
                        activeModules.Add(module);
                    }
                }

                return activeModules;
            }

            // Populate the grid cells from a list of Modules.
            set
            {
                List<HullModule> hullModules = value;

                foreach (Panel panel in panelMap)
                {
                    panel.Tag = null;
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

        /// <summary>
        /// Property to hide (Nova GUI case) or show (Component Designer case) empty
        /// hull modules. For the Nova GUI case we also shut down the editing context
        /// menu for each cell.
        /// </summary>
        public bool HideEmptyModules
        {
            get
            {
                return emptyModulesHidden;
            }
            set
            {
                emptyModulesHidden = value;
                if (emptyModulesHidden)
                {
                    foreach (Panel panel in panelMap)
                    {
                        panel.ContextMenuStrip = null;
                    }
                }
            }
        }

        public string HullName { get; set; }
    }
}

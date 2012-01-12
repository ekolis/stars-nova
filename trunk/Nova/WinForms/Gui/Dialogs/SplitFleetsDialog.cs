#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 stars-nova
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

namespace Nova.WinForms.Gui.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nova.Common;
    using Nova.Common.Components;

    public partial class SplitFleetDialog : Form
    {
        private List<Design> designs;
        private Dictionary<Design, int> leftFleet;
        private Dictionary<Design, int> rightFleet;
        private List<NumericUpDown> leftNumerics;
        private List<NumericUpDown> rightNumerics;


        private enum Side 
        { 
            Left, 
            Right 
        }

        public SplitFleetDialog()
        {
            InitializeComponent();
        }

        public void SetFleet(Fleet sourceFleet, Fleet otherFleet)
        {
            leftFleet = sourceFleet.Composition;
            rightFleet = otherFleet == null ? new Dictionary<Design, int>() : otherFleet.Composition;
            foreach (Design des in leftFleet.Keys)
            {
                if (!rightFleet.ContainsKey(des))
                {
                    rightFleet[des] = 0;
                }
            }
            foreach (Design des in rightFleet.Keys)
            {
                if (!leftFleet.ContainsKey(des))
                {
                    leftFleet[des] = 0;
                }
            }

            designs = new List<Design>();
            designs.AddRange(leftFleet.Keys.OrderBy(x => x.Name));
            
            leftNumerics = new List<NumericUpDown>();
            rightNumerics = new List<NumericUpDown>();

            lblFleetLeft.Text = sourceFleet.Name;
            lblFleetRight.Text = otherFleet == null ? "New Fleet" : otherFleet.Name;

            fleetLayoutPanel.Controls.Clear();
            fleetLayoutPanel.RowCount = designs.Count;
            foreach (RowStyle rowStyle in fleetLayoutPanel.RowStyles)
            {
                rowStyle.SizeType = SizeType.AutoSize;
            } 
            fleetLayoutPanel.Height = designs.Count * 26;
            this.Height += (designs.Count * 26) - 26;
            for (int i = 0; i < designs.Count; ++i)
            {
                int index = i;
                NumericUpDown num = new NumericUpDown();
                num.Width = 62;
                num.Maximum = leftFleet[designs[i]] + rightFleet[designs[i]];
                num.Value = leftFleet[designs[i]];
                leftNumerics.Add(num);
                num.ValueChanged += delegate { ValueChanged(Side.Left, index); };
                fleetLayoutPanel.Controls.Add(num, 0, i);

                Label designName = new Label();
                designName.Text = designs[i].Name;
                designName.Anchor = AnchorStyles.None;
                designName.TextAlign = ContentAlignment.MiddleCenter;
                fleetLayoutPanel.Controls.Add(designName, 1, i);

                num = new NumericUpDown();
                num.Width = 62;
                num.Maximum = leftFleet[designs[i]] + rightFleet[designs[i]];
                num.Value = rightFleet[designs[i]];
                rightNumerics.Add(num);
                num.ValueChanged += delegate { ValueChanged(Side.Right, index); };
                fleetLayoutPanel.Controls.Add(num, 2, i);
            }
        }

        private void ValueChanged(Side side, int index)
        {
            NumericUpDown left = leftNumerics[index];
            NumericUpDown right = rightNumerics[index];
            decimal max = left.Maximum;
            decimal newval = side == Side.Left ? left.Value : right.Value;
            newval = max - newval;
            if (side == Side.Left)
            {
                right.Value = newval;
            }
            else
            {
                left.Value = newval;
            }
        }

        public void ReassignShips(Fleet left, Fleet right)
        {
            for (int i = 0; i < designs.Count; i++)
            {
                int leftOldCount = leftFleet[designs[i]];
                int leftNewCount = (int)leftNumerics[i].Value;

                if (leftNewCount == leftOldCount)
                {
                    continue; // no moves of this design
                }

                Fleet from;
                Fleet to;
                int moveCount;
                if (leftNewCount > leftOldCount)
                {
                    from = right;
                    to = left;
                    moveCount = leftNewCount - leftOldCount;
                }
                else
                {
                    from = left;
                    to = right;
                    moveCount = leftOldCount - leftNewCount;
                }

                List<Ship> toMove = new List<Ship>();
                foreach (Ship fleetShip in from.FleetShips)
                {
                    if (fleetShip.Design.Key == designs[i].Key)
                    {
                        toMove.Add(fleetShip);
                        --moveCount;
                        if (moveCount == 0)
                        {
                            break;
                        }
                    }
                }
                foreach (Ship ship in toMove)
                {
                    from.FleetShips.Remove(ship);
                    to.FleetShips.Add(ship);
                }
            }

            // Ships are moved. Now to reassign fuel/cargo
            int ktToMove = 0;
            Cargo fromCargo = left.Cargo;
            Cargo toCargo = right.Cargo;
            if (left.Cargo.Mass > left.TotalCargoCapacity)
            {
                fromCargo = left.Cargo;
                toCargo = right.Cargo;
                ktToMove = left.Cargo.Mass - left.TotalCargoCapacity;
            }
            else if (right.Cargo.Mass > right.TotalCargoCapacity)
            {
                fromCargo = right.Cargo;
                toCargo = left.Cargo;
                ktToMove = right.Cargo.Mass - right.TotalCargoCapacity;
            }

            double proportion = (double)ktToMove / fromCargo.Mass;
            if (ktToMove > 0)
            {
                // Try and move cargo
                int ironToMove = (int)Math.Ceiling(fromCargo.Ironium * proportion);
                if (ironToMove > ktToMove)
                {
                    ironToMove = ktToMove;
                }
                toCargo.Ironium += ironToMove;
                fromCargo.Ironium -= ironToMove;
                ktToMove -= ironToMove;
            }
            if (ktToMove > 0)
            {
                // Try and move cargo
                int borToMove = (int)Math.Ceiling(fromCargo.Boranium * proportion);
                if (borToMove > ktToMove)
                {
                    borToMove = ktToMove;
                }
                toCargo.Boranium += borToMove;
                fromCargo.Boranium -= borToMove;
                ktToMove -= borToMove;
            }
            if (ktToMove > 0)
            {
                // Try and move cargo
                int germToMove = (int)Math.Ceiling(fromCargo.Germanium * proportion);
                if (germToMove > ktToMove)
                {
                    germToMove = ktToMove;
                }
                toCargo.Germanium += germToMove;
                fromCargo.Germanium -= germToMove;
                ktToMove -= germToMove;
            }
            if (ktToMove > 0)
            {
                // Try and move cargo
                int colsToMove = (int)Math.Ceiling(fromCargo.ColonistsInKilotons * proportion);
                if (colsToMove > ktToMove)
                {
                    colsToMove = ktToMove;
                }
                toCargo.ColonistsInKilotons += colsToMove;
                fromCargo.ColonistsInKilotons -= colsToMove;
                ktToMove -= colsToMove;
            }
            Debug.Assert(ktToMove == 0, "Must not be negative.");

            // fuel
            if (left.FuelAvailable > left.TotalFuelCapacity)
            {
                // Move excess to right and set left to max
                right.FuelAvailable += left.FuelAvailable - left.TotalFuelCapacity;
                left.FuelAvailable = left.TotalFuelCapacity;
            }
            else if (right.FuelAvailable > right.TotalFuelCapacity)
            {
                // Move excess to left and set right to max
                left.FuelAvailable += right.FuelAvailable - right.TotalFuelCapacity;
                right.FuelAvailable = right.TotalFuelCapacity;
            }
        }
    }
}

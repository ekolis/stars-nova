#region Copyright Notice
// ============================================================================
// Copyright (C) 2011-2012 The Stars-Nova Project
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
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    
    using Nova.Common;
    using Nova.Common.Components;
    
    public partial class SplitFleetDialog : Form
    {
        private List<ShipDesign> designs;
        public Dictionary<long, ShipToken> SourceComposition {get; private set;}
        public Dictionary<long, ShipToken> OtherComposition {get; private set;}
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
            // Use copies as fleets will get modified in the Task, not here.
            SourceComposition = new Dictionary<long, ShipToken>();
            OtherComposition = new Dictionary<long, ShipToken>();
            
            foreach (long key in sourceFleet.Composition.Keys)
            {
                if (!SourceComposition.ContainsKey(key))
                {
                    SourceComposition[key] = new ShipToken(sourceFleet.Composition[key].Design, sourceFleet.Composition[key].Quantity);
                }
                
                if (!OtherComposition.ContainsKey(key))
                {
                    OtherComposition[key] = new ShipToken(SourceComposition[key].Design, 0);
                }
            }
            
            if (otherFleet != null)
            {
                foreach (long key in otherFleet.Composition.Keys)
                {
                    if (!OtherComposition.ContainsKey(key))
                    {
                        OtherComposition[key] = new ShipToken(otherFleet.Composition[key].Design, otherFleet.Composition[key].Quantity);
                    }
                    else
                    {
                        OtherComposition[key].Quantity += otherFleet.Composition[key].Quantity;
                    }
                    
                    if (!SourceComposition.ContainsKey(key))
                    {
                        SourceComposition[key] = new ShipToken(OtherComposition[key].Design, 0);
                    }
                }
            }

            designs = new List<ShipDesign>();
            designs.AddRange(SourceComposition.Values.Select(d => d.Design).OrderBy(x => x.Name));
            
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
                num.Maximum = SourceComposition[designs[i].Key].Quantity + OtherComposition[designs[i].Key].Quantity;
                num.Value = SourceComposition[designs[i].Key].Quantity;
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
                num.Maximum = SourceComposition[designs[i].Key].Quantity + OtherComposition[designs[i].Key].Quantity;
                num.Value = OtherComposition[designs[i].Key].Quantity;
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
            decimal newval = (side == Side.Left) ? left.Value : right.Value;
            newval = max - newval;
            if (side == Side.Left)
            {
                right.Value = newval;
                OtherComposition[designs[index].Key].Quantity = (int)newval;
            }
            else
            {
                left.Value = newval;
                SourceComposition[designs[index].Key].Quantity = (int)newval;
            }
        } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.WinForms.Gui.Dialogs
{
    public partial class SplitFleetDialog : Form
    {
        private List<Design> designs;
        private Dictionary<Design, int> leftFleet;
        private Dictionary<Design, int> rightFleet;
        private List<NumericUpDown> leftNumerics;
        private List<NumericUpDown> rightNumerics;


        private enum Side { Left, Right };

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
                    rightFleet[des] = 0;
            }
            foreach (Design des in rightFleet.Keys)
            {
                if (!leftFleet.ContainsKey(des))
                    leftFleet[des] = 0;
            }

            designs = new List<Design>();
            designs.AddRange(leftFleet.Keys.OrderBy(x=>x.Name));
            
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
            this.Height += designs.Count*26 - 26;
            for( int i = 0; i < designs.Count; ++i)
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
                right.Value = newval;
            else
                left.Value = newval;
        }   
    }
}

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
// A simple control that combines a checkbox with an up-down counter.
// ===========================================================================
#endregion

using System;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.ControlLibrary
{

    public partial class EnabledCounter : UserControl
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnabledCounter()
        {
            InitializeComponent();
        }


        // ============================================================================
        // Get and set individual control values. These are meant to be used at design
        // time:
        //
        // Counter value (ControlCounter)
        // Control text  (ControlText)
        // Enabled mode  (ControlSelected)
        // Minimum Value (Minimum)
        // Maximum Value (Maximum)
        // ============================================================================
        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the current value of the counter.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int ControlCounter
        {
            get { return (int)UpDownCounter.Value; }
            set { UpDownCounter.Value = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the text displayed on the control.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string ControlText
        {
            get { return CheckBox.Text; }
            set { CheckBox.Text = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the selection checkbox within the control.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool ControlSelected
        {
            get { return CheckBox.Checked; }
            set { CheckBox.Checked = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the maximum value of the counter.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Minimum
        {
            get { return (int)UpDownCounter.Minimum; }
            set { UpDownCounter.Minimum = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or Set the maximum value of the counter.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public int Maximum
        {
            get { return (int)UpDownCounter.Maximum; }
            set { UpDownCounter.Maximum = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get and set control value as an EnabledValue. This interface is meant to be
        /// used at run-time.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public EnabledValue Value
        {
            get
            {
                EnabledValue result = new EnabledValue();
                result.IsChecked = CheckBox.Checked;
                result.NumericValue = (int)UpDownCounter.Value;
                return result;
            }

            set
            {
                UpDownCounter.Value = value.NumericValue;
                CheckBox.Checked = value.IsChecked;
            }
        }


        #endregion
    }

}

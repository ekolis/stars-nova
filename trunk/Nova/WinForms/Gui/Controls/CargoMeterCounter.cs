﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nova.WinForms.Gui.Controls
{
    public partial class CargoMeterCounter : UserControl
    {
        private bool reversed = false;
        private bool ignoreNumericChange = false;

        public delegate void ValueChangedHandler(int newValue);

        public event ValueChangedHandler ValueChanged;

        public CargoMeterCounter()
        {
            InitializeComponent();
            numeric.ValueChanged += numeric_ValueChanged;
            meterCargo.ValueChanged += meterCargo_ValueChanged;
        }

        public int Maximum
        {
            get 
            { 
                return meterCargo.Maximum;  
            }
            set 
            { 
                meterCargo.Maximum = value;
                numeric.Maximum = value;
            }
        }

        public int Value
        {
            get 
            { 
                return meterCargo.Value; 
            }
            set 
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                meterCargo.Value = value;
                ignoreNumericChange = true;
                numeric.Value = value;
                ignoreNumericChange = false;
            }
        }

        public CargoMeter.CargoType Cargo
        {
            get { return meterCargo.Cargo; }
            set { meterCargo.Cargo = value; }
        }

        public void meterCargo_ValueChanged(int newValue)
        {
            ignoreNumericChange = true;
            numeric.Value = newValue;
            ignoreNumericChange = false;
            FireValueChanged();
        }

        public void numeric_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreNumericChange)
            {
                meterCargo.Value = (int) numeric.Value;
                FireValueChanged();
            }
        }

        public bool Reversed
        {
            get 
            { 
                return reversed; 
            }

            set
            {
                if (reversed != value)
                {
                    reversed = value;
                    if (reversed)
                    {
                        tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Absolute;
                        tableLayoutPanel1.ColumnStyles[1].Width = 58f;
                        tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                        tableLayoutPanel1.ColumnStyles[1].Width = 50f;
                        tableLayoutPanel1.Controls.Clear();
                        tableLayoutPanel1.Controls.Add(meterCargo, 0, 0);
                        tableLayoutPanel1.Controls.Add(numeric, 1, 0);
                        numeric.Anchor = AnchorStyles.Left;
                    }
                    else
                    {
                        tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Absolute;
                        tableLayoutPanel1.ColumnStyles[1].Width = 58f;
                        tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                        tableLayoutPanel1.ColumnStyles[1].Width = 50f;
                        tableLayoutPanel1.Controls.Clear();
                        tableLayoutPanel1.Controls.Add(meterCargo, 1, 0);
                        tableLayoutPanel1.Controls.Add(numeric, 0, 0);
                        numeric.Anchor = AnchorStyles.Right;
                    }
                    Invalidate();
                }
            }
        }

        private void FireValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(Value);
            }
        }
    }
}

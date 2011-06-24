using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nova.WinForms.Gui.Controls
{
    public partial class CargoMeterCounter : UserControl
    {
        public CargoMeterCounter()
        {
            InitializeComponent();
            numeric.ValueChanged += numeric_ValueChanged;
            meterCargo.ValueChanged += meterCargo_ValueChanged;
        }

        public int Maximum
        {
            get { return meterCargo.Maximum;  }
            set 
            { 
                meterCargo.Maximum = value;
                numeric.Maximum = value;
            }
        }

        public int Value
        {
            get { return meterCargo.Value; }
            set 
            { 
                meterCargo.Value = value;
                numeric.Value = value;
            }
        }

        public CargoMeter.CargoType Cargo
        {
            get { return meterCargo.Cargo; }
            set { meterCargo.Cargo = value; }
        }

        void meterCargo_ValueChanged(int newValue)
        {
            numeric.Value = newValue;
        }

        void numeric_ValueChanged(object sender, EventArgs e)
        {
            meterCargo.Value = (int)numeric.Value;
        }
    }
}

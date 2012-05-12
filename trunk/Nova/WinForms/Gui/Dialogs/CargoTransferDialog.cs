#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010, 2011 stars-nova
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
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Nova.Common;

    public partial class CargoTransferDialog : Form
    {
        private Cargo leftCargo;
        private Cargo rightCargo;
        private int leftFuel;
        private int rightFuel;

        public CargoTransferDialog()
        {
            InitializeComponent();

            cargoIronLeft.ValueChanged += CargoIronLeft_ValueChanged;
            cargoIronRight.ValueChanged += CargoIronRight_ValueChanged;
            cargoBoraniumLeft.ValueChanged += CargoBoraniumLeft_ValueChanged;
            cargoBoraniumRight.ValueChanged += CargoBoraniumRight_ValueChanged;
            cargoGermaniumLeft.ValueChanged += CargoGermaniumLeft_ValueChanged;
            cargoGermaniumRight.ValueChanged += CargoGermaniumRight_ValueChanged;
            cargoColonistsLeft.ValueChanged += CargoColonistsLeft_ValueChanged;
            cargoColonistsRight.ValueChanged += CargoColonistsRight_ValueChanged;
            fuelLeft.ValueChanged += FuelLeft_ValueChanged;
            fuelRight.ValueChanged += FuelRight_ValueChanged;
        }

        private int ReJigValues(int newValue, out int newRightValue, int leftLevel, int rightLevel, int leftMass, int rightMass, int leftMax, int rightMax)
        {
            // First if we request more than there is then change to request the max available
            int totalLevel = leftLevel + rightLevel;
            if (newValue > totalLevel)
            {
                newValue = totalLevel;
            }

            leftMass -= leftLevel;
            rightMass -= rightLevel;
            if (leftMass + newValue > leftMax)
            {
                // Have blown the left cargo limit - work out how far we can go
                newValue = leftMax - leftMass;
            }

            // so now we work out the right side
            newRightValue = leftLevel + rightLevel - newValue;
            if (rightMass + newRightValue > rightMax)
            {
                // Have blown the left cargo limit - work out how far we can go
                newRightValue = rightMax - rightMass;
                newValue = leftLevel + rightLevel - newRightValue;
                if (leftMass + newValue > leftMax)
                {
                    // We can't do it - reset
                    newValue = leftLevel;
                    newRightValue = rightLevel;
                }
            }
            return newValue;
        }

        private void CargoIronLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.Ironium, rightCargo.Ironium, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.Ironium = newValue;
            rightCargo.Ironium = newRightValue;
            UpdateMeters();
        }


        public void CargoIronRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.Ironium, leftCargo.Ironium, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.Ironium = newLeftLevel;
            rightCargo.Ironium = newValue;
            UpdateMeters();
        }

        private void CargoBoraniumLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.Boranium, rightCargo.Boranium, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.Boranium = newValue;
            rightCargo.Boranium = newRightValue;
            UpdateMeters();
        }

        private void CargoBoraniumRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.Boranium, leftCargo.Boranium, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.Boranium = newLeftLevel;
            rightCargo.Boranium = newValue;
            UpdateMeters();
        }

        private void CargoGermaniumLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.Germanium, rightCargo.Germanium, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.Germanium = newValue;
            rightCargo.Germanium = newRightValue;
            UpdateMeters();
        }

        private void CargoGermaniumRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.Germanium, leftCargo.Germanium, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.Germanium = newLeftLevel;
            rightCargo.Germanium = newValue;
            UpdateMeters();
        }

        private void CargoColonistsLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.ColonistsInKilotons, rightCargo.ColonistsInKilotons, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.ColonistsInKilotons = newValue;
            rightCargo.ColonistsInKilotons = newRightValue;
            UpdateMeters();
        }

        private void CargoColonistsRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.ColonistsInKilotons, leftCargo.ColonistsInKilotons, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.ColonistsInKilotons = newLeftLevel;
            rightCargo.ColonistsInKilotons = newValue;
            UpdateMeters();
        }

        private void FuelLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftFuel, rightFuel, leftFuel, rightFuel, fuelLeft.Maximum, fuelRight.Maximum);
            leftFuel = newValue;
            rightFuel = newRightValue;
            UpdateMeters();
        }

        private void FuelRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightFuel, leftFuel, rightFuel, leftFuel, fuelRight.Maximum, fuelLeft.Maximum);
            leftFuel = newLeftLevel;
            rightFuel = newValue;
            UpdateMeters();
        }



        public void SetFleets(Fleet left, Fleet right)
        {
            leftCargo = new Cargo(left.Cargo);
            rightCargo = new Cargo(right.Cargo);
            leftFuel = (int) left.FuelAvailable;
            rightFuel = (int) right.FuelAvailable;

            cargoIronLeft.Maximum = left.TotalCargoCapacity;
            cargoBoraniumLeft.Maximum = left.TotalCargoCapacity;
            cargoGermaniumLeft.Maximum = left.TotalCargoCapacity;
            cargoColonistsLeft.Maximum = left.TotalCargoCapacity;

            cargoIronRight.Maximum = right.TotalCargoCapacity;
            cargoBoraniumRight.Maximum = right.TotalCargoCapacity;
            cargoGermaniumRight.Maximum = right.TotalCargoCapacity;
            cargoColonistsRight.Maximum = right.TotalCargoCapacity;

            fuelLeft.Maximum = left.TotalFuelCapacity;
            fuelRight.Maximum = right.TotalFuelCapacity;

            cargoMeterLeft.Maximum = left.TotalCargoCapacity;
            cargoMeterRight.Maximum = right.TotalCargoCapacity;

            labelFleet1.Text = left.Name;
            labelFleet2.Text = right.Name;

            UpdateMeters();
        }

        private void UpdateMeters()
        {
            cargoIronLeft.Value = leftCargo.Ironium;
            cargoBoraniumLeft.Value = leftCargo.Boranium;
            cargoGermaniumLeft.Value = leftCargo.Germanium;
            cargoColonistsLeft.Value = leftCargo.ColonistsInKilotons;

            cargoIronRight.Value = rightCargo.Ironium;
            cargoBoraniumRight.Value = rightCargo.Boranium;
            cargoGermaniumRight.Value = rightCargo.Germanium;
            cargoColonistsRight.Value = rightCargo.ColonistsInKilotons;

            fuelLeft.Value = leftFuel;
            fuelRight.Value = rightFuel;

            cargoMeterLeft.CargoLevels = leftCargo;
            cargoMeterRight.CargoLevels = rightCargo;
        }

        public Cargo LeftCargo
        {
            get { return leftCargo; }
        }

        public Cargo RightCargo
        {
            get { return rightCargo; }
        }

        public int LeftFuel
        {
            get { return leftFuel; }
        }

        public int RightFuel
        {
            get { return rightFuel; }
        }
    }
}

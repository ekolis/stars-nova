using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nova.Common;

namespace Nova.WinForms.Gui.Dialogs
{
    public partial class CargoTransferDialog : Form
    {
        private Cargo leftCargo;
        private Cargo rightCargo;
        private int leftFuel;
        private int rightFuel;



        public CargoTransferDialog()
        {
            InitializeComponent();

            cargoIronLeft.ValueChanged += cargoIronLeft_ValueChanged;
            cargoIronRight.ValueChanged += cargoIronRight_ValueChanged;
            cargoBoraniumLeft.ValueChanged += cargoBoraniumLeft_ValueChanged;
            cargoBoraniumRight.ValueChanged += cargoBoraniumRight_ValueChanged;
            cargoGermaniumLeft.ValueChanged += cargoGermaniumLeft_ValueChanged;
            cargoGermaniumRight.ValueChanged += cargoGermaniumRight_ValueChanged;
            cargoColonistsLeft.ValueChanged += cargoColonistsLeft_ValueChanged;
            cargoColonistsRight.ValueChanged += cargoColonistsRight_ValueChanged;
            fuelLeft.ValueChanged += fuelLeft_ValueChanged;
            fuelRight.ValueChanged += fuelRight_ValueChanged;
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

        private void cargoIronLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.Ironium, rightCargo.Ironium, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.Ironium = newValue;
            rightCargo.Ironium = newRightValue;
            UpdateMeters();
        }


        public void cargoIronRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.Ironium, leftCargo.Ironium, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.Ironium = newLeftLevel;
            rightCargo.Ironium = newValue;
            UpdateMeters();
        }

        private void cargoBoraniumLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.Boranium, rightCargo.Boranium, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.Boranium = newValue;
            rightCargo.Boranium = newRightValue;
            UpdateMeters();
        }

        private void cargoBoraniumRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.Boranium, leftCargo.Boranium, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.Boranium = newLeftLevel;
            rightCargo.Boranium = newValue;
            UpdateMeters();
        }

        private void cargoGermaniumLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.Germanium, rightCargo.Germanium, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.Germanium = newValue;
            rightCargo.Germanium = newRightValue;
            UpdateMeters();
        }

        private void cargoGermaniumRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.Germanium, leftCargo.Germanium, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.Germanium = newLeftLevel;
            rightCargo.Germanium = newValue;
            UpdateMeters();
        }

        private void cargoColonistsLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftCargo.ColonistsInKilotons, rightCargo.ColonistsInKilotons, leftCargo.Mass, rightCargo.Mass, cargoMeterLeft.Maximum, cargoMeterRight.Maximum);
            leftCargo.ColonistsInKilotons = newValue;
            rightCargo.ColonistsInKilotons = newRightValue;
            UpdateMeters();
        }

        private void cargoColonistsRight_ValueChanged(int newValue)
        {
            int newLeftLevel;
            newValue = ReJigValues(newValue, out newLeftLevel, rightCargo.ColonistsInKilotons, leftCargo.ColonistsInKilotons, rightCargo.Mass, leftCargo.Mass, cargoMeterRight.Maximum, cargoMeterLeft.Maximum);
            leftCargo.ColonistsInKilotons = newLeftLevel;
            rightCargo.ColonistsInKilotons = newValue;
            UpdateMeters();
        }

        private void fuelLeft_ValueChanged(int newValue)
        {
            int newRightValue;
            newValue = ReJigValues(newValue, out newRightValue, leftFuel, rightFuel, leftFuel, rightFuel, fuelLeft.Maximum, fuelRight.Maximum);
            leftFuel = newValue;
            rightFuel = newRightValue;
            UpdateMeters();
        }

        private void fuelRight_ValueChanged(int newValue)
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

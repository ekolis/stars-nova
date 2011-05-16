#region Copyright Notice
// ============================================================================
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
// This module manages the Race Restriction Dialog, used for selecting any
// primary racial trait or lesser racial trait restrictions which apply to
// a component.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nova.Common;
#endregion

namespace Nova.WinForms.ComponentEditor
{
    public partial class RaceRestrictionDialog : Form
    {
        public RaceRestriction Restrictions = null; // data representing the restriction
        private readonly RadioButton[,] radioMap = new RadioButton[23, 3]; // an array representation (map) of the radio button controls for ease of programatic manipulation


        /// <Summary>
        /// Initialising constructor for the dialog
        /// </Summary>
        /// <param name="existingName">The component's name.</param>
        /// <param name="existingImage">The component's Image</param> 
        /// <param name="existingRestrictions">The component's current <see cref="RaceRestriction"'s/></param>
        public RaceRestrictionDialog(string existingName, Bitmap existingImage, RaceRestriction existingRestrictions)
        {
            InitializeComponent();

            try
            {
                this.componentName.Text = existingName;
                this.componentImage.Image = existingImage;
            }
            catch
            {
                // incase the name or image are null.
            }

            // Map the RadioButton controls to an array for easier manipulation.
            // PRTs
            this.radioMap[0, 0] = this.notAvailableHE;
            this.radioMap[0, 1] = this.notRequiredHE;
            this.radioMap[0, 2] = this.requiredHE;

            this.radioMap[1, 0] = this.notAvailableSS;
            this.radioMap[1, 1] = this.notRequiredSS;
            this.radioMap[1, 2] = this.requiredSS;

            this.radioMap[2, 0] = this.notAvailableWM;
            this.radioMap[2, 1] = this.notRequiredWM;
            this.radioMap[2, 2] = this.requiredWM;

            this.radioMap[3, 0] = this.notAvailableCA;
            this.radioMap[3, 1] = this.notRequiredCA;
            this.radioMap[3, 2] = this.requiredCA;

            this.radioMap[4, 0] = this.notAvailableIS;
            this.radioMap[4, 1] = this.notRequiredIS;
            this.radioMap[4, 2] = this.requiredIS;

            this.radioMap[5, 0] = this.notAvailableSD;
            this.radioMap[5, 1] = this.notRequiredSD;
            this.radioMap[5, 2] = this.requiredSD;

            this.radioMap[6, 0] = this.notAvailablePP;
            this.radioMap[6, 1] = this.notRequiredPP;
            this.radioMap[6, 2] = this.requiredPP;

            this.radioMap[7, 0] = this.notAvailableIT;
            this.radioMap[7, 1] = this.notRequiredIT;
            this.radioMap[7, 2] = this.requiredIT;

            this.radioMap[8, 0] = this.notAvailableAR;
            this.radioMap[8, 1] = this.notRequiredAR;
            this.radioMap[8, 2] = this.requiredAR;

            this.radioMap[9, 0] = this.notAvailableJOAT;
            this.radioMap[9, 1] = this.notRequiredJOAT;
            this.radioMap[9, 2] = this.requiredJOAT;

            // LRTs
            this.radioMap[10, 0] = this.notAvailableIFE;
            this.radioMap[10, 1] = this.notRequiredIFE;
            this.radioMap[10, 2] = this.requiredIFE;

            this.radioMap[11, 0] = this.notAvailableTT;
            this.radioMap[11, 1] = this.notRequiredTT;
            this.radioMap[11, 2] = this.requiredTT;

            this.radioMap[12, 0] = this.notAvailableARM;
            this.radioMap[12, 1] = this.notRequiredARM;
            this.radioMap[12, 2] = this.requiredARM;

            this.radioMap[13, 0] = this.notAvailableISB;
            this.radioMap[13, 1] = this.notRequiredISB;
            this.radioMap[13, 2] = this.requiredISB;

            this.radioMap[14, 0] = this.notAvailableGR;
            this.radioMap[14, 1] = this.notRequiredGR;
            this.radioMap[14, 2] = this.requiredGR;

            this.radioMap[15, 0] = this.notAvailableUR;
            this.radioMap[15, 1] = this.notRequiredUR;
            this.radioMap[15, 2] = this.requiredUR;

            this.radioMap[16, 0] = this.notAvailableMA;
            this.radioMap[16, 1] = this.notRequiredMA;
            this.radioMap[16, 2] = this.requiredMA;

            this.radioMap[17, 0] = this.notAvailableNRSE;
            this.radioMap[17, 1] = this.notRequiredNRSE;
            this.radioMap[17, 2] = this.requiredNRSE;

            this.radioMap[18, 0] = this.notAvailableOBRM;
            this.radioMap[18, 1] = this.notRequiredOBRM;
            this.radioMap[18, 2] = this.requiredOBRM;

            this.radioMap[19, 0] = this.notAvailableNAS;
            this.radioMap[19, 1] = this.notRequiredNAS;
            this.radioMap[19, 2] = this.requiredNAS;

            this.radioMap[20, 0] = this.notAvailableLSP;
            this.radioMap[20, 1] = this.notRequiredLSP;
            this.radioMap[20, 2] = this.requiredLSP;

            this.radioMap[21, 0] = this.notAvailableBET;
            this.radioMap[21, 1] = this.notRequiredBET;
            this.radioMap[21, 2] = this.requiredBET;

            this.radioMap[22, 0] = this.notAvailableRS;
            this.radioMap[22, 1] = this.notRequiredRS;
            this.radioMap[22, 2] = this.requiredRS;

            // Initialise race restriction radio button selections.
            if (existingRestrictions != null)
            {
                Restrictions = new RaceRestriction(existingRestrictions);

                int n = 0;
                foreach (string trait in AllTraits.TraitKeys)
                {
                    // For each trait there are three radio buttons. We need to set one of them,
                    // which will clear the other two (as only one radio button in a set can be selected). The
                    // enumerated Availability maps onto which of the three buttons to select.
                    this.radioMap[n, (int)existingRestrictions.Availability(trait)].Checked = true;
                    n++;
                }
            }
            else
            {
                Restrictions = new RaceRestriction();
            }

        }


        /// <Summary>
        /// Save button - copies the form data to a RaceRestriction.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Save_Click(object sender, EventArgs e)
        {
            int n = 0;
            foreach (string trait in AllTraits.TraitKeys)
            {
                if (this.radioMap[n, 0].Checked)
                {
                    Restrictions.SetRestriction(trait, RaceAvailability.not_available);
                }
                else if (this.radioMap[n, 1].Checked)
                {
                    Restrictions.SetRestriction(trait, RaceAvailability.not_required);
                }
                else if (this.radioMap[n, 2].Checked)
                {
                    Restrictions.SetRestriction(trait, RaceAvailability.required);
                }
                n++;
            }

            Close();
        }


        /// <Summary>
        /// Cancel button - just close the form and discard any changes.
        /// </Summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }

}

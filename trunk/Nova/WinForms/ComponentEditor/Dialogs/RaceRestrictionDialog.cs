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
        private RadioButton[,] RadioMap = new RadioButton[23, 3]; // an array representation (map) of the radio button controls for ease of programatic manipulation


        /// <summary>
        /// Initialising constructor for the dialog
        /// </summary>
        /// <param name="existingName">The component's name.</param>
        /// <param name="existingImage">The component's Image</param> 
        /// <param name="existingRestrictions">The component's current <see cref="RaceRestriction"'s/></param>
        public RaceRestrictionDialog(string existingName, Bitmap existingImage, RaceRestriction existingRestrictions)
        {
            InitializeComponent();

            try
            {
                ComponentName.Text = existingName;
                ComponentImage.Image = existingImage;
            }
            catch
            {
                // incase the name or image are null.
            }

            // Map the RadioButton controls to an array for easier manipulation.
            // PRTs
            RadioMap[0, 0] = this.notAvailableHE; RadioMap[0, 1]   = this.notRequiredHE; RadioMap[0, 2]   = this.requiredHE;
            RadioMap[1, 0] = this.notAvailableSS; RadioMap[1, 1]   = this.notRequiredSS; RadioMap[1, 2]   = this.requiredSS;
            RadioMap[2, 0] = this.notAvailableWM; RadioMap[2, 1]   = this.notRequiredWM; RadioMap[2, 2]   = this.requiredWM;
            RadioMap[3, 0] = this.notAvailableCA; RadioMap[3, 1]   = this.notRequiredCA; RadioMap[3, 2]   = this.requiredCA;
            RadioMap[4, 0] = this.notAvailableIS; RadioMap[4, 1]   = this.notRequiredIS; RadioMap[4, 2]   = this.requiredIS;
            RadioMap[5, 0] = this.notAvailableSD; RadioMap[5, 1]   = this.notRequiredSD; RadioMap[5, 2]   = this.requiredSD;
            RadioMap[6, 0] = this.notAvailablePP; RadioMap[6, 1]   = this.notRequiredPP; RadioMap[6, 2]   = this.requiredPP;
            RadioMap[7, 0] = this.notAvailableIT; RadioMap[7, 1]   = this.notRequiredIT; RadioMap[7, 2]   = this.requiredIT;
            RadioMap[8, 0] = this.notAvailableAR; RadioMap[8, 1]   = this.notRequiredAR; RadioMap[8, 2]   = this.requiredAR;
            RadioMap[9, 0] = this.notAvailableJOAT; RadioMap[9, 1] = this.notRequiredJOAT; RadioMap[9, 2] = this.requiredJOAT;

            // LRTs
            RadioMap[10, 0] = this.notAvailableIFE; RadioMap[10, 1]  = this.notRequiredIFE; RadioMap[10, 2]  = this.requiredIFE;
            RadioMap[11, 0] = this.notAvailableTT; RadioMap[11, 1]   = this.notRequiredTT; RadioMap[11, 2]   = this.requiredTT;
            RadioMap[12, 0] = this.notAvailableARM; RadioMap[12, 1]  = this.notRequiredARM; RadioMap[12, 2]  = this.requiredARM;
            RadioMap[13, 0] = this.notAvailableISB; RadioMap[13, 1]  = this.notRequiredISB; RadioMap[13, 2]  = this.requiredISB;
            RadioMap[14, 0] = this.notAvailableGR; RadioMap[14, 1]   = this.notRequiredGR; RadioMap[14, 2]   = this.requiredGR;
            RadioMap[15, 0] = this.notAvailableUR; RadioMap[15, 1]   = this.notRequiredUR; RadioMap[15, 2]   = this.requiredUR;
            RadioMap[16, 0] = this.notAvailableMA; RadioMap[16, 1]   = this.notRequiredMA; RadioMap[16, 2]   = this.requiredMA;
            RadioMap[17, 0] = this.notAvailableNRSE; RadioMap[17, 1] = this.notRequiredNRSE; RadioMap[17, 2] = this.requiredNRSE;
            RadioMap[18, 0] = this.notAvailableOBRM; RadioMap[18, 1] = this.notRequiredOBRM; RadioMap[18, 2] = this.requiredOBRM;
            RadioMap[19, 0] = this.notAvailableNAS; RadioMap[19, 1]  = this.notRequiredNAS; RadioMap[19, 2]  = this.requiredNAS;
            RadioMap[20, 0] = this.notAvailableLSP; RadioMap[20, 1]  = this.notRequiredLSP; RadioMap[20, 2]  = this.requiredLSP;
            RadioMap[21, 0] = this.notAvailableBET; RadioMap[21, 1]  = this.notRequiredBET; RadioMap[21, 2]  = this.requiredBET;
            RadioMap[22, 0] = this.notAvailableRS; RadioMap[22, 1]   = this.notRequiredRS; RadioMap[22, 2]   = this.requiredRS;

            // Initialise race restriction radio button selections.
            if (existingRestrictions != null)
            {
                Restrictions = new RaceRestriction(existingRestrictions);

                int n = 0;
                foreach (String trait in AllTraits.TraitKeys)
                {
                    // For each trait there are three radio buttons. We need to set one of them,
                    // which will clear the other two (as only one radio button in a set can be selected). The
                    // enumerated Availability maps onto which of the three buttons to select.
                    RadioMap[n, (int)existingRestrictions.Availability(trait)].Checked = true;
                    n++;
                }
            }
            else
            {
                Restrictions = new RaceRestriction();
            }

        }


        /// <summary>
        /// Save button - copies the form data to a RaceRestriction.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Save_Click(object sender, EventArgs e)
        {
            int n = 0;
            foreach (String trait in AllTraits.TraitKeys)
            {
                if (RadioMap[n, 0].Checked)
                    Restrictions.SetRestriction(trait, RaceAvailability.not_available);
                else if (RadioMap[n, 1].Checked)
                    Restrictions.SetRestriction(trait, RaceAvailability.not_required);
                else if (RadioMap[n, 2].Checked)
                    Restrictions.SetRestriction(trait, RaceAvailability.required);
                n++;
            }

            Close();
        }


        /// <summary>
        /// Cancel button - just close the form and discard any changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }

}

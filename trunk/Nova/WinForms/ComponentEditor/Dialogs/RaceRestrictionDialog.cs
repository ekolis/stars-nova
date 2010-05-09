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
using NovaCommon;
#endregion

namespace Nova.WinForms.ComponentEditor
{
    public partial class RaceRestrictionDialog : Form
    {
        public RaceRestriction Restrictions = null; // data representing the restriction
        private RadioButton[,] RadioMap = new RadioButton[23,3]; // an array representation (map) of the radio button controls for ease of programatic manipulation


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
            RadioMap[0,0] = HE_NotAvailable; RadioMap[0,1]   = HE_NotRequired; RadioMap[0,2]   = HE_Required;
            RadioMap[1,0] = SS_NotAvailable; RadioMap[1,1]   = SS_NotRequired; RadioMap[1,2]   = SS_Required;
            RadioMap[2,0] = WM_NotAvailable; RadioMap[2,1]   = WM_NotRequired; RadioMap[2,2]   = WM_Required;
            RadioMap[3,0] = CA_NotAvailable; RadioMap[3,1]   = CA_NotRequired; RadioMap[3,2]   = CA_Required;
            RadioMap[4,0] = IS_NotAvailable; RadioMap[4,1]   = IS_NotRequired; RadioMap[4,2]   = IS_Required;
            RadioMap[5,0] = SD_NotAvailable; RadioMap[5,1]   = SD_NotRequired; RadioMap[5,2]   = SD_Required;
            RadioMap[6,0] = PP_NotAvailable; RadioMap[6,1]   = PP_NotRequired; RadioMap[6,2]   = PP_Required;
            RadioMap[7,0] = IT_NotAvailable; RadioMap[7,1]   = IT_NotRequired; RadioMap[7,2]   = IT_Required;
            RadioMap[8,0] = AR_NotAvailable; RadioMap[8,1]   = AR_NotRequired; RadioMap[8,2]   = AR_Required;
            RadioMap[9,0] = JOAT_NotAvailable; RadioMap[9,1] = JOAT_NotRequired; RadioMap[9,2] = JOAT_Required;

            //LRTs
            RadioMap[10, 0] = IFE_NotAvailable; RadioMap[10, 1]  = IFE_NotRequired; RadioMap[10, 2]  = IFE_Required;
            RadioMap[11, 0] =  TT_NotAvailable; RadioMap[11, 1]  =  TT_NotRequired; RadioMap[11, 2]  =  TT_Required;
            RadioMap[12, 0] = ARM_NotAvailable; RadioMap[12, 1]  = ARM_NotRequired; RadioMap[12, 2]  = ARM_Required;
            RadioMap[13, 0] = ISB_NotAvailable; RadioMap[13, 1]  = ISB_NotRequired; RadioMap[13, 2]  = ISB_Required;
            RadioMap[14, 0] = GR_NotAvailable; RadioMap[14, 1]   = GR_NotRequired; RadioMap[14, 2]   = GR_Required;
            RadioMap[15, 0] = UR_NotAvailable; RadioMap[15, 1]   = UR_NotRequired; RadioMap[15, 2]   = UR_Required;
            RadioMap[16, 0] = MA_NotAvailable; RadioMap[16, 1]   = MA_NotRequired; RadioMap[16, 2]   = MA_Required;
            RadioMap[17, 0] = NRSE_NotAvailable; RadioMap[17, 1] = NRSE_NotRequired; RadioMap[17, 2] = NRSE_Required;
            RadioMap[18, 0] = OBRM_NotAvailable; RadioMap[18, 1] = OBRM_NotRequired; RadioMap[18, 2] = OBRM_Required;
            RadioMap[19, 0] = NAS_NotAvailable; RadioMap[19, 1]  = NAS_NotRequired; RadioMap[19, 2]  = NAS_Required;
            RadioMap[20, 0] = LSP_NotAvailable; RadioMap[20, 1]  = LSP_NotRequired; RadioMap[20, 2]  = LSP_Required;
            RadioMap[21, 0] = BET_NotAvailable; RadioMap[21, 1]  = BET_NotRequired; RadioMap[21, 2]  = BET_Required;
            RadioMap[22, 0] = RS_NotAvailable; RadioMap[22, 1]   = RS_NotRequired; RadioMap[22, 2]   = RS_Required;

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
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
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
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }//RaceRestrictionDialog

}//namespace

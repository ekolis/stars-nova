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
// This file deals with component image selection.
// ===========================================================================
#endregion

#region Using Statements
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace Nova.WinForms.ComponentEditor
{
    public partial class ImageDisplay : UserControl
    {
        public string ImageFile = null;
        public ImageDisplay()
        {
            InitializeComponent();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Select an Image for loading into the picture control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void OnImageButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
           "Image Files (*.bmp;*.jpg;.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ComponentImage.Image = new Bitmap(dialog.OpenFile());
                ImageFile = dialog.FileName;

            }
            dialog.Dispose();
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get and set the value in the Image Box
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Image Image
        {
            get { return ComponentImage.Image; }
            set { ComponentImage.Image = value; }
        }

    }
}

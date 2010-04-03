// Nova. (c) 2008 Ken Reed
// (c) 2009, 2010, stars-nova
// See https://sourceforge.net/projects/stars-nova/
//
// This file deals with component image selection.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
#endregion

namespace ComponentEditor
{
    public partial class ImageDisplay : UserControl
    {
        public String ImageFile = null;
        public ImageDisplay()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Select an Image for loading into the picture control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
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


        /// <summary>
        /// Get and set the value in the Image Box
        /// </summary>
        public Image Image
        {
            get { return ComponentImage.Image; }
            set { ComponentImage.Image = value; }
        }

    }//ImageDisplay
}//namespace

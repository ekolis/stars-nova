// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
//
// This file deals with component image selection.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ComponentEditor
{
   public partial class ImageDisplay : UserControl
   {
       public String ImageFile = null;
      public ImageDisplay()
      {
         InitializeComponent();
      }



// ============================================================================
// Select an Image for loading into the picture control
// ============================================================================

      private void OnImageButtonClick(object sender, EventArgs e)
      {
         OpenFileDialog dialog = new OpenFileDialog();
         dialog.Filter = 
        "Image Files (*.bmp;*.jpg;.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";

         if (dialog.ShowDialog() == DialogResult.OK) {
            ComponentImage.Image = new Bitmap(dialog.OpenFile());
            ImageFile = dialog.FileName;
             
         }
         dialog.Dispose();
      }


// ============================================================================
// Get and set the value in the Image Box
// ============================================================================

      public Image Image {
         get { return ComponentImage.Image;  }
         set { ComponentImage.Image = value; }
      }

   }
}

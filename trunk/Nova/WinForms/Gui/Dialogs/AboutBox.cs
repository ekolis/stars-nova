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
// The "About" box dialog.
// ===========================================================================
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Nova.WinForms.Gui
{
    /// <summary>
    /// The "About" dialog box.
    /// </summary>
    partial class AboutBox : Form
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------------------------------
        public AboutBox()
        {
            InitializeComponent();

            this.logoPictureBox.Image = Nova.Properties.Resources.Nova;

            //  Initialize the AboutBox to display the product information from
            //  the assembly information.  Change assembly information settings
            //  for your application through either: -
            //  Project->Properties->Application->Assembly Information -
            //  AssemblyInfo.cs

            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.BoxDescription.Text =
              "Nova \r\n" +
              "\r\n" +
              "This is free software. You can redistribute it and/or modify " +
              "it under the terms of the GNU General Public License version " +
              "2 as published by the Free Software Foundation.";
        }

        #endregion

        #region Assembly Attribute Accessors


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get title
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute =
                       (AssemblyTitleAttribute)attributes[0];

                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }

                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get version
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get product
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get copyright
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get company
        /// </summary>
        /// ----------------------------------------------------------------------------
        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion
    }
}

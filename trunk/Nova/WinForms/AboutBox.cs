#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009, 2010 The Stars-Nova Project
//
// This file is part of Stars! Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This dialog displays the "About" box.
// ===========================================================================
#endregion

using System;
using System.Windows.Forms;
using System.Reflection;

namespace Nova.WinForms
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            logoPictureBox.Image = Properties.Resources.Nova;


            Text = String.Format("About {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("Version {0} - Build date {1}", ApplicationProductVersion, BuildDate.ToShortDateString());
            Description.Text =
                "Copyright © 2008 Ken Reed" + Environment.NewLine +
                "Copyright © 2009, 2010 The Stars-Nova Project" + Environment.NewLine +
                "" + Environment.NewLine +
                AssemblyProduct + " is licensed under two separate licenses for code and content." + Environment.NewLine +
                "" + Environment.NewLine +
                "Content (images, documentation and other media) is licensed under the " +
                "Creative Commons Attribution-ShareAlike 3.0 Unported license. Content " +
                "includes images, music, sounds, text and game content such as components " +
                "and races. You should have received a copy of the Creative Commons " +
                "Attribution-ShareAlike 3.0 Unported license along with this program. " +
                "If not, visit <http://creativecommons.org/licenses/by-sa/3.0/> or send a " +
                "letter to Creative Commons, 559 Nathan Abbott Way, Stanford, " +
                "California 94305, USA." + Environment.NewLine +
                "" + Environment.NewLine +
                "Everything else is licensed under the GNU General Public License " +
                "version 2. This includes, but is not limited to, source code, executable " +
                "and object code. You should have received a copy of the GNU General " +
                "Public License version 2 along with this program. If not, visit " +
                "<http://www.gnu.org/licenses/> or send a letter to the " +
                "Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, " +
                "MA 02110-1301, USA." + Environment.NewLine +
                "" + Environment.NewLine +
                "This program is distributed in the hope that it will be useful, but " +
                "WITHOUT ANY WARRANTY; without even the implied warranty of " +
                "MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General " +
                "Public License version 2 for more details.";
        }

        #region Assembly Attribute Accessors


        /// <summary>
        /// Get the assembly title.
        /// </summary>
        private string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }


        /// <summary>
        /// Get the assembly file version.
        /// </summary>
        private string ApplicationProductVersion
        {
            get
            {
                string version = Application.ProductVersion;
                string[] versionParts = version.Split('.');
                return string.Join(".", versionParts, 0, 3);
            }
        }

        private DateTime BuildDate
        {
            get
            {
                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                int buildNumber = assemblyName.Version.Build;
                int revision = assemblyName.Version.Revision;
                DateTime start = new DateTime(2000, 1, 1);
                DateTime buildDate = start.Add(new TimeSpan(buildNumber, 0, 0, 2 * revision, 0));

                return buildDate;
            }
        }

        /// <summary>
        /// Get the assembly product.
        /// </summary>
        private string AssemblyProduct
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

        #endregion
    
    }//AboutBox

}//namespace

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
// Dialog component dealing with properties common to all components.
// ===========================================================================
#endregion

#region Using Statements
using NovaCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
#endregion

namespace ComponentEditor
{

    public partial class CommonProperties : UserControl
    {
        public event EventHandler ListBoxChanged;
        private Hashtable AllComponents = null;

        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Construction
        /// </summary>
        /// ----------------------------------------------------------------------------
        public CommonProperties()
        {
            InitializeComponent();
            AllComponents = NovaCommon.AllComponents.Data.Components;
        }
        #endregion


        #region Event Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// List box selection changed. Delegate the processing of this event to the
        /// appropriate dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">A <see cref="EventArgs"/> that contains the event data.</param>
        /// ----------------------------------------------------------------------------
        private void ComponentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComponentList.SelectedItem == null)
            {
                return;
            }

            if (ListBoxChanged != null)
            {
                ListBoxChanged(sender, e);
            }
        }

        #endregion


        #region Suporting Methods

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate the list box wih all components of a specified type.
        /// </summary>
        /// <param name="objectType">The component <see cref="Type"/>.</param>
        /// ----------------------------------------------------------------------------
        public void UpdateListBox(Type objectType)
        {
            ComponentList.Items.Clear();

            foreach (NovaCommon.Component thing in AllComponents.Values)
            {
                if (thing.GetType() == objectType)
                {
                    ComponentList.Items.Add(thing.Name);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Populate the list box wih all components of a specified type name (used when
        /// the actual type isn't known.
        /// </summary>
        /// <param name="objectName">The type name.</param>
        /// ----------------------------------------------------------------------------
        public void UpdateListBox(string objectName)
        {
            ComponentList.Items.Clear();

            foreach (NovaCommon.Component thing in AllComponents.Values)
            {
                if (thing.Type == objectName)
                {
                    ComponentList.Items.Add(thing.Name);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Delete the currently selected component.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public void DeleteComponent()
        {
            string componentName = ComponentName.Text;

            if (componentName == null || componentName == "")
            {
                Report.Error("You must select a component to delete");
                return;
            }

            AllComponents.Remove(componentName);
            ComponentList.Items.Remove(componentName);

            if (ComponentList.Items.Count > 0)
            {
                ComponentList.SelectedIndex = 0;
            }
        }


        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get and set the properties common to all components.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public NovaCommon.Component Value
        {
            get
            {
                NovaCommon.Component component = new NovaCommon.Component();

                component.ComponentImage = ComponentImage.Image;
                component.Cost           = BasicProperties.Cost;
                component.Description    = Description.Text;
                component.Mass           = BasicProperties.Mass;
                component.Name           = ComponentName.Text;
                component.RequiredTech   = TechRequirements.Value;

                return component;
            }

            set
            {
                BasicProperties.Cost    = value.Cost;
                BasicProperties.Mass    = value.Mass;
                ComponentImage.Image    = value.ComponentImage;
                ComponentName.Text      = value.Name;
                Description.Text        = value.Description;
                TechRequirements.Value  = value.RequiredTech;
            }
        }
        #endregion

    }//CommonProperties

}//namespace

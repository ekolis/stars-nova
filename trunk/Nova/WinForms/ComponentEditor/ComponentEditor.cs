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
// This dialog allows all components to be created and edited.
// ===========================================================================
#endregion

#region New Property Checklist
// Checklist for Adding a New Component Property 
//
// 1. Add a new tab to ComponentEditor.cs [Design]
// 2. Add a Property->New <name> to the menu bar and set its click event to MenuItem_AddProperty
// 3. Create a new derived class from ComponentProperty (or use IntegerProperty/DoubleProperty for int/double values).
// 4. Add the property to the constructor for Component.cs
// 5. In ComponentEditor.SaveComponent(), copy tab data to Component.Property.
// 6. In componentEditor.SelectedIndexChanged(), update the tab from the component.
// 7. Update ComponentEditor.MenuItem_AddProperty(), to initialise the property tab.
// 8. Add the new property to Component.propertyKeys
// 9. Modify components.(xml/dat) save file to ensure correct loading of properties that are changed.
// 10. Add to ShipDesign.SumProperty()
#endregion
// ============================================================================
#region Bugs
// == BUGS (FIXME priority 3) ==
// Component->Copy seems to intermitently not copy race restrictions.
// Component->Copy cross-links Hull maps (i.e. the one map is used by both 
//   hulls). Suspect it is the individual modules that are x-linked not the 
//   whole map. May be doing something similar with race restrictions.
//   - These both seem to have a related cause. The work around is not to use 
//     Component->Copy, or to save and re-open the file after copying a component 
//     to make the hull map and race restrictions safe to modify.
//
//  == FEATURES (TODO priority 3) ==
// Healing property of fuel X-ports? - No, as it depends on other factors such as 
// movement. Will include in the game engine code.
// Add buttons to sort components alphabetically or by tech level (or perhaps by 
// an id number to put in a more Stars! like order?)
//
// == COSMETICS (TODO priority 2) ==
// Add Keyboard Shortcuts
//  - Up/Down key to navigate Component List
// Display dock/cargo capacity in hull map.
#endregion
// ============================================================================

#region Using Statements
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using Nova.Common;
using Nova.Common.Components;

#endregion

namespace Nova.WinForms.ComponentEditor
{
   public partial class ComponentEditorWindow : Form
   {
       // Keep track of when to save.
       private static bool fileDirty;
       private static bool componentDirty;

       // Are we browsing or edditing?
       private static bool editMode;

       // Keep a copy of the race restrictions.
       private static RaceRestriction restrictions;

       // Keep a copy of the hull map, for ships.
       private static ArrayList hullMap = new ArrayList();

       // ============================================================================
       //
       //                       Setup
       //
       // ============================================================================
       #region Setup

       /// <Summary>
       /// Initializes a new instance of the ComponentEditorWindow class.
       /// </Summary>
       public ComponentEditorWindow()
       {
           InitializeComponent();
       }


       /// <Summary>
       /// When the program starts, restore the component data. If the path to the
       /// component data file has not been set then set it now.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void OnLoad(object sender, EventArgs e)
       {
           // TODO (priority 3): without this an exception is raised when trying to launch a dialog 
           // from the non UI thread used to load the component definition file. The 
           // exception is caught and the program continues but no component images 
           // will be displayed. - dan_vale 28 Dec 09
           string temp = AllComponents.Graphics; // force program to ask for the graphics path if not already defined.

           // Tidy up the tab control:
           // In case I forget to shrink it to fit when adding controls.
           this.propertyTabs.Width = 479; 
           // Start showing no property tabs, until some property is loaded.
           this.propertyTabs.TabPages.Clear();

       }

       #endregion Setup


       #region Core Functions




       #endregion

       // ============================================================================
       //
       //                                  Menu Buttons
       //  File         Component            Property         About
       //   +-Open       +-Save               +-Add            +-About
       //   +-New        +-Delete             +-Delete
       //   +-Save       +-New
       //   +-Save As    +-Edit
       //   +-Exit       +-Copy
       //                +-Discard Changes
       //                +-Race Restrictions
       //
       // ============================================================================
       #region Menu Buttons

       #region File Menu

       /// <Summary>
       /// Menu->File->Open
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
       {
           // Save the current work, if any.
           if (fileDirty)
           {
               SaveComponent();
               AllComponents.Save();
           }
           OpenFileDialog fd = new OpenFileDialog();
           fd.Title = "Open component definition file";

           DialogResult result = fd.ShowDialog();

           if (result == DialogResult.OK)
           {
               // store the new component definition file name in the nova.conf
               using (Config conf = new Config())
               {
                   conf[Global.ComponentFileName] = fd.FileName;
               }
               
               try
               {
                   AllComponents.Restore();
                   this.propertyTabs.TabPages.Clear();

                   // start with something showing
                   this.componentType.Text = "Armor";
                   UpdateListBox("Armor");
                   if (this.componentList.Items.Count > 0)
                       this.componentList.SelectedIndex = 0; // pick the first Item in the list
                   fileDirty = false;
                   componentDirty = false;
                   EditModeOff();
                   UpdateTitleBar();
               }
               catch
               {
                   Report.Error("There was an error loading the component definition file. Current component definitions have been cleared.");
                   // If there were any problems restoring the components we may have a partial list of components
                   AllComponents.Data.Components.Clear();
                   this.propertyTabs.TabPages.Clear();
                   fileDirty = false;
                   componentDirty = false;
                   EditModeOff();
                   UpdateTitleBar();
                   this.componentList.Items.Clear();
               }

           }

       }


       /// <Summary>
       /// <para>Menu->File->New</para>
       /// Create a new, empty component definition file.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_NewFile_Click(object sender, EventArgs e)
       {
           // clear the in memory component list
           AllComponents.MakeNew();

           // setup for editing
           EditModeOn();
           fileDirty = true;
           componentDirty = true;

           // Tidy up the UI
           this.componentName.Text = "";
           this.description.Text = "";
           this.basicProperties.Cost = new Nova.Common.Resources(0, 0, 0, 0);
           this.basicProperties.Mass = 0;
           this.componentImage.Image = null;
           this.techRequirements.Value = new TechLevel(0);
           this.componentList.Items.Clear();
           this.propertyTabs.TabPages.Clear();
           hullMap = new ArrayList();

           UpdateTitleBar();
       }


       /// <Summary>
       /// Menu->File->Save
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_SaveFile_Click(object sender, EventArgs e)
       {
           if (componentDirty) SaveComponent();
           AllComponents.Save();
           fileDirty = false;
           EditModeOff();
           UpdateTitleBar();
       }


       /// <Summary>
       /// Menu->File->Save As
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_SaveFileAs_Click(object sender, EventArgs e)
       {
           if (componentDirty) SaveComponent();
           SaveFileDialog fd = new SaveFileDialog();
           fd.Title = "Save component definition file";
           fd.Filter = "dat files (*.dat)|*.dat|xml files (*.xml)|*.xml|compressed xml (*.xml.gz)|*.xml.gz|All files (*.*)|*.*";
           fd.FilterIndex = 3;
           fd.RestoreDirectory = true;

           DialogResult result = fd.ShowDialog();

           // only save if the user says OK!
           if (result == DialogResult.OK)
           {

               System.IO.FileInfo info = new System.IO.FileInfo(fd.FileName);

               // FIXME (priority 3) - Create the selected file, if it doesn't already exist. This is a workaround for the FIXME below.
               if (!info.Exists)
               {
                   System.IO.FileStream saveFile = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create);
                   saveFile.Close();
               }

               if (result == DialogResult.OK && fd.FileName != null)
               {
                   // FIXME (priority 3) - somehow the following line does not work! 
                   // The FileName gets stored and then imediately returned to what it was before? 
                   // Works only if the file selected already exists.
                   // See the workaround above which creates the file first.
                   using (Config conf = new Config())
                   {
                       conf[Global.ComponentFileName] = fd.FileName;
                   }

                   AllComponents.Save();
               }
               EditModeOff();
               UpdateTitleBar();
               // fd.Dispose();
           }
       }


       /// <Summary>
       /// <para>Menu-File-Exit</para>
       /// Exit button press. Close the program only if data is saved.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void ExitButton_Click(object sender, EventArgs e)
       {
           if (componentDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
                   SaveComponent();
           }
           else
           {
               Report.Debug("Component not dirty.");
           }
           if (fileDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component definition file?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
               {
                   if (AllComponents.Save())
                   {
                       Close(); // close after saving
                   }
                   else
                   {
                       Report.Error("Failed to save data - exit aborted.");
                       return; // don't close, didn't save.
                   }
               }
               else
               {
                   Close(); // without saving
               }
           }
           else
           {
               Report.Debug("File not dirty.");
               Close(); // no need to save
           }
       }

       #endregion File Menu

       #region Component Menu

       /// <Summary>
       /// Menu->Component->Save
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void SaveComponent_Click(object sender, EventArgs e)
       {
           SaveComponent();
       }


       /// <Summary>
       /// <para>Menu->Component->Delete</para>
       /// Deletes the currently selected component.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_DeleteComponent_Click(object sender, EventArgs e)
       {
           DeleteComponent();
           EditModeOff();
           UpdateListBox(this.componentType.Text);
       }


       /// <Summary><para>
       /// Menu->Component->New
       /// </para><para>
       /// Create a new component.
       /// </para> </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_NewComponent_Click(object sender, EventArgs e)
       {
           if (componentDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
                   SaveComponent();
           }


           ClearForm(); // To create a new component we just need to clear the form and set up default values.

           EditModeOn();
           this.componentName.Text = "New Component";
           this.componentName.Focus();
           this.componentName.SelectAll();
       }


       /// <Summary>
       /// <para>Menu->Component->Edit</para>
       /// This is one way of entering 'edit' mode from 'browsing' mode. In edit
       /// mode changing the component type changes the current component's type.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_EditComponent_Click(object sender, EventArgs e)
       {
           EditModeOn();
       }


       /// <Summary><para>
       /// Menu->Component->Copy
       /// </para><para>
       /// Take a copy of a component to use for a template for a new component.
       /// </para></Summary>
       private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
       {
           EditModeOn();
           this.componentName.Text = "New Component";
           this.componentName.Focus();
           this.componentName.SelectAll();

       }


       /// <Summary>
       /// <para>Menu->Component->Discard Changes</para>
       /// Discardes any changes to the currently selected compoenent by refreshing 
       /// the UI from the in memory components.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_DiscardComponentChanges_Click(object sender, EventArgs e)
       {
           UpdateListBox(this.componentType.Text);
           EditModeOff();
       }


       /// <Summary>
       /// <para>Menu->Component->Race Restrictions</para>
       /// Open the race restrictions dialog to edit what LRT or PRT restrictions
       /// apply to this component.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_RaceRestrictions_Click(object sender, EventArgs e)
       {
           EditModeOn();
           RaceRestrictionDialog dialog = new RaceRestrictionDialog(this.componentName.Text, (Bitmap)this.componentImage.Image, restrictions);
           dialog.ShowDialog();
           restrictions = new RaceRestriction(dialog.Restrictions);
           componentDirty = true;

           this.restrictionSummary.Text = restrictions.ToString();
           dialog.Dispose();

       }

       #endregion Component Menu

       #region Property Menu

       /// <Summary>
       /// <para>
       /// Menu->Property->Add
       /// </para><para>
       /// Add the tab for the appropriate property, reset the values on the tab 
       /// and give it focus.
       /// </para>
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void MenuItem_AddProperty(object sender, EventArgs e)
       {
           ToolStripMenuItem menuSelection = sender as ToolStripMenuItem;

           if (menuSelection == armorToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabArmor);
               this.propertyTabs.SelectedTab = tabArmor;

               this.armor.Value = 0;
           }
           else if (menuSelection == beamDeflectorToolStripMenuItem1)
           {
               this.propertyTabs.TabPages.Add(tabDeflector);
               this.propertyTabs.SelectedTab = tabDeflector;

               this.beamDeflector.Value = 0;
           }
           else if (menuSelection == battleMovementToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabMovement);
               this.propertyTabs.SelectedTab = tabMovement;

               this.battleMovement.Value = 0;

           }
           else if (menuSelection == bombToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabBomb);
               this.propertyTabs.SelectedTab = tabBomb;

               this.populationKill.Value = 0;
               this.minimumPopKill.Value = 0;
               this.installationsDestroyed.Value = 0;
               this.smartBomb.Checked = false;

           }
           else if (menuSelection == capacitorToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabCapacitor);
               this.propertyTabs.SelectedTab = tabCapacitor;

               this.beamDamage.Value = 0;

           }
           else if (menuSelection == cargoToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabCargo);
               this.propertyTabs.SelectedTab = tabCargo;

               this.cargoCapacity.Value = 0;

           }
           else if (menuSelection == cloakToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabCloak);
               this.propertyTabs.SelectedTab = tabCloak;

               this.cloaking.Value = 0;

           }
           else if (menuSelection == colonizationToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabColonization);
               this.propertyTabs.SelectedTab = tabColonization;

               this.colonizationModule.Checked = true;

           }
           else if (menuSelection == computerToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabComputer);
               this.propertyTabs.SelectedTab = tabComputer;

               this.accuracy.Value = 0;
               this.initiative.Value = 0;
           }
           else if (menuSelection == this.defenseToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabDefense);
               this.propertyTabs.SelectedTab = tabDefense;

               this.defenseCover1.Value = 0.00M;
               this.defenseCover40.Text = "0.00";
               this.defenseCover80.Text = "0.00";
               this.defenseCover100.Text = "0.00";
           }
           else if (menuSelection == energyDampenerToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabEnergyDampener);
               this.propertyTabs.SelectedTab = tabEnergyDampener;

               this.energyDampener.Value = 0.0M;
           }
           else if (menuSelection == engineToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabEngine);
               this.propertyTabs.SelectedTab = tabEngine;

               this.ramScoopCheckBox.Checked = false;
               this.engineFastestSafeSpeed.Value = 1;
               this.engineOptimalSpeed.Value = 1;

               this.warp1Fuel.Value = 0;
               this.warp2Fuel.Value = 0;
               this.warp3Fuel.Value = 0;
               this.warp4Fuel.Value = 0;
               this.warp5Fuel.Value = 0;
               this.warp6Fuel.Value = 0;
               this.warp7Fuel.Value = 0;
               this.warp8Fuel.Value = 0;
               this.warp9Fuel.Value = 0;
               this.warp10Fuel.Value = 0;

           }
           else if (menuSelection == fuelToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabFuel);
               this.propertyTabs.SelectedTab = tabFuel;

               this.fuelCapacity.Value = 0;
               this.fuelGeneration.Value = 0;

           }
           else if (menuSelection == gateToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabGate);
               this.propertyTabs.SelectedTab = tabGate;

               this.safeHullMass.Value = 0;
               this.safeRange.Value = 0;

           }
           else if (menuSelection == hullToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabHull);
               this.propertyTabs.SelectedTab = tabHull;

               hullMap = new ArrayList();
               this.hullArmor.Value = 0;
               this.hullInitiative.Value = 0;
               this.hullFuelCapacity.Value = 0;
               this.hullCargoCapacity.Value = 0;
               this.hullDockCapacity.Value = 0;
               this.alternateRealityMaxPop.Value = 0;


           }
           else if (menuSelection == hullAffinityToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabHullAffinity);
               this.propertyTabs.SelectedTab = tabHullAffinity;

               this.componentHullAffinity.Text = "";
               ComponentHullAffinity_PopulateList();
           }
           else if (menuSelection == jammerToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabJammer);
               this.propertyTabs.SelectedTab = tabJammer;

               this.deflection.Value = 0;

           }
           else if (menuSelection == massDriverToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabDriver);
               this.propertyTabs.SelectedTab = tabDriver;

               this.massDriverSpeed.Value = 0;

           }
           else if (menuSelection == mineLayerToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabMineLayer);
               this.propertyTabs.SelectedTab = tabMineLayer;

               this.mineLayingRate.Value = 0;
               this.mineSafeSpeed.Value = 0;
               this.mineDamagePerEngine.Value = 0;
               this.mineDamagePerRamScoop.Value = 0;
               this.mineMinFleetDamage.Value = 0;
               this.mineMinRamScoopDamage.Value = 0;
               this.mineHitChance.Value = 0.0M;

           }
           else if (menuSelection == layerEfficencyToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabLayerEfficiency);
               this.propertyTabs.SelectedTab = tabLayerEfficiency;

               this.mineLayerEfficiency.Value = 2.00M; // default to x2 
           }
           else if (menuSelection == miningRobotToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabRobot);
               this.propertyTabs.SelectedTab = tabRobot;

               this.miningRate.Value = 0;

           }
           else if (menuSelection == orbitalAdjusterToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabOrbitalAdjuster);
               this.propertyTabs.SelectedTab = tabOrbitalAdjuster;

               this.adjusterRate.Value = 0;

           }
           else if (menuSelection == radiationToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabRadiation);
               this.propertyTabs.SelectedTab = tabRadiation;

               this.radiation.Value = 0;
           }
           else if (menuSelection == scannerToolStripMenuItem || menuSelection == planetaryScannerToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabScanner);
               this.propertyTabs.SelectedTab = tabScanner;

               this.normalRange.Value = 0;
               this.penetratingRange.Value = 0;

           }
           else if (menuSelection == shieldToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabShield);
               this.propertyTabs.SelectedTab = tabShield;

               this.shield.Value = 0;

           }
           else if (menuSelection == tachyonDetectorToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabTachyonDetector);
               this.propertyTabs.SelectedTab = tabTachyonDetector;
               this.tachyonDetector.Value = 0;
           }
           else if (menuSelection == terraformingToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabTerraforming);
               this.propertyTabs.SelectedTab = tabTerraforming;

               this.gravityMod.Value = 0;
               this.temperatureMod.Value = 0;
               this.radiationMod.Value = 0;

           }
           else if (menuSelection == transportOnlyToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabTransportShipsOnly);
               this.propertyTabs.SelectedTab = tabTransportShipsOnly;
           }
           else if (menuSelection == weaponToolStripMenuItem)
           {
               this.propertyTabs.TabPages.Add(tabWeapon);
               this.propertyTabs.SelectedTab = tabWeapon;


               this.weaponPower.Value = 0;
               this.weaponRange.Value = 0;
               this.weaponInitiative.Value = 0;
               this.weaponAccuracy.Value = 0;

               isStandardBeam.Checked = true;

           }

           EditModeOn();
       } // add property


       /// <Summary>
       /// <para>Menu->Property->Delete Selected Property</para>
       /// Remove the selected property tab from the tab control, hidding it and
       /// letting us know not to add that property to the component.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void DeleteSelectedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
       {
           this.propertyTabs.TabPages.Remove(this.propertyTabs.SelectedTab);
           EditModeOn();
       }

       #endregion Property Menu

       #region About

       /// <Summary>
       /// Display the About box dialog
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void AboutMenuClick(object sender, EventArgs e)
       {
           DoDialog(new AboutBox());
       }

       #endregion About

       #endregion Menu Buttons



       // ============================================================================
       //
       //                       Event Methods
       //
       // ============================================================================
       #region Event Methods

       /// <Summary>
       /// When a selection in the list box changes repopulate the dialog with the
       /// values for that electrical component. The processing of this event is
       /// delegated from the Common Properties control.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void ComponentList_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (this.componentList.SelectedItem == null)
           {
               return;
           }

           string selectedComponentName = this.componentList.SelectedItem as string;

           Nova.Common.Components.Component selectedComponent = AllComponents.Data.Components[selectedComponentName] as Nova.Common.Components.Component;

           CommonProperties = selectedComponent;
           restrictions = selectedComponent.Restrictions;
           this.componentImage.ImageFile = selectedComponent.ImageFile;

           // Update all the property tabs with any properties of the selected component.
           try
           {
               this.propertyTabs.TabPages.Clear();                          // Start by clearing the current tabs.

               // check for the property
               if (selectedComponent.Properties.ContainsKey("Armor"))
               {
                   IntegerProperty armorProperty = selectedComponent.Properties["Armor"] as IntegerProperty; // get the property

                   this.armor.Value = (decimal)armorProperty.Value;         // set values on the tab

                   this.propertyTabs.TabPages.Add(tabArmor);                // make the tab vissible
                   this.propertyTabs.SelectedTab = tabArmor;                // and selected
               }
               if (selectedComponent.Properties.ContainsKey("Battle Movement"))
               {
                   DoubleProperty movementProperty = selectedComponent.Properties["Battle Movement"] as DoubleProperty;

                   this.battleMovement.Value = (decimal)movementProperty.Value;

                   this.propertyTabs.TabPages.Add(tabMovement);
                   this.propertyTabs.SelectedTab = tabMovement;
               }
               if (selectedComponent.Properties.ContainsKey("Beam Deflector"))
               {
                   ProbabilityProperty deflectorProperty = selectedComponent.Properties["Beam Deflector"] as ProbabilityProperty;

                   this.beamDeflector.Value = (decimal)deflectorProperty.Value;

                   this.propertyTabs.TabPages.Add(tabDeflector);
                   this.propertyTabs.SelectedTab = tabDeflector;
               }
               if (selectedComponent.Properties.ContainsKey("Bomb"))
               {
                   Bomb bombProperty = selectedComponent.Properties["Bomb"] as Bomb;

                   this.installationsDestroyed.Value = (decimal)bombProperty.Installations;
                   this.populationKill.Value = (decimal)bombProperty.PopKill;
                   this.minimumPopKill.Value = (decimal)bombProperty.MinimumKill;
                   this.smartBomb.Checked = bombProperty.IsSmart;

                   this.propertyTabs.TabPages.Add(tabBomb);
                   this.propertyTabs.SelectedTab = tabBomb;
               }
               if (selectedComponent.Properties.ContainsKey("Capacitor"))
               {
                   CapacitorProperty capacitorProperty = selectedComponent.Properties["Capacitor"] as CapacitorProperty;

                   this.beamDamage.Value = (decimal)capacitorProperty.Value;

                   this.propertyTabs.TabPages.Add(tabCapacitor);
                   this.propertyTabs.SelectedTab = tabCapacitor;
               }
               if (selectedComponent.Properties.ContainsKey("Cargo"))
               {
                   IntegerProperty cargoProperty = selectedComponent.Properties["Cargo"] as IntegerProperty;

                   this.cargoCapacity.Value = (decimal)cargoProperty.Value;

                   this.propertyTabs.TabPages.Add(tabCargo);
                   this.propertyTabs.SelectedTab = tabCargo;
               }
               if (selectedComponent.Properties.ContainsKey("Cloak"))
               {
                   ProbabilityProperty cloakProperty = selectedComponent.Properties["Cloak"] as ProbabilityProperty;

                   this.cloaking.Value = (decimal)cloakProperty.Value;

                   this.propertyTabs.TabPages.Add(tabCloak);
                   this.propertyTabs.SelectedTab = tabCloak;
               }
               if (selectedComponent.Properties.ContainsKey("Colonizer"))
               {
                   Colonizer colonizationProperty = selectedComponent.Properties["Colonizer"] as Colonizer;

                   if (colonizationProperty.Orbital)
                       this.orbitalColonizationModule.Checked = true;
                   else
                       this.colonizationModule.Checked = true;

                   this.propertyTabs.TabPages.Add(tabColonization);
                   this.propertyTabs.SelectedTab = tabColonization;

               }
               if (selectedComponent.Properties.ContainsKey("Computer"))
               {
                   Computer computerProperties = selectedComponent.Properties["Computer"] as Computer;

                   this.initiative.Value = (decimal)computerProperties.Initiative;
                   this.accuracy.Value = (decimal)computerProperties.Accuracy;

                   this.propertyTabs.TabPages.Add(tabComputer);
                   this.propertyTabs.SelectedTab = tabComputer;
               }
               if (selectedComponent.Properties.ContainsKey("Defense"))
               {
                   Defense defenseProperty = selectedComponent.Properties["Defense"] as Defense;

                   this.defenseCover1.Value = (decimal)defenseProperty.Value;
                   this.propertyTabs.TabPages.Add(tabDefense);
                   this.propertyTabs.SelectedTab = tabDefense;
               }
               if (selectedComponent.Properties.ContainsKey("Energy Dampener"))
               {
                   DoubleProperty dampenerProperty = selectedComponent.Properties["Energy Dampener"] as DoubleProperty;
                   this.energyDampener.Value = (decimal)dampenerProperty.Value;
                   this.propertyTabs.TabPages.Add(tabEnergyDampener);
                   this.propertyTabs.SelectedTab = tabEnergyDampener;
               }
               if (selectedComponent.Properties.ContainsKey("Engine"))
               {
                   Engine engineProperties = selectedComponent.Properties["Engine"] as Engine;

                   this.engineFastestSafeSpeed.Value = (decimal)engineProperties.FastestSafeSpeed;
                   this.engineOptimalSpeed.Value = (decimal)engineProperties.OptimalSpeed;
                   this.ramScoopCheckBox.Checked = engineProperties.RamScoop;

                   this.warp1Fuel.Value = (decimal)engineProperties.FuelConsumption[0];
                   this.warp2Fuel.Value = (decimal)engineProperties.FuelConsumption[1];
                   this.warp3Fuel.Value = (decimal)engineProperties.FuelConsumption[2];
                   this.warp4Fuel.Value = (decimal)engineProperties.FuelConsumption[3];
                   this.warp5Fuel.Value = (decimal)engineProperties.FuelConsumption[4];
                   this.warp6Fuel.Value = (decimal)engineProperties.FuelConsumption[5];
                   this.warp7Fuel.Value = (decimal)engineProperties.FuelConsumption[6];
                   this.warp8Fuel.Value = (decimal)engineProperties.FuelConsumption[7];
                   this.warp9Fuel.Value = (decimal)engineProperties.FuelConsumption[8];
                   this.warp10Fuel.Value = (decimal)engineProperties.FuelConsumption[9];

                   this.propertyTabs.TabPages.Add(tabEngine);
                   this.propertyTabs.SelectedTab = tabEngine;
               }

               if (selectedComponent.Properties.ContainsKey("Fuel"))
               {
                   Fuel fuelProperties = selectedComponent.Properties["Fuel"] as Fuel;

                   this.fuelCapacity.Value = (decimal)fuelProperties.Capacity;
                   this.fuelGeneration.Value = (decimal)fuelProperties.Generation;

                   this.propertyTabs.TabPages.Add(tabFuel);
                   this.propertyTabs.SelectedTab = tabFuel;
               }

               if (selectedComponent.Properties.ContainsKey("Gate"))
               {
                   Gate gateProperties = selectedComponent.Properties["Gate"] as Gate;

                   this.safeHullMass.Value = (decimal)gateProperties.SafeHullMass;
                   this.gateMassInfinite.Checked = this.safeHullMass.Value == -1;
                   this.safeRange.Value = (decimal)gateProperties.SafeRange;
                   this.gateRangeInfinite.Checked = this.safeRange.Value == -1;

                   this.propertyTabs.TabPages.Add(tabGate);
                   this.propertyTabs.SelectedTab = tabGate;
               }
               if (selectedComponent.Properties.ContainsKey("Hull"))
               {
                   Hull hullProperties = selectedComponent.Properties["Hull"] as Hull;

                   this.hullArmor.Value = (decimal)hullProperties.ArmorStrength;
                   this.hullFuelCapacity.Value = (decimal)hullProperties.FuelCapacity;
                   this.hullDockCapacity.Value = (decimal)hullProperties.DockCapacity;
                   this.infiniteDock.Checked = this.hullDockCapacity.Value == -1;
                   this.alternateRealityMaxPop.Value = (decimal)hullProperties.ARMaxPop;
                   this.hullCargoCapacity.Value = (decimal)hullProperties.BaseCargo;
                   this.hullInitiative.Value = (decimal)hullProperties.BattleInitiative;

                   // HullMap.HullGrid.ActiveModules = new ArrayList();
                   try
                   {
                       hullMap.Clear();
                       foreach (HullModule module in hullProperties.Modules)
                       {
                           hullMap.Add(module);
                       }
                   }
                   catch
                   {
                       // problem with the hull map; reset it.
                       Report.Error("Hull map error - resetting map.");
                       hullMap = new ArrayList();
                   }


                   this.propertyTabs.TabPages.Add(tabHull);
                   this.propertyTabs.SelectedTab = tabHull;
               }
               if (selectedComponent.Properties.ContainsKey("Hull Affinity"))
               {
                   HullAffinity hullAffinityProperty = selectedComponent.Properties["Hull Affinity"] as HullAffinity;
                   this.componentHullAffinity.Text = hullAffinityProperty.Value;

                   ComponentHullAffinity_PopulateList();

                   this.propertyTabs.TabPages.Add(tabHullAffinity);
                   this.propertyTabs.SelectedTab = tabHullAffinity;
               }

               if (selectedComponent.Properties.ContainsKey("Jammer"))
               {
                   ProbabilityProperty jammerProperty = selectedComponent.Properties["Jammer"] as ProbabilityProperty;

                   this.deflection.Value = (decimal)jammerProperty.Value;

                   this.propertyTabs.TabPages.Add(tabJammer);
                   this.propertyTabs.SelectedTab = tabJammer;
               }
               if (selectedComponent.Properties.ContainsKey("Mass Driver"))
               {
                   MassDriver driverProperty = selectedComponent.Properties["Mass Driver"] as MassDriver;

                   this.massDriverSpeed.Value = (decimal)driverProperty.Value;

                   this.propertyTabs.TabPages.Add(tabDriver);
                   this.propertyTabs.SelectedTab = tabDriver;
               }
               if (selectedComponent.Properties.ContainsKey("Mine Layer"))
               {
                   MineLayer mineLayerProperties = selectedComponent.Properties["Mine Layer"] as MineLayer;

                   this.mineLayingRate.Value = (decimal)mineLayerProperties.LayerRate;
                   this.mineSafeSpeed.Value = (decimal)mineLayerProperties.SafeSpeed;
                   this.mineHitChance.Value = (decimal)mineLayerProperties.HitChance;
                   this.mineDamagePerEngine.Value = (decimal)mineLayerProperties.DamagePerEngine;
                   this.mineDamagePerRamScoop.Value = (decimal)mineLayerProperties.DamagePerRamScoop;
                   this.mineMinFleetDamage.Value = (decimal)mineLayerProperties.MinFleetDamage;
                   this.mineMinRamScoopDamage.Value = (decimal)mineLayerProperties.MinRamScoopDamage;

                   this.propertyTabs.TabPages.Add(tabMineLayer);
                   this.propertyTabs.SelectedTab = tabMineLayer;
               }
               if (selectedComponent.Properties.ContainsKey("Mine Layer Efficiency"))
               {
                   DoubleProperty mineLayerProperties = selectedComponent.Properties["Mine Layer Efficiency"] as DoubleProperty;
                   this.mineLayerEfficiency.Value = (decimal)mineLayerProperties.Value;
                   this.propertyTabs.TabPages.Add(tabLayerEfficiency);
                   this.propertyTabs.SelectedTab = tabLayerEfficiency;
               }
               if (selectedComponent.Properties.ContainsKey("Mining Robot"))
               {
                   IntegerProperty robotProperty = selectedComponent.Properties["Mining Robot"] as IntegerProperty;

                   this.miningRate.Value = (decimal)robotProperty.Value;

                   this.propertyTabs.TabPages.Add(tabRobot);
                   this.propertyTabs.SelectedTab = tabRobot;
               }
               if (selectedComponent.Properties.ContainsKey("Orbital Adjuster"))
               {
                   IntegerProperty adjusterProperty = selectedComponent.Properties["Orbital Adjuster"] as IntegerProperty;

                   this.adjusterRate.Value = (decimal)adjusterProperty.Value;

                   this.propertyTabs.TabPages.Add(tabOrbitalAdjuster);
                   this.propertyTabs.SelectedTab = tabOrbitalAdjuster;
               }
               if (selectedComponent.Properties.ContainsKey("Radiation"))
               {
                   Radiation radiationProperty = selectedComponent.Properties["Radiation"] as Radiation;

                   this.radiation.Value = (decimal)radiationProperty.Value;

                   this.propertyTabs.TabPages.Add(tabRadiation);
                   this.propertyTabs.SelectedTab = tabRadiation;
               }

               if (selectedComponent.Properties.ContainsKey("Scanner"))
               {
                   Scanner scannerProperties = selectedComponent.Properties["Scanner"] as Scanner;

                   this.normalRange.Value = (decimal)scannerProperties.NormalScan;
                   this.penetratingRange.Value = (decimal)scannerProperties.PenetratingScan;

                   this.propertyTabs.TabPages.Add(tabScanner);
                   this.propertyTabs.SelectedTab = tabScanner;
               }
               if (selectedComponent.Properties.ContainsKey("Shield"))
               {
                   IntegerProperty shieldProperty = selectedComponent.Properties["Shield"] as IntegerProperty;

                   this.shield.Value = (decimal)shieldProperty.Value;

                   this.propertyTabs.TabPages.Add(tabShield);
                   this.propertyTabs.SelectedTab = tabShield;
               }
               if (selectedComponent.Properties.ContainsKey("Tachyon Detector"))
               {
                   ProbabilityProperty detectorProoperties = selectedComponent.Properties["Tachyon Detector"] as ProbabilityProperty;

                   this.tachyonDetector.Value = (decimal)detectorProoperties.Value;

                   this.propertyTabs.TabPages.Add(tabTachyonDetector);
                   this.propertyTabs.SelectedTab = tabTachyonDetector;
               }
               if (selectedComponent.Properties.ContainsKey("Terraform"))
               {
                   Terraform terraformProperties = selectedComponent.Properties["Terraform"] as Terraform;

                   this.gravityMod.Value = (decimal)terraformProperties.MaxModifiedGravity;
                   this.temperatureMod.Value = (decimal)terraformProperties.MaxModifiedTemperature;
                   this.radiationMod.Value = (decimal)terraformProperties.MaxModifiedRadiation;

                   this.propertyTabs.TabPages.Add(tabTerraforming);
                   this.propertyTabs.SelectedTab = tabTerraforming;
               }
               if (selectedComponent.Properties.ContainsKey("Transport Ships Only"))
               {
                   this.propertyTabs.TabPages.Add(tabTransportShipsOnly);
                   this.propertyTabs.SelectedTab = tabTransportShipsOnly;
               }
               if (selectedComponent.Properties.ContainsKey("Weapon"))
               {
                   Weapon weaponProperties = selectedComponent.Properties["Weapon"] as Weapon;

                   this.weaponPower.Value = (decimal)weaponProperties.Power;
                   this.weaponRange.Value = (decimal)weaponProperties.Range;
                   this.weaponInitiative.Value = (decimal)weaponProperties.Initiative;
                   this.weaponAccuracy.Value = (decimal)weaponProperties.Accuracy;
                   switch (weaponProperties.Group)
                   {
                       case WeaponType.standardBeam:
                           isStandardBeam.Checked = true;
                           break;
                       case WeaponType.shieldSapper:
                           isSapper.Checked = true;
                           break;
                       case WeaponType.gatlingGun:
                           isGattling.Checked = true;
                           break;
                       case WeaponType.torpedo:
                           isTorpedo.Checked = true;
                           break;
                       case WeaponType.missile:
                           isMissile.Checked = true;
                           break;
                       default:
                           isStandardBeam.Checked = true;
                           break;

                   }

                   this.propertyTabs.TabPages.Add(tabWeapon);
                   this.propertyTabs.SelectedTab = tabWeapon;
               }

           }
           catch
           {
               MessageBox.Show("Error accessing component properties.");
           }

       }


       /// <Summary>
       /// Clicking in the component type box brings up a 'drop down' menu to 
       /// select from.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void ComponentType_Changed(object sender, EventArgs e)
       {
           // in edit mode, just change the component type. In view mode, repopulate the component list.
           if (!editMode)
           {
               UpdateListBox(this.componentType.Text);
               if (this.componentList.Items.Count > 0)
               {
                   this.componentList.SelectedIndex = 0; // pick the first Item in the list
               }
               else
               {
                   // Blank the component information
                   this.propertyTabs.TabPages.Clear();
                   this.basicProperties.Cost = new Nova.Common.Resources(0, 0, 0, 0);
                   this.basicProperties.Mass = 0;
                   this.componentImage.Image = null;
                   this.componentName.Text = "";
                   this.description.Text = "";
                   this.techRequirements.Value = new TechLevel(0);
               }

           }

       }


       /// <Summary>
       /// Draw tabs horizontally to the right of the tab control.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void PropertyTabs_DrawItem(object sender, DrawItemEventArgs e)
       {
           Graphics g = e.Graphics;

           // Get the Item from the collection.
           TabPage tabPage = this.propertyTabs.TabPages[e.Index];

           // Make the tab background 'Control' grey. 
           // Everytime I change the colection they are reset, so easiest to do it programatically here.
           tabPage.BackColor = Color.FromKnownColor(KnownColor.Control);

           // Get the real bounds for the tab rectangle.
           Rectangle tabBounds = this.propertyTabs.GetTabRect(e.Index);

           Brush textBrush = new SolidBrush(Color.Black);

           // Use our own font. Because we CAN.
           Font tabFont = new Font("Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);

           // Draw string. Center the text.
           StringFormat stringFlags = new StringFormat();
           stringFlags.Alignment = StringAlignment.Center;
           stringFlags.LineAlignment = StringAlignment.Center;
           
           g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, new StringFormat(stringFlags));
           
       }


       /// <Summary>
       /// When the 'Edit Hull' button is clicked, pop up the <see cref='HullDialog'/>.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void ButtonEditHull_Click(object sender, EventArgs e)
       {
           // create a new hull grid dialog
           HullDialog dialog = new HullDialog();

           // and populate it
           dialog.HullGrid.ActiveModules = hullMap;

           // do the dialog
           dialog.ShowDialog();

           // copy the new hull map
           hullMap.Clear();
           hullMap = dialog.HullGrid.ActiveModules;

           dialog.Dispose();

       }


       /// <Summary>
       /// When the program is terminated, save the component data. This function is
       /// invoked no matter which method is used to terminate the program.
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       private void OnFormClosing(object sender, FormClosingEventArgs e)
       {
           if (componentDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
                   SaveComponent();
           }
#if (DEBUG)
           else
           {
               MessageBox.Show("Component not dirty.");
           }
#endif
           if (fileDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component definition file?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
               {
                   if (!AllComponents.Save())
                   {
                       MessageBox.Show("Failed to save data.");
                   }
               }
           }
           else
           {
#if (DEBUG)
               MessageBox.Show("File not dirty.");
#endif
           }
       }


       #endregion Event Methods


       // ============================================================================
       //
       //                            Utility Functions
       //
       // ============================================================================
       #region Utility


       /// <Summary>
       /// Save the currently selected component.
       /// </Summary>
       private void SaveComponent()
       {
           string componentName = this.componentName.Text;

           if (componentName == null || componentName == "")
           {
               Report.Error("You must specify a component name");
               return;
           }
           if (this.componentType.Text == null || this.componentType.Text == "")
           {
               Report.Error("You must specify a component type");
               return;
           }

           Nova.Common.Components.Component newComponent = new Nova.Common.Components.Component(CommonProperties);
           newComponent.Type = this.componentType.Text;
           newComponent.Restrictions = new RaceRestriction(restrictions);
           newComponent.ImageFile = this.componentImage.ImageFile;

           if (this.propertyTabs.Contains(tabArmor))
           {
               IntegerProperty armorProperty = new IntegerProperty();
               armorProperty.Value = (int)this.armor.Value;
               newComponent.Properties.Add("Armor", armorProperty);

           }
           if (this.propertyTabs.Contains(tabMovement))
           {
               DoubleProperty movementProperties = new DoubleProperty();
               movementProperties.Value = (double)this.battleMovement.Value;
               newComponent.Properties.Add("Battle Movement", movementProperties);
           }
           if (this.propertyTabs.Contains(tabDeflector))
           {
               ProbabilityProperty deflectorProperty = new ProbabilityProperty();
               deflectorProperty.Value = (double)this.beamDeflector.Value;
               newComponent.Properties.Add("Beam Deflector", deflectorProperty);
           }
           if (this.propertyTabs.Contains(tabBomb))
           {
               Bomb bombProperty = new Bomb();
               bombProperty.Installations = (int)this.installationsDestroyed.Value;
               bombProperty.PopKill = (double)this.populationKill.Value;
               bombProperty.MinimumKill = (int)this.minimumPopKill.Value;
               bombProperty.IsSmart = this.smartBomb.Checked;
               newComponent.Properties.Add("Bomb", bombProperty);
           }
           if (this.propertyTabs.Contains(tabCapacitor))
           {
               CapacitorProperty capacitorProperty = new CapacitorProperty();
               capacitorProperty.Value = (int)this.beamDamage.Value;
               newComponent.Properties.Add("Capacitor", capacitorProperty);
           }
           if (this.propertyTabs.Contains(tabCargo))
           {
               IntegerProperty cargoProperty = new IntegerProperty();
               cargoProperty.Value = (int)this.cargoCapacity.Value;
               newComponent.Properties.Add("Cargo", cargoProperty);
           }
           if (this.propertyTabs.Contains(tabCloak))
           {
               ProbabilityProperty cloakProperty = new ProbabilityProperty();
               cloakProperty.Value = (int)this.cloaking.Value;
               newComponent.Properties.Add("Cloak", cloakProperty);
           }
           if (this.propertyTabs.Contains(tabColonization))
           {
               Colonizer colonizationProperty = new Colonizer();
               colonizationProperty.Orbital = this.orbitalColonizationModule.Checked;
               newComponent.Properties.Add("Colonizer", colonizationProperty);
           }
           if (this.propertyTabs.Contains(tabComputer))
           {
               Computer computerProperties = new Computer();
               computerProperties.Initiative = (int)this.initiative.Value;
               computerProperties.Accuracy = (int)this.accuracy.Value;
               newComponent.Properties.Add("Computer", computerProperties);
           }
           if (this.propertyTabs.Contains(tabDefense))
           {
               Defense defenseProperties = new Defense();
               defenseProperties.Value = (double)this.defenseCover1.Value;
               newComponent.Properties.Add("Defense", defenseProperties);
           }
           if (this.propertyTabs.Contains(tabEnergyDampener))
           {
               DoubleProperty dampenerProperties = new DoubleProperty();
               dampenerProperties.Value = (double)this.energyDampener.Value;
               newComponent.Properties.Add("Energy Dampener", dampenerProperties);
           }
           if (this.propertyTabs.Contains(tabEngine))
           {
               Engine engineProperties = new Engine();
               engineProperties.RamScoop = this.ramScoopCheckBox.Checked;
               engineProperties.FastestSafeSpeed = (int)this.engineFastestSafeSpeed.Value;
               engineProperties.OptimalSpeed = (int)this.engineOptimalSpeed.Value;
               engineProperties.FuelConsumption[0] = (int)this.warp1Fuel.Value;
               engineProperties.FuelConsumption[1] = (int)this.warp2Fuel.Value;
               engineProperties.FuelConsumption[2] = (int)this.warp3Fuel.Value;
               engineProperties.FuelConsumption[3] = (int)this.warp4Fuel.Value;
               engineProperties.FuelConsumption[4] = (int)this.warp5Fuel.Value;
               engineProperties.FuelConsumption[5] = (int)this.warp6Fuel.Value;
               engineProperties.FuelConsumption[6] = (int)this.warp7Fuel.Value;
               engineProperties.FuelConsumption[7] = (int)this.warp8Fuel.Value;
               engineProperties.FuelConsumption[8] = (int)this.warp9Fuel.Value;
               engineProperties.FuelConsumption[9] = (int)this.warp10Fuel.Value;
               newComponent.Properties.Add("Engine", engineProperties);

           }
           if (this.propertyTabs.Contains(tabFuel))
           {
               Fuel fuelProperty = new Fuel();
               fuelProperty.Capacity = (int)this.fuelCapacity.Value;
               fuelProperty.Generation = (int)this.fuelGeneration.Value;
               newComponent.Properties.Add("Fuel", fuelProperty);
           }
           if (this.propertyTabs.Contains(tabGate))
           {
               Gate gateProperties = new Gate();
               gateProperties.SafeHullMass = (int)this.safeHullMass.Value;
               gateProperties.SafeRange = (int)this.safeRange.Value;
               newComponent.Properties.Add("Gate", gateProperties);
           }
           if (this.propertyTabs.Contains(tabHull))
           {
               Hull hullProperties = new Hull();
               hullProperties.ArmorStrength = (int)this.hullArmor.Value;
               hullProperties.FuelCapacity = (int)this.hullFuelCapacity.Value;
               hullProperties.DockCapacity = (int)this.hullDockCapacity.Value;
               hullProperties.ARMaxPop = (int)this.alternateRealityMaxPop.Value;
               hullProperties.BaseCargo = (int)this.hullCargoCapacity.Value;
               hullProperties.BattleInitiative = (int)this.hullInitiative.Value;

               hullProperties.Modules = new ArrayList();
               foreach (HullModule module in hullMap)
               {
                   HullModule newModule = new HullModule(module);
                   hullProperties.Modules.Add(newModule);
               }

               newComponent.Properties.Add("Hull", hullProperties);
           }
           if (this.propertyTabs.Contains(tabHullAffinity))
           {
               HullAffinity hullAffinityProperty = new HullAffinity();
               hullAffinityProperty.Value = this.componentHullAffinity.Text;
               newComponent.Properties.Add("Hull Affinity", hullAffinityProperty);
           }
           if (this.propertyTabs.Contains(tabJammer))
           {
               ProbabilityProperty jammerProperty = new ProbabilityProperty();
               jammerProperty.Value = (int)this.deflection.Value;
               newComponent.Properties.Add("Jammer", jammerProperty);
           }
           if (this.propertyTabs.Contains(tabDriver))
           {
               MassDriver driverProperty = new MassDriver();
               driverProperty.Value = (int)this.massDriverSpeed.Value;
               newComponent.Properties.Add("Mass Driver", driverProperty);
           }
           if (this.propertyTabs.Contains(tabMineLayer))
           {
               MineLayer mineLayerProperty = new MineLayer();
               mineLayerProperty.LayerRate = (int)this.mineLayingRate.Value;
               mineLayerProperty.SafeSpeed = (int)this.mineSafeSpeed.Value;
               mineLayerProperty.HitChance = (double)this.mineHitChance.Value;
               mineLayerProperty.DamagePerEngine = (int)this.mineDamagePerEngine.Value;
               mineLayerProperty.DamagePerRamScoop = (int)this.mineDamagePerRamScoop.Value;
               mineLayerProperty.MinFleetDamage = (int)this.mineMinFleetDamage.Value;
               mineLayerProperty.MinRamScoopDamage = (int)this.mineMinRamScoopDamage.Value;
               newComponent.Properties.Add("Mine Layer", mineLayerProperty);
           }
           if (this.propertyTabs.Contains(tabLayerEfficiency))
           {
               DoubleProperty layerEfficiencyProperty = new DoubleProperty();
               layerEfficiencyProperty.Value = (double)this.mineLayerEfficiency.Value;
               newComponent.Properties.Add("Mine Layer Efficiency", layerEfficiencyProperty);
           }
           if (this.propertyTabs.Contains(tabRobot))
           {
               IntegerProperty robotProperty = new IntegerProperty();
               robotProperty.Value = (int)this.miningRate.Value;
               newComponent.Properties.Add("Mining Robot", robotProperty);
           }
           if (this.propertyTabs.Contains(tabOrbitalAdjuster))
           {
               IntegerProperty adjusterProperty = new IntegerProperty();
               adjusterProperty.Value = (int)this.adjusterRate.Value;
               newComponent.Properties.Add("Orbital Adjuster", adjusterProperty);
           }
           if (this.propertyTabs.Contains(tabRadiation))
           {
               Radiation radiationProperty = new Radiation();
               radiationProperty.Value = (double)this.radiation.Value;
               newComponent.Properties.Add("Radiation", radiationProperty);
           }
           if (this.propertyTabs.Contains(tabScanner))
           {
               Scanner scannerProperty = new Scanner();
               scannerProperty.NormalScan = (int)this.normalRange.Value;
               scannerProperty.PenetratingScan = (int)this.penetratingRange.Value;
               newComponent.Properties.Add("Scanner", scannerProperty);
           }
           if (this.propertyTabs.Contains(tabShield))
           {
               IntegerProperty shieldProperty = new IntegerProperty();
               shieldProperty.Value = (int)this.shield.Value;
               newComponent.Properties.Add("Shield", shieldProperty);
           }
           if (this.propertyTabs.Contains(tabTachyonDetector))
           {
               ProbabilityProperty detectorProperty = new ProbabilityProperty();
               detectorProperty.Value = (double)this.tachyonDetector.Value;
               newComponent.Properties.Add("Tachyon Detector", detectorProperty);
           }
           if (this.propertyTabs.Contains(tabTerraforming))
           {
               Terraform terraformProperty = new Terraform();
               terraformProperty.MaxModifiedGravity = (int)this.gravityMod.Value;
               terraformProperty.MaxModifiedTemperature = (int)this.temperatureMod.Value;
               terraformProperty.MaxModifiedRadiation = (int)this.radiationMod.Value;
               newComponent.Properties.Add("Terraform", terraformProperty);
           }
           if (this.propertyTabs.Contains(tabTransportShipsOnly))
           {
               SimpleProperty transportProperty = new SimpleProperty();
               newComponent.Properties.Add("Transport Ships Only", transportProperty);
           }
           if (this.propertyTabs.Contains(tabWeapon))
           {
               Weapon weaponProperty = new Weapon();

               weaponProperty.Power = (int)this.weaponPower.Value;
               weaponProperty.Range = (int)this.weaponRange.Value;
               weaponProperty.Initiative = (int)this.weaponInitiative.Value;
               weaponProperty.Accuracy = (int)this.weaponAccuracy.Value;
               if (isStandardBeam.Checked) weaponProperty.Group = WeaponType.standardBeam;
               else if (isSapper.Checked) weaponProperty.Group = WeaponType.shieldSapper;
               else if (isGattling.Checked) weaponProperty.Group = WeaponType.gatlingGun;
               else if (isTorpedo.Checked) weaponProperty.Group = WeaponType.torpedo;
               else if (isMissile.Checked) weaponProperty.Group = WeaponType.missile;
               else weaponProperty.Group = WeaponType.standardBeam;

               newComponent.Properties.Add("Weapon", weaponProperty);
           }

           Nova.Common.Components.AllComponents.Data.Components[newComponent.Name] = newComponent;

           EditModeOff();
           fileDirty = true;
           componentDirty = false;

           SelectComponent(this.componentType.Text, newComponent.Name);

           Report.Information("Component design has been saved.");
       }


       /// <Summary>
       /// Update the title with key information.
       /// </Summary>
       private void UpdateTitleBar()
       {
           using (Config conf = new Config())
           {
               Text = "Nova Component Editor - ";
               if (!String.IsNullOrEmpty(conf[Global.ComponentFileName]))
               {
                   Text += conf[Global.ComponentFileName];
               }
               else
               {
                   Text += "New Component Definintions";
               }

               if (editMode)
               {
                   Text += " - Edit Mode";
               }
               else
               {
                   Text += " - Browsing Mode";
               }
           }
       }


       /// <Summary>
       /// Update the list box of components base on the selected <see cref="Type"/>
       /// </Summary>
       /// <param name="componentTypeSelected">Type of component.</param>
       public void UpdateListBox(string componentTypeSelected)
       {
           this.componentList.Items.Clear();

           foreach (Component thing in AllComponents.Data.Components.Values)
           {
               if (thing.Type == componentTypeSelected)
               {
                   this.componentList.Items.Add(thing.Name);
               }
           }
       }


       /// <Summary>
       /// Makes a given component the selected component in the Component Editor
       /// </Summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
       public void SelectComponent(string type, string name)
       {
           UpdateListBox(type);
           EditModeOff();

           // keep the current component selected
           for (int n = 0; n < this.componentList.Items.Count; n++)
           {
               if ((string)this.componentList.Items[n] == name)
               {
                   this.componentList.SelectedIndex = n;
                   break;
               }
           }
       }


       /// <Summary>
       /// Delete the currently selected component.
       /// </Summary>
       public void DeleteComponent()
       {
           string componentName = this.componentName.Text;

           if (componentName == null || componentName == "")
           {
               Report.Error("You must select a component to delete");
               return;
           }

           AllComponents.Data.Components.Remove(componentName);
           this.componentList.Items.Remove(componentName);

           if (this.componentList.Items.Count > 0)
           {
               this.componentList.SelectedIndex = 0;
           }

           fileDirty = true;
           componentDirty = false;
           EditModeOff();
       }


      /// <Summary><para>
      /// Enter component editing mode.
      /// </para><para>
      /// In this mode changing the component type modifies the currently selected 
      /// component.
      /// </para></Summary>
      private void EditModeOn()
      {
          editMode = true;
          this.componentName.ReadOnly = false;
          this.description.ReadOnly = false;
          // TODO (priority 2) - enable all component editing fields/menu items
          UpdateTitleBar();
      }


      /// <Summary><para>
      /// Enter component browsing mode. 
      /// </para><para>
      /// In this mode changing the component type updates the component list with 
      /// components of the new type.
      /// </para></Summary>
      private void EditModeOff()
      {
          editMode = false;
          this.componentName.ReadOnly = true;
          this.description.ReadOnly = true;
          // TODO (priority 2) - disable all component editing fields.
          UpdateTitleBar();

      }


      /// <Summary>
      /// General dialog handling
      /// </Summary>
      /// <param name="dialog">A dialog <see cref="Form"/></param>
      private void DoDialog(Form dialog)
      {
          dialog.ShowDialog();
          dialog.Dispose();
      }


      /// <Summary>
      /// Clear all the entry fields on the component editor and associated forms.
      /// </Summary>
      private void ClearForm()
      {
          this.propertyTabs.TabPages.Clear();
          this.basicProperties.Cost = new Nova.Common.Resources(0, 0, 0, 0);
          this.basicProperties.Mass = 0;
          this.componentImage.Image = null;
          this.componentName.Text = "";
          this.description.Text = "";
          this.restrictionSummary.Text = "";
          this.techRequirements.Value = new TechLevel(0);
          restrictions = new RaceRestriction();
          hullMap = new ArrayList();
      }


      /// <Summary>
      /// Update the list of Hulls so that only the hulls this Item may be fitted to can be selected. 
      /// (Hull affinity limits which hulls an Item may be fitted too.)
      /// </Summary>
      private void ComponentHullAffinity_PopulateList()
      {
          this.componentHullAffinity.Items.Clear();
          foreach (Nova.Common.Components.Component thing in Nova.Common.Components.AllComponents.Data.Components.Values)
          {
              if (thing.Type == "Hull")
              {
                  this.componentHullAffinity.Items.Add(thing.Name);
              }
          }
      }

      #endregion Utility



      // ============================================================================
      //
      //                            Properties
      // - CommonProperties
      //
      // ============================================================================
      #region Properties

      /// <Summary>
      /// Get and set the properties common to all components.
      /// </Summary>
      public Nova.Common.Components.Component CommonProperties
      {
          get
          {
              Nova.Common.Components.Component component = new Nova.Common.Components.Component();

              component.ComponentImage = this.componentImage.Image;
              component.Cost = this.basicProperties.Cost;
              component.Description = this.description.Text;
              component.Mass = this.basicProperties.Mass;
              component.Name = this.componentName.Text;
              component.RequiredTech = this.techRequirements.Value;

              return component;
          }

          set
          {
              this.basicProperties.Cost = value.Cost;
              this.basicProperties.Mass = value.Mass;
              this.componentImage.Image = value.ComponentImage;
              this.componentName.Text = value.Name;
              this.description.Text = value.Description;
              this.restrictionSummary.Text = value.Restrictions.ToString();

              this.techRequirements.Value = value.RequiredTech;
          }
      }

      #endregion


   }
}



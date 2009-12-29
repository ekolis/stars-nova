// This file needs -*- c++ -*- mode
// ============================================================================
// Nova. (c) 2008 Ken Reed
// Modified Daniel Vale 2009
//
// This dialog allows all components to be created and edited.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// ============================================================================
//             Checklist for Adding a New Component Property 
//
// 1. Add a new tab to ComponentEditor.cs [Design]
// 2. Add a Property->New <name> to the menu bar and set its click event to menuItem_AddProperty
// 3. Create a new derived class from ComponentProperty (or use IntegerProperty/DoubleProperty for int/double values).
// 4. Add the property to the constructor for Component.cs
// 5. In ComponentEditor.SaveComponent(), copy tab data to Component.Property.
// 6. In componentEditor.SelectedIndexChanged(), update the tab from the component.
// 7. Update ComponentEditor.menuItem_AddProperty(), to initialise the property tab.
// 8. Add the new property to Component.propertyKeys
// 9. Modify components.(xml/dat) save file to ensure correct loading of properties that are changed.
// 10. Add to ShipDesign.SumProperty()
//
// ============================================================================
//
//                     TODO (low priority)
//
//  == BUGS (FIXME) ==
// Component->Copy seems to intermitently not copy race restrictions.
// Component->Copy cross-links Hull maps (i.e. the one map is used by both 
//   hulls). Suspect it is the individual modules that are x-linked not the 
//   whole map. May be doing something similar with race restrictions.
//   - These both seem to have a related cause. The work around is not to use 
//     Component->Copy, or to save and re-open the file after copying a component 
//     to make the hull map and race restrictions safe to modify.
//
//  == FEATURES ==
// Healing property of fuel X-ports? - No, as it depends on other factors such as 
// movement. Will include in the game engine code.
// Add buttons to sort components alphabetically or by tech level (or perhaps by 
// an id number to put in a more Stars! like order?)
//
// == COSMETICS ==
// Add Keyboard Shortcuts
//  - Up/Down key to navigate Component List
// Order of hull property tab stops
// Display dock/cargo capacity in hull map.
// When program starts with no component definition file all component property tabs are displayed. They should be hidden.
//
// ============================================================================

using NovaCommon;
using System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;

using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;

namespace ComponentEditor
{

// ============================================================================
// Dialog for creating and editing components.
// ============================================================================

 
   public partial class ComponentEditorWindow : Form
   {
       

       // This shortcut clashes in this context, so we need to use the full form,
       // Left here for a reminder.
       // private Hashtable AllComponents = null;

       // Keep track of when to save.
       private static bool FileDirty = false;
       private static bool ComponentDirty = false;

       // Are we browsing or edditing?
       private static bool EditMode = false;

       // Keep a copy of the race restrictions.
       private static RaceRestriction Restrictions = null;

       // Keep a copy of the hull map, for ships.
       private static ArrayList HullMap = new ArrayList();

       #region Setup
       // ============================================================================
       //
       //                                 Setup
       //
       // ============================================================================

       // ----------------------------------------------------------------------------
       // Construction
       // ----------------------------------------------------------------------------
       public ComponentEditorWindow()
       {
           InitializeComponent();

           // This shortcut clashes in this context, so we need to use the full form
           // AllComponents = NovaCommon.AllComponents.Data.Components; 
       }

       // ----------------------------------------------------------------------------
       // When the program starts, restore the component data. If the path to the
       // component data file has not been set then set it now.
       // ----------------------------------------------------------------------------
       private void OnLoad(object sender, EventArgs e)
	   {
           // TODO: without this an exception is raised when trying to launch a dialog 
           // from the non UI thread used to load the component definition file. The 
           // exception is caught and the program continues but no component images 
           // will be displayed. - dan_vale 28 Dec 09
           String temp = AllComponents.Graphics; // force program to ask for the graphics path if not already defined.

	   }

       #endregion Setup


       // ============================================================================
       //
       //                       Core Functions
       // - SaveComponent
       // - OnFormClosing
       // 
       //
       // ============================================================================
       #region Core Functions

       // -------------------------------------------------------------------------      
       // Save the currently selected component.
       // -------------------------------------------------------------------------      
       private void SaveComponent()
       {
           string componentName = ComponentName.Text;

           if (componentName == null || componentName == "")
           {
               Report.Error("You must specify a component name");
               return;
           }
           if (ComponentType.Text == null || ComponentType.Text == "")
           {
               Report.Error("You must specify a component type");
               return;
           }

           NovaCommon.Component newComponent = new NovaCommon.Component(CommonProperties);
           newComponent.Type = ComponentType.Text;
           newComponent.Restrictions = new RaceRestriction(Restrictions);
           newComponent.ImageFile = ComponentImage.ImageFile;

           if (PropertyTabs.Contains(tabArmor))
           {
               IntegerProperty armorProperty = new IntegerProperty();
               armorProperty.Value = (int)Armor.Value;
               newComponent.Properties.Add("Armor", armorProperty);

           }
           if (PropertyTabs.Contains(tabMovement))
           {
               DoubleProperty movementProperties = new DoubleProperty();
               movementProperties.Value = (double)BattleMovement.Value;
               newComponent.Properties.Add("Battle Movement", movementProperties);
           }
           if (PropertyTabs.Contains(tabDeflector))
           {
               ProbabilityProperty deflectorProperty = new ProbabilityProperty();
               deflectorProperty.Value = (double)BeamDeflector.Value;
               newComponent.Properties.Add("Beam Deflector", deflectorProperty);
           }
           if (PropertyTabs.Contains(tabBomb))
           {
               Bomb bombProperty = new Bomb();
               bombProperty.Installations = (int)InstallationsDestroyed.Value;
               bombProperty.PopKill = (double)PopulationKill.Value;
               bombProperty.MinimumKill = (int)MinimumPopKill.Value;
               bombProperty.IsSmart = SmartBomb.Checked;
               newComponent.Properties.Add("Bomb", bombProperty);
           }
           if (PropertyTabs.Contains(tabCapacitor))
           {
               CapacitorProperty capacitorProperty = new CapacitorProperty();
               capacitorProperty.Value = (int)BeamDamage.Value;
               newComponent.Properties.Add("Capacitor", capacitorProperty);
           }
           if (PropertyTabs.Contains(tabCargo))
           {
               IntegerProperty cargoProperty = new IntegerProperty();
               cargoProperty.Value = (int)CargoCapacity.Value;
               newComponent.Properties.Add("Cargo", cargoProperty);
           }
           if (PropertyTabs.Contains(tabCloak))
           {
               ProbabilityProperty cloakProperty = new ProbabilityProperty();
               cloakProperty.Value = (int)Cloaking.Value;
               newComponent.Properties.Add("Cloak", cloakProperty);
           }
           if (PropertyTabs.Contains(tabColonization))
           {
               Colonizer colonizationProperty = new Colonizer();
               colonizationProperty.Orbital = OrbitalColonizationModule.Checked;
               newComponent.Properties.Add("Colonizer", colonizationProperty);
           }
           if (PropertyTabs.Contains(tabComputer))
           {
               Computer computerProperties = new Computer();
               computerProperties.Initiative = (int)Initiative.Value;
               computerProperties.Accuracy = (int)Accuracy.Value;
               newComponent.Properties.Add("Computer", computerProperties);
           }
           if (PropertyTabs.Contains(tabDefense))
           {
               Defense defenseProperties = new Defense();
               defenseProperties.Value = (double)DefenseCover1.Value;
               newComponent.Properties.Add("Defense", defenseProperties);
           }
           if (PropertyTabs.Contains(tabEnergyDampener))
           {
               DoubleProperty dampenerProperties = new DoubleProperty();
               dampenerProperties.Value = (double)EnergyDampener.Value;
               newComponent.Properties.Add("Energy Dampener", dampenerProperties);
           }
           if (PropertyTabs.Contains(tabEngine))
           {
               Engine engineProperties = new Engine();
               engineProperties.RamScoop = RamScoopCheckBox.Checked;
               engineProperties.FastestSafeSpeed = (int)EngineFastestSafeSpeed.Value;
               engineProperties.OptimalSpeed = (int)EngineOptimalSpeed.Value;
               engineProperties.FuelConsumption[0] = (int)W1Fuel.Value;
               engineProperties.FuelConsumption[1] = (int)W2Fuel.Value;
               engineProperties.FuelConsumption[2] = (int)W3Fuel.Value;
               engineProperties.FuelConsumption[3] = (int)W4Fuel.Value;
               engineProperties.FuelConsumption[4] = (int)W5Fuel.Value;
               engineProperties.FuelConsumption[5] = (int)W6Fuel.Value;
               engineProperties.FuelConsumption[6] = (int)W7Fuel.Value;
               engineProperties.FuelConsumption[7] = (int)W8Fuel.Value;
               engineProperties.FuelConsumption[8] = (int)W9Fuel.Value;
               engineProperties.FuelConsumption[9] = (int)W10Fuel.Value;
               newComponent.Properties.Add("Engine", engineProperties);

           }
           if (PropertyTabs.Contains(tabFuel))
           {
               Fuel fuelProperty = new Fuel();
               fuelProperty.Capacity = (int)FuelCapacity.Value;
               fuelProperty.Generation = (int)FuelGeneration.Value;
               newComponent.Properties.Add("Fuel", fuelProperty);
           }
           if (PropertyTabs.Contains(tabGate))
           {
               Gate gateProperties = new Gate();
               gateProperties.SafeHullMass = (int)SafeHullMass.Value;
               gateProperties.SafeRange = (int)SafeRange.Value;
               newComponent.Properties.Add("Gate", gateProperties);
           }
           if (PropertyTabs.Contains(tabHull))
           {
               Hull hullProperties = new Hull();
               hullProperties.ArmorStrength = (int)HullArmor.Value;
               hullProperties.FuelCapacity = (int)HullFuelCapacity.Value;
               hullProperties.DockCapacity = (int)HullDockCapacity.Value;
               hullProperties.ARMaxPop = (int)ARMaxPop.Value;
               hullProperties.BaseCargo = (int)HullCargoCapacity.Value;
               hullProperties.BattleInitiative = (int)HullInitiative.Value;

               hullProperties.Modules = new ArrayList();
               foreach (HullModule module in HullMap)
               {
                   HullModule newModule = new HullModule(module);
                   hullProperties.Modules.Add(newModule);
               }

               newComponent.Properties.Add("Hull", hullProperties);
           }
           if (PropertyTabs.Contains(tabHullAffinity))
           {
               HullAffinity hullAffinityProperty = new HullAffinity();
               hullAffinityProperty.Value = ComponentHullAffinity.Text;
               newComponent.Properties.Add("Hull Affinity", hullAffinityProperty);
           }
           if (PropertyTabs.Contains(tabJammer))
           {
               ProbabilityProperty jammerProperty = new ProbabilityProperty();
               jammerProperty.Value = (int)Deflection.Value;
               newComponent.Properties.Add("Jammer", jammerProperty);
           }
           if (PropertyTabs.Contains(tabDriver))
           {
               MassDriver driverProperty = new MassDriver();
               driverProperty.Value = (int)MassDriverSpeed.Value;
               newComponent.Properties.Add("Mass Driver", driverProperty);
           }
           if (PropertyTabs.Contains(tabMineLayer))
           {
               MineLayer mineLayerProperty = new MineLayer();
               mineLayerProperty.LayerRate = (int)MineLayingRate.Value;
               mineLayerProperty.SafeSpeed = (int)MineSafeSpeed.Value;
               mineLayerProperty.HitChance = (double)MineHitChance.Value;
               mineLayerProperty.DamagePerEngine = (int)MineDamagePerEngine.Value;
               mineLayerProperty.DamagePerRamScoop = (int)MineDamagePerRamScoop.Value;
               mineLayerProperty.MinFleetDamage = (int)MineMinFleetDamage.Value;
               mineLayerProperty.MinRamScoopDamage = (int)MineMinRamScoopDamage.Value;
               newComponent.Properties.Add("Mine Layer", mineLayerProperty);
           }
           if (PropertyTabs.Contains(tabLayerEfficiency))
           {
               DoubleProperty layerEfficiencyProperty = new DoubleProperty();
               layerEfficiencyProperty.Value = (double) MineLayerEfficiency.Value;
               newComponent.Properties.Add("Mine Layer Efficiency", layerEfficiencyProperty);
           }
           if (PropertyTabs.Contains(tabRobot))
           {
               IntegerProperty robotProperty = new IntegerProperty();
               robotProperty.Value = (int)MiningRate.Value;
               newComponent.Properties.Add("Mining Robot", robotProperty);
           }
           if (PropertyTabs.Contains(tabOrbitalAdjuster))
           {
               IntegerProperty adjusterProperty = new IntegerProperty();
               adjusterProperty.Value = (int)AdjusterRate.Value;
               newComponent.Properties.Add("Orbital Adjuster", adjusterProperty);
           }
           if (PropertyTabs.Contains(tabRadiation))
           {
               Radiation radiationProperty = new Radiation();
               radiationProperty.Value = (double)Radiation.Value;
               newComponent.Properties.Add("Radiation", radiationProperty);
           }
           if (PropertyTabs.Contains(tabScanner))
           {
               Scanner scannerProperty = new Scanner();
               scannerProperty.NormalScan = (int)NormalRange.Value;
               scannerProperty.PenetratingScan = (int)PenetratingRange.Value;
               newComponent.Properties.Add("Scanner", scannerProperty);
           }
           if (PropertyTabs.Contains(tabShield))
           {
               IntegerProperty shieldProperty = new IntegerProperty();
               shieldProperty.Value = (int)Shield.Value;
               newComponent.Properties.Add("Shield", shieldProperty);
           }
           if (PropertyTabs.Contains(tabTachyonDetector))
           {
               ProbabilityProperty detectorProperty = new ProbabilityProperty();
               detectorProperty.Value = (double)TachyonDetector.Value;
               newComponent.Properties.Add("Tachyon Detector", detectorProperty);
           }
           if (PropertyTabs.Contains(tabTerraforming))
           {
               Terraform terraformProperty = new Terraform();
               terraformProperty.MaxModifiedGravity = (int)GravityMod.Value;
               terraformProperty.MaxModifiedTemperature = (int)TemperatureMod.Value;
               terraformProperty.MaxModifiedRadiation = (int)RadiationMod.Value;
               newComponent.Properties.Add("Terraform", terraformProperty);
           }
           if (PropertyTabs.Contains(tabTransportShipsOnly))
           {
               SimpleProperty transportProperty = new SimpleProperty();
               newComponent.Properties.Add("Transport Ships Only", transportProperty);
           }
           if (PropertyTabs.Contains(tabWeapon))
           {
               Weapon weaponProperty = new Weapon();

               weaponProperty.Power = (int)WeaponPower.Value;
               weaponProperty.Range = (int)WeaponRange.Value;
               weaponProperty.Initiative = (int)WeaponInitiative.Value;
               weaponProperty.Accuracy = (int)WeaponAccuracy.Value;
               if (isStandardBeam.Checked) weaponProperty.Group = WeaponType.standardBeam;
               else if (isSapper.Checked) weaponProperty.Group = WeaponType.shieldSapper;
               else if (isGattling.Checked) weaponProperty.Group = WeaponType.gatlingGun;
               else if (isTorpedo.Checked) weaponProperty.Group = WeaponType.torpedo;
               else if (isMissile.Checked) weaponProperty.Group = WeaponType.missile;
               else weaponProperty.Group = WeaponType.standardBeam;

               newComponent.Properties.Add("Weapon", weaponProperty);
           }

           NovaCommon.AllComponents.Data.Components[newComponent.Name] = newComponent;

           EditModeOff();
           FileDirty = true;
           ComponentDirty = false;

           SelectComponent(ComponentType.Text, newComponent.Name);

           Report.Information("Component design has been saved.");
       }//SaveComponent


       //-----------------------------------------------------------------------------
       // When the program is terminated, save the component data. This function is
       // invoked no matter which method is used to terminate the program.
       // TODO Saving here has been disabled in debug mode as a quick backout for debugging - Daniel May 2009
       //-----------------------------------------------------------------------------
       private void OnFormClosing(object sender, FormClosingEventArgs e)
       {
#if (DEBUG)
           return;
#endif
           if (ComponentDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
                   SaveComponent();
           }
#if (DEBUG)
           else MessageBox.Show("Component not dirty.");
#endif
           if (FileDirty)
           {
               DialogResult reply = MessageBox.Show("Save the current component definition file?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
               if (reply == DialogResult.Cancel) return;
               else if (reply == DialogResult.Yes)
               {
                   if ( ! AllComponents.Save())
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

       /// <summary>
       /// Update the list of Hulls so that hulls this item may (only) be fitted to can be selected. (Hull affinity limits which hulls an item may be fitted to.)
       /// </summary>
       private void ComponentHullAffinity_PopulateList()
       {
           ComponentHullAffinity.Items.Clear();
           foreach (NovaCommon.Component thing in NovaCommon.AllComponents.Data.Components.Values)
           {
               if (thing.Type == "Hull")
               {
                   ComponentHullAffinity.Items.Add(thing.Name);
               }
           }
       }

       #endregion

       // ============================================================================
       //
       //                       GUI Update
       //
       // ============================================================================
       #region GUI Update

       // ----------------------------------------------------------------------------
       // When a selection in the list box changes repopulate the dialog with the
       // values for that electrical component. The processing of this event is
       // delegated from the Common Properties control.
       // ----------------------------------------------------------------------------
       private void ComponentList_SelectedIndexChanged(object sender,
                                                      EventArgs e)
      {
          if (ComponentList.SelectedItem == null)
          {
              return;
          }
          
          string selectedComponentName = ComponentList.SelectedItem as string;
          
          NovaCommon.Component selectedComponent = AllComponents.Data.Components[selectedComponentName] as NovaCommon.Component;

          CommonProperties = selectedComponent;
          Restrictions = selectedComponent.Restrictions;
          ComponentImage.ImageFile = selectedComponent.ImageFile;

          // Update all the property tabs with any properties of the selected component.
          try
          {
              PropertyTabs.TabPages.Clear();                          // Start by clearing the current tabs.

              if (selectedComponent.Properties.ContainsKey("Armor"))  // check for the property
              {
                  IntegerProperty armorProperty = selectedComponent.Properties["Armor"] as IntegerProperty; // get the property

                  Armor.Value = (decimal)armorProperty.Value;         // set values on the tab

                  PropertyTabs.TabPages.Add(tabArmor);                // make the tab vissible
                  PropertyTabs.SelectedTab = tabArmor;                // and selected
              }
              if (selectedComponent.Properties.ContainsKey("Battle Movement"))
              {
                  DoubleProperty movementProperty = selectedComponent.Properties["Battle Movement"] as DoubleProperty;

                  BattleMovement.Value = (decimal)movementProperty.Value;

                  PropertyTabs.TabPages.Add(tabMovement);
                  PropertyTabs.SelectedTab = tabMovement;
              }
              if (selectedComponent.Properties.ContainsKey("Beam Deflector"))
              {
                  ProbabilityProperty deflectorProperty = selectedComponent.Properties["Beam Deflector"] as ProbabilityProperty;

                  BeamDeflector.Value = (decimal)deflectorProperty.Value;

                  PropertyTabs.TabPages.Add(tabDeflector);
                  PropertyTabs.SelectedTab = tabDeflector; 
              }
              if (selectedComponent.Properties.ContainsKey("Bomb"))
              {
                  Bomb bombProperty = selectedComponent.Properties["Bomb"] as Bomb;

                  InstallationsDestroyed.Value = (decimal)bombProperty.Installations;
                  PopulationKill.Value = (decimal)bombProperty.PopKill;
                  MinimumPopKill.Value = (decimal)bombProperty.MinimumKill;
                  SmartBomb.Checked = bombProperty.IsSmart;

                  PropertyTabs.TabPages.Add(tabBomb);
                  PropertyTabs.SelectedTab = tabBomb;
              }
              if (selectedComponent.Properties.ContainsKey("Capacitor"))
              {
                  CapacitorProperty capacitorProperty = selectedComponent.Properties["Capacitor"] as CapacitorProperty;

                  BeamDamage.Value = (decimal)capacitorProperty.Value;

                  PropertyTabs.TabPages.Add(tabCapacitor);
                  PropertyTabs.SelectedTab = tabCapacitor;
              }
              if (selectedComponent.Properties.ContainsKey("Cargo"))
              {
                  IntegerProperty cargoProperty = selectedComponent.Properties["Cargo"] as IntegerProperty;

                  CargoCapacity.Value = (decimal)cargoProperty.Value;

                  PropertyTabs.TabPages.Add(tabCargo);
                  PropertyTabs.SelectedTab = tabCargo;
              }
              if (selectedComponent.Properties.ContainsKey("Cloak"))
              {
                  ProbabilityProperty cloakProperty = selectedComponent.Properties["Cloak"] as ProbabilityProperty;

                  Cloaking.Value = (decimal)cloakProperty.Value;

                  PropertyTabs.TabPages.Add(tabCloak);
                  PropertyTabs.SelectedTab = tabCloak;
              }
              if (selectedComponent.Properties.ContainsKey("Colonizer"))
              {
                  Colonizer colonizationProperty = selectedComponent.Properties["Colonizer"] as Colonizer;

                  if (colonizationProperty.Orbital)
                      OrbitalColonizationModule.Checked = true;
                  else
                      ColonizationModule.Checked = true;

                  PropertyTabs.TabPages.Add(tabColonization);
                  PropertyTabs.SelectedTab = tabColonization;
                  
              }
              if (selectedComponent.Properties.ContainsKey("Computer"))
              {
                  Computer computerProperties = selectedComponent.Properties["Computer"] as Computer;

                  Initiative.Value = (decimal)computerProperties.Initiative;
                  Accuracy.Value = (decimal)computerProperties.Accuracy;

                  PropertyTabs.TabPages.Add(tabComputer);
                  PropertyTabs.SelectedTab = tabComputer;
              }
              if (selectedComponent.Properties.ContainsKey("Defense"))
              {
                  Defense defenseProperty = selectedComponent.Properties["Defense"] as Defense;

                  DefenseCover1.Value = (decimal)defenseProperty.Value;
                  PropertyTabs.TabPages.Add(tabDefense);
                  PropertyTabs.SelectedTab = tabDefense;
              }
              if (selectedComponent.Properties.ContainsKey("Energy Dampener"))
              {
                  DoubleProperty dampenerProperty = selectedComponent.Properties["Energy Dampener"] as DoubleProperty;
                  EnergyDampener.Value = (decimal)dampenerProperty.Value;
                  PropertyTabs.TabPages.Add(tabEnergyDampener);
                  PropertyTabs.SelectedTab = tabEnergyDampener;
              }
              if (selectedComponent.Properties.ContainsKey("Engine"))
              {
                  Engine engineProperties = selectedComponent.Properties["Engine"] as Engine;

                  EngineFastestSafeSpeed.Value = (decimal)engineProperties.FastestSafeSpeed;
                  EngineOptimalSpeed.Value = (decimal)engineProperties.OptimalSpeed;
                  RamScoopCheckBox.Checked = engineProperties.RamScoop;

                  W1Fuel.Value = (decimal)engineProperties.FuelConsumption[0];
                  W2Fuel.Value = (decimal)engineProperties.FuelConsumption[1];
                  W3Fuel.Value = (decimal)engineProperties.FuelConsumption[2];
                  W4Fuel.Value = (decimal)engineProperties.FuelConsumption[3];
                  W5Fuel.Value = (decimal)engineProperties.FuelConsumption[4];
                  W6Fuel.Value = (decimal)engineProperties.FuelConsumption[5];
                  W7Fuel.Value = (decimal)engineProperties.FuelConsumption[6];
                  W8Fuel.Value = (decimal)engineProperties.FuelConsumption[7];
                  W9Fuel.Value = (decimal)engineProperties.FuelConsumption[8];
                  W10Fuel.Value = (decimal)engineProperties.FuelConsumption[9];
                  
                  PropertyTabs.TabPages.Add(tabEngine);
                  PropertyTabs.SelectedTab = tabEngine;
              }

              if (selectedComponent.Properties.ContainsKey("Fuel"))
              {
                  Fuel fuelProperties = selectedComponent.Properties["Fuel"] as Fuel;

                  FuelCapacity.Value = (decimal)fuelProperties.Capacity;
                  FuelGeneration.Value = (decimal)fuelProperties.Generation;

                  PropertyTabs.TabPages.Add(tabFuel);
                  PropertyTabs.SelectedTab = tabFuel;
              }

              if (selectedComponent.Properties.ContainsKey("Gate"))
              {
                  Gate gateProperties = selectedComponent.Properties["Gate"] as Gate;

                  SafeHullMass.Value = (decimal)gateProperties.SafeHullMass;
                  GateMassInfinite.Checked = (SafeHullMass.Value == -1);
                  SafeRange.Value = (decimal)gateProperties.SafeRange;
                  GateRangeInfinite.Checked = (SafeRange.Value == -1);

                  PropertyTabs.TabPages.Add(tabGate);
                  PropertyTabs.SelectedTab = tabGate;
              }
              if (selectedComponent.Properties.ContainsKey("Hull"))
              {
                  Hull hullProperties = selectedComponent.Properties["Hull"] as Hull;

                  HullArmor.Value = (decimal)hullProperties.ArmorStrength;
                  HullFuelCapacity.Value = (decimal)hullProperties.FuelCapacity;
                  HullDockCapacity.Value = (decimal)hullProperties.DockCapacity;
                  InfiniteDock.Checked = (HullDockCapacity.Value == -1);
                  ARMaxPop.Value = (decimal)hullProperties.ARMaxPop;
                  HullCargoCapacity.Value = (decimal)hullProperties.BaseCargo;
                  HullInitiative.Value = (decimal)hullProperties.BattleInitiative;

                  // HullMap.HullGrid.ActiveModules = new ArrayList();
                  try
                  {
                      HullMap.Clear();
                      foreach (HullModule module in hullProperties.Modules)
                      {
                          HullModule newModule = new HullModule(module);
                          HullMap.Add(module);

                      }
                  }
                  catch
                  {
                      // problem with the hull map; reset it.
                      Report.Error("Hull map error - resetting map.");
                      HullMap = new ArrayList();
                  }


                  PropertyTabs.TabPages.Add(tabHull);
                  PropertyTabs.SelectedTab = tabHull;
              }
              if (selectedComponent.Properties.ContainsKey("Hull Affinity"))
              {
                  HullAffinity hullAffinityProperty = selectedComponent.Properties["Hull Affinity"] as HullAffinity;
                  ComponentHullAffinity.Text = hullAffinityProperty.Value;

                  ComponentHullAffinity_PopulateList();

                  PropertyTabs.TabPages.Add(tabHullAffinity);
                  PropertyTabs.SelectedTab = tabHullAffinity;
              }

              if (selectedComponent.Properties.ContainsKey("Jammer"))
              {
                  ProbabilityProperty jammerProperty = selectedComponent.Properties["Jammer"] as ProbabilityProperty;

                  Deflection.Value = (decimal)jammerProperty.Value;

                  PropertyTabs.TabPages.Add(tabJammer);
                  PropertyTabs.SelectedTab = tabJammer;
              }
              if (selectedComponent.Properties.ContainsKey("Mass Driver")) 
              {
                  MassDriver driverProperty = selectedComponent.Properties["Mass Driver"] as MassDriver;

                  MassDriverSpeed.Value = (decimal)driverProperty.Value;

                  PropertyTabs.TabPages.Add(tabDriver);
                  PropertyTabs.SelectedTab = tabDriver;
              }
              if (selectedComponent.Properties.ContainsKey("Mine Layer"))
              {
                  MineLayer mineLayerProperties = selectedComponent.Properties["Mine Layer"] as MineLayer;

                  MineLayingRate.Value = (decimal)mineLayerProperties.LayerRate;
                  MineSafeSpeed.Value = (decimal)mineLayerProperties.SafeSpeed;
                  MineHitChance.Value = (decimal)mineLayerProperties.HitChance;
                  MineDamagePerEngine.Value = (decimal)mineLayerProperties.DamagePerEngine;
                  MineDamagePerRamScoop.Value = (decimal)mineLayerProperties.DamagePerRamScoop;
                  MineMinFleetDamage.Value = (decimal)mineLayerProperties.MinFleetDamage;
                  MineMinRamScoopDamage.Value = (decimal)mineLayerProperties.MinRamScoopDamage;

                  PropertyTabs.TabPages.Add(tabMineLayer);
                  PropertyTabs.SelectedTab = tabMineLayer;
              } 
              if (selectedComponent.Properties.ContainsKey("Mine Layer Efficiency"))
              {
                  DoubleProperty mineLayerProperties = selectedComponent.Properties["Mine Layer Efficiency"] as DoubleProperty;
                  MineLayerEfficiency.Value = (decimal)mineLayerProperties.Value;
                  PropertyTabs.TabPages.Add(tabLayerEfficiency);
                  PropertyTabs.SelectedTab = tabLayerEfficiency;
              }
              if (selectedComponent.Properties.ContainsKey("Mining Robot"))
              {
                  IntegerProperty robotProperty = selectedComponent.Properties["Mining Robot"] as IntegerProperty;

                  MiningRate.Value = (decimal)robotProperty.Value;

                  PropertyTabs.TabPages.Add(tabRobot);
                  PropertyTabs.SelectedTab = tabRobot;
              }
              if (selectedComponent.Properties.ContainsKey("Orbital Adjuster"))
              {
                  IntegerProperty adjusterProperty = selectedComponent.Properties["Orbital Adjuster"] as IntegerProperty;

                  AdjusterRate.Value = (decimal)adjusterProperty.Value;

                  PropertyTabs.TabPages.Add(tabOrbitalAdjuster);
                  PropertyTabs.SelectedTab = tabOrbitalAdjuster;
              }
              if (selectedComponent.Properties.ContainsKey("Radiation"))
              {
                  Radiation radiationProperty = selectedComponent.Properties["Radiation"] as Radiation;

                  Radiation.Value = (decimal)radiationProperty.Value;

                  PropertyTabs.TabPages.Add(tabRadiation);
                  PropertyTabs.SelectedTab = tabRadiation;
              }

              if (selectedComponent.Properties.ContainsKey("Scanner"))
              {
                  Scanner scannerProperties = selectedComponent.Properties["Scanner"] as Scanner;

                  NormalRange.Value = (decimal)scannerProperties.NormalScan;
                  PenetratingRange.Value = (decimal)scannerProperties.PenetratingScan;

                  PropertyTabs.TabPages.Add(tabScanner);
                  PropertyTabs.SelectedTab = tabScanner;
              }
              if (selectedComponent.Properties.ContainsKey("Shield"))
              {
                  IntegerProperty shieldProperty = selectedComponent.Properties["Shield"] as IntegerProperty;

                  Shield.Value = (decimal)shieldProperty.Value;

                  PropertyTabs.TabPages.Add(tabShield);
                  PropertyTabs.SelectedTab = tabShield;
              }
              if (selectedComponent.Properties.ContainsKey("Tachyon Detector"))
              {
                  ProbabilityProperty detectorProoperties = selectedComponent.Properties["Tachyon Detector"] as ProbabilityProperty;

                  TachyonDetector.Value = (decimal)detectorProoperties.Value;

                  PropertyTabs.TabPages.Add(tabTachyonDetector);
                  PropertyTabs.SelectedTab = tabTachyonDetector;
              }
              if (selectedComponent.Properties.ContainsKey("Terraform"))
              {
                  Terraform terraformProperties = selectedComponent.Properties["Terraform"] as Terraform;

                  GravityMod.Value = (decimal)terraformProperties.MaxModifiedGravity;
                  TemperatureMod.Value = (decimal)terraformProperties.MaxModifiedTemperature;
                  RadiationMod.Value = (decimal)terraformProperties.MaxModifiedRadiation;

                  PropertyTabs.TabPages.Add(tabTerraforming);
                  PropertyTabs.SelectedTab = tabTerraforming;
              }
              if (selectedComponent.Properties.ContainsKey("Transport Ships Only"))
              {
                  SimpleProperty mineLayerProperties = selectedComponent.Properties["Transport Ships Only"] as SimpleProperty;
                  PropertyTabs.TabPages.Add(tabTransportShipsOnly);
                  PropertyTabs.SelectedTab = tabTransportShipsOnly;
              }
              if (selectedComponent.Properties.ContainsKey("Weapon"))
              {
                  Weapon weaponProperties = selectedComponent.Properties["Weapon"] as Weapon;

                  WeaponPower.Value = (decimal)weaponProperties.Power;
                  WeaponRange.Value = (decimal)weaponProperties.Range;
                  WeaponInitiative.Value = (decimal)weaponProperties.Initiative;
                  WeaponAccuracy.Value = (decimal)weaponProperties.Accuracy;
                  switch (weaponProperties.Group)
                  {
                      case WeaponType.standardBeam: isStandardBeam.Checked = true; break;
                      case WeaponType.shieldSapper: isSapper.Checked = true; break;
                      case WeaponType.gatlingGun: isGattling.Checked = true; break;
                      case WeaponType.torpedo: isTorpedo.Checked = true; break;
                      case WeaponType.missile: isMissile.Checked = true; break;
                      default: isStandardBeam.Checked = true; break;

                  }

                  PropertyTabs.TabPages.Add(tabWeapon);
                  PropertyTabs.SelectedTab = tabWeapon;
              }

          }
          catch
          {
              MessageBox.Show("Error accessing component properties.");
          }

      }

       // -------------------------------------------------------------------------
       // Clicking in the component type box brings up a 'drop down' menu to 
       // select from.
       // -------------------------------------------------------------------------
       private void ComponentType_Changed(object sender, EventArgs e)
       {
           // in edit mode, just change the component type. In view mode, repopulate the component list.
           if (!EditMode)
           {
               UpdateListBox(ComponentType.Text);
               if (ComponentList.Items.Count > 0)
               {
                   ComponentList.SelectedIndex = 0; // pick the first item in the list
               }
               else
               {
                   // Blank the component information
                   PropertyTabs.TabPages.Clear();
                   BasicProperties.Cost = new NovaCommon.Resources(0, 0, 0, 0);
                   BasicProperties.Mass = 0;
                   ComponentImage.Image = null;
                   ComponentName.Text = "";
                   Description.Text = "";
                   TechRequirements.Value = new TechLevel(0);
               }

           }

       }
       
       // -------------------------------------------------------------------------      
       // Update the title with key information.
       // -------------------------------------------------------------------------      
       private void UpdateTitleBar()
       {
           Text = "Nova Component Editor - ";
           if (AllComponents.ComponentFile != null)
           {
               Text += AllComponents.ComponentFile;
           }
           else
           {
               Text += "New Component Definintions";
           }

           if (EditMode)
           {
               Text += " - Edit Mode";
           }
           else
           {
               Text += " - Browsing Mode";
           }
       }


       // new version using the Component.Type property
       public void UpdateListBox(String ComponentTypeSelected)
       {
           ComponentList.Items.Clear();

           foreach (NovaCommon.Component thing in AllComponents.Data.Components.Values)
           {
               if (thing.Type == ComponentTypeSelected)
               {
                   ComponentList.Items.Add(thing.Name);
               }
           }
       }



       // -------------------------------------------------------------------------      
       // Draw tabs horizontally to the right of the tab control.
       // -------------------------------------------------------------------------
       private void PropertyTabs_DrawItem(object sender, DrawItemEventArgs e)
       {
           Graphics g = e.Graphics;
           Brush _TextBrush;

           // Get the item from the collection.
           TabPage _TabPage = PropertyTabs.TabPages[e.Index];

           // Make the tab background 'Control' grey. 
           // Everytime I change the colection they are reset, so easiest to do it programatically here.
           _TabPage.BackColor = Color.FromKnownColor(KnownColor.Control);
           PropertyTabs.Width = 479; // Incase I forget to shrink it to fit when adding controls.

           // Get the real bounds for the tab rectangle.
           Rectangle _TabBounds = PropertyTabs.GetTabRect(e.Index);

           _TextBrush = new SolidBrush(Color.Black);

           // Use our own font. Because we CAN.
           Font _TabFont = new Font("Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);

           // Draw string. Center the text.
           StringFormat _StringFlags = new StringFormat();
           _StringFlags.Alignment = StringAlignment.Center;
           _StringFlags.LineAlignment = StringAlignment.Center;
           
           g.DrawString(_TabPage.Text, _TabFont, _TextBrush,
                        _TabBounds, new StringFormat(_StringFlags));
           
       }



       // -------------------------------------------------------------------------
       // Update the defense cover display. Shows the coverage for 40/80/100 
       // defenses.
       // -------------------------------------------------------------------------
       private void DefenseCover1_ValueChanged(object sender, EventArgs e)
       {
           ComponentDirty = true;

           DefenseCover40.Text = ((1.0 - Math.Pow((double)(1.00M - DefenseCover1.Value / 100), 40.0)) * 100).ToString("f2");
           DefenseCover80.Text = ((1.0 - Math.Pow((double)(1.00M - DefenseCover1.Value / 100), 80.0)) * 100).ToString("f2");
           DefenseCover100.Text = ((1.0 - Math.Pow((double)(1.00M - DefenseCover1.Value / 100), 100.0)) * 100).ToString("f2");
       }

       // -------------------------------------------------------------------------
       // Update the display of the number of mines a weapon will sweep.
       // -------------------------------------------------------------------------
       private void UpdateMinesSwept(object sender, EventArgs e)
       {
           if (isStandardBeam.Checked)
           {
               decimal minesSwept = WeaponRange.Value * WeaponPower.Value;
               MinesSwept.Text = minesSwept.ToString() + " mines swept per year.";
           }
           else if (isGattling.Checked)
           {
               decimal minesSwept = 16 * WeaponPower.Value;
               MinesSwept.Text = minesSwept.ToString() + " mines swept per year.";
           }
           else
           {
               MinesSwept.Text = "Weapon doesn't sweep mines.";
           }
       }


       // -------------------------------------------------------------------------
       // Update the display of the engine free speed - the highest warp speed with fuel cost of 0.
       // -------------------------------------------------------------------------
       private void UpdateFastestFreeSpeed(object sender, EventArgs e)
       {
           if (W10Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 10;
           else if (W9Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 9;
           else if (W8Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 8;
           else if (W7Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 7;
           else if (W6Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 6;
           else if (W5Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 5;
           else if (W4Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 4;
           else if (W3Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 3;
           else if (W2Fuel.Value == 0)
               EngineFastestFreeSpeed.Value = 2;
           else
               EngineFastestFreeSpeed.Value = 1;


       }

       // -------------------------------------------------------------------------
       // Provide a convenient way of setting gates with infinite capabilities.
       // The value -1 is used as the internal representation of infinite capabilities.
       // -------------------------------------------------------------------------
       private void GateMassInfinite_CheckedChanged(object sender, EventArgs e)
       {
           if (GateMassInfinite.Checked)
           {
               SafeHullMass.Value = -1;
               SafeHullMass.ReadOnly = true;
           }
           else
           {
               SafeHullMass.ReadOnly = false;
           }
       }

       // -------------------------------------------------------------------------
       // Provide a convenient way of setting gates with infinite capabilities.
       // The value -1 is used as the internal representation of infinite capabilities.
       // -------------------------------------------------------------------------
       private void GateRangeInfinite_CheckedChanged(object sender, EventArgs e)
       {
           if (GateRangeInfinite.Checked)
           {
               SafeRange.Value = -1;
               SafeRange.ReadOnly = true;
           }
           else
           {
               SafeRange.ReadOnly = false;
           }
       }

       // -------------------------------------------------------------------------
       // Provide a convenient way of setting space stations with infinite dock capabilities.
       // The value -1 is used as the internal representation of infinite capabilities.
       // -------------------------------------------------------------------------
       private void InfiniteDock_CheckedChanged(object sender, EventArgs e)
       {
           if (InfiniteDock.Checked)
           {
               HullDockCapacity.Value = -1;
               HullDockCapacity.ReadOnly = true;
           }
           else
           {
               HullDockCapacity.ReadOnly = false;
           }

       }


       #endregion GUI Update

       // ============================================================================
       //
       //                            Utility Functions
       //
       // ============================================================================
       #region Utility

       // -------------------------------------------------------------------------      
       // Makes a given component the selected component in the Component Editor
      // -------------------------------------------------------------------------      
      public void SelectComponent(String type, String name)
       {
          UpdateListBox(type);
          EditModeOff();

          // keep the current component selected
          for (int n = 0; n < ComponentList.Items.Count; n++)
          {
              if ((string) ComponentList.Items[n] == name)
              {
                  ComponentList.SelectedIndex = n;
                  break;
              }
          }
       }

      // -------------------------------------------------------------------------      
      // Get and set the properties common to all components.
      // -------------------------------------------------------------------------      
      public NovaCommon.Component CommonProperties
      {
          get
          {
              NovaCommon.Component component = new NovaCommon.Component();

              component.ComponentImage = ComponentImage.Image;
              component.Cost = BasicProperties.Cost;
              component.Description = Description.Text;
              component.Mass = BasicProperties.Mass;
              component.Name = ComponentName.Text;
              component.RequiredTech = TechRequirements.Value;

              return component;
          }

          set
          {
              BasicProperties.Cost = value.Cost;
              BasicProperties.Mass = value.Mass;
              ComponentImage.Image = value.ComponentImage;
              ComponentName.Text = value.Name;
              Description.Text = value.Description;
              RestrictionSummary.Text = value.Restrictions.ToString();
              
              TechRequirements.Value = value.RequiredTech;
          }
      }//CommonProperties

      // -------------------------------------------------------------------------      
      // Delete the currently selected component.
      // -------------------------------------------------------------------------      
      public void DeleteComponent()
      {
          string componentName = ComponentName.Text;

          if (componentName == null || componentName == "")
          {
              Report.Error("You must select a component to delete");
              return;
          }

          AllComponents.Data.Components.Remove(componentName);
          ComponentList.Items.Remove(componentName);

          if (ComponentList.Items.Count > 0)
          {
              ComponentList.SelectedIndex = 0;
          }

          FileDirty = true;
          ComponentDirty = false;
          EditModeOff();
      }// DeleteComponent

      // -------------------------------------------------------------------------
      // As the defense coverage changes, update the coverage display for 
      // multiple defenses.
      // -------------------------------------------------------------------------
      private void SetComponentDirty(object sender, EventArgs e)
      {
          ComponentDirty = true;
      }


      // -------------------------------------------------------------------------
      /// <summary><para>
      /// Enter component editing mode.
      /// </para><para>
      /// In this mode changing the component type modifies the currently selected 
      /// component.
      /// </para></summary>
      // -------------------------------------------------------------------------
      private void EditModeOn()
      {
          EditMode = true;
          ComponentName.ReadOnly = false;
          Description.ReadOnly = false;
          // TODO (low priority) - enable all component editing fields/menu items
          UpdateTitleBar();
      }

      // -------------------------------------------------------------------------
      /// <summary><para>
      /// Enter component browsing mode. 
      /// </para><para>
      /// In this mode changing the component type updates the component list with 
      /// components of the new type.
      /// </para></summary>
      // -------------------------------------------------------------------------
      private void EditModeOff()
      {
          EditMode = false;
          ComponentName.ReadOnly = true;
          Description.ReadOnly = true;
          // TODO (low priority) - disable all component editing fields.
          UpdateTitleBar();

      }

      //-----------------------------------------------------------------------------
      // General dialog handling
      //-----------------------------------------------------------------------------
      private void DoDialog(Form dialog)
      {
          dialog.ShowDialog();
          dialog.Dispose();
      }

      // -------------------------------------------------------------------------
      /// <summary>
      /// Clear all the entry fields on the component editor and associated forms.
      /// </summary>
      // -------------------------------------------------------------------------
      private void ClearForm()
      {
          PropertyTabs.TabPages.Clear();
          BasicProperties.Cost = new NovaCommon.Resources(0, 0, 0, 0);
          BasicProperties.Mass = 0;
          ComponentImage.Image = null;
          ComponentName.Text = "";
          Description.Text = "";
          RestrictionSummary.Text = "";
          TechRequirements.Value = new TechLevel(0);
          Restrictions = new RaceRestriction();
          HullMap = new ArrayList();
      }

      #endregion Utility





      #region Menu Buttons

      // ============================================================================
      //
      //                                  Menu Buttons
      //
      // ============================================================================

      #region File Menu
      // ============================================================================
      // File Menu
      // ============================================================================

      // -------------------------------------------------------------------------
      // Menu->File->Open
      // -------------------------------------------------------------------------
      private void openToolStripMenuItem_Click(object sender, EventArgs e)
      {
          // Save the current work, if any.
          if (FileDirty)
          {
              SaveComponent();
              AllComponents.Save();
          }
          OpenFileDialog fd = new OpenFileDialog();
          fd.Title = "Open component definition file";

          DialogResult result = fd.ShowDialog();

          if (result == DialogResult.OK)
          {
              AllComponents.ComponentFile = fd.FileName;
              AllComponents.Restore();

              PropertyTabs.TabPages.Clear();

              // start with something showing
              ComponentType.Text = "Armor";
              UpdateListBox("Armor");
              if (ComponentList.Items.Count > 0)
                ComponentList.SelectedIndex = 0; // pick the first item in the list
              FileDirty = false;
              ComponentDirty = false;
              EditModeOff();
              UpdateTitleBar();

          }

      }

      // -------------------------------------------------------------------------
      // Menu->File->New
      // Create a new, empty component definition file.
      // -------------------------------------------------------------------------
      private void menuItem_NewFile_Click(object sender, EventArgs e)
      {
          // clear the in memory component list
          AllComponents.MakeNew();

          // setup for editing
          EditModeOn();
          FileDirty = true;
          ComponentDirty = true;

          // Tidy up the UI
          ComponentName.Text = "";
          Description.Text = "";
          BasicProperties.Cost = new NovaCommon.Resources(0, 0, 0, 0);
          BasicProperties.Mass = 0;
          ComponentImage.Image = null;
          TechRequirements.Value = new TechLevel(0);
          ComponentList.Items.Clear();
          PropertyTabs.TabPages.Clear();
          HullMap = new ArrayList();

          UpdateTitleBar();
      }

      // -------------------------------------------------------------------------
      // Menu->File->Save
      // -------------------------------------------------------------------------
      private void menuItem_SaveFile_Click(object sender, EventArgs e)
      {
          if (ComponentDirty) SaveComponent();
          AllComponents.Save();
          FileDirty = false;
          EditModeOff();
          UpdateTitleBar();
      }

      // -------------------------------------------------------------------------
      // Menu->File->Save As
      // -------------------------------------------------------------------------
      private void menuItem_SaveFileAs_Click(object sender, EventArgs e)
      {
          if (ComponentDirty) SaveComponent();
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

			  // FIXME - Create the selected file, if it doesn't already exist. This is a workaround for the FIXME below.
			  if (!info.Exists)
			  {
				  System.IO.FileStream saveFile = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create);
				  saveFile.Close();
			  }

			  if (result == DialogResult.OK && fd.FileName != null)
			  {
				  // FIXME- somehow the following line does not work! 
				  // The FileName gets stored and then imediately returned to what it was before ??? 
				  // Works only if the file selected already exists.
				  // See the workaround above which creates the file first.
				  AllComponents.ComponentFile = fd.FileName;

				  AllComponents.Save();
			  }
			  EditModeOff();
			  UpdateTitleBar();
			  // fd.Dispose();
		  }
      }

      //-----------------------------------------------------------------------------
      // Menu-File-Exit
      // Exit button press. Close the program only if data is saved.
      //-----------------------------------------------------------------------------
      private void ExitButton_Click(object sender, EventArgs e)
      {
          if (ComponentDirty)
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
          if (FileDirty) 
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
      // -------------------------------------------------------------------------
      // Menu->Component->Save
      // -------------------------------------------------------------------------
      private void SaveComponent_Click(object sender, EventArgs e)
      {
          SaveComponent();
      }


      // -------------------------------------------------------------------------
      // Menu->Component->Delete
      // Deletes the currently selected component.
      // -------------------------------------------------------------------------
      private void menuItem_DeleteComponent_Click(object sender, EventArgs e)
      {
          DeleteComponent();
          EditModeOff();
          UpdateListBox(ComponentType.Text);
      }



    // -------------------------------------------------------------------------
    /// <summary><para>
    /// Menu->Component->New
    /// </para><para>
    /// Create a new component.
    /// </para> </summary>
    // -------------------------------------------------------------------------
      private void menuItem_NewComponent_Click(object sender, EventArgs e)
      {
          if (ComponentDirty)
          {
              DialogResult reply = MessageBox.Show("Save the current component?", "Caption", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
              if (reply == DialogResult.Cancel) return;
              else if (reply == DialogResult.Yes)
                  SaveComponent();
          }


          ClearForm(); // To create a new component we just need to clear the form and set up default values.

          EditModeOn();
          ComponentName.Text = "New Component";
          ComponentName.Focus();
          ComponentName.SelectAll();
      }

      // -------------------------------------------------------------------------
      // Menu->Component->Edit
      // This is one way of entering 'edit' mode from 'browsing' mode. In edit
      // mode changing the component type changes the current component's type.
      // -------------------------------------------------------------------------
      private void menuItem_EditComponent_Click(object sender, EventArgs e)
      {
          EditModeOn();
      }

// -------------------------------------------------------------------------
/// <summary><para>
/// Menu->Component->Copy
/// </para><para>
/// Take a copy of a component to use for a template for a new component.
/// </para></summary>
/// 
// -------------------------------------------------------------------------
      private void copyToolStripMenuItem_Click(object sender, EventArgs e)
      {
          EditModeOn();
          ComponentName.Text = "New Component";
          ComponentName.Focus();
          ComponentName.SelectAll();

      }

      // -------------------------------------------------------------------------
      // Menu->Component->Discard Changes
      // Discardes any changes to the currently selected compoenent by refreshing 
      // the UI from the in memory components.
      // -------------------------------------------------------------------------
      private void menuItem_DiscardComponentChanges_Click(object sender, EventArgs e)
      {
          UpdateListBox(ComponentType.Text);
          EditModeOff();
      }

      // -------------------------------------------------------------------------
      // Menu->Component->Race Restrictions
      // Open the race restrictions dialog to edit what LRT or PRT restrictions
      // apply to this component.
      // -------------------------------------------------------------------------
      private void menuItem_RaceRestrictions_Click(object sender, EventArgs e)
      {
          //MessageBox.Show(ComponentName.Text);
          EditModeOn();
          RaceRestrictionDialog dialog = new RaceRestrictionDialog(ComponentName.Text, (Bitmap)ComponentImage.Image, Restrictions);
          DialogResult result = dialog.ShowDialog();
          Restrictions = new RaceRestriction(dialog.Restrictions);
          ComponentDirty = true;

          RestrictionSummary.Text = Restrictions.ToString();
          dialog.Dispose();

      }

      #endregion Component Menu

      #region Property Menu

      // -------------------------------------------------------------------------
      /// <summary><para>
      /// Menu->Property->Add
      /// </para><para>
      /// Add the tab for the appropriate property, reset the values on the tab 
      /// and give it focus.
      /// </para></summary>
      // -------------------------------------------------------------------------
      private void menuItem_AddProperty(object sender, EventArgs e)
      {
          ToolStripMenuItem menuSelection = sender as ToolStripMenuItem;

          if (menuSelection == armorToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabArmor);
              PropertyTabs.SelectedTab = tabArmor;

              Armor.Value = 0;
          }
          else if (menuSelection == beamDeflectorToolStripMenuItem1)
          {
              PropertyTabs.TabPages.Add(tabDeflector);
              PropertyTabs.SelectedTab = tabDeflector;

              BeamDeflector.Value = 0;
          }
          else if (menuSelection == battleMovementToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabMovement);
              PropertyTabs.SelectedTab = tabMovement;

              BattleMovement.Value = 0;

          }
          else if (menuSelection == bombToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabBomb);
              PropertyTabs.SelectedTab = tabBomb;

              PopulationKill.Value = 0;
              MinimumPopKill.Value = 0;
              InstallationsDestroyed.Value = 0;
              SmartBomb.Checked = false;

          }
          else if (menuSelection == capacitorToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabCapacitor);
              PropertyTabs.SelectedTab = tabCapacitor;

              BeamDamage.Value = 0;

          }
          else if (menuSelection == cargoToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabCargo);
              PropertyTabs.SelectedTab = tabCargo;

              CargoCapacity.Value = 0;

          }
          else if (menuSelection == cloakToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabCloak);
              PropertyTabs.SelectedTab = tabCloak;

              Cloaking.Value = 0;

          }
          else if (menuSelection == colonizationToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabColonization);
              PropertyTabs.SelectedTab = tabColonization;

              ColonizationModule.Checked = true;

          }
          else if (menuSelection == computerToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabComputer);
              PropertyTabs.SelectedTab = tabComputer;

              Accuracy.Value = 0;
              Initiative.Value = 0;
          }
          else if (menuSelection == DefenseToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabDefense);
              PropertyTabs.SelectedTab = tabDefense;

              DefenseCover1.Value = 0.00M;
              DefenseCover40.Text = "0.00";
              DefenseCover80.Text = "0.00";
              DefenseCover100.Text = "0.00";
          }
          else if (menuSelection == energyDampenerToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabEnergyDampener);
              PropertyTabs.SelectedTab = tabEnergyDampener;

              EnergyDampener.Value = 0.0M;
          }
          else if (menuSelection == engineToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabEngine);
              PropertyTabs.SelectedTab = tabEngine;

              RamScoopCheckBox.Checked = false;
              EngineFastestSafeSpeed.Value = 1;
              EngineOptimalSpeed.Value = 1;

              W1Fuel.Value = 0;
              W2Fuel.Value = 0;
              W3Fuel.Value = 0;
              W4Fuel.Value = 0;
              W5Fuel.Value = 0;
              W6Fuel.Value = 0;
              W7Fuel.Value = 0;
              W8Fuel.Value = 0;
              W9Fuel.Value = 0;
              W10Fuel.Value = 0;

          }
          else if (menuSelection == fuelToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabFuel);
              PropertyTabs.SelectedTab = tabFuel;

              FuelCapacity.Value = 0;
              FuelGeneration.Value = 0;

          }
          else if (menuSelection == gateToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabGate);
              PropertyTabs.SelectedTab = tabGate;

              SafeHullMass.Value = 0;
              SafeRange.Value = 0;

          }
          else if (menuSelection == hullToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabHull);
              PropertyTabs.SelectedTab = tabHull;

              HullMap = new ArrayList();
              HullArmor.Value = 0;
              HullInitiative.Value = 0;
              HullFuelCapacity.Value = 0;
              HullCargoCapacity.Value = 0;
              HullDockCapacity.Value = 0;
              ARMaxPop.Value = 0;


          }
          else if (menuSelection == hullAffinityToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabHullAffinity);
              PropertyTabs.SelectedTab = tabHullAffinity;

              ComponentHullAffinity.Text = "";
              ComponentHullAffinity_PopulateList();
          }
          else if (menuSelection == jammerToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabJammer);
              PropertyTabs.SelectedTab = tabJammer;

              Deflection.Value = 0;

          }
          else if (menuSelection == massDriverToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabDriver);
              PropertyTabs.SelectedTab = tabDriver;

              MassDriverSpeed.Value = 0;

          }
          else if (menuSelection == mineLayerToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabMineLayer);
              PropertyTabs.SelectedTab = tabMineLayer;

              MineLayingRate.Value = 0;
              MineSafeSpeed.Value = 0;
              MineDamagePerEngine.Value = 0;
              MineDamagePerRamScoop.Value = 0;
              MineMinFleetDamage.Value = 0;
              MineMinRamScoopDamage.Value = 0;
              MineHitChance.Value = 0.0M;

          }
          else if (menuSelection == layerEfficencyToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabLayerEfficiency);
              PropertyTabs.SelectedTab = tabLayerEfficiency;

              MineLayerEfficiency.Value = 2.00M; // default to x2 
          }
          else if (menuSelection == miningRobotToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabRobot);
              PropertyTabs.SelectedTab = tabRobot;

              MiningRate.Value = 0;

          }
          else if (menuSelection == orbitalAdjusterToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabOrbitalAdjuster);
              PropertyTabs.SelectedTab = tabOrbitalAdjuster;

              AdjusterRate.Value = 0;

          }
          else if (menuSelection == radiationToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabRadiation);
              PropertyTabs.SelectedTab = tabRadiation;

              Radiation.Value = 0;
          }
          else if (menuSelection == scannerToolStripMenuItem || menuSelection == planetaryScannerToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabScanner);
              PropertyTabs.SelectedTab = tabScanner;

              NormalRange.Value = 0;
              PenetratingRange.Value = 0;

          }
          else if (menuSelection == shieldToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabShield);
              PropertyTabs.SelectedTab = tabShield;

              Shield.Value = 0;

          }
          else if (menuSelection == tachyonDetectorToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabTachyonDetector);
              PropertyTabs.SelectedTab = tabTachyonDetector;
              TachyonDetector.Value = 0;
          }
          else if (menuSelection == terraformingToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabTerraforming);
              PropertyTabs.SelectedTab = tabTerraforming;

              GravityMod.Value = 0;
              TemperatureMod.Value = 0;
              RadiationMod.Value = 0;

          }
          else if (menuSelection == transportOnlyToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabTransportShipsOnly);
              PropertyTabs.SelectedTab = tabTransportShipsOnly;
          }
          else if (menuSelection == weaponToolStripMenuItem)
          {
              PropertyTabs.TabPages.Add(tabWeapon);
              PropertyTabs.SelectedTab = tabWeapon;


              WeaponPower.Value = 0;
              WeaponRange.Value = 0;
              WeaponInitiative.Value = 0;
              WeaponAccuracy.Value = 0;

              isStandardBeam.Checked = true;

          }

          EditModeOn();
      } // add property

      // -------------------------------------------------------------------------
      // Menu->Property->Delete Selected Property
      // Remove the selected property tab from the tab control, hidding it and
      // letting us know not to add that property to the component.
      // -------------------------------------------------------------------------
      private void deleteSelectedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
      {
          PropertyTabs.TabPages.Remove(PropertyTabs.SelectedTab);
          EditModeOn();
      }

      #endregion Property Menu

      #region About Box
      //-----------------------------------------------------------------------------
      // Display the About box dialog
      //-----------------------------------------------------------------------------
      private void AboutMenuClick(object sender, EventArgs e)
      {
          DoDialog(new AboutBox());
      }

      #endregion About Box

      #endregion Menu Buttons


      // ============================================================================
      //
      //                                  Miscellanious
      //
      // ============================================================================

      #region Misc

      private void buttonEditHull_Click(object sender, EventArgs e)
      {
          // create a new hull grid dialog
          HullDialog dialog = new HullDialog();

          // and populate it
          dialog.HullGrid.ActiveModules = HullMap;

          // do the dialog
          dialog.ShowDialog();

          // copy the new hull map
          HullMap.Clear();
          HullMap = dialog.HullGrid.ActiveModules;

          dialog.Dispose();

      }

      #endregion

   }
}



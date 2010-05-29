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
// Primary racial trait definitions/data. Each race has exactly one Primary 
// racial trait.
//
// This data may be replaced by runtime loaded data in the future. Use AllTraits
// to access this data.
//
// The cost per trait is (negative numbers mean that
// you gain those advantage points for selecting that trait):
//
// Improved Fuel Efficiency   78 pts.     No Ram Scoop Engines         -53 pts.
// Total Terraforming        140 pts.     Cheap Engines                 80 pts.
// Advanced Remote Mining     53 pts.     Only Basic Remote Mining     -85 pts.
// Improved Starbases         67 pts.     No Advanced Scanners         -95 pts.
// Generalized Research      -13 pts.     Low Starting Population      -60 pts.
// Ultimate Recycling         80 pts.     Bleeding Edge Technology     -23 pts.
// Mineral Alchemy            51 pts.     Regenerating Shields         -10 pts.
// Cheap Factories            58 pts.
// ===========================================================================
#endregion


using System;

// The TraitData namespace should only be accessed by AllTraits. Other objects
// should access this data through AllTraits in case it is run time loaded in
// the future.
namespace Nova.Common
{

// ============================================================================
// Static definition of all the secondary traits
// ============================================================================

   public static class SecondaryTraits
   {
      public static TraitEntry[] Traits = new TraitEntry[15] {

// ============================================================================
// Improved fuel efficiency (implemented)
// ============================================================================

         new TraitEntry("Improved Fuel Efficiency", "IFE", 78,
           "This gives you the Fuel Mizer and Galaxy Scoop engines and "
         + "increases your starting propulsion tech by 1 level. "
         + "All engines use 15% less fuel"), 


// ============================================================================
// Total Terraforming
// ============================================================================

         new TraitEntry("Total Terraforming", "TT", 140,
           "Allows you to terraform by investing solely in Biotechnology "
         + " You may terraform a variable up to 30%. "
         + " Terraforming costs 30% less."),

// ============================================================================
// Advanced Remote Mining
// ============================================================================

         new TraitEntry("Advanced Remote Mining", "ARM", 53, 
           "Gives you three additional mining hulls and two new robots. "
         + "You will start the game with two Midget Miners. "
         + "Do not pick Only Basic Remote Mining with this option (or "
         + "you will get no benefit from this selection)."),

// ============================================================================
// Improved Starbases (implemented)
// ============================================================================

         new TraitEntry("Improved Star Bases", "ISB", 67, 
           "Gives you two new starbase designs. The Stardock allows you to "
         + "build light ships. The Ultra Station is a formidable weapons "
         + "platform. Your starbases cost 20% less and are 20% cloaked."),

// ============================================================================
// Generalised Research
// ============================================================================

         new TraitEntry("Generalised Research", "GR", 67, 
           "Your race takes a holistic approach to research, Only half of "
         + "the resources dedicated to research will be applied to the "
         + "current field of research. However, 15% of the total will be "
         + "applied to all other fields."),

// ============================================================================
// Ultimate Recycling (implemented)
// ============================================================================

         new TraitEntry("Ultimate Recycling", "UR", 80,
           "When you scarp a fleet at a starbase you recover 90% of the "
         + "minerals and some of the resources. The resources are available "
         + "for use the following year. Scrapping a fleet at a planet gives "
         + "half the starbase amount."),

// ============================================================================
// MineralAlchemy
// ============================================================================

         new TraitEntry("Mineral Alchemy", "MA", 51, 
           "Allows you turn resources into minerals four times more "
         + "efficiently than other races. This may be performed at any "
         + "planet you own."),

// ============================================================================
// Cheap Factories (implmented)
// ============================================================================

         new TraitEntry("Cheap Factories", "CF", 58,
           "Factories cost 1kT less Germanium to build."),
 
// ============================================================================
// No RAM scoop engines (implemented)
// ============================================================================

         new TraitEntry("No Ram Scoop Engines", "NRS", -53,
           "No engines which travel at warp 5 or greater burning no fuel "
         + "will be available. However, the Interspace 10 engine will be "
         + "available. This drive will travel at warp 10 without taking "
         + "damage."),

// ============================================================================
// Cheap Engines
// ============================================================================

         new TraitEntry("Cheap Engines", "CE", 80,
           "You can throw together engines at half cost. However, at speeds "
         + "greater than wap 6 there is a 10% chance the engines will not "
         + "engage. You start with a propulsion one level higher than you "
         + "would otherwise."),

// ============================================================================
// Basic Remote Mining
// ============================================================================

         new TraitEntry("Only Basic Remote Mining", "OBRM", -85,
           "The only mining ship available to you will be the Mini-Miner." 
         + "This trait overrides Advanced Remote Mining. Your maximum "
         + "population per planet is increased by 10%."),

// ============================================================================
// No Advanced Scanners (implemented)
// ============================================================================

         new TraitEntry("No Advanced Scanners", "NAS", -95, 
           "No planet penetrating scanners will be available to you. However, "
         + "conventional scanners will have their range doubled."),

// ============================================================================
// Low Starting Population (implemented)
// ============================================================================

         new TraitEntry("Low Starting Population", "LSP", -60, 
           "You start with 30% fewer colonists."),

// ============================================================================
// Bleeding Edge Technology
// ============================================================================

         new TraitEntry("Bleeding Edge Technology", "BET", -23, 
           "New techs initially cost twice as much to build. As soon as you "
         + "exceed all of the tech requirements by one level the cost drops "
         + "back to normal. Miniaturization occurs at 5% a level and pegs "
         + "at 80%."),

// ============================================================================
// Regenerating Shields
// ============================================================================

         new TraitEntry("Regenerating Shields", "RS", -10,
           "All shields are 40% stronger than the listed rating. Shields "
         + "regenerate 10% of maxium strength after every round of battle. "
         + "However, your armor will only be 50% of its rated strength.")
      };
   }
}

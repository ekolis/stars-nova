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

// The TraitData namespace should only be accessed by AllTraits. Other objects
// should access this data through AllTraits in case it is run time loaded in
// the future.
namespace Nova.Common
{
    /// <summary>
    /// <para>
    /// Lesser racial trait (LRT) definitions/data. Each race can have several LRTs.
    /// This data may be replaced by runtime loaded data in the future. Use AllTraits
    /// to access this data.
    /// </para>
    /// <para>
    /// The cost per trait is (negative numbers mean that
    /// you gain those advantage points for selecting that trait):
    /// <list type="table">
    /// <listheader><term>Trait</term><description>Cost</description></listheader>
    /// <item><term>Improved Fuel Efficiency</term><description>78 pts.</description></item>
    /// <item><term>Total Terraforming</term><description>140 pts.</description></item>
    /// <item><term>Advanced Remote Mining</term><description>53 pts.</description></item>
    /// <item><term>Improved Starbases</term><description>67 pts.</description></item>
    /// <item><term>Generalized Research</term><description>-13 pts.</description></item>
    /// <item><term>Ultimate Recycling</term><description>80 pts.</description></item>
    /// <item><term>Mineral Alchemy</term><description>51 pts.</description></item>
    /// <item><term>Cheap Factories</term><description>58 pts.</description></item>
    /// <item><term>No Ram Scoop Engines</term><description>-53 pts.</description></item>
    /// <item><term>Cheap Engines</term><description>80 pts.</description></item>
    /// <item><term>Only Basic Remote Mining</term><description>-85 pts.</description></item>
    /// <item><term>No Advanced Scanners</term><description>-95 pts.</description></item>
    /// <item><term>Low Starting Population</term><description>-60 pts.</description></item>
    /// <item><term>Bleeding Edge Technology</term><description>-23 pts.</description></item>
    /// <item><term>Regenerating Shields</term><description>-10 pts.</description></item>
    /// <item><term>Extra Tech Start at L4</term><description>-180 pts.</description></item>
    /// </list>
    /// </para>
    /// </summary>
    public static class SecondaryTraits
    {
        private const string ImprovedFuelEfficiencyDescription = "This gives you the Fuel Mizer and Galaxy Scoop engines and increases your starting propulsion tech by 1 level. All engines use 15% less fuel";
        private const string TotalTerraformingDescription = "Allows you to terraform by investing solely in Biotechnology  You may terraform a variable up to 30%.  Terraforming costs 30% less.";
        private const string AdvancedRemoteMiningDescription = "Gives you three additional mining hulls and two new robots. You will start the game with two Midget Miners. Do not pick Only Basic Remote Mining with this option (or you will get no benefit from this selection).";
        private const string ImprovedStarBasesDescription = "Gives you two new starbase designs. The Stardock allows you to build light ships. The Ultra Station is a formidable weapons platform. Your starbases cost 20% less and are 20% cloaked.";
        private const string GeneralisedResearchDescription = "Your race takes a holistic approach to research, Only half of the resources dedicated to research will be applied to the current field of research. However, 15% of the total will be applied to all other fields.";
        private const string UltimateRecyclingDescription = "When you scarp a fleet at a starbase you recover 90% of the minerals and some of the resources. The resources are available for use the following year. Scrapping a fleet at a planet gives half the starbase amount.";
        private const string MineralAlchemyDescription = "Allows you turn resources into minerals four times more efficiently than other races. This may be performed at any planet you own.";
        private const string CheapFactoriesDescription = "Factories cost 1kT less Germanium to build.";
        private const string NoRamScoopEnginesDescription = "No engines which travel at warp 5 or greater burning no fuel will be available. However, the Interspace 10 engine will be available. This drive will travel at warp 10 without taking damage.";
        private const string CheapEnginesDecription = "You can throw together engines at half cost. However, at speeds greater than wap 6 there is a 10% chance the engines will not engage. You start with a propulsion one level higher than you would otherwise.";
        private const string OnlyBasicRemoteMiningDescription = "The only mining ship available to you will be the Mini-Miner.This trait overrides Advanced Remote Mining. Your maximum population per planet is increased by 10%.";
        private const string NoAdvancedScannersDecription = "No planet penetrating scanners will be available to you. However, conventional scanners will have their range doubled.";
        private const string LowStartingPopulationDescription = "You start with 30% fewer colonists.";
        private const string BleedingEdgeTechnologyDescription = "New techs initially cost twice as much to build. As soon as you exceed all of the tech requirements by one level the cost drops back to normal. Miniaturization occurs at 5% a level and pegs at 80%.";
        private const string RegeneratingShieldsDescription = "All shields are 40% stronger than the listed rating. Shields regenerate 10% of maxium strength after every round of battle. However, your armor will only be 50% of its rated strength.";
        private const string ExtraCostStartLevel4Description = "All Techs with 75% extra Costs start at Tech Level 3, for JOAT at Tech Level 4.";

        public static readonly TraitEntry[] Traits = new TraitEntry[]
            {
                new TraitEntry("Improved Fuel Efficiency", "IFE", 78, ImprovedFuelEfficiencyDescription), 
                new TraitEntry("Total Terraforming", "TT", 140, TotalTerraformingDescription),
                new TraitEntry("Advanced Remote Mining", "ARM", 53, AdvancedRemoteMiningDescription),
                new TraitEntry("Improved Star Bases", "ISB", 67, ImprovedStarBasesDescription),
                new TraitEntry("Generalised Research", "GR", 67, GeneralisedResearchDescription),
                new TraitEntry("Ultimate Recycling", "UR", 80, UltimateRecyclingDescription),
                new TraitEntry("Mineral Alchemy", "MA", 51, MineralAlchemyDescription),
                new TraitEntry("Cheap Factories", "CF", 58, CheapFactoriesDescription), // This is not a normal LRT!
                new TraitEntry("No Ram Scoop Engines", "NRS", -53, NoRamScoopEnginesDescription),
                new TraitEntry("Cheap Engines", "CE", -80, CheapEnginesDecription),
                new TraitEntry("Only Basic Remote Mining", "OBRM", -85, OnlyBasicRemoteMiningDescription),
                new TraitEntry("No Advanced Scanners", "NAS", -95, NoAdvancedScannersDecription),
                new TraitEntry("Low Starting Population", "LSP", -60, LowStartingPopulationDescription),
                new TraitEntry("Bleeding Edge Technology", "BET", -23, BleedingEdgeTechnologyDescription),
                new TraitEntry("Regenerating Shields", "RS", -10, RegeneratingShieldsDescription),
                new TraitEntry("Extra Tech", "ExtraTech", -10, RegeneratingShieldsDescription) // This is not a normal LRT!
          };
   }
}

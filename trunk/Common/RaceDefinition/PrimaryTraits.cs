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
    /// Primary racial trait (PRT) definitions/data. Each race has exactly one PRT.
    /// This data may be replaced by runtime loaded data in the future. Use AllTraits
    /// to access this data.
    /// </para>
    /// <para>
    /// The race costs are:
    /// <list type="table">
    /// <listheader><term>Trait</term><description>Cost</description></listheader>
    /// <item><term>Hyper-Expansion</term><description>63 pts.</description></item>
    /// <item><term>Super-Stealth</term><description>81 pts.</description></item>
    /// <item><term>War Monger</term><description>65 pts.</description></item>
    /// <item><term>Claim Adjuster</term><description>53 pts.</description></item>
    /// <item><term>Inner-Strength</term><description>17 pts.</description></item>
    /// <item><term>Space Demolition</term><description>0 pts.</description></item>
    /// <item><term>Packet Physics</term><description>90 pts.</description></item>
    /// <item><term>Inter-stellar Traveller</term><description>110 pts.</description></item>
    /// <item><term>Alternate Reality</term><description>80 pts.</description></item>
    /// <item><term>Jack of all Trades</term><description>28 pts.</description></item>
    /// </list>
    /// </para>
    /// </summary>
    public static class PrimaryTraits
    {
        private const string HyperExpansionDescription = "You must expand to survive. You are given small and cheap colony hull and an engine which travels at warp 6 using no fuel. Your race will grow at twice the growth rate you choose. However, the maximum population for a given planet is cut in half. The completely flexible Meta Morph hull will only be available to your race (at construction research level 10).";
        private const string SuperStealthDescription = "You can sneak through enemy territory and execute stunning surprise attacks. You are given top-drawer cloaks and all your ships have 75% cloaking built in Cargo does not decrease your cloaking abilities. The Stealth Bomber and Rogue are at your disposal as are a scanner, shield and armor with stealthy properties. Two scanners which allow you to steal minerals from enemy fleets and planets are also available. You may safely travel through mine fields at one warp factorfaster than the limits.";
        private const string WarMongerDescription = "You rule the battlefield. Your colonists attack better, your ships are faster in battle and you build weapons 25% cheaper than other races You start the game with a knowledge of Tech 6 weapons and Tech 1 in Energy and propulsion. Unfortunately, your race doesn\'t understand the necessity of building any but the most basic planetary Defenses and cannot lay mine fields.";
        private const string ClaimAdjusterDescription = "You are an expert at fiddling with planetary environments. You start the game with Tech 6 in Biotechnology and a ship capable of terraforming planets from orbit. You can arm your ships with bombs that unterraform enemy worlds. Terraforming costs you nothing and planets you leave revert to their original environments. Planets you own have up to a 10% chance of permanently improving an environment variable by 1% per year.";
        private const string InnerStrengthDescription = "You are hard to defeat. Your colonists repel attacks better, your ships heal faster, you have special battle devices that protect your ships and can lay Speed Trap mine fields. You have a device that acts as a shield and armorYour peace-loving people refuse to build Smart Bombs. Planetary Defenses cost you 40% less, although weapons cost you 25% more. Your colonists are able to reproduce while being transported by your fleets.";
        private const string SpaceDemolitionDescription = "You are an expert in laying mine fields. You have a vast array of mine types at your disposal and two unique hull designs which are made for mine dispersal. Your mine fields act as scanner and you have the ability to remotely detonate your own standard mine fields. You may safely travel two  warp speeds faster than the stated limits through enemy mine fields. You start the game with 2 mine laying ships amd Tech 2 in propulsion and BioTech.";
        private const string PacketPhysicsDescription = "Your race excels at accelerating mineral packets to distant planets. You start with a warp 5 accelerator at your home starbase and Tech 4 Energy. You will eventually be able to fling packets at the mind numbing speed of warp 13. You can fling smaller packets and all of your packets have penetrating scanners embedded in them. Packets you fling that aren\'t fully caught have a chance  of terraforming the target planet.";
        private const string InterStellarTravellerDescription = "Your race excels in building stargates. You start with Tech 5 in propulsion and construction. Both planets have stargates. Eventually you may build stargates which have unlimited capabilities. Stargatescost you 25% less to build. Your race can automatically scan any enemy planet with a stargate which is in range of one of your stargates. Exceeding the safety limits of stargates is less likely to destroy your ships.";
        private const string AlternateRealityDescription = "Your race developed in an  alternate plane. Your people cannot survive on  planets and live in orbit on your starbases, which are 20% cheaper to build. You cannot build planetary installations, but your people have an intrinsic ability to mine and scan for enemy fleets. You can remote mine your own worlds. If a starbase is destroyed all your colonists orbiting that world are killed. Your population maximums are determined by the type of starbase you have. You will eventually be able to build the death star.";
        private const string JackOfAllTradesDescription = "Your race does not specialise in a single area. You start the game with Tech 3 in all areas and the components associated with those levels are available to you (including a penetrating scanner). Your maximum planetary population is 20% greater than other races.";

        public static readonly TraitEntry[] Traits = new TraitEntry[]
            {
                new TraitEntry("Hyper Expansion", "HE", HyperExpansionDescription),
                new TraitEntry("Super Stealth", "SS", SuperStealthDescription),
                new TraitEntry("War Monger", "WM", WarMongerDescription),
                new TraitEntry("Claim Adjuster", "CA", ClaimAdjusterDescription),
                new TraitEntry("Inner Strength", "IS", InnerStrengthDescription),
                new TraitEntry("Space Demolition", "SD", SpaceDemolitionDescription),
                new TraitEntry("Packet Physics", "PP", PacketPhysicsDescription),
                new TraitEntry("Inter-Stellar Traveller", "IT", InterStellarTravellerDescription),
                new TraitEntry("Alternate Reality", "AR", AlternateRealityDescription),
                new TraitEntry("Jack of all Trades", "JOAT", JackOfAllTradesDescription)
            };
    }
}

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
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
//
// Primary racial trait definitions/data. Each race has exactly one Primary 
// racial trait.
//
// This data may be replaced by runtime loaded data in the future. Use AllTraits
// to access this data.
//
// The race costs are:
//
// Hyper-Expansion     63 pts.     Space Demolition             0 pts.
// Super-Stealth       81 pts.     Packet Physics              90 pts.
// War Monger          65 pts.     Inter-stellar Traveller    110 pts.
// Claim Adjuster      53 pts.     Alternate Reality           80 pts.
// Inner-Strength      17 pts.     Jack of all Trades          28 pts.
// ===========================================================================
#endregion

using System;

// The TraitData namespace should only be accessed by AllTraits. Other objects
// should access this data through AllTraits in case it is run time loaded in
// the future.
namespace NovaCommon.TraitData
{

// ============================================================================
// Static definition of all the primary racial traits
// ============================================================================
    public static class PrimaryTraits
    {
      public static TraitEntry[] Traits = new TraitEntry[10] {

// ============================================================================
// Hyper Expansion 
// ============================================================================

         new TraitEntry("Hyper Expansion", "HE", 63,
           "You must expand to survive. You are "
         + "given small and cheap colony hull and an engine which travels "
         + "at warp 6 using no fuel. Your race will grow at twice the "
         + "growth rate you choose. However, "
         + "the maximum population for a given planet is cut in half. "
         + "The completely flexible Meta Morph hull will only be "
         + "available to your race (at construction research level 10)."),

// ============================================================================
// Super Stealth
// ============================================================================

         new TraitEntry("Super Stealth", "SS", 81, 
           "You can sneak through enemy "
         + "territory and execute stunning surprise attacks. "
         + "You are given top-drawer cloaks and all your ships "
         + "have 75% cloaking built in Cargo does not decrease "
         + "your cloaking abilities. The Stealth Bomber and "
         + "Rogue are at your disposal as are a scanner, shield "
         + "and armor with stealthy properties. Two scanners "
         + "which allow you to steal minerals from enemy fleets "
         + "and planets are also available. You may safely travel "
         + "through mine fields at one warp factorfaster than the "
         + "limits."),

// ============================================================================
// War Monger
// ============================================================================

         new TraitEntry("War Monger", "WM", 65,
           "You rule the batle field. Your "
         + "colonists attack better, your ships are faster in battle "
         + "and you build weapons 25% cheaper than other races "
         + "You start the game with a knowledge of Tech 6 weapons "
         + "and Tech 1 in Energy and propulsion. Unfortunately, "
         + "your race doesn't understand the necessity of "
         + "building any but the most basic "
         + "planetary Defenses and cannot lay mine fields."),

// ============================================================================
// Claim Adjuster
// ============================================================================

         new TraitEntry("Claim Adjuster", "CA", 53,
           "You are an expert at fiddling with "
         + "planetary environments. You start the game with Tech 6 in "
         + "Biotechnology and a ship capable of terraforming planets "
         + "from orbit. You can arm your ships with bombs that "
         + "unterraform enemy worlds. Terraforming costs you nothing "
         + "and planets you leave revert to their original "
         + "environments. Planets you own have up to a 10% chance "
         + "of permanently improving an environment variable by "
         + "1% per year."),

// ============================================================================
// Inner Strength
// ============================================================================

         new TraitEntry("Inner Strength", "IS", 17,
           "You are hard to defeat. Your "
         + "colonists repel attacks better, your ships heal faster, "
         + "you have special battle devices that protect your ships "
         + "and can lay Speed Trap mine fields. You have a device that "
         + "acts as a shield and armor"
         + "Your peace-loving people refuse to build Smart Bombs. "
         + "Planetary Defenses cost you 40% less, although weapons "
         + "cost you 25% more. Your colonists are able to reproduce "
         + "while being transported by your fleets."),

// ============================================================================
// Space Demolition
// ============================================================================

         new TraitEntry("Space Demolition", "SD", 0,
           "You are an expert in laying mine "
         + "fields. You have a vast array of mine types at your "
         + "disposal and two unique hull designs which are made "
         + "for mine dispersal. Your mine fields act as scanner "
         + "and you have the ability to remotely detonate your "
         + "own standard mine fields. You may safely travel two "
         + " warp speeds faster than the stated limits "
         + "through enemy mine fields. You start the game with 2 mine "
         + "laying ships amd Tech 2 in propulsion and BioTech."),


// ============================================================================
// Packet Physics
// ============================================================================

         new TraitEntry("Packet Physics", "PP", 90,
           "Your race excels at accelerating "
         + "mineral packets to distant planets. You start with a "
         + "warp 5 accelerator at your home starbase and Tech 4 "
         + "Energy. You will eventually be able to fling packets "
         + "at the mind numbing speed of warp 13. You can fling "
         + "smaller packets and all of your packets have penetrating "
         + "scanners embedded in them. Packets you fling "
         + "that aren't fully caught have a chance  of terraforming "
         + "the target planet."),

// ============================================================================
// Inter-stellar traveller
// ============================================================================

         new TraitEntry("Inter-Stellar Traveller", "IT", 110,
           "Your race excels in building "
         + "stargates. You start with Tech 5 in propulsion and "
         + "construction. "
         + "Both planets have stargates. Eventually "
         + "you may build stargates which have unlimited capabilities. "
         + "Stargatescost you 25% less to build. Your race can "
         + "automatically scan any enemy planet with a stargate which "
         + "is in range of one of your stargates. Exceeding the safety "
         + "limits of stargates is less likely to destroy your ships."),

// ============================================================================
// Alternate Reality
// ============================================================================

         new TraitEntry("Alternate Reality", "AR", 80,
           "Your race developed in an "
         + " alternate plane. Your people cannot survive on "
         + " planets and live in orbit on your starbases, which "
         + "are 20% cheaper to build. You cannot "
         + "build planetary installations, but your people have an "
         + "intrinsic ability to mine and scan for enemy fleets. "
         + "You can remote mine your own worlds. If a starbase is "
         + "destroyed all your colonists orbiting that world are "
         + "killed. Your population maximums are determined by the "
         + "type of starbase you have. You will eventually be "
         + "able to build the death star."),

// ============================================================================
// Jack of all Trades
// ============================================================================

         new TraitEntry("Jack of all Trades", "JOAT", 28,
           "Your race does not specialise in a single area. "
         + "You start the game with Tech 3 in all areas "
         + "and the components associated with those levels are available "
         + "to you (including a penetrating scanner). "
         + "Your maximum planetary population is 20% greater than other "
         + "races.")
      };
   }
}


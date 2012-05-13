﻿#region Copyright Notice
// ============================================================================
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

namespace Nova.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This class maintains a (singleton) list of all Traits.
    /// Currently trait data is staticly defined in PrimaryTraits and SecondaryTraits
    /// but may later be loaded at run time. All access to this data should be through
    /// this object.
    /// </summary>
    public sealed class AllTraits 
    {
        public TraitList All = new TraitList();
        public TraitList Primary = new TraitList();
        public TraitList Secondary = new TraitList();

        private static readonly object Padlock = new object();
        private static AllTraits instance;

        /// <summary>
        /// Private constructor to prevent anyone else creating instances of this class.
        /// </summary>
        private AllTraits()
        {
            foreach (TraitEntry trait in PrimaryTraits.Traits)
            {
                All.Add(trait);
                Primary.Add(trait);
            }

            foreach (TraitEntry trait in SecondaryTraits.Traits)
            {
                All.Add(trait);
                Secondary.Add(trait);
            }
        }

        /// <summary>
        /// Provide a mechanism of accessing the single instance of this class that we
        /// will create locally. Creation of the data is thread-safe.
        /// </summary>
        public static AllTraits Data
        {
            get
            {
                if (instance == null)
                {
                    lock (Padlock)
                    {
                        if (instance == null)
                        {
                            instance = new AllTraits();
                        }
                    }
                }
                return instance;
            }

            // ----------------------------------------------------------------------------

            set
            {
                instance = value;
            }
        }

        public const int NumberOfPrimaryRacialTraits = 10;
        public const int NumberOfSecondaryRacialTraits = 14;
      
        /// <summary>
        /// Provide a list of all trait keys. 
        /// These can be used to index AllTraits.Data.All or in a foreach loop.
        /// </summary>
        public static string[] TraitKeys = 
        {
            // 10 PRTs
            "HE", "SS", "WM", "CA", "IS", "SD", "PP", "IT", "AR", "JOAT",
            // 14 LRTs
            "IFE", "TT", "ARM", "ISB", "GR", "UR", "MA", "NRSE", "OBRM", "CE", "NAS", "LSP", "BET", "RS"
        };

        /// <summary>
        /// Provide a list of all the trait names. This can be used to get the printable name of a trait.
        /// </summary>
        public static string[] TraitString = 
        {
            // 10 PRTs
            "Hyper Expansion", 
            "Super Stealth", 
            "War Monger", 
            "Claim Adjuster", 
            "Inner Strength", 
            "Space Demolition", 
            "Packet Pysics", 
            "Interstellar Traveler", 
            "Artificial Reality", 
            "Jack of all Trades",
            // 14 LRTs
            "Improved Fuel Efficiency", 
            "Total Terraforming", 
            "Advanced Remote Mining", 
            "Improved Star Bases", 
            "Generalised Research", 
            "Ultimate Recycling", 
            "Mineral Alchemy", 
            "No Ram Scoop Engines", 
            "Cheap Engines",
            "Only Basic Remote Mining", 
            "No Advanced Scanners", 
            "Low Starting Population", 
            "Bleeding Edge Technology", 
            "Regenerating Shields"
        };


    }
}

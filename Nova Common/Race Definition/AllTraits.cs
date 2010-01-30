// ============================================================================
// Nova. (c) 2009 Daniel Vale
//
// This module maintains a (singleton) list of all Traits.
// Currently trait data is staticly defined in PrimaryTraits and SecondaryTraits
// but may later be loaded at run time. All access to this data should be through
// this object.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using NovaCommon.TraitData;

namespace NovaCommon
{
    public sealed class AllTraits 
    {
        public TraitList All = new TraitList();
        public TraitList Primary = new TraitList();
        public TraitList Secondary = new TraitList();

        private static AllTraits Instance = null;
        private static Object Padlock = new Object();

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

        public static AllTraits Data
        {
            get
            {
                if (Instance == null)
                {
                    lock (Padlock)
                    {
                        if (Instance == null)
                        {
                            Instance = new AllTraits();
                        }
                    }
                }
                return Instance;
            }

            // ----------------------------------------------------------------------------

            set
            {
                Instance = value;
            }
        }

        public const int NUMBER_OF_PRIMARY_RACIAL_TRAITS = 10;
        public const int NUMBER_OF_SECONDARY_RACIAL_TRAITS = 13;
       
        public static string[] TraitKeys = 
        {
            // 10 PRTs
            "HE", "SS", "WM", "CA", "IS", "SD", "PP", "IT", "AR", "JOAT",
            // 14 LRTs
            "IFE", "TT", "ARM", "ISB", "GR", "UR", "MA", "NRSE", "OBRM", "CE", "NAS", "LSP", "BET", "RS"
        };

        public static string[] TraitString = 
        {
            // 10 PRTs
            "Hyper Expansion", 
            "Supper Stealth", 
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
            "No Ram Scoop Eengines", 
            "Cheap Engines",
            "Only Basic Remote Mining", 
            "No Advanced Scanners", 
            "Low Starting Population", 
            "Bleeding Edge Technology", 
            "Regenerating Shields"
        };

    }

}

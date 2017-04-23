#region Copyright Notice
// ============================================================================
// Copyright (C) 2012 The Stars-Nova Project
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
    using System.Linq;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.DataStructures;

    /// <summary>
    /// A special Fleet used in the battle engine and battle viewer. It contains
    /// only one ShipToken of Quantity ships of a single design, and holds the key
    /// of the Fleet that spawned it.
    /// </summary>
    public class Stack : Fleet
    {
        /// <summary>
        /// Gets or sets the Stack to target in battle.
        /// </summary>
        public Stack Target
        {
            get;
            set;
        }
        
        /// <summary>
        /// The Key of the Fleet which originated this Stack.
        /// </summary>
        public long ParentKey
        {
            get;
            private set;            
        }
        
        /// <summary>
        /// Returns this Stack's battle speed.
        /// </summary>
        public double BattleSpeed
        {
            get
            {
                return Token.Design.BattleSpeed;
            }
        }

        /// <summary>
        /// Return the current Defense capability of a stack.
        /// </summary>
        public double Defenses
        {
            get
            {
                 return Token.Armor + Token.Shields;
            }
        }

        /// <summary>
        /// Quick check if a Stack has been destroyed.
        /// </summary>
        public bool IsDestroyed
        {
            get
            {
                return (Token == null || Token.Quantity <= 0 || Token.Armor <= 0);
            }
        }

        /// <summary>
        /// Sets or Gets the single ShipToken this Stack is allowed to have. This is a reference so the Fleet token is modified when this token is modified.
        /// </summary>
        public ShipToken Token
        {
            get
            {
                if (Composition.Count == 0) return null;

                return Composition.First().Value;
            }
            
            private set
            {
                Composition.Clear();
                Composition.Add(value.Key, value);
            }
        }
        
        /// <summary>
        /// Generates a Stack from a fleet and a specified ShipToken.
        /// </summary>
        /// <param name="fleet">Parent Fleet</param>
        /// <param name="stackId">Unique Battle Engine ID</param>
        /// <param name="token">Shiptoken for this Stack</param>
        public Stack(Fleet fleet, uint stackId, ShipToken token)
            : base(fleet)
        {
            Id = stackId;
            ParentKey = fleet.Key;
            Name = "Stack #" + stackId.ToString(System.Globalization.CultureInfo.InvariantCulture);
            BattlePlan = fleet.BattlePlan;
            InOrbit = fleet.InOrbit;
            Token = token;  // note this is a reference to the actual token in the fleet
        }
        
        /// <summary>
        /// Copy constructor. This is only used by the battle engine so only the fields
        /// used by it in creating stacks need to be copied. Note that we copy the
        /// token as well. Be careful when using the copy; It is a different object.
        /// </summary>
        /// <param name="copy">The fleet to copy.</param>
        /// <remarks>
        /// Why are we copying Stacks? 
        /// For the battle viewer, copies are required so the originals are not destroyed in the battle report. this allows the battle to be replayed multiple times.
        /// In the battle engine, copies are required to create the battle report, so all stacks present at the start of the battle are represented.
        /// </remarks>
        public Stack(Stack copy)
            : base(copy)
        {
            ParentKey = copy.ParentKey;
            Key = copy.Key;
            BattlePlan = copy.BattlePlan;            
            Target = copy.Target;
            InOrbit = copy.InOrbit;
            Token = new ShipToken(copy.Token.Design, copy.Token.Quantity, copy.Token.Armor); 
        }
        
        /// <summary>
        /// Load: Initialising constructor to load a Stack from an XmlNode (save file).
        /// </summary>
        /// <param name="node">An XmlNode representing the Stack.</param>
        public Stack(XmlNode node)
            : base(node)
        {
        
        }
    }
}

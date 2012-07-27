#region Copyright Notice
// ============================================================================
// Copyright (C) 2008 Ken Reed
// Copyright (C) 2009-2012 The Stars-Nova Project
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

namespace Nova.Common.Waypoints
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml;
    
    using Nova.Common;
    using Nova.Common.Components;
    
    /// <summary>
    /// Performs Star Colonisation.
    /// </summary>
    public class SplitMergeTask : IWaypointTask
    {           
        private List<Message> messages = new List<Message>();
        
        /// <inheritdoc />
        public List<Message> Messages
        {
            get{return messages;}
        }
        
        /// <inheritdoc />
        public string Name
        {
            get {
                if (OtherFleetKey == 0)
                {
                    return "Split Fleet";
                }
                else
                {
                    return "Merge Fleet";
                }
            }
        }
        
        /// <summary>
        /// Cargo object representing the amount to Load or Unload
        /// </summary>
        public Dictionary<long, ShipToken> LeftComposition {get; set;}
        
        /// <summary>
        /// Load or Unload cargo. Mixed operations are represented by more than one Task.
        /// </summary>
        public Dictionary<long, ShipToken> RightComposition {get; set;}
        
        public long OtherFleetKey {get; set;}
        
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SplitMergeTask(Dictionary<long, ShipToken> leftComposition, Dictionary<long, ShipToken> rightComposition, long otherFleetKey = 0)
        {
            LeftComposition = leftComposition;            
            RightComposition = rightComposition;
            OtherFleetKey = otherFleetKey;
        }
        
        
        /// <summary>
        /// Copy Constructor.
        /// </summary>
        /// <param name="other">SplitTask to copy</param>
        public SplitMergeTask(SplitMergeTask copy)
        {
            LeftComposition = new Dictionary<long, ShipToken>(copy.LeftComposition);            
            RightComposition = new Dictionary<long, ShipToken>(copy.RightComposition);
            OtherFleetKey = copy.OtherFleetKey;
        }
        
        
        /// <summary>
        /// Load: Read an object of this class from and XmlNode representation.
        /// </summary>
        /// <param name="node">An XmlNode containing a representation of this object</param>
        public SplitMergeTask(XmlNode node)
        {
            if (node == null)
            {
                return;
            }
            
            LeftComposition = new Dictionary<long, ShipToken>();
            RightComposition = new Dictionary<long, ShipToken>();
            
            XmlNode mainNode = node.FirstChild;
            XmlNode subNode;
            ShipToken token;
            while (mainNode != null)
            {
                try
                {
                    subNode = mainNode.FirstChild;
                    
                    switch (mainNode.Name.ToLower())
                    {                            
                        case "rightkey":
                            OtherFleetKey = long.Parse(subNode.Value, System.Globalization.NumberStyles.HexNumber);
                            break;
                            
                        case "leftcomposition":
                            while (subNode != null)
                            {
                                token = new ShipToken(subNode);
                                LeftComposition.Add(token.Key, token);
                                subNode = subNode.NextSibling;
                            }
                            break;
                            
                        case "rightcomposition":
                            while (subNode != null)
                            {
                                token = new ShipToken(subNode);
                                RightComposition.Add(token.Key, token);
                                subNode = subNode.NextSibling;
                            }
                            break;                             
                    }
                }
                catch (Exception e)
                {
                    Report.Error(e.Message);
                }
                mainNode = mainNode.NextSibling;
            }
        }
        
        
        /// <inheritdoc />
        public XmlElement ToXml(XmlDocument xmldoc)
        {
            XmlElement xmlelTask = xmldoc.CreateElement("SplitMergeTask");            
            
            // We are either merging or splitting. For a merge we need two keys (one comes from this waypoint's owner)
            // and for a split, one key and 2 compositions. Avoid extra XML clutter.
            if (OtherFleetKey != 0)
            {
                Global.SaveData(xmldoc, xmlelTask, "RightKey", OtherFleetKey.ToString("X"));
            }
            else
            {            
                XmlElement xmlelLeft = xmldoc.CreateElement("LeftComposition");
                foreach (ShipToken token in LeftComposition.Values)
                {
                    xmlelLeft.AppendChild(token.ToXml(xmldoc));
                }            
                xmlelTask.AppendChild(xmlelLeft);
                
                XmlElement xmlelRight = xmldoc.CreateElement("RightComposition");
                foreach (ShipToken token in RightComposition.Values)
                {
                    xmlelRight.AppendChild(token.ToXml(xmldoc));
                }            
                xmlelTask.AppendChild(xmlelRight);
            }
            
            return xmlelTask;
        }

        
        /// <inheritdoc />
        public bool isValid(Fleet fleet, Mappable target, EmpireData sender, EmpireData receiver)
        {
            // fleet is the original fleet to split, or the recipient of a merge.
            // target (as Fleet) is the new fleet in a split, or the fleet to be merged into FirstFleet.           
            
            //TODO (priority 5) - Validate:
            // fleet & target valid vs sender versions
            // FirstFleet cargo + SecondFleet cargo = original cargo from either fleet + target or senders's fleets.
            return true;
        }
        
        
        /// <inheritdoc />
        public bool Perform(Fleet fleet, Mappable target, EmpireData sender, EmpireData receiver)
        { 
            Fleet secondFleet = null;
            
            // Look for an appropiate fleet for a merge.
            if (OtherFleetKey != 0)
            {
                // This allows to merge with other empires if desired at some point.
                if(receiver.OwnedFleets.ContainsKey(OtherFleetKey))
                {
                    secondFleet = receiver.OwnedFleets[OtherFleetKey];
                }
                else if (sender.OwnedFleets.ContainsKey(OtherFleetKey)) // Do we own the key?
                {
                    secondFleet = sender.OwnedFleets[OtherFleetKey];    
                }
            }
            
            // Found fleet => Merge            
            if (secondFleet != null)
            {
                MergeFleets(fleet, secondFleet);
            }
            else
            {
                // Else it's a split. Need a new fleet so clone original
                // and change stuff.
                secondFleet = sender.MakeNewFleet(fleet);
            
                ReassignShips(fleet, secondFleet);

                // Now send new Fleets to limbo pending inclusion.
                sender.TemporaryFleets.Add(secondFleet);                
            }

            return true;            
        }
                
        
        /// <summary>
        /// Reassign Fleet compositions and Cargo
        /// </summary>
        /// <param name="left">Original Fleet</param>
        /// <param name="right">New Fleet</param>
        private void ReassignShips(Fleet left, Fleet right)
        {            
            foreach (long key in LeftComposition.Keys)
            {
                int leftNewCount = LeftComposition[key].Quantity;
                int leftOldCount = left.Composition.ContainsKey(key) ? left.Composition[key].Quantity : 0;                

                if (leftNewCount == leftOldCount)
                {
                    continue; // no moves of this design
                }

                Fleet from;
                Fleet to;
                int moveCount;
                
                if (leftNewCount > leftOldCount)
                {
                    from = right;
                    to = left;
                    moveCount = leftNewCount - leftOldCount;
                }
                else
                {
                    from = left;
                    to = right;
                    moveCount = leftOldCount - leftNewCount;
                }
                
                if (!to.Composition.ContainsKey(key))
                {
                    to.Composition.Add(key, new ShipToken(from.Composition[key].Design, 0));
                }
                
                to.Composition[key].Quantity += moveCount;
                from.Composition[key].Quantity -= moveCount;
                
                if (from.Composition[key].Quantity == 0)
                {
                    from.Composition.Remove(key);
                }
                
                ReassignCargo(left, right);
            }            
        }
        
        
        /// <summary>
        /// Used to merge fleets. Much cruder than ReassignShips, but requires
        /// less information.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void MergeFleets(Fleet left, Fleet right)
        {
            foreach (ShipToken token in right.Composition.Values)
            {
                if (!left.Composition.ContainsKey(token.Key))
                {
                    left.Composition.Add(token.Key, token);
                }
                else
                {
                    left.Composition[token.Key].Quantity += token.Quantity;
                    left.Composition[token.Key].Armor += token.Armor; //FIXME (priority 6) - How do we average merged damaged tokens? 
                }
            }
            
            right.Composition.Clear();
            left.Cargo.Add(right.Cargo);
        }
        
        
        /// <summary>
        /// Used to redistribute cargo once fleets are split.
        /// </summary>
        private void ReassignCargo(Fleet left, Fleet right)
        {
            // Ships are moved. Now to reassign fuel/cargo
            int ktToMove = 0;
            Cargo fromCargo = left.Cargo;
            Cargo toCargo = right.Cargo;
            if (left.Cargo.Mass > left.TotalCargoCapacity)
            {
                fromCargo = left.Cargo;
                toCargo = right.Cargo;
                ktToMove = left.Cargo.Mass - left.TotalCargoCapacity;
            }
            else if (right.Cargo.Mass > right.TotalCargoCapacity)
            {
                fromCargo = right.Cargo;
                toCargo = left.Cargo;
                ktToMove = right.Cargo.Mass - right.TotalCargoCapacity;
            }

            double proportion = (double)ktToMove / fromCargo.Mass;
            if (ktToMove > 0)
            {
                // Try and move cargo
                int ironToMove = (int)Math.Ceiling(fromCargo.Ironium * proportion);
                if (ironToMove > ktToMove)
                {
                    ironToMove = ktToMove;
                }
                toCargo.Ironium += ironToMove;
                fromCargo.Ironium -= ironToMove;
                ktToMove -= ironToMove;
            }
            if (ktToMove > 0)
            {
                // Try and move cargo
                int borToMove = (int)Math.Ceiling(fromCargo.Boranium * proportion);
                if (borToMove > ktToMove)
                {
                    borToMove = ktToMove;
                }
                toCargo.Boranium += borToMove;
                fromCargo.Boranium -= borToMove;
                ktToMove -= borToMove;
            }
            if (ktToMove > 0)
            {
                // Try and move cargo
                int germToMove = (int)Math.Ceiling(fromCargo.Germanium * proportion);
                if (germToMove > ktToMove)
                {
                    germToMove = ktToMove;
                }
                toCargo.Germanium += germToMove;
                fromCargo.Germanium -= germToMove;
                ktToMove -= germToMove;
            }
            if (ktToMove > 0)
            {
                // Try and move cargo
                int colonistsToMoveInKilotons = (int)Math.Ceiling(fromCargo.ColonistsInKilotons * proportion);
                if (colonistsToMoveInKilotons > ktToMove)
                {
                    colonistsToMoveInKilotons = ktToMove;
                }
                toCargo.ColonistsInKilotons += colonistsToMoveInKilotons;
                fromCargo.ColonistsInKilotons -= colonistsToMoveInKilotons;
                ktToMove -= colonistsToMoveInKilotons;
            }
            Debug.Assert(ktToMove == 0, "Must not be negative.");

            // fuel
            if (left.FuelAvailable > left.TotalFuelCapacity)
            {
                // Move excess to right and set left to max
                right.FuelAvailable += left.FuelAvailable - left.TotalFuelCapacity;
                left.FuelAvailable = left.TotalFuelCapacity;
            }
            else if (right.FuelAvailable > right.TotalFuelCapacity)
            {
                // Move excess to left and set right to max
                left.FuelAvailable += right.FuelAvailable - right.TotalFuelCapacity;
                right.FuelAvailable = right.TotalFuelCapacity;
            }
        }
    }
}

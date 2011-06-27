#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
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

namespace Nova.Server.TurnSteps
{
    using System;
    using System.Collections.Generic;
    
    using Nova.Common;
    
    /// <summary>
    /// This step updates intel with scanning information.
    /// </summary>
    public class ScanStep : ITurnStep
    {
        private ServerState stateData;
        
        public ScanStep()
        {
        }
        
        public void Process(ServerState stateData)
        {
            foreach (EmpireData empire in stateData.AllEmpires.Values)
            {
                this.stateData = stateData;
                
                DetermineIntel(empire);
            }
            
        }
        
        /// <Summary>
        /// Build a list of all fleets that are visible to the player.
        /// </Summary>
        /// <remarks>
        /// This consists of:
        /// (1) Fleets owned by the player
        /// (2) Fleets within the range of scanners on ships owned by the player
        /// (3) Fleets within the range of scanners on planets owned by the player
        ///
        /// </remarks>
        private void DetermineIntel(EmpireData empire)
        {
            // First pass intel. Gets Owned Stars.
            foreach (Star star in stateData.AllStars.Values)
            {
                // If star has no report (First turn) add it. Else
                // update inherent visibility.
                if (!empire.StarReports.Contains(star.Name))
                {
                    empire.StarReports.Add(new StarIntel(star, IntelLevel.None));
                }
                else
                {
                    // FIXME:(priority 6) If two empires use the same race&name, this gives both
                    // the same intel access to each other's stars. Implement EmpireID
                    if (star.Owner == empire.EmpireRace.Name)
                    {
                        empire.StarReports[star.Name].Update(star, IntelLevel.Owned);                    
                    }
                    else
                    {
                        empire.StarReports[star.Name].Update(star, IntelLevel.None);    
                    }
                } 
            }
            
            // Second pass. Remove visibility from all fleets and age their reports.
            foreach (FleetIntel fleet in empire.FleetReports.Values)
            {
                fleet.Unsee();
            }
            
            // Get fleets owned by the player.
            foreach (Fleet fleet in stateData.AllFleets.Values)
            {
                if (fleet.Owner == empire.EmpireRace.Name)
                {
                    if (!empire.FleetReports.Contains(fleet.Name))
                    {
                        empire.FleetReports.Add(new FleetIntel(fleet, IntelLevel.Owned));
                    }
                    else
                    {                        
                        empire.FleetReports[fleet.Name].Update(fleet, IntelLevel.Owned);
                    }
                }
            }
            
            // (2) Not so easy. Objects within the scanning range of the player's
            // Fleets.            
            foreach (FleetIntel fleet in empire.FleetReports.Values)
            {
                // TODO: Need to restrict this to fleets under the scan range somehow. -Aeglos 27 Jun 11
                foreach (Fleet scanned in stateData.AllFleets.Values)
                {
                    double range = 0;
                    range = PointUtilities.Distance(fleet.Position, scanned.Position);
                    if (range <= fleet.ScanRange)
                    {
                        if (!empire.FleetReports.Contains(scanned.Name))
                        {
                            empire.FleetReports.Add(new FleetIntel(scanned, IntelLevel.InScan));
                        }
                        else
                        {                        
                            empire.FleetReports[fleet.Name].Update(scanned, IntelLevel.InScan);
                        }
                    }
                }
                
                foreach (Star scanned in stateData.AllStars.Values)
                {
                    if (scanned.Owner != fleet.Owner)
                    {
                        if (scanned == fleet.InOrbit)
                        {
                            if (fleet.CanScan)
                            {
                                empire.StarReports[scanned.Name].Update(scanned, IntelLevel.InDeepScan);
                            }
                            else
                            {
                                empire.StarReports[scanned.Name].Update(scanned, IntelLevel.InOrbit);    
                            }
                        }
                        else
                        {
                            double range = 0;
                            range = PointUtilities.Distance(fleet.Position, scanned.Position);
                            if (range <= fleet.PenScanRange)
                            {
                                empire.StarReports[scanned.Name].Update(scanned, IntelLevel.InDeepScan);
                            }
                            else if (range <= fleet.ScanRange)
                            {                        
                                empire.StarReports[scanned.Name].Update(scanned, IntelLevel.InScan);
                            }
                        }
                    }
                }
            }

            // (3) Now that we know how to deal with ship scanners planet scanners
            // are just the same.
            foreach (StarIntel star in empire.StarReports.Values)
            {
                if (star.Owner == empire.EmpireRace.Name)
                {
                    foreach (Fleet scanned in stateData.AllFleets.Values)
                    {
                        if (PointUtilities.Distance(star.Position, scanned.Position)
                            <= star.ScanRange)
                        {
                            if (!empire.FleetReports.Contains(scanned.Name))
                            {
                                empire.FleetReports.Add(new FleetIntel(scanned, IntelLevel.InScan));
                            }
                            else
                            {                        
                                empire.FleetReports[scanned.Name].Update(scanned, IntelLevel.InScan);
                            }
                        }
                    }
                    
                    foreach (Star scanned in stateData.AllStars.Values)
                    {
                        if (scanned.Owner != star.Owner)
                        {
                            double range = 0;
                            range = PointUtilities.Distance(star.Position, scanned.Position);
                            // TODO:(priority 6) First check should use planetary Pen-Scan range, but it is not implemented yet.
                            if (range <= star.ScanRange)
                            {
                                empire.StarReports[scanned.Name].Update(scanned, IntelLevel.InDeepScan);
                            }
                            else if (range <= star.ScanRange)
                            {                        
                                empire.StarReports[scanned.Name].Update(scanned, IntelLevel.InScan);
                            }
                        }
                    }
                }
            }
            
            // (4) Now, we must remove any report from fleets not in any scan range.
            // We can increase the Age threshold value, to keep reports a couple of years after loosing
            // sight and then discard them.
            
            // Use a workaround, as removing within a foreach is a no-no
            List<FleetIntel> toRemove = new List<FleetIntel>();
            foreach (FleetIntel fleet in empire.FleetReports.Values)
            {
                if (fleet.Age > Global.DiscardFleetReportAge)
                {
                    toRemove.Add(fleet);
                }    
            }
            
            foreach (FleetIntel fleet in toRemove)
            {
                empire.FleetReports.Remove(fleet);
            }
        }
    }
}


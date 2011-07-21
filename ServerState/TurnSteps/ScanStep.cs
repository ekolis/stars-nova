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
                
                AddStars(empire);
                AddFleets(empire);
                ScanWithFleets(empire);
                ScanWithStars(empire);
            }  
        }
        
        private void AddStars(EmpireData empire)
        {             
            foreach (Star star in stateData.AllStars.Values)
            {
                if (star.Owner == empire.Id)
                {
                    if (!empire.OwnedStars.Contains(star))
                    {
                        empire.OwnedStars.Add(star);
                    }
                    else
                    {
                        empire.OwnedStars[star.Name] = stateData.AllStars[star.Name];
                    }
                    
                    if (!empire.StarReports.ContainsKey(star.Name))
                    {
                       empire.StarReports.Add(star.Name, star.GenerateReport(ScanLevel.Owned, stateData.TurnYear));   
                    }
                    else
                    {
                        empire.StarReports[star.Name].Update(star, ScanLevel.Owned, stateData.TurnYear);
                    }
                    
                }
                else
                {
                    if (empire.OwnedStars.Contains(star))
                    {
                        empire.OwnedStars.Remove(star);                        
                    }
                    
                    if (!empire.StarReports.ContainsKey(star.Name))
                    {
                        empire.StarReports.Add(star.Name, star.GenerateReport(ScanLevel.None, Global.Unset));
                    }
                }
            }    
        }
        
        private void AddFleets(EmpireData empire)
        {
            // Get fleets owned by the player.
            foreach (Fleet fleet in stateData.AllFleets.Values)
            {
                if (fleet.Owner == empire.Id)
                {
                    if (!empire.OwnedFleets.Contains(fleet))
                    {
                        empire.OwnedFleets.Add(fleet);
                    }
                    else
                    {
                        empire.OwnedFleets[fleet.Key] = stateData.AllFleets[fleet.Key];
                    }
                    
                    if (!empire.FleetReports.ContainsKey(fleet.Key))
                    {
                       empire.FleetReports.Add(fleet.Key, fleet.GenerateReport(ScanLevel.Owned, stateData.TurnYear));   
                    }
                    else
                    {
                        empire.FleetReports[fleet.Key].Update(fleet, ScanLevel.Owned, stateData.TurnYear);
                    }
                }
            }    
        }
        
        private void ScanWithFleets(EmpireData empire)
        {            
            foreach (Fleet fleet in empire.OwnedFleets.Values)
            {
                // TODO: Need to restrict this to fleets under the scan range somehow. -Aeglos 27 Jun 11
                foreach (Fleet scanned in stateData.AllFleets.Values)
                {
                    if (scanned.Owner != empire.Id)
                    {
                        double range = 0;
                        range = PointUtilities.Distance(fleet.Position, scanned.Position);
                        if (range <= fleet.ScanRange)
                        {
                            if (!empire.FleetReports.ContainsKey(scanned.Key))
                            {
                                empire.FleetReports.Add(scanned.Key, scanned.GenerateReport(ScanLevel.InScan, stateData.TurnYear));
                            }
                            else
                            {                        
                                empire.FleetReports[scanned.Key].Update(scanned, ScanLevel.InScan, stateData.TurnYear);
                            }
                        }
                    }
                }
                    
                foreach (Star scanned in stateData.AllStars.Values)
                {
                    ScanLevel scanLevel;
                    
                    if (scanned.Owner != empire.Id)
                    {
                        if (scanned == fleet.InOrbit)
                        {
                            scanLevel = fleet.CanScan ? ScanLevel.InDeepScan : ScanLevel.InPlace;
                        }
                        else
                        {
                            double range = 0;
                            range = PointUtilities.Distance(fleet.Position, scanned.Position);                                
                            scanLevel = (range <= fleet.PenScanRange) ? ScanLevel.InDeepScan : ScanLevel.None;
                        }
                        
                        if (scanLevel == ScanLevel.None)
                        {
                            continue;
                        }
                        
                        if (empire.StarReports.ContainsKey(scanned.Name))
                        {
                            empire.StarReports[scanned.Name].Update(scanned, scanLevel, stateData.TurnYear);
                        }
                        else
                        {
                            empire.StarReports.Add(scanned.Name, scanned.GenerateReport(scanLevel, stateData.TurnYear));
                        }                          
                    }
                }
            }           
        }     
        
        private void ScanWithStars(EmpireData empire)
        {
            foreach (Star star in empire.OwnedStars.Values)
            {
                foreach (Fleet scanned in stateData.AllFleets.Values)
                {
                    if (scanned.Owner != empire.Id)
                    {
                        if (PointUtilities.Distance(star.Position, scanned.Position)
                            <= star.ScanRange)
                        {
                            if (!empire.FleetReports.ContainsKey(scanned.Key))
                            {
                                empire.FleetReports.Add(scanned.Key, scanned.GenerateReport(ScanLevel.InScan, stateData.TurnYear));
                            }
                            else
                            {                        
                                empire.FleetReports[scanned.Key].Update(scanned, ScanLevel.InScan, stateData.TurnYear);
                            }
                        }
                    }
                }
                
                foreach (Star scanned in stateData.AllStars.Values)
                {
                    ScanLevel scanLevel;
                    
                    if (scanned.Owner != empire.Id)
                    {
                        double range = 0;
                        range = PointUtilities.Distance(star.Position, scanned.Position);
                        // TODO:(priority 6) Planetary Pen-Scan not implemented yet.
                        if (range <= star.ScanRange)
                        {
                            scanLevel = ScanLevel.InScan;
                        }
                        else
                        {
                            scanLevel = ScanLevel.None;
                        }
                        
                        if (scanLevel == ScanLevel.None)
                        {
                            continue;
                        }
                        
                        if (empire.StarReports.ContainsKey(scanned.Name))
                        {
                            empire.StarReports[scanned.Name].Update(scanned, scanLevel, stateData.TurnYear);
                        }
                        else
                        {
                            empire.StarReports.Add(scanned.Name, scanned.GenerateReport(scanLevel, stateData.TurnYear));
                        }
                    }
                }
            }    
        }
        
        // TODO: (priority 2) Move this to the client so players can decide how long to keep
        // old reports.
        private void DiscardOldReports(EmpireData empire)
        {
            List<FleetIntel> toRemove = new List<FleetIntel>();
            
            foreach (FleetIntel report in empire.FleetReports.Values)
            {
                if (stateData.TurnYear - report.Year > Global.DiscardFleetReportAge)
                {
                    toRemove.Add(report);
                }    
            }
            
            foreach (FleetIntel report in toRemove)
            {
                empire.FleetReports.Remove(report.Key);
            }    
        }
    }
}


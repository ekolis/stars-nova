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
        private ServerData serverState;
        
        public ScanStep()
        {
        }
        
        public void Process(ServerData serverState)
        {
            this.serverState = serverState;
            
            foreach (EmpireData empire in serverState.AllEmpires.Values)
            {                            
                AddStars(empire);
                ScanWithFleets(empire);
                ScanWithStars(empire);
            }  
        }
        
        private void AddStars(EmpireData empire)
        {             
            foreach (Star star in serverState.AllStars.Values)
            {
                if (star.Owner == empire.Id)
                {
                    if (!empire.OwnedStars.Contains(star))
                    {
                        empire.OwnedStars.Add(star);
                    }
                    else
                    {
                        empire.OwnedStars[star.Name] = serverState.AllStars[star.Name];
                    }
                    
                    if (!empire.StarReports.ContainsKey(star.Name))
                    {
                       empire.StarReports.Add(star.Name, star.GenerateReport(ScanLevel.Owned, serverState.TurnYear));   
                    }
                    else
                    {
                        empire.StarReports[star.Name].Update(star, ScanLevel.Owned, serverState.TurnYear);
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
        
        private void ScanWithFleets(EmpireData empire)
        {            
            foreach (Fleet fleet in empire.OwnedFleets.Values)
            {
                // Update own reports
                empire.FleetReports[fleet.Key].Update(fleet, ScanLevel.Owned, empire.TurnYear);
                
                // TODO: Need to restrict this to fleets under the scan range somehow. -Aeglos 27 Jun 11
                foreach (Fleet scanned in serverState.IterateAllFleets())
                {
                    if (scanned.Owner != empire.Id)
                    {
                        double range = 0;
                        range = PointUtilities.Distance(fleet.Position, scanned.Position);
                        if (range <= fleet.ScanRange)
                        {
                            if (!empire.FleetReports.ContainsKey(scanned.Key))
                            {
                                empire.FleetReports.Add(scanned.Key, scanned.GenerateReport(ScanLevel.InScan, serverState.TurnYear));
                            }
                            else
                            {                        
                                empire.FleetReports[scanned.Key].Update(scanned, ScanLevel.InScan, serverState.TurnYear);
                            }
                        }
                    }
                }
                    
                foreach (Star scanned in serverState.AllStars.Values)
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
                            empire.StarReports[scanned.Name].Update(scanned, scanLevel, serverState.TurnYear);
                        }
                        else
                        {
                            empire.StarReports.Add(scanned.Name, scanned.GenerateReport(scanLevel, serverState.TurnYear));
                        }                          
                    }
                }
            }           
        }     
        
        private void ScanWithStars(EmpireData empire)
        {
            foreach (Star star in empire.OwnedStars.Values)
            {
                foreach (Fleet scanned in serverState.IterateAllFleets())
                {
                    if (scanned.Owner != empire.Id)
                    {
                        if (PointUtilities.Distance(star.Position, scanned.Position)
                            <= star.ScanRange)
                        {
                            if (!empire.FleetReports.ContainsKey(scanned.Key))
                            {
                                empire.FleetReports.Add(scanned.Key, scanned.GenerateReport(ScanLevel.InScan, serverState.TurnYear));
                            }
                            else
                            {                        
                                empire.FleetReports[scanned.Key].Update(scanned, ScanLevel.InScan, serverState.TurnYear);
                            }
                        }
                    }
                }
                
                foreach (Star scanned in serverState.AllStars.Values)
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
                            empire.StarReports[scanned.Name].Update(scanned, scanLevel, serverState.TurnYear);
                        }
                        else
                        {
                            empire.StarReports.Add(scanned.Name, scanned.GenerateReport(scanLevel, serverState.TurnYear));
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
                if (serverState.TurnYear - report.Year > Global.DiscardFleetReportAge)
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


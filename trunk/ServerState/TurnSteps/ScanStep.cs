#region Copyright Notice
// ============================================================================
// Copyright (C) 2011-2012 The Stars-Nova Project
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
    using System.Linq;
    
    using Nova.Common;
    using Nova.Common.Components;
    
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
                Scan(empire);
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
        
        private void Scan(EmpireData empire)
        {
            foreach (Mappable scanner in empire.IterateAllMappables())
            {
                int scanRange = 0;
                int penScanRange = 0;
                
                //Do some self scanning (Update reports) and set ranges..
                if (scanner is Star)
                {
                    scanRange = (scanner as Star).ScanRange;
                    penScanRange = (scanner as Star).ScanRange; // TODO:(priority 6) Planetary Pen-Scan not implemented yet.
                    empire.StarReports[scanner.Name].Update(scanner as Star, ScanLevel.Owned, serverState.TurnYear);    
                }
                else
                {
                    scanRange = (scanner as Fleet).ScanRange;
                    penScanRange = (scanner as Fleet).PenScanRange;
                    empire.FleetReports[scanner.Key].Update(scanner as Fleet, ScanLevel.Owned, empire.TurnYear);    
                }
                
                // Scan everything
                foreach (Mappable scanned in serverState.IterateAllMappables())
                {
                    // ...That isn't ours!
                    if (scanned.Owner == empire.Id)
                    {
                        continue;
                    }
                    
                    ScanLevel scanLevel = ScanLevel.None;
                    double range = 0;
                    range = PointUtilities.Distance(scanner.Position, scanned.Position);
                    
                    if (scanned is Star)
                    {
                        Star star = scanned as Star;
                        
                        // There are two ways to get information from a Star:
                        // 1. In orbit with a fleet,
                        // 2. Not in orbit with a Pen Scan (fleet or star).
                        // Non penetrating distance scans won't tell anything about it.
                        if((scanner is Fleet) && range == 0)
                        {
                            scanLevel = (scanner as Fleet).CanScan ? ScanLevel.InDeepScan : ScanLevel.InPlace;     
                        }
                        else // scanner is Star or non orbiting Fleet
                        {
                            scanLevel = (range <= penScanRange) ? ScanLevel.InDeepScan : ScanLevel.None;
                        }
                        
                        // Dont update if we didn't scan to allow report to age.                        
                        if (scanLevel == ScanLevel.None)
                        {
                            continue;
                        }
                        
                        if (empire.StarReports.ContainsKey(scanned.Name))
                        {
                            empire.StarReports[scanned.Name].Update((scanned as Star), scanLevel, serverState.TurnYear);
                        }
                        else
                        {
                            empire.StarReports.Add(scanned.Name, (scanned as Star).GenerateReport(scanLevel, serverState.TurnYear));
                        }
                    }
                    else // scanned is Fleet
                    {
                        // Fleets are simple as scan levels (PenScan for example) won't affect them. We only
                        // care for non penetrating distance scans.
                        if (range > scanRange)
                        {
                            continue;
                        }

                        // Check if we have a record of this design(s).
                        foreach(ShipToken token in (scanned as Fleet).Composition.Values)
                        {
                            if (empire.EmpireReports[scanned.Owner].Designs.ContainsKey(token.Design.Key))
                            {  
                                continue;
                            }
                            
                            // If not, add just the empty Hull.
                            ShipDesign newDesign = new ShipDesign(token.Design);
                            newDesign.Key = token.Design.Key;
                            newDesign.ClearAllocated();
                            empire.EmpireReports[scanned.Owner].Designs.Add(newDesign.Key, newDesign);
                        }
                        
                        if (!empire.FleetReports.ContainsKey(scanned.Key))
                        {
                            empire.FleetReports.Add(scanned.Key, (scanned as Fleet).GenerateReport(ScanLevel.InScan, serverState.TurnYear));
                        }
                        else
                        {                        
                            empire.FleetReports[scanned.Key].Update((scanned as Fleet), ScanLevel.InScan, serverState.TurnYear);
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


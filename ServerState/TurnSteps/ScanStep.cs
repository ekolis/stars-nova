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
                
                ReportOwnedStars(empire);
                ReportOwnedFleets(empire);
                ScanWithFleets(empire);
                ScanWithStars(empire);
            }  
        }
        
        private void ReportOwnedStars(EmpireData empire)
        {
            foreach (Star star in stateData.AllStars.Values)
            {
                // If star has no report (First turn) add it.
                if (!empire.OwnedStars.Contains(star.Name))
                {
                    if (star.Owner == empire.Id)
                    {
                        empire.OwnedStars.Add(new StarIntel(star, IntelLevel.Owned, stateData.TurnYear));
                    }
                    else
                    {
                        empire.OwnedStars.Add(new StarIntel(star, IntelLevel.None, stateData.TurnYear));
                    }
                }
                else
                {
                    if (star.Owner == empire.Id)
                    {
                        empire.OwnedStars[star.Name].Update(star, IntelLevel.Owned, stateData.TurnYear);                    
                    }
                    else
                    {
                        empire.OwnedStars[star.Name].Update(star, IntelLevel.None, stateData.TurnYear);    
                    }
                } 
            }    
        }
        
        private void ReportOwnedFleets(EmpireData empire)
        {
            // Get fleets owned by the player.
            foreach (Fleet fleet in stateData.AllFleets.Values)
            {
                if (fleet.Owner == empire.Id)
                {
                    if (!empire.OwnedFleets.ContainsKey(fleet.Key))
                    {
                        empire.OwnedFleets.Add(new FleetIntel(fleet, IntelLevel.Owned, stateData.TurnYear));
                    }
                    else
                    {                        
                        empire.OwnedFleets[fleet.Key].Update(fleet, IntelLevel.Owned, stateData.TurnYear);
                    }
                }
            }    
        }
        
        private void ScanWithFleets(EmpireData empire)
        {
            List<FleetIntel> toAdd = new List<FleetIntel>();
            
            foreach (FleetIntel fleet in empire.OwnedFleets.Values)
            {
                if (fleet.Owner == empire.Id)
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
                                if (!empire.OwnedFleets.ContainsKey(scanned.Key))
                                {
                                    toAdd.Add(new FleetIntel(scanned, IntelLevel.InScan, stateData.TurnYear));
                                }
                                else
                                {                        
                                    empire.OwnedFleets[fleet.Key].Update(scanned, IntelLevel.InScan, stateData.TurnYear);
                                }
                            }
                        }
                    }
                    
                    foreach (Star scanned in stateData.AllStars.Values)
                    {
                        if (scanned.Owner != empire.Id)
                        {
                            if (scanned == fleet.InOrbit)
                            {
                                if (fleet.CanScan)
                                {
                                    empire.OwnedStars[scanned.Name].Update(scanned, IntelLevel.InDeepScan, stateData.TurnYear);
                                }
                                else
                                {
                                    empire.OwnedStars[scanned.Name].Update(scanned, IntelLevel.InPlace, stateData.TurnYear);    
                                }
                            }
                            else
                            {
                                double range = 0;
                                range = PointUtilities.Distance(fleet.Position, scanned.Position);
                                if (range <= fleet.PenScanRange)
                                {
                                    empire.OwnedStars[scanned.Name].Update(scanned, IntelLevel.InDeepScan, stateData.TurnYear);
                                }
                            }
                        }
                    }
                    
                    foreach (FleetIntel scanned in toAdd)
                    {
                        empire.OwnedFleets.Add(scanned);
                    }
                }
            }    
        }     
        
        private void ScanWithStars(EmpireData empire)
        {
            foreach (StarIntel star in empire.OwnedStars.Values)
            {
                if (star.Owner == empire.Id)
                {
                    foreach (Fleet scanned in stateData.AllFleets.Values)
                    {
                        if (scanned.Owner != empire.Id)
                        {
                            if (PointUtilities.Distance(star.Position, scanned.Position)
                                <= star.ScanRange)
                            {
                                if (!empire.OwnedFleets.ContainsKey(scanned.Key))
                                {
                                    empire.OwnedFleets.Add(new FleetIntel(scanned, IntelLevel.InScan, stateData.TurnYear));
                                }
                                else
                                {                        
                                    empire.OwnedFleets[scanned.Key].Update(scanned, IntelLevel.InScan, stateData.TurnYear);
                                }
                            }
                        }
                    }
                    
                    foreach (Star scanned in stateData.AllStars.Values)
                    {
                        if (scanned.Owner != empire.Id)
                        {
                            double range = 0;
                            range = PointUtilities.Distance(star.Position, scanned.Position);
                            // TODO:(priority 6) Check should use planetary Pen-Scan range, but it is not implemented yet.
                            if (range <= star.ScanRange)
                            {
                                empire.OwnedStars[scanned.Name].Update(scanned, IntelLevel.InDeepScan, stateData.TurnYear);
                            }
                        }
                    }
                }
            }    
        }
        
        private void DiscardOldReports(EmpireData empire)
        {
            List<FleetIntel> toRemove = new List<FleetIntel>();
            
            foreach (FleetIntel fleet in empire.OwnedFleets.Values)
            {
                if (stateData.TurnYear - fleet.Year > Global.DiscardFleetReportAge)
                {
                    toRemove.Add(fleet);
                }    
            }
            
            foreach (FleetIntel fleet in toRemove)
            {
                empire.OwnedFleets.Remove(fleet);
            }    
        }
    }
}


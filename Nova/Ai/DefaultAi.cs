#region Copyright Notice
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nova.Client;
using Nova.Common;
using Nova.Common.Components;

namespace Nova.Ai
{
    public class DefaultAi : AbstractAI
    {
        private Intel turnData;

        private void HandleProduction()
        {
            foreach (Star star in ClientState.Data.PlayerStars.Values)
            {
                star.ManufacturingQueue.Queue.Clear();
                ProductionQueue.Item item = new ProductionQueue.Item();
                Design design;

                // build factories (limited by Germanium, and don't want to use it all)
                if (star.ResourcesOnHand.Germanium > 50)
                {
                    item.Name = "Factory";
                    item.Quantity = (int)((star.ResourcesOnHand.Germanium - 50) / 5);
                    item.Quantity = Math.Max(0, item.Quantity);

                    design = turnData.AllDesigns[ClientState.Data.RaceName + "/" + item.Name] as Design;

                    item.BuildState = design.Cost;

                    star.ManufacturingQueue.Queue.Add(item);

                }

                // build mines
                item = new ProductionQueue.Item();
                item.Name = "Mine";
                item.Quantity = 100;
                design = turnData.AllDesigns[ClientState.Data.RaceName + "/" + item.Name] as Design;
                item.BuildState = design.Cost;
                star.ManufacturingQueue.Queue.Add(item);

                // build defenses
                int defenceToBuild = Global.MaxDefenses - star.Defenses;
                item = new ProductionQueue.Item();
                item.Name = "Defenses";
                item.Quantity = defenceToBuild;
                design = turnData.AllDesigns[ClientState.Data.RaceName + "/" + item.Name] as Design;
                item.BuildState = design.Cost;
                star.ManufacturingQueue.Queue.Add(item);
            }
        }
        public override void DoMove()
        {
            try
            {
                turnData = ClientState.Data.InputTurn;

                HandleProduction();
                HandleResearch();
                HandleMovements();

            }
            catch (Exception)
            {
                Report.FatalError("AI failed to take proper actions.");
            }
        }

        private void HandleMovements()
        {
            
        }

        /// <summary>
        /// Always go to the min tech
        /// </summary>
        private void HandleResearch()
        {
            // check if messages contains info about tech advence
            foreach (Message msg in ClientState.Data.Messages)
            {
                if (msg.Text.Contains("Your race has advanced to Tech Level") == true)
                {
                    int minLevel = int.MaxValue;
                    Nova.Common.TechLevel.ResearchField rs = TechLevel.ResearchField.Electronics;
                    foreach (Nova.Common.TechLevel.ResearchField t in ClientState.Data.ResearchLevel)
                    {
                        minLevel = Math.Min(minLevel, ClientState.Data.ResearchLevel[t]);
                        rs = t;
                    }
                    ClientState.Data.ResearchTopic = rs;
                }
            }
           
        }
    }
}

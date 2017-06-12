#region Copyright Notice
// ============================================================================
// Copyright (C) 2009 - 2017 stars-nova
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

namespace Nova.Ai
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Nova.Client;
    using Nova.Common;
    using Nova.Common.Commands;
    using Nova.Common.Components;
    using Nova.Common.DataStructures;
    using Nova.Common.Waypoints;


    /// <summary>
    /// A helper object for the default AI for managing planets.
    /// </summary>
    public class DefaultPlanetAI
    {
        private const int FactoryProductionPrecedence = 0;
        private const int MineProductionPrecedence = 1;
        private ClientData clientState;
        private DefaultAIPlanner aiPlan = null;

        private Star planet;

        /// <summary>
        /// Initializing constructor.
        /// </summary>
        /// <param name="newStar">The planet the ai is to manage.</param>
        public DefaultPlanetAI(Star newStar, ClientData newState, DefaultAIPlanner newAIPlan)
        {
            planet = newStar;
            clientState = newState;
            aiPlan = newAIPlan;
        }

        public void HandleProduction()
        {
            // keep track of the position in the production queue
            int productionIndex = 0;

            // Clear the current manufacturing queue (except for partially built ships/starbases).
            Queue<ProductionCommand> clearProductionList = new Queue<ProductionCommand>();
            foreach (ProductionOrder productionOrderToclear in this.planet.ManufacturingQueue.Queue)
            {
                if (productionOrderToclear.Unit.Cost == productionOrderToclear.Unit.RemainingCost)
                {
                    ProductionCommand clearProductionCommand = new ProductionCommand(CommandMode.Delete, productionOrderToclear, this.planet.Key);
                    if (clearProductionCommand.IsValid(clientState.EmpireState))
                    {
                        // Put the items to be cleared in a queue, as the actual cleanup can not be done while itterating the list.
                        clearProductionList.Enqueue(clearProductionCommand);
                        clientState.Commands.Push(clearProductionCommand);
                    }
                }
                else
                {
                    productionIndex++;
                }
            }

            foreach (ProductionCommand clearProductionCommand in clearProductionList)
            {
                clearProductionCommand.ApplyToState(clientState.EmpireState);
            }

            // build factories (limited by Germanium, and don't want to use it all)
            if (this.planet.ResourcesOnHand.Germanium > 50)
            {
                int factoryBuildCostGerm = clientState.EmpireState.Race.HasTrait("CF") ? 3 : 4;
                int factoriesToBuild = (int)((this.planet.ResourcesOnHand.Germanium - 50) / factoryBuildCostGerm);
                if (factoriesToBuild > (this.planet.GetOperableFactories() - this.planet.Factories))
                {
                    factoriesToBuild = this.planet.GetOperableFactories() - this.planet.Factories;
                }

                if (factoriesToBuild > 0)
                {
                    ProductionOrder factoryOrder = new ProductionOrder(factoriesToBuild, new FactoryProductionUnit(clientState.EmpireState.Race), false);
                    ProductionCommand factoryCommand = new ProductionCommand(CommandMode.Add, factoryOrder, this.planet.Key, FactoryProductionPrecedence);
                    productionIndex++;
                    if (factoryCommand.IsValid(clientState.EmpireState))
                    {
                        factoryCommand.ApplyToState(clientState.EmpireState);
                        this.clientState.Commands.Push(factoryCommand);
                    }
                }
            }

            // build mines
            int maxMines = this.planet.GetOperableMines();
            if (this.planet.Mines < maxMines)
            {
                ProductionOrder mineOrder = new ProductionOrder(maxMines - this.planet.Mines, new MineProductionUnit(clientState.EmpireState.Race), false);
                ProductionCommand mineCommand = new ProductionCommand(CommandMode.Add, mineOrder, this.planet.Key, Math.Min(MineProductionPrecedence, productionIndex));
                productionIndex++;
                if (mineCommand.IsValid(clientState.EmpireState))
                {
                    mineCommand.ApplyToState(clientState.EmpireState);
                    clientState.Commands.Push(mineCommand);
                }
            }

            // Build ships
            productionIndex = BuildShips(productionIndex);

            // build defenses
            int defenseToBuild = Global.MaxDefenses - this.planet.Defenses;
            if (defenseToBuild > 0)
            {
                ProductionOrder defenseOrder = new ProductionOrder(defenseToBuild, new DefenseProductionUnit(), false);
                ProductionCommand defenseCommand = new ProductionCommand(CommandMode.Add, defenseOrder, this.planet.Key, productionIndex);
                productionIndex++;
                if (defenseCommand.IsValid(clientState.EmpireState))
                {
                    defenseCommand.ApplyToState(clientState.EmpireState);
                    clientState.Commands.Push(defenseCommand);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int BuildShips(int productionIndex)
        {
            productionIndex = BuildScout(productionIndex);
            productionIndex = BuildColonizer(productionIndex);

            return productionIndex;
        } // Build ships

        /// <summary>
        /// Add a scout to the production que, if required and we can aford it.
        /// </summary>
        /// <param name="productionIndex">The current insertion point into the planet's production queue.</param>
        /// <returns>The updated productionIndex.</returns>
        private int BuildScout(int productionIndex)
        {
            if (this.planet.GetResourceRate() > DefaultAIPlanner.LowProduction && this.aiPlan.ScoutCount < DefaultAIPlanner.EarlyScouts)
            {
                if (this.aiPlan.ScoutDesign != null)
                {
                    ProductionOrder scoutOrder = new ProductionOrder(1, new ShipProductionUnit(this.aiPlan.ScoutDesign), false);
                    ProductionCommand scoutCommand = new ProductionCommand(CommandMode.Add, scoutOrder, this.planet.Key, productionIndex);
                    if (scoutCommand.IsValid(clientState.EmpireState))
                    {
                        scoutCommand.ApplyToState(clientState.EmpireState);
                        clientState.Commands.Push(scoutCommand);
                        productionIndex++;
                    }
                }
            }
            return productionIndex;
        } // BuildScouts()

        /// <summary>
        /// Add a colonizer to the production que, if required and we can aford it.
        /// </summary>
        /// <param name="productionIndex">The current insertion point into the planet's production queue.</param>
        /// <returns>The updated productionIndex.</returns>
        /// <remarks>
        /// Always make one spare colonizer.
        /// </remarks>
        private int BuildColonizer(int productionIndex)
        {
            if (this.planet.GetResourceRate() > DefaultAIPlanner.LowProduction && this.aiPlan.ColonizerCount < (this.aiPlan.PlanetsToColonize - this.aiPlan.ColonizerCount + 1))
            {
                ShipDesign colonizerDesign = this.aiPlan.ColonizerDesign;
                if (this.aiPlan.ColonizerDesign != null)
                {
                    ProductionOrder colonizerOrder = new ProductionOrder(1, new ShipProductionUnit(this.aiPlan.ColonizerDesign), false);
                    ProductionCommand colonizerCommand = new ProductionCommand(CommandMode.Add, colonizerOrder, this.planet.Key, productionIndex);
                    if (colonizerCommand.IsValid(clientState.EmpireState))
                    {
                        colonizerCommand.ApplyToState(clientState.EmpireState);
                        clientState.Commands.Push(colonizerCommand);
                        productionIndex++;
                    }
                }
            }
            return productionIndex;
        } // BuildScouts()

    }
}


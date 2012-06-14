using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Server.NewGame
{
    using Nova.Common;

    public class HomeStarLeftoverpointsAdjuster
    {
        public static void Adjust(Star star, Race race)
        {
            string leftoverAdvantagePointsTarget = race.LeftoverPointTarget;
            int leftoverAdvantagePoints = race.GetLeftoverAdvantagePoints();

            if (leftoverAdvantagePoints == 0)
            {
                return;
            }

            if (leftoverAdvantagePointsTarget == "Mineral concentration")
            {
                // 1% for three points for the poorest mineral
                int additionalConcentration = leftoverAdvantagePoints / 3;
                if (star.MineralConcentration.Boranium > star.MineralConcentration.Germanium)
                {
                    if (star.MineralConcentration.Germanium > star.MineralConcentration.Ironium)
                    {
                        star.MineralConcentration.Ironium += additionalConcentration;
                    }
                    else
                    {
                        star.MineralConcentration.Germanium += additionalConcentration;
                    }
                }
                else
                {
                    if (star.MineralConcentration.Boranium > star.MineralConcentration.Ironium)
                    {
                        star.MineralConcentration.Ironium += additionalConcentration;
                    }
                    else
                    {
                        star.MineralConcentration.Boranium += additionalConcentration;
                    }
                }
            }
            else if (leftoverAdvantagePointsTarget == "Mines")
            {
                // one mine for two points
                star.Mines += leftoverAdvantagePoints / 2;
            }
            else if (leftoverAdvantagePointsTarget == "Factories")
            {
                // one Factory for five points
                star.Factories += leftoverAdvantagePoints / 5;
            }
            else if (leftoverAdvantagePointsTarget == "Defenses")
            {
                // one Defense for ten points
                star.Defenses += leftoverAdvantagePoints / 10;
            }
            else // if (leftoverAdvantagePointsTarget == "Surface minerals")
            {
                // 10kT for each point, distribution weighted for the rarest minerals
                int total = leftoverAdvantagePoints * 10;
                // 
                int germanium = Math.Max(1, star.ResourcesOnHand.Germanium);
                int boranium = Math.Max(1, star.ResourcesOnHand.Boranium);
                int ironium = Math.Max(1, star.ResourcesOnHand.Ironium);
                double distributionFactorDividend = boranium + germanium + ironium;
                double distributionFactorBoranium = distributionFactorDividend / boranium;
                double distributionFactorGermanium = distributionFactorDividend / germanium;
                double distributionFactorIronium = distributionFactorDividend / ironium;
                double distributedTotal = distributionFactorBoranium + distributionFactorGermanium + distributionFactorIronium;

                star.ResourcesOnHand.Boranium += (int)(Math.Round(distributionFactorBoranium / distributedTotal * total));
                star.ResourcesOnHand.Germanium += (int)(Math.Round(distributionFactorGermanium / distributedTotal * total));
                star.ResourcesOnHand.Ironium += (int)(Math.Round(distributionFactorIronium / distributedTotal * total));
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;

namespace Nova.Common.RaceDefinition
{
    public class RaceAdvantagePointCalculator
    {
        const string PRT_HE = "HE";
        const string PRT_AR = "AR";
        const string PRT_PP = "PP";
        const string PRT_SS = "SS";
        const string PRT_JT = "JOAT";
        const string LRT_TT = "TT";
        private static Dictionary<string, int> prtCost; // int[] prtCost = new int[10] { 40, 95, 45, 10, -100, -150, 120, 180, 90, -66 };
        private static Dictionary<string, int> lrtCost; // int lrtCost[14]={-235,-25,-159,-201,40,-240,-155,160,240,255,325,180,70,30};
        private static int[] scienceCost = new int[] { 150, 330, 540, 780, 1050, 1380 };

        static RaceAdvantagePointCalculator()
        {
            prtCost = new Dictionary<string, int>();
            prtCost.Add("HE", 40);
            prtCost.Add("SS", 95);
            prtCost.Add("WM", 45);
            prtCost.Add("CA", 10);
            prtCost.Add("IS", -100);
            prtCost.Add("SD", -150);
            prtCost.Add("PP", 120);
            prtCost.Add("IT", 180);
            prtCost.Add("AR", 90);
            prtCost.Add("JOAT", -66);

            lrtCost = new Dictionary<string, int>();
            lrtCost.Add("IFE", -235);
            lrtCost.Add(LRT_TT, -25);
            lrtCost.Add("ARM", -159);
            lrtCost.Add("ISB", -201);
            lrtCost.Add("GR", 40);
            lrtCost.Add("UR", -240);
            lrtCost.Add("MA", -155);
            lrtCost.Add("NRS", 160); // NRSE
            lrtCost.Add("CE", 240);
            lrtCost.Add("OBRM", 255);
            lrtCost.Add("NAS", 325);
            lrtCost.Add("LSP", 180);
            lrtCost.Add("BET", 70);
            lrtCost.Add("RS", 30);
        }

//        int[] lowerHab = new int[3];
//        int[] centerHab = new int[3];
//        int[] upperHab = new int[3];

        bool IMMUNE(int a)
        {
            return ((a) == -1);
        }

        int planetValueCalc(Race race, int[] testPlanetHab)
        {
            Star star = new Star();
            star.Gravity = testPlanetHab[0];
            star.Radiation = testPlanetHab[1];
            star.Temperature = testPlanetHab[2];

            return (int)(race.HabitalValue(star) * 100);
        }

        //void BoundsCheck(Race race)
        //{
        //    int i, tmp;


        //    for (i = 0; i < 3; i++)
        //    {
        //        lowerHab[i] = race.lowerHab(i);
        //        centerHab[i] = race.centerHab(i);
        //        upperHab[i] = race.upperHab(i);
        //    }

        //    for (i = 0; i < 3; i++)
        //    {
        //        if (IMMUNE(lowerHab[i]))
        //        {
        //            if (!IMMUNE(upperHab[i]) || !IMMUNE(centerHab[i]))
        //            {
        //                upperHab[i] = centerHab[i] = -1;
        //            }
        //        }
        //        else
        //        {
        //            if (lowerHab[i] < 0)
        //            {
        //                lowerHab[i] = 0;
        //            }
        //            if (lowerHab[i] > 100)
        //            {
        //                lowerHab[i] = 100;
        //            }
        //            if (upperHab[i] > 100)
        //            {
        //                upperHab[i] = 100;
        //            }
        //            if (lowerHab[i] > upperHab[i])
        //            {
        //                lowerHab[i] = upperHab[i];
        //            }
        //            tmp = centerHab[i];
        //            if (tmp != (lowerHab[i] + (upperHab[i] - lowerHab[i]) / 2))
        //            {
        //                tmp = lowerHab[i] + (upperHab[i] - lowerHab[i]) / 2;
        //            }
        //        }
        //    }

        //    if (race.GrowthRate > 20)
        //    {
        //        growthRate = 20;
        //    }
        //    if (growthRate < 1)
        //    {
        //        growthRate = 1;
        //    }

        //    for (i = 0; i < 16; i++)
        //    {
        //        if (playerData->stats[i] < statsMin[i])
        //        {
        //            playerData->stats[i] = statsMin[i];
        //        }
        //        if (playerData->stats[i] > statsMax[i])
        //        {
        //            playerData->stats[i] = statsMax[i];
        //        }
        //    }
        //}

        private int habPoints(Race race)
        {
            bool isTotalTerraforming;
            double advantagePoints,v136,v13E;
            int v12E,planetDesir;
            int v100,tmpHab,TTCorrFactor,h,i,j,k;
            int[] v108 = new int[3];
            int[] testHabStart = new int[3];
            int[] testHabWidth = new int[3];
            int[] iterNum = new int[3];
            int[] testPlanetHab = new int[3];

            advantagePoints = 0.0;
            isTotalTerraforming = race.Traits.Contains("TT"); // getbit(playerData->grbit, GRBIT_LRT_TT);

            v108[0]=v108[1]=v108[2]=0;

            for (h=0;h<3;h++)
            {
                if (h==0) TTCorrFactor=0;
                else if (h==1) TTCorrFactor = isTotalTerraforming?8:5;
                else TTCorrFactor = isTotalTerraforming?17:15;

                for (i=0; i<3; i++)
                {
                    //if (playerData->centerHab[i]>100 ||
                    //    playerData->lowerHab[i]>100 ||
                    //    playerData->upperHab[i]>100 ||
                    //    playerData->centerHab[i]<0 ||
                    //    playerData->lowerHab[i]<0 ||
                    //    playerData->upperHab[i]<0)
                    //{
                    //    if (!IMMUNE(playerData->centerHab[i]) &&
                    //        !IMMUNE(playerData->lowerHab[i]) &&
                    //        !IMMUNE(playerData->upperHab[i]))
                    //    {
                    //        playerData->centerHab[i]=playerData->lowerHab[i]=playerData->upperHab[i]=-1;					
                    //    }
                    //}

                    if (race.isImmune(i))
                    {
                        testHabStart[i] = 50;
                        testHabWidth[i] = 11;
                        iterNum[i] = 1;
                    }
                    else
                    {
                        testHabStart[i] = race.lowerHab(i)-TTCorrFactor;
                        if (testHabStart[i]<0) testHabStart[i]=0;
                        tmpHab = race.upperHab(i)+TTCorrFactor;
                        if (tmpHab>100) tmpHab=100;
                        testHabWidth[i] = tmpHab-testHabStart[i];
                        iterNum[i] = 11;
                    }
                }
                /* loc_92AAC */
                v13E = 0.0;
                for (i=0;i<iterNum[0];i++)
                {
                    if (i==0 || iterNum[0]<=1)
                        tmpHab = testHabStart[0];
                    else
                        tmpHab = (testHabWidth[0]*i) / (iterNum[0]-1) + testHabStart[0];

                    if (h!=0 && !race.GravityTolerance.Immune)
                    {
                        v100 = race.centerHab(0) - tmpHab;
                        if (Math.Abs(v100)<=TTCorrFactor) v100=0;
                        else if (v100<0) v100+=TTCorrFactor;
                        else v100-=TTCorrFactor;
                        v108[0] = v100;
                        tmpHab = race.centerHab(0) - v100;
                    }
                    testPlanetHab[0] = tmpHab;
                    v136 = 0.0;
                    for (j=0;j<iterNum[1];j++)
                    {
                        if (j==0 || iterNum[1]<=1)
                            tmpHab = testHabStart[1];
                        else
                            tmpHab = (testHabWidth[1]*j) / (iterNum[1]-1) + testHabStart[1];

                        if (h != 0 && !race.TemperatureTolerance.Immune)
                        {
                            v100 = race.centerHab(1) - tmpHab;
                            if (Math.Abs(v100)<=TTCorrFactor) v100=0;
                            else if (v100<0) v100+=TTCorrFactor;
                            else v100-=TTCorrFactor;
                            v108[1] = v100;
                            tmpHab = race.centerHab(1) - v100;
                        }
                        testPlanetHab[1] = tmpHab;
                        v12E = 0;
                        for (k=0;k<iterNum[2];k++)
                        {
                            if (k==0 || iterNum[2]<=1)
                                tmpHab = testHabStart[2];
                            else
                                tmpHab = (testHabWidth[2]*k) / (iterNum[2]-1) + testHabStart[2];

                            if (h != 0 && !race.RadiationTolerance.Immune)
                            {
                                v100 = race.centerHab(2) - tmpHab;
                                if (Math.Abs(v100)<=TTCorrFactor) v100=0;
                                else if (v100<0) v100+=TTCorrFactor;
                                else v100-=TTCorrFactor;
                                v108[2] = v100;
                                tmpHab = race.centerHab(2) - v100;
                            }
                            testPlanetHab[2] = tmpHab;

                            planetDesir = planetValueCalc(race, testPlanetHab);

                            v100 = v108[0]+v108[1]+v108[2];
                            if (v100>TTCorrFactor)
                            {
                                planetDesir-=v100-TTCorrFactor;
                                if (planetDesir<0) planetDesir=0;
                            }
                            planetDesir *= planetDesir;
                            switch (h)
                            {
                                case 0: planetDesir*=7; break;
                                case 1: planetDesir*=5; break;
                                default: planetDesir *= 6; break;
                            }
                            v12E+=planetDesir;
                        }
                        /* loc_92D34 */
                        if (!race.RadiationTolerance.Immune) v12E = (v12E*testHabWidth[2])/100;
                        else v12E *= 11;

                        v136 += v12E;
                    }
                    if (!race.TemperatureTolerance.Immune) v136 = (v136 * testHabWidth[1]) / 100;
                    else v136 *= 11;

                    v13E += v136;
                }
                if (!race.GravityTolerance.Immune) v13E = (v13E * testHabWidth[0]) / 100;
                else v13E *= 11;

                advantagePoints += v13E;
            }

            return (int)(advantagePoints/10.0+0.5);
        }

        public int calculateAdvantagePoints(Race race)
        {
            int points = 1650, hab;
            int grRateFactor, facOperate, tenFacRes, operPoints, costPoints, prodPoints, tmpPoints, grRate, i, j, k;
            string PRT;

            /*cout << "Step 1, points = " << points << endl;*/

            // BoundsCheck(race);
            PRT = race.Traits.Primary.Code;
            hab = habPoints(race) / 2000;

            /*cout << "Step 2, hab points = " << hab << endl;*/

            grRateFactor = (int)race.GrowthRate;
            grRate = grRateFactor;
            if (grRateFactor <= 5) points += (6 - grRateFactor) * 4200;
            else if (grRateFactor <= 13)
            {
                switch (grRateFactor)
                {
                    case 6: points += 3600; break;
                    case 7: points += 2250; break;
                    case 8: points += 600; break;
                    case 9: points += 225; break;
                }
                grRateFactor = grRateFactor * 2 - 5;
            }
            else if (grRateFactor < 20) grRateFactor = (grRateFactor - 6) * 3;
            else grRateFactor = 45;

            points -= (hab * grRateFactor) / 24;

            /*cout << "Step 3, points = " << points << endl;*/

            i = 0;
            for (j = 0; j < 3; j++)
            {
                if (race.isImmune(j)) //if (race.centerHab(j) < 0)
                {
                    i++;
                }
                else
                {
                    points += Math.Abs(race.centerHab(j) - 50) * 4;
                }
            }

            if (i > 1) points -= 150;

            /*cout << "Step 4, points = " << points << endl;*/

            facOperate = race.OperableFactories; // playerData->stats[STAT_FAC_OPERATE];
            tenFacRes = race.FactoryProduction; // playerData->stats[STAT_TEN_FAC_RES];

            if (facOperate > 10 || tenFacRes > 10)
            {
                facOperate -= 9; if (facOperate < 1) facOperate = 1;
                tenFacRes -= 9; if (tenFacRes < 1) tenFacRes = 1;

                tenFacRes *= (2 + (PRT == PRT_HE ? 1 : 0));

                /*additional penalty for two- and three-immune*/
                if (i >= 2)
                    points -= ((tenFacRes * facOperate) * grRate) / 2;
                else
                    points -= ((tenFacRes * facOperate) * grRate) / 9;
            }

            j = race.ColonistsPerResource / 100; //  playerData->stats[STAT_RES_PER_COL];
            if (j > 25) j = 25;
            if (j <= 7) points -= 2400;
            else if (j == 8) points -= 1260;
            else if (j == 9) points -= 600;
            else if (j > 10) points += (j - 10) * 120;

            /*cout << "Step 5, points = " << points << endl;*/

            if (PRT != PRT_AR)
            {
                /*factories*/
                prodPoints = 10 - race.FactoryProduction; // 10 - playerData->stats[STAT_TEN_FAC_RES];
                costPoints = 10 - race.FactoryBuildCost; // 10 - playerData->stats[STAT_FAC_COST];
                operPoints = 10 - race.OperableFactories; // 10 - playerData->stats[STAT_FAC_OPERATE];
                tmpPoints = 0;

                if (prodPoints > 0) tmpPoints = prodPoints * 100;
                else tmpPoints += prodPoints * 121;
                if (costPoints > 0) tmpPoints += costPoints * costPoints * (-60);
                else tmpPoints += costPoints * (-55);
                if (operPoints > 0) tmpPoints += operPoints * 40;
                else tmpPoints += operPoints * 35;
                if (tmpPoints > 700) tmpPoints = (tmpPoints - 700) / 3 + 700;

                if (operPoints <= -7)
                {
                    if (operPoints < -11)
                    {
                        if (operPoints < -14) tmpPoints -= 360;
                        else tmpPoints += (operPoints + 7) * 45;
                    }
                    else tmpPoints += (operPoints + 6) * 30;
                }

                if (prodPoints <= -3) tmpPoints += (prodPoints + 2) * 60;

                points += tmpPoints;

                if (race.Traits.Contains("CF")) points -= 175; // if (getbit(playerData->grbit, GRBIT_FACT_GER_LESS) != 0) points -= 175;

                /*mines*/
                prodPoints = 10 - race.MineProductionRate;  // 10 - playerData->stats[STAT_TEN_MINES_PROD];
                costPoints = 3 - race.MineBuildCost; // 3 - playerData->stats[STAT_MINE_COST];
                operPoints = 10 - race.OperableMines; // 10 - playerData->stats[STAT_MINE_OPERATE];
                tmpPoints = 0;

                if (prodPoints > 0) tmpPoints = prodPoints * 100;
                else tmpPoints += prodPoints * 169;
                if (costPoints > 0) tmpPoints -= 360;
                else tmpPoints += costPoints * (-65) + 80;
                if (operPoints > 0) tmpPoints += operPoints * 40;
                else tmpPoints += operPoints * 35;

                points += tmpPoints;
            }
            else points += 210; /* AR */

            /*cout << "Step 6, points = " << points << endl;*/

            /*LRTs*/
            points -= prtCost[PRT];
            i = k = 0;

            //for(j=0;j<=13;j++)
            //{
            //    if (getbit(playerData->grbit,j)!=0)
            //    {
            //        if (lrtCost[j]>=0) i++;
            //        else k++;
            //        points+=lrtCost[j];
            //    }
            //}
            foreach (DictionaryEntry de in AllTraits.Data.Secondary)
            {
                TraitEntry trait = de.Value as TraitEntry;
                if (trait.Code == "CF" || trait.Code == "ExtraTech")
                {
                    continue; // this is not a LRT!
                }
                else if (race.HasTrait(trait.Code))
                {
                    if (lrtCost[trait.Code] >= 0) i++;
                    else k++;
                    points += lrtCost[trait.Code];
                }
            }
            if ((k + i) > 4) points -= (k + i) * (k + i - 4) * 10;
            if ((i - k) > 3) points -= (i - k - 3) * 60;
            if ((k - i) > 3) points -= (k - i - 3) * 40;

            if (race.Traits.Contains("NAS")) // if (getbit(playerData->grbit, GRBIT_LRT_NAS) != 0)
            {
                if (PRT == PRT_PP) points -= 280;
                else if (PRT == PRT_SS) points -= 200;
                else if (PRT == PRT_JT) points -= 40;
            }

            /*cout << "Step 7, points = " << points << endl;*/

            /*science*/
            tmpPoints = 0;
            // for (j=STAT_ENERGY_COST; j<=STAT_BIO_COST; j++) tmpPoints += playerData->stats[j]-1;
            int researchStat;
            foreach (int tech in race.ResearchCosts)
            {
                if (tech == 150) researchStat = 2;         // expensive / +75% research cost
                else if (tech == 100) researchStat = 1;    // normal
                else /*if (tech == 50)*/ researchStat = 0; // cheap / -50% research cost
                tmpPoints += (researchStat - 1);
            }
            if (tmpPoints > 0)
            {
                points -= tmpPoints * tmpPoints * 130;
                if (tmpPoints == 6) points += 1430;
                else if (tmpPoints == 5) points += 520;
            }
            else if (tmpPoints < 0)
            {
                points += scienceCost[-tmpPoints - 1];
                if (tmpPoints < -4 && (race.ColonistsPerResource / 100) < 10) points -= 190; // if (tmpPoints < -4 && playerData->stats[STAT_RES_PER_COL] < 10) points -= 190;
            }
            if (race.Traits.Contains("ExtraTech")) points -= 180; // if (getbit(playerData->grbit, GRBIT_TECH75_LVL3) != 0) points -= 180;               
            if (PRT == PRT_AR && race.ResearchCosts[TechLevel.ResearchField.Energy] == 50/*50% less*/) points -= 100;   // if (PRT==PRT_AR && playerData->stats[STAT_ENERGY_COST]==2/*50% less*/) points -= 100;

            /*cout << "Step 8, points = " << points << endl;*/

            /*cout << "Returning final value of " << points/3 << endl;*/

            return points / 3;
        }
    }
}

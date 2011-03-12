using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Common
{
    public class Gravity
    {
    	// Stars! Gravity values
    	// after RedDragon in rec.games.computer.stars 1998/05/29 "Finally the equations for Gravity ranges ;-)"
    	// 
    	// ...
    	// A.   x=1 to 26 (equivalent gravity range 0.12-0.50):
        //      for each x, grav value= F(x), where
        //      F(x)= 2^(-LOG[-0.24x + 8.24, 2]),
        //      note that Log[a,b] means log of "a" at base "b"
        //      inverse of F(x)= F(x)'= G(y), where y is the grav value input by a 
        //      user, then
        //      x= G(y)= (2^(-LOG[y,2]) - 8.24) / -0.24
        //
        // B.   x=27 to 51 (equivalent gravity range 0.50-1.00):
        //      for each x, grav value= F(x), where
        //      F(x)= 2^(-LOG[-0.04x + 3.04, 2]),
        //      note that Log[a,b] means log of "a" at base "b"
        //      inverse of F(x)= F(x)'= G(y), where y is the grav value input by a
        //      user, then
        //      x= G(y)= (2^(-LOG[y,2]) - 3.04) / -0.04
        // 
        // C.   x=52 to 76 (equivalent gravity range 1.00-2.00):
        //      for each x, grav value= F(x), where
        //      F(x)= 0.04x - 1.04,
        //      inverse of F(x)= F(x)'= G(y), where y is the grav value input by a
        //      user, then
        //      x= G(y)= (y + 1.04) / 0.04
        //
        // D.   x=77 to 101 (equivalent gravity range 2.00-8.00):
        //      for each x, grav value= F(x), where
        //      F(x)= 0.24x - 16.24,
        //      inverse of F(x)= F(x)'= G(y), where y is the grav value input by a
        //      user, then
        //      x= G(y)= (y + 16.24) / 0.24 
    	// ...
        //                                  1     2     3     4     5     6     7     8     9    10
        private static double[] gValues = {                                                      0.125,  //   0
                                           0.129,0.133,0.137,0.142,0.147,0.152,0.158,0.164,0.171,0.178,  //  10
                                           0.186,0.195,0.204,0.215,0.227,0.24 ,0.25 ,0.27 ,0.29 ,0.31,   //  22
                                           0.33 ,0.36 ,0.40 ,0.44 ,0.50 ,0.51 ,0.52 ,0.53 ,0.54 ,0.55,   //  30
                                           0.56 ,0.58 ,0.59 ,0.60 ,0.62 ,0.64 ,0.65 ,0.67 ,0.69 ,0.71,   //  40
                                           0.73 ,0.75 ,0.78 ,0.80 ,0.83 ,0.86 ,0.89 ,0.92 ,0.96 ,1.00,   //  50
                                           1.04 ,1.08 ,1.12 ,1.16 ,1.20 ,1.24 ,1.28 ,1.32 ,1.36 ,1.40,   //  60
                                           1.44 ,1.48 ,1.52 ,1.56 ,1.60 ,1.64 ,1.68 ,1.72 ,1.76 ,1.80,   //  70
                                           1.84 ,1.88 ,1.92 ,1.96 ,2.00 ,2.24 ,2.48 ,2.72 ,2.96 ,3.20,   //  80
                                           3.44 ,3.68 ,3.92 ,4.16 ,4.40 ,4.64 ,4.88 ,5.12 ,5.36 ,5.60,   //  90
                                           5.84 ,6.08 ,6.32 ,6.56 ,6.80 ,7.04 ,7.28 ,7.52 ,7.76 ,8.00    // 100
                                           };

        public static double BarPositionToEnvironmentValue(int pos)
        {
            if (pos > 100)
            {
                return gValues[100];
            }
            else if (pos < 0)
            {
                return gValues[0];
            }
            else
            {
                return gValues[pos];
            }
        }
        
        public static string Format(int value) 
        {
        	// always trunkate gravity for GUI values to two decimal points
            return BarPositionToEnvironmentValue(value).ToString("F2");
        }

        public static string FormatWithUnit(int value)
        {
            return Format(value) + GetUnit();
        }

        public static string GetUnit()
        {
            return "g";
        }
    }
}

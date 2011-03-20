using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Common
{
    public class Temperature
    {
        public static string FormatWithUnit(int value)
        {
            return Format(value) + GetUnit();
        }

        public static string Format(int value)
        {
            // always trunkate temperature for GUI values to 0 decimal points
            return BarPositionToEnvironmentValue(value).ToString("F0");
        }

        public static string GetUnit()
        {
            return "°C";
        }
        
        public static double BarPositionToEnvironmentValue(int pos)
        {
            return pos * 4 - 200;
        }       
    }
}

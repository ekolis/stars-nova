using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Common
{
    [Serializable]
    public class RadiationTolerance : EnvironmentTolerance
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization?.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public RadiationTolerance() 
        {
            Name = "RadiationTolerance";
        }

        #endregion

        // Calculate the minimum and maximum values of the tolerance ranges
        // expressed as a percentage of the total range. 
        // Radiation is in the range 0 to 100.
        override protected int MakeInternalValue(double value)
        {
            return (int)value;
        }

        override protected string Format(int value)
        {
            return value.ToString("F0") + "mR";
        }
    }
}

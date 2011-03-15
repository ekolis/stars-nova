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
        override public int MakeInternalValue(double value)
        {
            return (int)value;
        }

        override public double MakeRealValue(int value)
        {
            return (double)value;
        }

    }
}

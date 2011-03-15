using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Common
{
    [Serializable]
    public class GravityTolerance : EnvironmentTolerance
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization?.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public GravityTolerance() 
        {
            Name = "GravityTolerance";
        }

        #endregion

        // Calculate the minimum and maximum values of the tolerance ranges
        // expressed as a percentage of the total range. 
        // Gravity is in the range 0 to 10.
        override public int MakeInternalValue(double value)
        {
            return (int)(value * 10);
        }

        override public double MakeRealValue(int value)
        {
            return (double)(value / 10.0);
        }
    }
}

﻿using System;
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
        public GravityTolerance() {}

        #endregion

        // Calculate the minimum and maximum values of the tolerance ranges
        // expressed as a percentage of the total range. 
        // Gravity was in the range 0 to 10, UI values will differ, see Nova.Common.Gravity!
        override protected int MakeInternalValue(double value)
        {
            return (int)(value * 10);
        }

        override protected string Format(int value)
        {
            return Gravity.FormatWithUnit(value);
        }
    }
}

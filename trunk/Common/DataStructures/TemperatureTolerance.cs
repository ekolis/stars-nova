﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nova.Common
{
    [Serializable]
    public class TemperatureTolerance : EnvironmentTolerance
    {
        #region Construction

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Default constructor, required for serialization?.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public TemperatureTolerance() 
        {
            Name = "TemperatureTolerance";
        }

        #endregion

        // Calculate the minimum and maximum values of the tolerance ranges
        // expressed as a percentage of the total range. 
        // Temperature is in the range -200 to 200.
        override protected int MakeInternalValue(double value)
        {
            return (int)((200 + value) / 4);
        }

        override protected string Format(int value)
        {
            return Temperature.FormatWithUnit(value);
        }
    }
}

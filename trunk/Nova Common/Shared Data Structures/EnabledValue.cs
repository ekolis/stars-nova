// ============================================================================
// Nova. (c) 2008 Ken Reed
// (Refactored from EnabledCounter)
//
// A simple control that combines a checkbox with an up-down counter.
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon
{
    // ============================================================================
    // Type used by control.
    // ============================================================================

    [Serializable]
    public class EnabledValue
    {
        public bool IsChecked = false;
        public int NumericValue = 0;

        // ============================================================================
        // Construction (iniialising and non-initialising)
        // ============================================================================

        public EnabledValue(bool check, int value)
        {
            IsChecked = check;
            NumericValue = value;
        }

        public EnabledValue() { }

    }

}

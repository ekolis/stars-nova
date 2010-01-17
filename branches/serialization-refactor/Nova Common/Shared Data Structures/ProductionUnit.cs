// ============================================================================
// Nova. (c) 2010 Pavel Kazlou
//
// This interface is to be used in ProductionOrder for specifying what is
// the result of construction (a ship, a factory and so on).
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace NovaCommon.Shared_Data_Structures
{
    /// <summary>
    /// Generic interface for any single production unit: 1 ship, 1 factory, 
    /// 1 mine, 1% terraform, 1 alchemy and so on.
    /// The implementation should contain all the needed information 
    /// in order to perform actual construction (creating/changing game 
    /// objects).
    /// </summary>
    interface ProductionUnit
    {
        /// <summary>
        /// Method which checks whether another one unit can be constructed.
        /// The unit cannot be constructed either because of lack 
        /// of minerals/resources or because of other game restrictions
        /// (for example another factory cannot be constructed if maximum
        /// factory number limit is reached).
        /// </summary>
        /// <returns>true in case unit can be constructed, false otherwise</returns>
        bool IsSkipped();

        /// <summary>
        /// Method which performs actual construction
        /// </summary>
        void Construct();
    }
}

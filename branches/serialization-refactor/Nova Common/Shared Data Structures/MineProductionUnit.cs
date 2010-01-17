// ============================================================================
// Nova. (c) 2010 Pavel Kazlou
//
// This class is used for constructing 1 mine.
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
    /// Implementation of ProductionUnit for mine building.
    /// </summary>
    class MineProductionUnit : ProductionUnit
    {
        private Star star;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="star">Star on which the mine is to be constructed</param>
        public MineProductionUnit(Star star)
        {
            this.star = star;
        }

        #region ProductionUnit Members

        public bool IsSkipped()
        {
            if (star.Mines >= star.GetOperableMines())
            {
                return true;
            }

            if (!(star.ResourcesOnHand >= star.ThisRace.GetMineResources()))
            {
                return true;
            }

            return false;
        }

        public void Construct()
        {
            if (IsSkipped())
            {
                return;
            }

            star.ResourcesOnHand = star.ResourcesOnHand - star.ThisRace.GetMineResources();
            star.Mines++;
        }

        #endregion
    }
}

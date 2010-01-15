// ============================================================================
// Nova. (c) 2010 Pavel Kazlou
//
// This class is used for constructing 1 factory.
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
    /// Implementation of ProductionUnit for factory building.
    /// </summary>
    class FactoryProductionUnit : ProductionUnit
    {
        private Star star;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="star">Star on which the factory is to be constructed</param>
        public FactoryProductionUnit(Star star)
        {
            this.star = star;
        }

        #region ProductionUnit Members

        public bool IsSkipped()
        {
            if (star.Factories >= star.GetOperableFactories())
            {
                return true;
            }

            if (!(star.ResourcesOnHand >= star.ThisRace.GetFactoryResources()))
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

            star.ResourcesOnHand = star.ResourcesOnHand - star.ThisRace.GetFactoryResources();
            star.Factories++;
        }

        #endregion
    }
}

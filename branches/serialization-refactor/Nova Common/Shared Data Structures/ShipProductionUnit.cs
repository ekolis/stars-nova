// ============================================================================
// Nova. (c) 2010 Pavel Kazlou
//
// This class is used for constructing 1 ship.
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
    class ShipProductionUnit : ProductionUnit
    {
        private Star star;
        private ShipDesign shipDesign;

        public ShipProductionUnit(Star star, ShipDesign shipDesign)
        {
            this.star = star;
            this.shipDesign = shipDesign;
        }

        #region ProductionUnit Members

        public bool IsSkipped()
        {
            throw new NotImplementedException();
        }

        public void Construct()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

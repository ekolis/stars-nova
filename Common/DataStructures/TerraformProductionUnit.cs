// ============================================================================
// Nova. (c) 2010 Pavel Kazlou
//
// This class is used for "constructing" terraform 1%.
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
    class TerraformProductionUnit : ProductionUnit
    {
        private Star star;

        public TerraformProductionUnit(Star star)
        {
            this.star = star;
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

        public Resources NeededResources()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

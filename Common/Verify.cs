#region Copyright Notice
// ============================================================================
// Copyright (C) 2009, 2010 stars-nova
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

#region Module Description
// ===========================================================================
// This module provides mechanisms to do common verification tasks and throw
// appropriate exceptions.
// ===========================================================================
#endregion

namespace Nova.Common
{
    #region Using Statements
    using System.Diagnostics;
    using System;
    #endregion

    //-------------------------------------------------------------------
    /// <summary>
    /// Provides validating methods for different types and common verification.
    /// </summary>
    //-------------------------------------------------------------------
    public sealed class Verify 
    {
        #region Constructors
        //-------------------------------------------------------------------
        /// <summary>
        /// Private constructor so default constructor is not created since all methods are static.
        /// </summary>
        //-------------------------------------------------------------------
        private Verify()
        {
        }
        #endregion

        #region Methods
        //-------------------------------------------------------------------
        /// <summary>
        /// Validates the object is not a null reference.
        /// </summary>
        /// <param name="value">
        /// The <see cref="T:System.Object"/> to validate.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <i>value</i> is a null reference.
        /// </exception>
        //-------------------------------------------------------------------
        public static void NotNull( object value )
        {
            Verify.NotNull( value, "value" );
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Validates the value is not null.
        /// </summary>
        /// <param name="value">
        /// The <see cref="T:System.Object"/> to validate.
        /// </param>
        /// <param name="valueName">
        /// The <i>value</i> parameter's name.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// <i>value</i> is a null reference.
        /// </exception>
        //-------------------------------------------------------------------
        public static void NotNull( object value, string valueName )
        {
            ValidateName( valueName );
            if( true == object.ReferenceEquals( value, null ) )
            {
                throw new ArgumentNullException( valueName );
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// ValidateName
        /// </summary>
        /// <param name="name"></param>
        //-------------------------------------------------------------------
        [Conditional( "DEBUG" )]
        private static void ValidateName( string name )
        {
            Debug.Assert( null != name );
            Debug.Assert( 0 < name.Length );
        }
        #endregion
    }
}

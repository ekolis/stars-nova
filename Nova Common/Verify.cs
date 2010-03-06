namespace NovaCommon
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

namespace Tundra.Models.Tables
{
    /// <summary>
    /// Cache Model Class
    /// </summary>
    public class CacheTable
    {
        #region CacheTable - CTOR

        /// <summary>
        /// Defined the cache lifetime 
        /// </summary>
        public enum CacheLifetime
        {
            /// <summary>
            /// The clear on home
            /// </summary>
            ClearOnHome,

            /// <summary>
            /// The clear on logout
            /// </summary>
            ClearOnLogout,

            /// <summary>
            /// The clear on exit
            /// </summary>
            ClearOnExit,

            /// <summary>
            /// The clear on start
            /// </summary>
            ClearOnStart,

            /// <summary>
            /// The none
            /// </summary>
            None
        }

        #endregion

        #region CacheTable - Properties

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the cache lifetime.
        /// </summary>
        /// <value>
        /// The cache lifetime.
        /// </value>
        public CacheLifetime Lifetime { get; set; }

        #endregion
    }
}
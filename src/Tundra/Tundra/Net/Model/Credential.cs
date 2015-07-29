using GalaSoft.MvvmLight;
using Tundra.Interfaces.Credentials;

namespace Tundra.Net.Model
{
    /// <summary>
    /// Credential Class
    /// </summary>
    public class Credential : ObservableObject, INetCredential
    {
        #region Credential - Fields

        /// <summary>
        /// The domain
        /// </summary>
        private string _domain = string.Empty;

        /// <summary>
        /// The password
        /// </summary>
        private string _password = string.Empty;

        /// <summary>
        /// The user name
        /// </summary>
        private string _userName = string.Empty;

        #endregion

        #region Credential - Properties

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        public string Domain
        {
            get
            {
                return this._domain;
            }
            set
            {
                base.Set(ref this._domain, value);
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                base.Set(ref this._password, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                base.Set(ref this._userName, value);
            }
        }

        #endregion

        #region Credential - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="Credential"/> class.
        /// </summary>
        public Credential()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credential"/> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public Credential(string userName, string password)
        {
            this._userName = userName;
            this._password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credential" /> class.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain.</param>
        public Credential(string userName, string password, string domain)
            : this(userName, password)
        {
            this._domain = domain;
        }

        #endregion
    }
}
namespace Tundra.Interfaces.Credentials
{
    /// <summary>
    /// Network Credential Interface
    /// </summary>
    public interface INetCredential
    {
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        /// <value>
        /// The domain.
        /// </value>
        string Domain { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }
    }
}
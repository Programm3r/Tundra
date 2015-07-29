using System;

namespace Tundra.Interfaces.View
{
    /// <summary>
    /// View Interface Declaration
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Occurs when the data retrieval has been completed.
        /// </summary>
        event EventHandler<EventArgs> RetrieveDataEventHandler;

        /// <summary>
        /// Sets the user information.
        /// </summary>
        /// <value>
        /// The user information.
        /// </value>
        string UserInformation { set; }
    }
}
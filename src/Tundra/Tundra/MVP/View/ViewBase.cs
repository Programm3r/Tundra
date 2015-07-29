using System;
using Tundra.Interfaces.View;

namespace Tundra.MVP.View
{
    /// <summary>
    /// View Base Class. This calls implements the <see cref="IView"/> interface as well as <see cref="System.IDisposable"/>
    /// </summary>
    public abstract class ViewBase : IView, IDisposable
    {
        #region ViewBase - Private Fields

        /// <summary>
        /// Stores the user information
        /// </summary>
        private string _userInformation = string.Empty;

        #endregion

        #region IView - Implementation

        /// <summary>
        /// Occurs when the data retrieval has been completed.
        /// </summary>
        public event EventHandler<EventArgs> RetrieveDataEventHandler;

        /// <summary>
        /// Sets the user information.
        /// </summary>
        /// <value>
        /// The user information.
        /// </value>
        public string UserInformation
        {
            set
            {
                this._userInformation = value;
                this.OnUserInformationChanged(this._userInformation);
            }
        }

        #endregion

        #region ViewBase - Virtual Methods

        /// <summary>
        /// Raises the <see cref="RetrieveDataEventHandler" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <remarks>
        /// The event-invoking method that derived classes can override.
        /// </remarks>
        protected virtual void OnDataRetrieved(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribe
            // immediately after the null check and before the event is raised.
            var handler = this.RetrieveDataEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Called when the user information has changed.
        /// </summary>
        /// <param name="userInformation">The user information.</param>
        protected virtual void OnUserInformationChanged(string userInformation)
        { }

        #endregion

        #region IDisposable - Implementation

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ViewBase"/> is reclaimed by garbage collection.
        /// </summary>
        ~ViewBase()
        {
            // Finalize calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }

        #endregion
    }
}
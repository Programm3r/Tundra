using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using Ninject;
using Tundra.Interfaces.Data;

namespace Tundra.MVVM.ViewModel
{
    /// <summary>
    /// A simple base class for the ViewModel classes in the MVVM pattern.
    /// </summary>
    public class TundraBaseViewModel : ViewModelBase, INotifyPropertyChanged, IDisposable
    {
        #region TundraBaseViewModel - Fields

        /// <summary>
        /// The active user session
        /// </summary>
        private bool _activeUserSession;

        /// <summary>
        /// The busy information
        /// </summary>
        private string _busyInformation = string.Empty;

        /// <summary>
        /// The error occurred
        /// </summary>
        private bool _errorOccurred;

        /// <summary>
        /// The busy indicator
        /// </summary>
        private bool _isBusy;

        /// <summary>
        /// The token source
        /// </summary>
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        /// <summary>
        /// The user message
        /// </summary>
        private string _userMessage = string.Empty;

        #endregion

        #region TundraBaseViewModel - Properties

        /// <summary>
        /// Gets or sets a value indicating whether an active user session is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an active user session is running; otherwise, <c>false</c>.
        /// </value>
        public bool ActiveUserSession
        {
            get
            {
                return this._activeUserSession;
            }
            set
            {
                base.Set(ref this._activeUserSession, value);
            }
        }

        /// <summary>
        /// Gets or sets the busy information.
        /// </summary>
        /// <value>
        /// The busy information.
        /// </value>
        public string BusyInformation
        {
            get
            {
                return _busyInformation;
            }
            set
            {
                base.Set(ref this._busyInformation, value);
                if (this.BusyInformationChanged != null)
                {
                    this.BusyInformationChanged(this._busyInformation);
                }
            }
        }

        /// <summary>
        /// Gets or sets the busy information changed.
        /// </summary>
        /// <value>
        /// The busy information changed.
        /// </value>
        public Action<string> BusyInformationChanged { get; set; }

        /// <summary>
        /// Gets or sets the data store.
        /// </summary>
        /// <value>
        /// The data store.
        /// </value>
        public IDataAccess DataStore { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether an error has occurred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an error occurred; otherwise, <c>false</c>.
        /// </value>
        public bool ErrorOccurred
        {
            get
            {
                return this._errorOccurred;
            }
            set
            {
                base.Set(ref this._errorOccurred, value);
            }
        }

        /// <summary>
        /// Gets or sets whether the view model is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the view model is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get
            {
                return this._isBusy;
            }
            set
            {
                base.Set(ref this._isBusy, value, true);
                if (!this._isBusy)
                {
                    this.BusyInformation = string.Empty;
                }
                if (this.IsBusyChanged != null)
                {
                    this.IsBusyChanged(this._isBusy);
                }
            }
        }

        /// <summary>
        /// Gets or sets the is busy changed.
        /// </summary>
        /// <value>
        /// The is busy changed.
        /// </value>
        public Action<bool> IsBusyChanged { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public CancellationToken Token { get; private set; }

        /// <summary>
        /// Gets or sets the token source.
        /// </summary>
        /// <value>
        /// The token source.
        /// </value>
        public CancellationTokenSource TokenSource
        {
            get
            {
                return this._tokenSource;
            }
            set
            {
                this._tokenSource = value;
            }
        }

        /// <summary>
        /// Gets or sets the user message.
        /// </summary>
        /// <value>
        /// The user message.
        /// </value>
        public string UserMessage
        {
            get
            {
                return _userMessage;
            }
            set
            {
                base.Set(ref this._userMessage, value, true);
            }
        }

        #endregion

        #region TundraBaseViewModel - Injection Properties

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        /// <value>
        /// The navigation service.
        /// </value>
        [Inject]
        public INavigationService NavigationService { get; set; }

        #endregion

        #region TundraBaseViewModel - ICommand Properties

        /// <summary>
        /// Gets the cancel token.
        /// </summary>
        /// <value>
        /// The cancel token.
        /// </value>
        public ICommand CancelToken { get; private set; }

        /// <summary>
        /// Gets or sets the go back.
        /// </summary>
        /// <value>
        /// The go back.
        /// </value>
        public ICommand GoBack { get; private set; }

        #endregion

        #region TundraBaseViewModel - CTOR

        /// <summary>
        /// Prevents a default instance of the <see cref="TundraBaseViewModel"/> class from being created.
        /// </summary>
        protected TundraBaseViewModel()
        {
            this.Token = this._tokenSource.Token;
            this.CancelToken = new RelayCommand(this.ExecuteCancelCommand);
            this.GoBack = new RelayCommand(this.ExecuteGoBack, this.ExecuteCanGoBack);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TundraBaseViewModel"/> class.
        /// </summary>
        /// <param name="dataAccessProvider">The data access provider.</param>
        protected TundraBaseViewModel(IDataAccess dataAccessProvider)
            : this()
        {
            this.DataStore = dataAccessProvider;
        }

        #endregion

        #region TundraBaseViewModel - ICommand Implementation

        /// <summary>
        /// Determines whether the go back command is allowed to execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the command can execute; otherwise <c>false</c>.
        /// </returns>
        public virtual bool ExecuteCanGoBack()
        {
            return true;
        }

        /// <summary>
        /// Executes the go back command.
        /// </summary>
        public virtual void ExecuteGoBack()
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Executes the cancel command.
        /// </summary>
        private void ExecuteCancelCommand()
        {
            this.TokenSource.Cancel();
        }

        #endregion

        #region TundraBaseViewModel - Public Vitual Methods

        /// <summary>
        /// Loads state when the view model is loaded.
        /// </summary>
        /// <param name="navigationParameter">The navigation parameter.</param>
        /// <param name="pageState">The state to load.</param>
        public virtual void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
        }

        /// <summary>
        /// Saves state when the view model is unloaded.
        /// </summary>
        /// <param name="pageState">The state to save to.</param>
        public virtual void SaveState(Dictionary<string, object> pageState)
        {
        }

        /// <summary>
        /// Called when this instance has been initialized.
        /// </summary>
        public virtual void OnInitialized()
        {
        }

        #endregion

        #region TundraBaseViewModel - Public Static Methods

        /// <summary>
        /// Deserializes the json asynchronous.
        /// </summary>
        /// <typeparam name="T">The expected type of the value which will be deserialized.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        /// an instance of <see cref="T" />.
        /// </returns>
        public static Task<T> DeserializeJsonAsync<T>(string value)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value));
        }

        /// <summary>
        /// Serializes the json asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of the value which will be serialized.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Task<string> SerializeJsonAsync<T>(T value)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(value));
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Finalizes an instance of the <see cref="TundraBaseViewModel"/> class.
        /// </summary>
        ~TundraBaseViewModel()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources  
                if (this._tokenSource != null)
                {
                    this._tokenSource.Dispose();
                }
            }
            // free native resources if there are any.
        }

        #endregion
    }
}
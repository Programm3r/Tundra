using System;
using Tundra.Interfaces.Platform;

namespace Tundra.MVVM.ViewModel
{
    /// <summary>
    /// Our base-view model based on the ViewModelBase of MVVM-Light portable, with
    /// a generic Model.
    /// </summary>
    /// <typeparam name="TModel">Type of our current model-class we want to use</typeparam>
    public abstract class BaseViewModel<TModel> : TundraBaseViewModel
        where TModel : class
    {
        /// <summary>
        /// Our navigation service we need.
        /// </summary>
        private readonly INavigator _navigationService;

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public TModel Model { get; private set; }

        /// <summary>
        /// Our navigation service we need.
        /// </summary>
        /// <value>
        /// The navigation service.
        /// </value>
        public INavigator NavigationService
        {
            get
            {
                return this._navigationService;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel{TModel}" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentNullException">model</exception>
        protected BaseViewModel(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            this.Model = model;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel{TModel}" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <exception cref="System.ArgumentNullException">model
        /// or
        /// navigationService</exception>
        protected BaseViewModel(TModel model, INavigator navigationService)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (navigationService == null)
                throw new ArgumentNullException("navigationService");

            this.Model = model;
            this._navigationService = navigationService;
        }
    }
}
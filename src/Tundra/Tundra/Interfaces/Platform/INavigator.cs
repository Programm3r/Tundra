using System;
using Tundra.Interfaces.Container;

namespace Tundra.Interfaces.Platform
{
    /// <summary>
    /// Interface for navigation in an application.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        IContainer Container { get; }

        /// <summary>
        /// Gets whether there is a previous page to go back to.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Goes back to the previous page.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Navigates to the page for a given view model.
        /// </summary>
        /// <typeparam name="TViewModel">The type of view model to navigate to.</typeparam>
        /// <param name="parameter">An optional navigation parameter.</param>
        void NavigateToViewModel<TViewModel>(object parameter = null);

        /// <summary>
        /// Navigates to the page for a given view model.
        /// </summary>
        /// <param name="viewModelType">Type of the view model to navigate to.</param>
        /// <param name="parameter">An optional navigation parameter.</param>
        void NavigateToViewModel(Type viewModelType, object parameter = null);
    }
}
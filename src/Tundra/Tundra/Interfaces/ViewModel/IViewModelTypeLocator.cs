using System;
using System.Collections.Generic;

namespace Tundra.Interfaces.ViewModel
{
    /// <summary>
    /// View Model Type Locator Interface
    /// </summary>
    public interface IViewModelTypeLocator
    {
        /// <summary>
        /// Gets the specified view model instance.
        /// </summary>
        /// <value>
        /// The view model instance.
        /// </value>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns>
        /// the <see cref="System.Type" /> associated with the view model name
        /// </returns>
        dynamic this[string viewModelName] { get; }

        /// <summary>
        /// Gets or sets the view models.
        /// </summary>
        /// <value>
        /// The view models.
        /// </value>
        Dictionary<string, Type> ViewModels { get; set; }
    }
}
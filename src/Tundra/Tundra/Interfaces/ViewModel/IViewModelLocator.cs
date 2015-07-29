using Tundra.Collections;

namespace Tundra.Interfaces.ViewModel
{
    /// <summary>
    /// The base for our ViewModel locator.
    /// </summary>
    public interface IViewModelLocator
    {
        /// <summary>
        /// Gets the specified view model instance.
        /// </summary>
        /// <value>
        /// The specified view model instance..
        /// </value>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns></returns>
        dynamic this[string viewModelName] { get; }

        /// <summary>
        /// Gets or sets the view models.
        /// </summary>
        /// <value>
        /// The view models.
        /// </value>
        ObservableDictionary<string, dynamic> ViewModels { get; }
    }
}

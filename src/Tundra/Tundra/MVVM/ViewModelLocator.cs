using Tundra.Collections;
using Tundra.Interfaces.ViewModel;

namespace Tundra.MVVM
{
    /// <summary>
    /// The ViewModel locator.
    /// </summary>
    public class ViewModelLocator : IViewModelLocator
    {
        /// <summary>
        /// Set and get your ViewModels here.
        /// </summary>
        /// <param name="viewModelName">The name of the view model to get or set.</param>
        /// <returns>The view model selected.</returns>
        public dynamic this[string viewModelName]
        {
            get
            {
                if (this.ViewModels.ContainsKey(viewModelName))
                {
                    return this.ViewModels[viewModelName];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the view models.
        /// </summary>
        /// <value>
        /// The view models.
        /// </value>
        public ObservableDictionary<string, dynamic> ViewModels { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
        /// </summary>
        public ViewModelLocator()
        {
            this.ViewModels = new ObservableDictionary<string, dynamic>();
        }
    }
}
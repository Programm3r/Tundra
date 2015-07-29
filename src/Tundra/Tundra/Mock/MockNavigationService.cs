using GalaSoft.MvvmLight.Views;

namespace Tundra.Mock
{
    /// <summary>
    /// Design Navigation Service Class
    /// </summary>
    /// <remarks>
    /// Has no use what so ever besides the fact that it aids with design time development
    /// </remarks>
    public class MockNavigationService : INavigationService
    {
        /// <summary>
        /// If possible, instructs the navigation service
        /// to discard the current page and display the previous page
        /// on the navigation stack.
        /// </summary>
        public void GoBack()
        {

        }

        /// <summary>
        /// Instructs the navigation service to display a new page
        /// corresponding to the given key. Depending on the platforms,
        /// the navigation service might have to be configured with a
        /// key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        public void NavigateTo(string pageKey)
        {

        }

        /// <summary>
        /// Instructs the navigation service to display a new page
        /// corresponding to the given key, and passes a parameter
        /// to the new page.
        /// Depending on the platforms, the navigation service might
        /// have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        public void NavigateTo(string pageKey, object parameter)
        {

        }

        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
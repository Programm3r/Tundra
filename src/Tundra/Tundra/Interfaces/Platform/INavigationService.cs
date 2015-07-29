using System;

namespace Tundra.Interfaces.Platform
{
    /// <summary>
    /// The navigation service to
    /// enable page navigation.
    /// For all our platforms.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Go back to the previous page.
        /// Used for Windows Phone and Windows 8.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Navigate to a specific page.
        /// Used for Windows phone.
        /// </summary>
        /// <param name="page">The absolute URI to the page to navigate to.</param>
        void NavigateTo(Uri page);

        /// <summary>
        /// Used for Windows 8.
        /// </summary>
        /// <param name="pageToNavigateTo">The page to navigate to.</param>
        void NavigateTo(Type pageToNavigateTo);
    }
}
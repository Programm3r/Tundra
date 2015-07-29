using System;
using Tundra.Interfaces.Platform;
using Tundra.Interfaces.View;
using Tundra.MVVM.ViewModel;

namespace Tundra.Mock
{
    /// <summary>
    /// Mock Tool Window Class
    /// </summary>
    /// <remarks>
    /// Has no use what so ever besides the fact that it aids with design time development
    /// </remarks>
    public class MockToolWindow : IToolWindow
    {
        /// <summary>
        /// Occurs when tool window is closed.
        /// </summary>
        public event EventHandler CloseRequested;

        /// <summary>
        /// Shows the tool window.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="showCloseAction">if set to <c>true</c> the default close action will be shown.</param>
        public void ShowToolWindow(string title, string message, bool showCloseAction = false)
        {
        }

        /// <summary>
        /// Shows the tool window.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <typeparam name="TView">The type of the view.</typeparam>
        /// <param name="title">The title.</param>
        /// <param name="showCloseAction">if set to <c>true</c> the default close action will be shown.</param>
        /// <example>
        /// // inject the tool window interface
        /// IToolWindow toolWindow;
        /// // call the show tool window method
        /// toolWindow.ShowToolWindow&lt;PersonViewModel, IPersonView&gt;("This is some title");
        /// // call the show tool window method without any body view
        /// toolWindow.ShowToolWindow("This is a title", "this is some message text");
        /// </example>
        public void ShowToolWindow<TViewModel, TView>(string title, bool showCloseAction = true)
            where TViewModel : TundraBaseViewModel
            where TView : IBaseView
        {

        }

        /// <summary>
        /// Dismisses the tool window.
        /// </summary>
        public void DismissToolWindow()
        {
        }
    }
}
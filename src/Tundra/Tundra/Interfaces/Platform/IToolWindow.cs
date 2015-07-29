using System;
using Tundra.Interfaces.View;
using Tundra.MVVM.ViewModel;

namespace Tundra.Interfaces.Platform
{
    /// <summary>
    /// Tool Window Interface Declaration
    /// </summary>
    public interface IToolWindow
    {
        /// <summary>
        /// Occurs when tool window is closed.
        /// </summary>
        event EventHandler CloseRequested;

        /// <summary>
        /// Shows the tool window.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="showCloseAction">if set to <c>true</c> the default close action will be shown.</param>
        void ShowToolWindow(string title, string message, bool showCloseAction = false);

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
        void ShowToolWindow<TViewModel, TView>(string title, bool showCloseAction = true)
            where TViewModel : TundraBaseViewModel
            where TView : IBaseView;

        /// <summary>
        /// Dismisses the tool window.
        /// </summary>
        void DismissToolWindow();
    }
}
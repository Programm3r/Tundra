using Tundra.Interfaces.View;

namespace Tundra.Interfaces.Presenter
{
    /// <summary>
    /// Presenter Base Interface Declaration
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <typeparam name="TProcessor">The type of the processor.</typeparam>
    public interface IPresenterBase<out TView, out TProcessor>
        where TView : IView
        where TProcessor : class
    {
        /// <summary>
        /// Gets the processor.
        /// </summary>
        /// <value>
        /// The processor.
        /// </value>
        TProcessor Processor { get; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        TView View { get; }
    }
}
namespace Tundra.Interfaces.View
{
    /// <summary>
    /// Base View Interface Declaration
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        /// <value>
        /// The data context.
        /// </value>
        object DataContext { get; set; }
    }
}
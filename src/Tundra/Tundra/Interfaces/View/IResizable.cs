namespace Tundra.Interfaces.View
{
    /// <summary>
    /// Resizable View Interface
    /// </summary>
    public interface IResizable
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        int Height { get; set; }
    }
}
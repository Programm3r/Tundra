using System;

namespace Tundra.Interfaces.Errors
{
    /// <summary>
    /// Error Information Interface
    /// </summary>
    public interface IErrorInformation
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IErrorInformation"/> was caused by a cancellation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancellation; otherwise, <c>false</c>.
        /// </value>
        bool Cancellation { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        Exception Error { get; set; }
    }
}
using System;

namespace Tundra.Extension
{
    /// <summary>
    /// Event Handler Extension Methods
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raises the specified sender.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}
using Tundra.Interfaces.Container;

namespace Tundra.Container
{
    /// <summary>
    /// Inversion of Control Class
    /// </summary>
    /// <remarks>
    /// This is a singleton implementation and is marked as sealed. Thus inheritance cannot take place.
    /// </remarks>
    public sealed class Ioc
    {
        /// <summary>
        /// The container
        /// </summary>
        private static IContainer _container;

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_lock)
                    {
                        if (_container == null)
                        {
                            _container = new SimpleContainer();
                        }
                    }
                }

                return _container;
            }
            set
            {
                lock (_lock)
                {
                    _container = value;
                }
            }
        }
    }
}
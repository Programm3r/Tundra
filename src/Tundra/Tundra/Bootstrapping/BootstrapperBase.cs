using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Ninject;
using Ninject.Modules;
using Tundra.Interfaces.ViewModel;
using Tundra.Modules;

namespace Tundra.Bootstrapping
{
    /// <summary>
    /// This is a simple implementation of our boot-strapper for Ninject.
    /// </summary>
    public abstract class BootstrapperBase : IDisposable
    {
        #region BaseBootstrapper - Properties

        /// <summary>
        /// The container (Ninject Kernel) used to bind the types to the interfaces.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static IKernel Container { get; private set; }

        /// <summary>
        /// The ninject modules to be loaded by the container (Ninject Kernel)
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public ObservableCollection<INinjectModule> Modules { get; private set; }

        /// <summary>
        /// The ViewModel-Locator that holds the instantiated ViewModels to bind the XAML against.
        /// </summary>
        /// <value>
        /// The view model locator.
        /// </value>
        /// <exception cref="System.Exception">Initialize a new instance of an IKernel (Container) before using the view model locator.</exception>
        public IViewModelLocator ViewModelLocator
        {
            get
            {
                if (Container == null)
                {
                    throw new Exception("Initialize a new instance of an IKernel (Container) before using the view model locator.");
                }

                return Container.Get<IViewModelLocator>();
            }
        }

        #endregion

        #region BaseBootstrapper - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        protected BootstrapperBase()
        {
            // instantiate the ninject modules collection
            this.Modules = new ObservableCollection<INinjectModule>()
            {
                // add the view model locator module to the collection
                new ViewModelLocatorModule()
            };
            // instantiate the ninject kernel
            Container = new StandardKernel(this.Modules.ToArray());
            // register to the collection changed event
            this.Modules.CollectionChanged += ModulesOnCollectionChanged;
        }

        #endregion

        #region BaseBootstrapper - Private Methods

        /// <summary>
        /// Occurred when the module collection has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="changedEventArgs">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private static void ModulesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs changedEventArgs)
        {
            switch (changedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Container.Load(changedEventArgs.NewItems.Cast<INinjectModule>());
                    break;
            }
        }

        #endregion

        #region IDisposable - Implementation

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        ~BootstrapperBase()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.Modules != null)
                {
                    // register to the collection changed event
                    this.Modules.CollectionChanged -= ModulesOnCollectionChanged;
                }
            }
            // free native resources if there are any.
        }

        #endregion
    }
}
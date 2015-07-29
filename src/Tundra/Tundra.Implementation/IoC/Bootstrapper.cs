using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Ninject;
using Tundra.Bootstrapping;
using Tundra.Extension;
using Tundra.Implementation.Modules;
using Tundra.Implementation.ViewModel;
using Tundra.Mock;

namespace Tundra.Implementation.IoC
{
    /// <summary>
    /// Bootstrapper Class
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Gets the main view model.
        /// </summary>
        /// <value>
        /// The main view model.
        /// </value>
        public MainViewModel MainViewModel
        {
            get
            {
                return Container.Get<MainViewModel>();
            }
        }

        /// <summary>
        /// Gets the registration view model.
        /// </summary>
        /// <value>
        /// The registration view model.
        /// </value>
        public RegistrationViewModel RegistrationViewModel
        {
            get
            {
                return Container.Get<RegistrationViewModel>();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void Initialize()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Container.BindToConstant<INavigationService, MockNavigationService>(new MockNavigationService());
            }
            else
            {
                // non platform specific bindings
            }

            // initialize the modules
            this.Modules.Add(new MyDataAccessModule("testDatabase"));
            this.Modules.Add(new MyCacheProviderModule());
            // lastly bind the view models
            this.Modules.Add(new ViewModelModule());
        }
    }
}
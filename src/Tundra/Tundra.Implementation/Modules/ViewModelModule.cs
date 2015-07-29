using Ninject.Modules;
using Tundra.Extension;
using Tundra.Implementation.ViewModel;

namespace Tundra.Implementation.Modules
{
    /// <summary>
    /// View Model Module Class
    /// </summary>
    public class ViewModelModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // BindToSelfSingleton is extension methods that can be located in Tundra
            base.Kernel.BindToSelfSingleton<MainViewModel>();
            base.Kernel.BindToSelfSingleton<RegistrationViewModel>();
        }
    }
}
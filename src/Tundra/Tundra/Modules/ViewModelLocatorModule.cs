using Ninject.Modules;
using Tundra.Interfaces.ViewModel;
using Tundra.MVVM;

namespace Tundra.Modules
{
    /// <summary>
    /// View Model Locator Module Class
    /// </summary>
    public class ViewModelLocatorModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            base.Kernel.Bind<IViewModelLocator>().ToConstant(new ViewModelLocator()).InSingletonScope();
        }
    }
}
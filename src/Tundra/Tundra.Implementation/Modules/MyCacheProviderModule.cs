using Ninject.Modules;
using Tundra.Implementation.Provider;
using Tundra.Implementation.Provider.Interfaces;

namespace Tundra.Implementation.Modules
{
    public class MyCacheProviderModule : NinjectModule
    {
        public override void Load()
        {
            base.Kernel.Bind<IMyCacheProvider>().To<MyCacheProvider>().InSingletonScope();
        }
    }
}
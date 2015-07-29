using Ninject;
using Ninject.Modules;
using Tundra.Bootstrapping;
using Tundra.Extension;
using Tundra.Interfaces.Data;
using Tundra.Modules;
using Tundra.MVVM.ViewModel;

namespace Tundra.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            var viewModel = bootstrapper.ViewModelLocator[typeof(PersonViewModel).Name];
            if (viewModel != null)
            {
            }
        }
    }

    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Container.Load(new ViewModelModule(), new DataAccessModule("testDatabase"));

            this.StrapViewModels();
        }

        private void StrapViewModels()
        {
            base.ViewModelLocator.ViewModels.Add(typeof(PersonViewModel).Name, Container.Get<PersonViewModel>());
        }
    }

    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            base.Kernel.BindToSelf<PersonViewModel>();
        }
    }

    public class PersonViewModel : TundraBaseViewModel
    {
        public string Name { get; set; }

        public PersonViewModel(IDataAccess dataAccess)
            : base(dataAccess)
        {

        }
    }
}

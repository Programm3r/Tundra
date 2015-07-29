using GalaSoft.MvvmLight.Views;
using Tundra.Implementation.ViewModel.NavigationKeys;
using Tundra.WindowsApp.View;

namespace Tundra.WindowsApp.Extensions
{
    internal static class NavigationServiceExtensions
    {
        internal static NavigationService ConfigureNavigationService(this NavigationService navigationService)
        {
            navigationService.Configure(ViewModelPageKeys.MainViewModelKey, typeof(MainView));
            navigationService.Configure(ViewModelPageKeys.RegistrationViewModelKey, typeof(RegistrationView));

            return navigationService;
        }
    }
}
using System;
using System.Windows;
using GalaSoft.MvvmLight.Views;
using Ninject;
using Tundra.Bootstrapping;
using Tundra.Extension;
using Tundra.Implementation.IoC;
using Tundra.Implementation.ViewModel.NavigationKeys;

namespace Tundra.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            // this was added because the App.xaml resources aren't being loaded as with WinRT and Windows Phone App.xaml pages
            FindResource("Bootstrapper");

            var navigationService = new FrameNavigationService();
            navigationService.Configure(ViewModelPageKeys.MainViewModelKey, new Uri("MainView.xaml", UriKind.Relative));
            navigationService.Configure(ViewModelPageKeys.RegistrationViewModelKey, new Uri("RegistrationView.xaml", UriKind.Relative));

            BootstrapperBase.Container.BindToConstant<INavigationService, FrameNavigationService>(navigationService);    
        }
    }
}
# BootstrapperBase

## Introduction

> _Please note that knowledge about Ninject or for that matter any DI, will be required._

The following describes what the purpose of the `BootstrapperBase `is, as well as how to implement it. As we all know, the bootstrapper is responsible for the initialization of an application. I've found myself writing and rewriting numerous bootstrappers, hence the reason for this implementation. It doesn't solve all the bootstrapping issues in XAML based applications, but it's getting close. 

First things first, let's look at the members of the BootstrapperBase:
```csharp
    public abstract class BootstrapperBase
    {
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
                ...
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        protected BootstrapperBase()
        {
			...
        }
    }
```
As you probable noticed the base bootstrapper has implementation for the Ninject IKernel as well as the Ninject Modules. Lastly, I have added a view model locator. Seeing that the `BootstrapperBase` won't know which view models will be added / used, it can only provide a mechanism to add the bindings. This removes some of the scaffolding that is required in a bootstrapper.

So to start off with the implementation of the base class, we'll need to create a class derived from BootstrapperBase:
```csharp
    public class Bootstrapper : BootstrapperBase
    {
        ...
    }
```

So the next step is to declare our `Bootstrapper `in our App.xaml page. This will allow us to bind against our bootstrapper in XAML pages. The declaration below will actually create an instance of our `Bootstrapper`
```xaml
<Application
    x:Class="Tundra.WindowsApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:Tundra.WindowsApp"
    xmlns:IoC="using:Tundra.Implementation.IoC"
    xmlns:sys="using:System">
    <Application.Resources>
        <IoC:Bootstrapper x:Key="Bootstrapper"/>
    </Application.Resources>
</Application>
```
Going forward, I would like to mentioned that there are two implementation that can be used to get hold of the view models through the bootstrapper:

+ 1: Declare properties of the view models and use the Container to get hold of the instances
 + **Advantage:**
   + Blendability
   + IntelliSense during design time
+ 2: Add the view models instances to the view model locator dictionary
 + **Advantage:**
   + Blendability
 + **Disadvantage:**
   + No IntelliSense during design time - the dictionary within the view model locator (`IViewModelLocator`) makes use of the `dynamic` datatype to store the various instances of the view models. It is my understanding that the designer can't resolve the type during design time. 

### Option 1 - View Model Property Declaration:
You'll notice there is a property called `MainViewModelInstance`, which contains the instance of the MainViewModel. The DI (Dependency Injection) binding occurred inside a Ninject Module - see code below:
```csharp
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Gets the main view model instance.
        /// </summary>
        /// <value>
        /// The main view model instance.
        /// </value>
        public MainViewModel MainViewModelInstance
        {
            get
            {
                return Container.Get<MainViewModel>();
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
                // design time bindings
            }
            else
            {
                // actual binding implementations
            }

            this.Modules.Add(new ViewModelModule());
        }
    }
```
**View Model Module** - this was used to setup the DI registration:
```csharp
    public class ViewModelModule : NinjectModule
    {
        public override void Load()
        {
            // BindToSelfSingleton is extension methods that can be located in Tundra
            base.Kernel.BindToSelfSingleton<MainViewModel>();
            base.Kernel.BindToSelfSingleton<RegistrationViewModel>();
        }
    }
```

Now that we have declared our `BootStrapper`, we can use it in our front-end XAML page (WinRT Example):
```xaml
<Page
    x:Class="Tundra.WindowsApp.View.MainView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:Tundra.WindowsApp"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    DataContext="{Binding MainViewModelInstance, Source={StaticResource Bootstrapper}}">

    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid>
            <TextBlock Grid.Row="3" Text="{Binding SomePropInViewModel}"
                       Foreground="Aqua" FontSize="25" />
    </Grid>
</Page>
```

### Option 2 - View Model Locator:
Everything is pretty much the same as the above example except for the following. I made the following change in the `Initialize `method, which adds a new instance of the `MainViewModel `to the locator.
```csharp
        protected void Initialize()
        {
            // code the same as above....

            // lastly bind the view models
            this.Modules.Add(new ViewModelModule());
            // add the view models to the dictionary
            // make use of the class name as the key in the dictionary
            base.ViewModelLocator.ViewModels.AddRange(new Dictionary<string, dynamic>()
            {
                {typeof(MainViewModel).Name, Container.Get<MainViewModel>()}
            });
        }
```
Our XAML page has the changes. It will now make use of the view model locator in it's `DataContext`:
```xaml
<Page
    x:Class="Tundra.WindowsApp.View.MainView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:Tundra.WindowsApp"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    DataContext="{Binding ViewModelLocator[MainViewModel], Source={StaticResource Bootstrapper}}">
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
        <Grid>
            <TextBlock Grid.Row="3" Text="{Binding SomePropInViewModel}"
                       Foreground="Aqua" FontSize="25" />
    </Grid>
</Page>
```
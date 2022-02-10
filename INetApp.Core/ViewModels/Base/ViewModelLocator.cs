using INetApp.APIWebServices;
using INetApp.Services;
using INetApp.Services.Dependency;
using INetApp.Services.Identity;
using INetApp.Services.NFC;
using INetApp.Services.Settings;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace INetApp.ViewModels.Base
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            // Services - by default, TinyIoC will register interface registrations as singletons.
            SettingsService settingsService = new SettingsService();
            IDeviceService deviceService = Xamarin.Forms.DependencyService.Get<IDeviceService>();
            RestApiImpl restapi = new RestApiImpl();
            Xamarin.Forms.DependencyService.RegisterSingleton<ISettingsService>(settingsService);
            Xamarin.Forms.DependencyService.RegisterSingleton<INavigationService>(new NavigationService(settingsService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDialogService>(new DialogService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IIdentityService>(new IdentityService(settingsService, deviceService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDependencyService>(new Services.Dependency.DependencyService());

            Xamarin.Forms.DependencyService.RegisterSingleton<IDBService>(new DBService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IRepositoryWebService>(new RepositoryWebService(restapi, new IdentityService(settingsService, deviceService)));
            Xamarin.Forms.DependencyService.RegisterSingleton<IRepositoryService>(new RepositoryService(new DBService()));

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            Xamarin.Forms.DependencyService.Register<LoginViewModel>();
            Xamarin.Forms.DependencyService.Register<MainViewModel>();
            Xamarin.Forms.DependencyService.Register<CategoryViewModel>();
            Xamarin.Forms.DependencyService.Register<MessageViewModel>();
            Xamarin.Forms.DependencyService.Register<MessageFavoriteViewModel>();
            Xamarin.Forms.DependencyService.Register<MessageDetailsViewModel>();
            Xamarin.Forms.DependencyService.Register<OptionsViewModel>();
            Xamarin.Forms.DependencyService.Register<InfoViewModel>();
            Xamarin.Forms.DependencyService.Register<LectorQRViewModel>();
        }

        public static void UpdateDependencies()
        {
            // Change injected dependencies
            IRepositoryWebService repositoryWebService = Xamarin.Forms.DependencyService.Get<IRepositoryWebService>();
            IRepositoryService repositoryService = Xamarin.Forms.DependencyService.Get<IRepositoryService>();

            Xamarin.Forms.DependencyService.RegisterSingleton<IUserService>(new UserService(repositoryWebService));
            Xamarin.Forms.DependencyService.RegisterSingleton<ICategoryService>(new CategoryService(repositoryWebService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IMessageService>(new MessageService(repositoryWebService, repositoryService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IOptionsService>(new OptionsService(repositoryWebService));
            Xamarin.Forms.DependencyService.RegisterSingleton<ILectorQRService>(new LectorQRService(repositoryWebService));
            Xamarin.Forms.DependencyService.RegisterSingleton<INFCService>(new NFCService(repositoryWebService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IPushService>(new PushService(repositoryWebService));
            //Xamarin.Forms.DependencyService.RegisterSingleton<IPushService>(new PushService());
        }

        public static T Resolve<T>() where T : class
        {
            return Xamarin.Forms.DependencyService.Get<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Element view = bindable as Element;
            if (view == null)
            {
                return;
            }

            Type viewType = view.GetType();
            string viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            string viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            Type viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            object viewModel = Activator.CreateInstance(viewModelType);

            view.BindingContext = viewModel;
        }

        //public static void CustomPlatforms(Action<ViewModelLocator> execute)
        //{
        //    execute?.Invoke(this);
        //}

        //public static void RegisterAll(Action<ViewModelLocator> execute) => ViewModelLocator.CustomPlatforms(execute);
    }

}
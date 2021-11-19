using System;
using System.Globalization;
using System.Reflection;
using INetApp.APIWebServices;
using INetApp.Services;
using INetApp.Services.Basket;
using INetApp.Services.Catalog;
using INetApp.Services.Dependency;
using INetApp.Services.FixUri;
using INetApp.Services.Identity;
using INetApp.Services.Marketing;
using INetApp.Services.Order;
using INetApp.Services.RequestProvider;
using INetApp.Services.Settings;
using INetApp.Services.User;
using Xamarin.Forms;

namespace INetApp.ViewModels.Base
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        static ViewModelLocator()
        {
            // Services - by default, TinyIoC will register interface registrations as singletons.
            SettingsService settingsService = new SettingsService();
            RequestProvider requestProvider = new RequestProvider();
            IDeviceService deviceService = Xamarin.Forms.DependencyService.Get<IDeviceService>();
            RestApi restapi = new RestApi();
            Xamarin.Forms.DependencyService.RegisterSingleton<ISettingsService>(settingsService);
            Xamarin.Forms.DependencyService.RegisterSingleton<INavigationService>(new NavigationService(settingsService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDialogService>(new DialogService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IRequestProvider>(requestProvider);
            Xamarin.Forms.DependencyService.RegisterSingleton<IIdentityService>(new IdentityService(requestProvider, settingsService, deviceService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDependencyService>(new Services.Dependency.DependencyService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IFixUriService>(new FixUriService(settingsService));

            Xamarin.Forms.DependencyService.RegisterSingleton<IRepositoryWebService>(new RepositoryWebService(restapi, new IdentityService(requestProvider, settingsService, deviceService)));

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            Xamarin.Forms.DependencyService.Register<LoginViewModel>();
            Xamarin.Forms.DependencyService.Register<MainViewModel>();
            Xamarin.Forms.DependencyService.Register<BandejaViewModel>();

            Xamarin.Forms.DependencyService.Register<CatalogViewModel>();
            Xamarin.Forms.DependencyService.Register<CheckoutViewModel>();
            Xamarin.Forms.DependencyService.Register<OrderDetailViewModel>();
            Xamarin.Forms.DependencyService.Register<ProfileViewModel>();
            Xamarin.Forms.DependencyService.Register<CampaignViewModel>();
            Xamarin.Forms.DependencyService.Register<CampaignDetailsViewModel>();
        }

        public static void UpdateDependencies()
        {
            // Change injected dependencies
            IRequestProvider requestProvider = Xamarin.Forms.DependencyService.Get<IRequestProvider>();
            IFixUriService fixUriService = Xamarin.Forms.DependencyService.Get<IFixUriService>();
            Xamarin.Forms.DependencyService.RegisterSingleton<IBandejaService>(new BandejaService(requestProvider, fixUriService));
            Xamarin.Forms.DependencyService.RegisterSingleton<ICampaignService>(new CampaignService(requestProvider, fixUriService));
            Xamarin.Forms.DependencyService.RegisterSingleton<ICatalogService>(new CatalogService(requestProvider, fixUriService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IOrderService>(new OrderService(requestProvider));
            Xamarin.Forms.DependencyService.RegisterSingleton<IUserService>(new UserService(requestProvider));

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
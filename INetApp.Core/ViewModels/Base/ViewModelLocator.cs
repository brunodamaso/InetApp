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
using INetApp.Services;
using INetApp.Services;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using INetApp.APIWebServices;
using INetApp.Services.Theme;

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
            var settingsService = new SettingsService ();
            var requestProvider = new RequestProvider ();
            var deviceService = Xamarin.Forms.DependencyService.Get<IDeviceService>();
            var restapi = new RestApi();
            Xamarin.Forms.DependencyService.RegisterSingleton<ISettingsService>(settingsService);
            Xamarin.Forms.DependencyService.RegisterSingleton<INavigationService>(new NavigationService(settingsService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDialogService>(new DialogService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IRequestProvider>(requestProvider);
            Xamarin.Forms.DependencyService.RegisterSingleton<IIdentityService>(new IdentityService(requestProvider ,settingsService , deviceService));
            Xamarin.Forms.DependencyService.RegisterSingleton<IDependencyService>(new Services.Dependency.DependencyService());
            Xamarin.Forms.DependencyService.RegisterSingleton<IFixUriService>(new FixUriService(settingsService));

            Xamarin.Forms.DependencyService.RegisterSingleton<IRepositoryWebService>(new RepositoryWebService(restapi, new IdentityService(requestProvider, settingsService , deviceService)));

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            Xamarin.Forms.DependencyService.Register<BasketViewModel> ();
            Xamarin.Forms.DependencyService.Register<CatalogViewModel> ();
            Xamarin.Forms.DependencyService.Register<CheckoutViewModel> ();
            Xamarin.Forms.DependencyService.Register<LoginViewModel> ();
            Xamarin.Forms.DependencyService.Register<MainViewModel> ();
            Xamarin.Forms.DependencyService.Register<OrderDetailViewModel> ();
            Xamarin.Forms.DependencyService.Register<ProfileViewModel> ();
            Xamarin.Forms.DependencyService.Register<CampaignViewModel> ();
            Xamarin.Forms.DependencyService.Register<CampaignDetailsViewModel> ();
        }

        public static void UpdateDependencies()
        {
            // Change injected dependencies
            var requestProvider = Xamarin.Forms.DependencyService.Get<IRequestProvider>();
            var fixUriService = Xamarin.Forms.DependencyService.Get<IFixUriService>();
            Xamarin.Forms.DependencyService.RegisterSingleton<IBasketService>(new BasketService(requestProvider, fixUriService));
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
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            var viewModel = Activator.CreateInstance (viewModelType);

            view.BindingContext = viewModel;
        }

        //public static void CustomPlatforms(Action<ViewModelLocator> execute)
        //{
        //    execute?.Invoke(this);
        //}

        //public static void RegisterAll(Action<ViewModelLocator> execute) => ViewModelLocator.CustomPlatforms(execute);
    }
  
}
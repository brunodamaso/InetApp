using INetApp;
using INetApp.Services.Settings;
using INetApp.Services.Theme;
using INetApp.ViewModels.Base;
using INetApp.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace INetApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            Xamarin.Forms.Application.Current.On<Windows>().SetImageDirectory("Assets");

            InitApp();

            MainPage = new AppShell ();
        }

        private void InitApp()
        {
            //_settingsService = ViewModelLocator.Resolve<ISettingsService>();
            ViewModelLocator.UpdateDependencies();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            base.OnStart();

            OnResume();
        }

        protected override void OnSleep()
        {
            SetStatusBar();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            SetStatusBar();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetStatusBar();
            });
        }

        void SetStatusBar()
        {
            var nav = Current.MainPage as NavigationPage;

            var e = DependencyService.Get<ITheme>();
            if (Current.RequestedTheme == OSAppTheme.Dark)
            {
                //e?.SetStatusBarColor(Color.Black, false);
                e?.SetStatusBarColor(new Color(0, 156, 196), false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = new Color(0, 156, 196); ;// Color.Black;
                    nav.BarTextColor = new Color(0,156,196) ;
                }
            }
            else
            {
                //e?.SetStatusBarColor(Color.White, true);
                e?.SetStatusBarColor(new Color(0, 156, 196), true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = new Color(0, 156, 196);  //Color.White;
                    nav.BarTextColor = new Color(0, 156, 196);
                }
            }
        }
    }
}
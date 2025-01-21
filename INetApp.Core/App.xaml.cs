using System.Threading.Tasks;
using INetApp.Services;
using INetApp.Services.Theme;
using INetApp.ViewModels.Base;
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

            Current.On<Windows>().SetImageDirectory("Assets");

            InitApp();

            MainPage = new AppShell();
        }

        private void InitApp()
        {
            //_settingsService = ViewModelLocator.Resolve<ISettingsService>();
            ViewModelLocator.UpdateDependencies();
        }

        private Task InitNavigation()
        {
            INavigationService navigationService = ViewModelLocator.Resolve<INavigationService>();
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
            base.OnResume();
            SetStatusBar();

            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        //protected override bool OnBackButtonPressed()
        //{
        //	UnsubscribeEvents();
        //	CrossNFC.Current.StopListening();
        //	return base.OnBackButtonPressed();
        //}

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetStatusBar();
            });
        }

        private void SetStatusBar()
        {
            NavigationPage nav = Current.MainPage as NavigationPage;

            ITheme e = DependencyService.Get<ITheme>();
            //todo quitar cuando se quiera usar temas
            Current.UserAppTheme = OSAppTheme.Light;
            if (Current.RequestedTheme == OSAppTheme.Dark)
            {
                //e?.SetStatusBarColor(Color.Black, false);
                e?.SetStatusBarColor(new Color(52,99,172), false);
                if (nav != null)
                {
                    nav.BarBackgroundColor = new Color(52,99,172); ;// Color.Black;
                    nav.BarTextColor = new Color(52,99,172);
                }
            }
            else
            {
                //e?.SetStatusBarColor(Color.White, true);
                e?.SetStatusBarColor(new Color(52,99,172), true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = new Color(52,99,172);  //Color.White;
                    nav.BarTextColor = new Color(52,99,172);
                }
            }
        }
    }
}
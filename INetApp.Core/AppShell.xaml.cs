using System;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Resources;
using INetApp.Services;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using INetApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace INetApp
{
    public partial class AppShell : Shell
    {
        public const string MIME_TYPE = "*/*";
        private readonly ISettingsService settingsService;
        public bool isLogin = false;
        public AppShell()
        {
            InitializeRouting();
            InitializeComponent();
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
                       
            VersionApp.Text = string.Format("{0} {1}: {2}", Literales.version, DeviceInfo.Platform, VersionTracking.CurrentVersion);
            //todo verificar si es la version de uwp o la solucion
            IUserService userService = ViewModelLocator.Resolve<IUserService>();


            isLogin = Login().Result;

            if (!isLogin)
            {
                GoToAsync("//Login");
                NameInitial.Text = settingsService.NameInitial;
                NameUser.Text = settingsService.NameFull;
            }

        }

        private Task<bool> Login()
        {
            return Task.Factory.StartNew(() =>
            {
                bool islogin;
                IUserService userService = ViewModelLocator.Resolve<IUserService>();

                if (string.IsNullOrEmpty(settingsService.AuthAccessToken))
                {
                    islogin = false;
                }
                else
                {
                    UserLoggedDto userLoggedDto = userService.GetUserLoggedDto().Result;
                    if (userLoggedDto.IsOk) // && userLoggedDto.UserLoggedModel.permission)
                    {
                        //if (userService.CheckVersion().Result)
                        //{
                        //    try
                        //    {
                        //        this.DisplayAlert(Literales.dialog_approve_install,
                        //            Literales.dialog_approve_title_search,
                        //            Literales.dialog_approve_positive_install);

                        //        Browser.OpenAsync(settingsService.Url, BrowserLaunchMode.SystemPreferred);
                        //        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Console.WriteLine(ex.Message);
                        //    }
                        //}
                        islogin = true;
                        NameInitial.Text = settingsService.NameInitial;
                        NameUser.Text = settingsService.NameFull;
                    }
                    else
                    {
                        islogin = false;
                    }
                }

                return islogin;
            });
        }

        private void InitializeRouting()
        {
            Routing.RegisterRoute("//MainView", typeof(MainView));
            Routing.RegisterRoute("//MainView/LectorQR", typeof(LectorQRView));
            Routing.RegisterRoute("//MainView/Category", typeof(CategoryView));
            Routing.RegisterRoute("//MainView/MessageFavorite", typeof(MessageFavoriteView));
            Routing.RegisterRoute("//MainView/Options", typeof(OptionsView));
            Routing.RegisterRoute("Message", typeof(MessageView));
            Routing.RegisterRoute("MessageDetails", typeof(MessageDetailsView));
            Routing.RegisterRoute("MessageDetails/WebView", typeof(WebInecoView));
            Routing.RegisterRoute("WebView", typeof(WebInecoView));
            Routing.RegisterRoute("InfoView", typeof(InfoView));

        }
    
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Login?Logout=true");
        }
    }
}
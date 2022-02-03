using System;
using System.Text;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.NFC;
using INetApp.Services;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using INetApp.Views;
using Xamarin.Forms;

namespace INetApp
{
    public partial class AppShell : Shell
    {
        public const string MIME_TYPE = "*/*";
        private readonly ISettingsService settingsService;
        public AppShell()
        {
            InitializeRouting();
            InitializeComponent();
            settingsService = ViewModelLocator.Resolve<ISettingsService>();

            bool isLogin = Login().Result;

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
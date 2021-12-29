using System;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Services.Settings;
using INetApp.Services;
using INetApp.ViewModels.Base;
using INetApp.Views;
using Xamarin.Forms;

namespace INetApp
{
    public partial class AppShell : Shell
    {
        private ISettingsService settingsService;

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
            Routing.RegisterRoute("//MainView/Category", typeof(CategoryView));
            Routing.RegisterRoute("Message", typeof(MessageView));
            Routing.RegisterRoute("//MainView/MessageFavorite", typeof(MessageFavoriteView));
            Routing.RegisterRoute("MessageDetails", typeof(MessageDetailsView));
            Routing.RegisterRoute("MessageDetails/WebView", typeof(WebInecoView));
            Routing.RegisterRoute("//MainView/Options", typeof(OptionsView));

        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Login?Logout=true");
        }

    }
}

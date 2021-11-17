using System;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using INetApp.Views;
using Xamarin.Forms;

namespace INetApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeRouting();
            InitializeComponent();

            ISettingsService settingsService = ViewModelLocator.Resolve<ISettingsService>();

            if (string.IsNullOrEmpty(settingsService.AuthAccessToken))
            {
                GoToAsync("//Login");
            }

            NameInitial.Text = settingsService.NameInitial;
            NameUser.Text = settingsService.NameUser;
        }

        private void InitializeRouting()
        {
            Routing.RegisterRoute("Basket", typeof(BasketView));
            Routing.RegisterRoute("MainView", typeof(MainView));
            Routing.RegisterRoute("OrderDetail", typeof(OrderDetailView));
            Routing.RegisterRoute("CampaignDetails", typeof(CampaignDetailsView));
            Routing.RegisterRoute("Checkout", typeof(CheckoutView));
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Login?Logout=true");
        }

    }
}

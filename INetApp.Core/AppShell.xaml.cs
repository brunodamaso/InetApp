using System;
using System.Collections.Generic;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using INetApp.Views;
using Xamarin.Forms;

namespace INetApp
{
    public partial class AppShell : Shell
    {
        public AppShell ()
        {
            InitializeRouting();
            InitializeComponent();

            var settingsService = ViewModelLocator.Resolve<ISettingsService>();

            if (string.IsNullOrEmpty (settingsService.AuthAccessToken))
            {
                this.GoToAsync ("//Login");
            }
        }

        private void InitializeRouting()
        {
            Routing.RegisterRoute ("Basket", typeof (BasketView));
            Routing.RegisterRoute ("OrderDetail", typeof (OrderDetailView));
            Routing.RegisterRoute ("CampaignDetails", typeof(CampaignDetailsView));
            Routing.RegisterRoute ("Checkout", typeof (CheckoutView));
        }


    }
}

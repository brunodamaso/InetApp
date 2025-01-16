using System;
using System.Xml;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace INetApp.Views
{
    public abstract class ContentPageBase : ContentPage
    {
        public ContentPageBase()
        {
            if (!this.ToString().Contains("LectorQR"))
            {
				NavigationPage.SetBackButtonTitle(this, string.Empty);
                ToolbarItem QR = new ToolbarItem
                {
                    IconImageSource = Device.RuntimePlatform != Device.UWP ? "qr.png" : "ineco.png",
                    Order = ToolbarItemOrder.Default,
                    Priority = 0,
                };
                QR.Clicked += OnQRClicked;
				ToolbarItems.Add(QR);
			}
		}

        private async void OnQRClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("LectorQR");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (this.BindingContext is ViewModelBase vmb)
            {
                if (!vmb.IsInitialized || vmb.MultipleInitialization)
                {
                    await vmb.InitializeAsync(null);
                }
            }
        }
    }
}

using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.Views
{
    public abstract class ContentPageBase : ContentPage
    {
        public ContentPageBase()
        {
            NavigationPage.SetBackButtonTitle(this, string.Empty);
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

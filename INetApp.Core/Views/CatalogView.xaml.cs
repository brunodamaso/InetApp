using System;
using Xamarin.CommunityToolkit.Extensions;

namespace INetApp.Views
{
    public partial class CatalogView : ContentPageBase
    {
        private readonly FiltersView _filterView = new FiltersView();

        public CatalogView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _filterView.BindingContext = this.BindingContext;
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            this.Navigation.ShowPopup(_filterView);
        }
    }
}
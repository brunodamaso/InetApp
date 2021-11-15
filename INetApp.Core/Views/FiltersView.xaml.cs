using System.Threading.Tasks;
using INetApp.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace INetApp.Views
{
    public partial class FiltersView : Popup
    {
        public FiltersView()
        {
            InitializeComponent();
        }

        protected override object GetLightDismissResult ()
        {
            return false;
        }

        void OnFilterClicked (System.Object sender, System.EventArgs e)
        {
            if(BindingContext is CatalogViewModel viewModel)
            {
                viewModel.FilterCommand.Execute (null);
                this.Dismiss (true);
            }
        }
    }
}

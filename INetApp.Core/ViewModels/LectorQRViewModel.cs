using System.Collections.ObjectModel;
using System.Windows.Input;
using INetApp.Models;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class LectorQRViewModel : ViewModelBase
    {
        private readonly ILectorQRService lectorQRService;

        #region Properties

        #endregion

        //public ICommand SelectOptionsCommand => new Command<OptionsModel>(OnSelectMessage);
        public ICommand ScanCommand => new Command<ZXing.Result>(OnScanExecute);

        public LectorQRViewModel()
        {
            lectorQRService = DependencyService.Get<ILectorQRService>();
        }

        private async void OnScanExecute(ZXing.Result result)
        {
            IsBusy = true;

            //await NavigationService.NavigateToAsync("//MainView?scanresult="+ result.Text);

            IsBusy = false;
        }
    }
}
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
        private ObservableCollection<OptionsModel> _OptionsItems;
        private readonly IOptionsService optionsService;

        #region Properties

        public ObservableCollection<OptionsModel> OptionsItems
        {
            get => _OptionsItems;
            set
            {
                _OptionsItems = value;
                RaisePropertyChanged(() => OptionsItems);
            }
        }

        #endregion

        //public ICommand SelectOptionsCommand => new Command<OptionsModel>(OnSelectMessage);
        public ICommand ScanCommand => new Command<ZXing.Result>(OnScanExecute);

        public LectorQRViewModel()
        {
            optionsService = DependencyService.Get<IOptionsService>();
        }

        private async void OnScanExecute(ZXing.Result result)
        {
            IsBusy = true;

            //await NavigationService.NavigateToAsync("//MainView?scanresult="+ result.Text);

            IsBusy = false;
        }
    }
}
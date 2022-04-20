using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class OptionsViewModel : ViewModelBase
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
                OnPropertyChanged(nameof(OptionsItems));
            }
        }

        #endregion

        public ICommand AproveCommand => new Command(OnAproveOptions);

        public OptionsViewModel()
        {
            optionsService = DependencyService.Get<IOptionsService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            await Sincroniza();
            //await base.InitializeAsync(query);
        }

        private async Task Sincroniza()
        {
            IsBusy = true;

            OptionsDto optionsDto = await optionsService.GetOptionsAsync();

            OptionsItems = optionsDto.IsOk ? new ObservableCollection<OptionsModel>(optionsDto.OptionsModel) : new ObservableCollection<OptionsModel>();

            IsBusy = false;
        }

        private async void OnAproveOptions()
        {
            IsBusy = true;

            if (await optionsService.MarkOptionsAsync(OptionsItems.Where(a => a.checkeado).ToList()))
            {
                await DialogService.ShowAlertAsync(Literales.toast_approve_options, "", Literales.btn_text_accept);
            }
            await NavigationService.NavigateToAsync("//MainView");

            IsBusy = false;
        }
    }
}
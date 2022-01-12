using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CategoryViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categoryItems;
        private readonly ICategoryService CategoryService;
        private bool _IsRefreshing;

        #region Properties
        public ObservableCollection<CategoryModel> CategoryItems
        {
            get => _categoryItems;
            set
            {
                _categoryItems = value;
                RaisePropertyChanged(() => CategoryItems);
            }
        }
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set
            {
                _IsRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }
        #endregion

        public ICommand RefreshCommand => new Command(async () => await OnRefreshCommand());
        public ICommand SelectCategoryCommand => new Command<CategoryModel>(OnSelectCategory);
        public ICommand InfoCommand => new Command(OnInfoCommand);

        public CategoryViewModel()
        {
            CategoryService = DependencyService.Get<ICategoryService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            await Sincroniza();
        }

        private async Task Sincroniza()
        {
            IsBusy = true;
            CategorysDto categoryDto = await CategoryService.GetCategoryAsync();
            if (categoryDto.IsOk)
            {
                CategoryItems = new ObservableCollection<CategoryModel>(categoryDto.CategorysModel);
            }
            else
            {
                CategoryItems = new ObservableCollection<CategoryModel>();
            }

            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            IsBusy = false;
        }

        private async Task OnRefreshCommand()
        {
            if (IsRefreshing)
            {
                return;
            }
            IsRefreshing = true;
            IsBusy = true;
            await Sincroniza();
            IsRefreshing = false;
            IsBusy = false;
        }

        private async void OnSelectCategory(CategoryModel categoryModel)
        {
            if (categoryModel.pendingMessages > 0)
            {
                IsBusy = true;
                Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "Name", categoryModel.name },
                    { "CategoryId", categoryModel.categoryId.ToString() }
                };
                await NavigationService.NavigateToAsync("Message", Parametro);
                IsBusy = false;
            }
        }
        private async void OnInfoCommand()
        {
            IsBusy = true;
            Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "Formulario", "//MainView/Category"}
                };

            await NavigationService.NavigateToAsync("InfoView", Parametro);

            IsBusy = false;
        }
    }
}
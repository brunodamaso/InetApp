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
        private string _mensajeListView;
        private readonly ICategoryService CategoryService;
        private bool _IsRefreshing;

        #region Properties
        public ObservableCollection<CategoryModel> CategoryItems
        {
            get => _categoryItems;
            set
            {
                _categoryItems = value;
                RaisePropertyChanged(() => this.CategoryItems);
            }
        }

        public string MensajeListView
        {
            get => _mensajeListView;
            set
            {
                _mensajeListView = value;
                RaisePropertyChanged(() => this.MensajeListView);
            }
        }
        public bool IsRefreshing
        {
            get => _IsRefreshing;
            set
            {
                _IsRefreshing = value;
                RaisePropertyChanged(() => this.IsRefreshing);
            }
        }
        #endregion

        public ICommand RefreshCommand => new Command(async () => await OnRefreshCommand());
        public ICommand SelectCategoryCommand => new Command<CategoryModel>(OnSelectCategory);

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
            this.MensajeListView = "Cargando Datos";
            this.IsBusy = true;
            CategoryDto categoryDto = await CategoryService.GetCategoryAsync();
            if (categoryDto.IsOk)
            {
                this.CategoryItems = new ObservableCollection<CategoryModel>(categoryDto.CategoryModels);
            }
            else
            {
                this.CategoryItems = new ObservableCollection<CategoryModel>();
            }

            this.MensajeListView = Literales.empty_categories;
            this.Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            this.IsBusy = false;
        }

        private async Task OnRefreshCommand()
        {
            if (this.IsRefreshing)
            {
                return;
            }
            this.IsRefreshing = true;
            this.IsBusy = true;
            await Sincroniza();
            this.IsRefreshing = false;
            this.IsBusy = false;
        }

        private async void OnSelectCategory(CategoryModel categoryModel)
        {
            if (categoryModel.pendingMessages > 0)
            {
                this.IsBusy = true;
                var Parametro = new Dictionary<string, string>
                {
                    { "Name", categoryModel.name },
                    { "CategoryId", categoryModel.categoryId.ToString() }
                };
                await NavigationService.NavigateToAsync("Message", Parametro);
                this.IsBusy = false;
            }
        }

        //private async Task CheckoutAsync()
        //{
        //    if (this.BasketItems?.Any() ?? false)
        //    {
        //        await NavigationService.NavigateToAsync("Checkout");
        //    }
        //}
    }
}
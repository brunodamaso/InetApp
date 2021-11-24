using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Models.Basket;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Forms;
using System.Linq;
using System;
using INetApp.Views.Components;
using INetApp.Extensions;
using INetApp.Models.Marketing;

namespace INetApp.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categoryItems;
        private string _mensajeListView;        
        private readonly ICategoryService CategoryService;
        private int selectecTab;
        private bool _SelectAll;
        private string _Title;


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
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                RaisePropertyChanged(() => this.Title);
            }
        }
        public bool SelectAll
        {
            get => _SelectAll;
set
{
                _SelectAll = value;
                RaisePropertyChanged(() => this.SelectAll);
            }
        }
        public int SelectecTab
        {
            get => selectecTab;
            set
            {
                selectecTab = value;
                RaisePropertyChanged(() => this.SelectecTab);
            }
        }

        #endregion

        public ICommand RefreshCommand => new Command(async () => await OnRefreshCommand());
        public ICommand SelectCategoryCommand => new Command<CategoryModel>(OnSelectCategory);

        public MessageViewModel()
        {
            CategoryService = DependencyService.Get<ICategoryService>();
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            if (query.TryGetValue("Name", out string title))
            {
                Title = title;
            }
//Todo aceptar parametro categoryId para traer solo esos mensajes
            await Sincroniza();
        }

        private async Task Sincroniza()
        {
            this.MensajeListView = "Cargando Datos";
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
            this.Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now);

        }

        private async Task OnRefreshCommand()
        {
            
            this.IsBusy = true;
            await Sincroniza();
                        this.IsBusy = false;
        }

        private async void OnSelectCategory(CategoryModel categoryModel)
        {
            
            this.IsBusy = true;
            await Sincroniza();
            this.IsBusy = false;
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
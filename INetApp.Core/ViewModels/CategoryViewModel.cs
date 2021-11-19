using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Models.Basket;
using INetApp.Resources;
using INetApp.Services.Category;
using INetApp.Services.User;
using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private ObservableCollection<CategoryModel> _categoryItems;
        
        private readonly ICategoryService CategoryService;

        public CategoryViewModel()
        {
            CategoryService = DependencyService.Get<ICategoryService>();
        }

        public ObservableCollection<CategoryModel> CategoryItems
        {
            get => _categoryItems;
            set
            {
                _categoryItems = value;
                RaisePropertyChanged(() => this.CategoryItems);
            }
        }

        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            CategoryDto categoryDto = await CategoryService.GetCategoryAsync();
            if (categoryDto.IsOk)
            {
                this.CategoryItems = new ObservableCollection<CategoryModel>(categoryDto.CategoryModels);
            }
            else
            {
                this.CategoryItems = new ObservableCollection<CategoryModel>();
            }

            this.Text_last_update = string.Format(Literales.view_text_last_updated, "-");

            RaisePropertyChanged(() => this.CategoryItems);
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
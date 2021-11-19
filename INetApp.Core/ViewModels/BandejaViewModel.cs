using INetApp.Models.Basket;
using INetApp.Models.Catalog;
using INetApp.Resources;
using INetApp.Services.Basket;
using INetApp.Services.Settings;
using INetApp.Services.User;
using INetApp.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class BandejaViewModel : ViewModelBase
    {
        private int _badgeCount;
        private ObservableCollection<BasketItem> _basketItems;
        private decimal _total;

        private IBandejaService BandejaService;
        private IUserService _userService;

        public BandejaViewModel()
        {
            BandejaService = DependencyService.Get<IBandejaService> ();
            _userService = DependencyService.Get<IUserService> ();
        }

        public int BadgeCount
        {
            get => _badgeCount;
            set
            {
                _badgeCount = value;
                RaisePropertyChanged(() => BadgeCount);
            }
        }

        public ObservableCollection<BasketItem> BasketItems
        {
            get => _basketItems;
            set
            {
                _basketItems = value;
                RaisePropertyChanged(() => BasketItems);
            }
        }

        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                RaisePropertyChanged(() => Total);
            }
        }

        public ICommand AddCommand => new Command<BasketItem>(async (item) => await AddItemAsync(item));

        public ICommand DeleteCommand => new Command<BasketItem> (async (item) => await DeleteBasketItemAsync (item));

        public ICommand CheckoutCommand => new Command(async () => await CheckoutAsync());

        public override async Task InitializeAsync (IDictionary<string, string> query)
        {
            if (BasketItems == null)
                BasketItems = new ObservableCollection<BasketItem> ();


            this.Text_last_update = string.Format(Literales.view_text_last_updated, "-");
            
            RaisePropertyChanged (() => BasketItems);
        }

        private async Task AddCatalogItemAsync(CatalogItem item)
        {
            BasketItems.Add(new BasketItem
            {
                ProductId = item.Id,
                ProductName = item.Name,
                PictureUrl = item.PictureUri,
                UnitPrice = item.Price,
                Quantity = 1
            });

            await ReCalculateTotalAsync();
        }

        private async Task AddItemAsync(BasketItem item)
        {
            BadgeCount++;
            await AddBasketItemAsync(item);
            RaisePropertyChanged(() => BasketItems);
        }

        private async Task AddBasketItemAsync(BasketItem item)
        {
            BasketItems.Add(item);
            await ReCalculateTotalAsync();
        }

        private async Task DeleteBasketItemAsync (BasketItem item)
        {
            BasketItems.Remove (item);

           

            await ReCalculateTotalAsync ();
        }

        private async Task ReCalculateTotalAsync()
        {
            Total = 0;

            if (BasketItems == null)
            {
                return;
            }

            foreach (var orderItem in BasketItems)
            {
                Total += (orderItem.Quantity * orderItem.UnitPrice);
            }
        }

        private async Task CheckoutAsync()
        {
            if (BasketItems?.Any() ?? false)
            {
                await NavigationService.NavigateToAsync ("Checkout");
            }
        }
    }
}
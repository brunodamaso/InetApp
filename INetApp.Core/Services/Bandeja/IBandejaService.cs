using INetApp.Models.Basket;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services.Bandeja
{
    public interface IBandejaService
    {
        IEnumerable<BasketItem> LocalBasketItems { get; set; }
        Task<CustomerBasket> GetBandejaAsync(string guidUser, string token);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket, string token);
        Task CheckoutAsync(BasketCheckout basketCheckout, string token);
        Task ClearBasketAsync(string guidUser, string token);
    }
}
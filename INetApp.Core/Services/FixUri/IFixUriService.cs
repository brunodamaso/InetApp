using System.Collections.Generic;
using INetApp.Models.Basket;
using INetApp.Models.Catalog;
using INetApp.Models.Marketing;

namespace INetApp.Services.FixUri
{
    public interface IFixUriService
    {
        void FixCatalogItemPictureUri(IEnumerable<CatalogItem> catalogItems);
        void FixBasketItemPictureUri(IEnumerable<BasketItem> basketItems);
        void FixCampaignItemPictureUri(IEnumerable<CampaignItem> campaignItems);
    }
}

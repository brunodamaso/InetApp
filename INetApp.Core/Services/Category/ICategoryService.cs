using INetApp.APIWebServices.Dtos;
using INetApp.Models.Basket;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services.Category
{
    public interface ICategoryService
    {
        IEnumerable<BasketItem> LocalBasketItems { get; set; }
        Task<CategoryDto> GetCategoryAsync();
       
    }
}
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;

namespace INetApp.Services
{
    public interface ICategoryService
    {
        Task<CategorysDto> GetCategoryAsync();

    }
}
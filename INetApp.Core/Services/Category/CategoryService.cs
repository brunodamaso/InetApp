using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.ViewModels.Base;

namespace INetApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryWebService repositoryWebService;

        public CategoryService(IRepositoryWebService _repositoryWebService)
        {
            repositoryWebService = _repositoryWebService;
        }

        public async Task<CategorysDto> GetCategoryAsync()
        {
            CategorysDto categoryDto = await repositoryWebService.GetCategory();
            if (categoryDto.IsOk)
            {

            }
            return categoryDto;
        }
    }
}
using System;
using System.Threading.Tasks;
using INetApp.Services.RequestProvider;
using INetApp.Models.Basket;
using INetApp.Services.FixUri;
using INetApp.Helpers;
using System.Collections.Generic;
using INetApp.APIWebServices.Dtos;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using INetApp.Services.Identity;
using INetApp.Services;

namespace INetApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryWebService repositoryWebService;
        private protected readonly IUserService userService;

        public CategoryService(IRepositoryWebService _repositoryWebService)
        {
            repositoryWebService = _repositoryWebService;
            userService = ViewModelLocator.Resolve<IUserService>();
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
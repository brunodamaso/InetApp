using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public class OptionsService : IOptionsService
    {
        private readonly IRepositoryWebService repositoryWebService;
        private protected readonly IUserService userService;

        public OptionsService(IRepositoryWebService _repositoryWebService)
        {
            repositoryWebService = _repositoryWebService;
            userService = ViewModelLocator.Resolve<IUserService>();
        }

        public async Task<OptionsDto> GetOptionsAsync()
        {
            return await repositoryWebService.GetOptions();            
        }

        public async Task<bool> MarkOptionsAsync(List<OptionsModel> optionsModels)
        {
            return await repositoryWebService.MarkOptions(optionsModels);
        }
    }
}
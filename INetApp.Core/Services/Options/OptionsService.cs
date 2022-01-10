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
        private List<OptionsModel> optionsModelApi;

        public OptionsService(IRepositoryWebService _repositoryWebService)
        {
            repositoryWebService = _repositoryWebService;
            userService = ViewModelLocator.Resolve<IUserService>();
        }

        public async Task<OptionsDto> GetOptionsAsync()
        {
            OptionsDto retorno = await repositoryWebService.GetOptions();
            return retorno;
        }
        
    }
}
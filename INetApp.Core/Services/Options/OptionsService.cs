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
        private readonly IRepositoryService repositoryService;
        private protected readonly IUserService userService;
        private List<MessageModel> messageModelApi;

        public OptionsService(IRepositoryWebService _repositoryWebService, IRepositoryService _repositoryService)
        {
            repositoryWebService = _repositoryWebService;
            repositoryService = _repositoryService;
            userService = ViewModelLocator.Resolve<IUserService>();
        }

        public async Task<bool> GetOptionsAsync()
        {
            bool retorno = await repositoryWebService.GetOptions();
            return retorno;
        }
        
    }
}
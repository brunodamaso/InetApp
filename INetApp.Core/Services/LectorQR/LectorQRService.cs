using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public class LectorQRService : ILectorQRService
    {
        private readonly IRepositoryWebService repositoryWebService;

        public LectorQRService(IRepositoryWebService _repositoryWebService)
        {
            repositoryWebService = _repositoryWebService;            
        }

        public async Task<bool> GetAccesoAsync(string QR)
        {
            bool retorno =await repositoryWebService.GetAccesoQR(QR);
            if (retorno)
            {
            }
            return retorno;
        }
    }
}
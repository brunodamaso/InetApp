using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Services.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IUserService
    {
        Task<UserLoggedDto> GetUserLoggedDto(string userName, string userPass);
        Task<UserLoggedDto> GetUserLoggedDto();

        /// <summary>
        /// Chequea version de la app vs la devuelta en el API Service
        /// </summary>
        Task<bool> CheckVersion();
    }
}

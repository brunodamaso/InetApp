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
        /// 
        /// </summary>
        /// <returns>Devuelve false= si es la misma version o postpone la actualizacion, true= se va a actualizar la version</returns>
        Task<bool> CheckVersion();
    }
}

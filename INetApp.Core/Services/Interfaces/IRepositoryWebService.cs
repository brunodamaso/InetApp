using System;
using System.Collections.Generic;
using INetApp.Models;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Responses;


namespace INetApp.Services
{
    public interface IRepositoryWebService
    {
        Task<UserLoggedDto> GetUserLogged(string usuario, string password);
        //Task<TDto> GetDatos<TDto, TResponse>(string Tabla) where TResponse : Response where TDto : BaseDto, new();

    }
}

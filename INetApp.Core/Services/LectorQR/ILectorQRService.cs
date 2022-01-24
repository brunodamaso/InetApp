using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface ILectorQRService
    {
        Task<bool> GetAccesoAsync(string QR);        
    }
}
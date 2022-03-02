using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IWorkPartsService
    {
        Task<WorkPartsDto> GetWorkPartsAsync(string FechaIni = null, string FechaFin = null, int? IdSemana = null);
        Task<InecoProjectsDto> GetInecoProjectsAsync(bool ineco, string pronumero, string titulo);
    }
}
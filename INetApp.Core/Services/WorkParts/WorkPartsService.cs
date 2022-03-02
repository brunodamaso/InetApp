using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public class WorkPartsService : IWorkPartsService
    {
        private readonly IRepositoryWebService repositoryWebService;

        public WorkPartsService(IRepositoryWebService _repositoryWebService)
        {
            repositoryWebService = _repositoryWebService;
        }

        public async Task<WorkPartsDto> GetWorkPartsAsync(string FechaIni = null, string FechaFin = null, int? IdSemana = null)
        {
            WorkPartsDto workPartsDto = await repositoryWebService.GetWorkParts(FechaIni, FechaFin, IdSemana);
            if (workPartsDto.IsOk)
            {

            }
            return workPartsDto;
        }
        public async Task<InecoProjectsDto> GetInecoProjectsAsync(bool ineco, string pronumero, string titulo)
        {            
            string pronumero2 = pronumero == null || pronumero.Equals("") ? "null" : pronumero;
            string titulo2 = titulo == null || titulo.Equals("") ? "null" : titulo;
            bool ineco2 = titulo == null && pronumero == null;
            InecoProjectsDto workPartsDto = await repositoryWebService.GetInecoProjects(ineco2, pronumero2, titulo2);
            if (workPartsDto.IsOk)
            {

            }
            return workPartsDto;
        }

    }
}
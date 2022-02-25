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
    }
}
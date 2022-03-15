using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<PeriodoActivoDto> GetPeriodoActivoAsync()
        {
            PeriodoActivoDto periodoActivoDto = await repositoryWebService.GetPeriodoActivo();
            if (periodoActivoDto.IsOk)
            {

            }
            return periodoActivoDto;


        }
        public List<ItemTableProjectModel> GetItemTableProjects(List<LineasDetalle> lineasDetalle, bool Editable, int periodoActivo)
        {
            List<ItemTableProjectModel> TableProjects = new List<ItemTableProjectModel>();
            string pronum = "";
            string fechaFirma = "";
            foreach (LineasDetalle item in lineasDetalle)
            {
                DayOfWeek dia = DateTime.ParseExact(item.fechaImputacion, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).DayOfWeek;
                if (pronum != item.pronumero || fechaFirma != item.fechaFirma)
                {
                    ItemTableProjectModel TableProject = new ItemTableProjectModel();
                    pronum = item.pronumero;
                    fechaFirma = item.fechaFirma;
                    TableProject.pronumero = pronum;
                    TableProject.fechaFirma = fechaFirma;
                    TableProject.protitulo = item.protitulo;
                    TableProject.pdeStatus = item.pdeStatus;
                    TableProject.prpCodigo = item.prpCodigo;
                    TableProject.pdelineaIDRechazo = item.pdelineaIDRechazo;
                    TableProject.perParteId = item.perParteId;
                    TableProject.editable = CheckEditable(item, Editable, periodoActivo);

                    TableProjects.Add(TableProject);
                }
                //else
                //{
                //    if (fechaFirma != item.fechaFirma)
                //    {
                //        ItemTableProjectModel TableProject = new ItemTableProjectModel();
                //        pronum = item.pronumero;
                //        fechaFirma = item.fechaFirma;
                //        TableProject.pronumero = pronum;
                //        TableProject.fechaFirma = fechaFirma;
                //        TableProject.protitulo = item.protitulo;
                //        TableProject.pdeStatus = item.pdeStatus;
                //        TableProject.prpCodigo = item.prpCodigo;
                //        TableProject.pdelineaIDRechazo = item.pdelineaIDRechazo;
                //        TableProject.perParteId = item.perParteId;
                //        TableProject.editable = CheckEditable(item, Editable, periodoActivo);

                //        TableProjects.Add(TableProject);
                //    }
                //}
                switch (dia)
                {
                    case DayOfWeek.Monday:
                        TableProjects[TableProjects.Count - 1].procuentaLunes = item.procuenta;
                        TableProjects[TableProjects.Count - 1].pdLineaIdLunes = item.pdLineaId;
                        break;
                    case DayOfWeek.Tuesday:
                        TableProjects[TableProjects.Count - 1].procuentaMartes = item.procuenta;
                        TableProjects[TableProjects.Count - 1].pdLineaIdMartes = item.pdLineaId;
                        break;
                    case DayOfWeek.Wednesday:
                        TableProjects[TableProjects.Count - 1].procuentaMiercoles = item.procuenta;
                        TableProjects[TableProjects.Count - 1].pdLineaIdMiercoles = item.pdLineaId;
                        break;
                    case DayOfWeek.Thursday:
                        TableProjects[TableProjects.Count - 1].procuentaJueves = item.procuenta;
                        TableProjects[TableProjects.Count - 1].pdLineaIdJueves = item.pdLineaId;
                        break;
                    case DayOfWeek.Friday:
                        TableProjects[TableProjects.Count - 1].procuentaViernes = item.procuenta;
                        TableProjects[TableProjects.Count - 1].pdLineaIdViernes = item.pdLineaId;
                        break;
                    default:
                        break;
                }
            }
            TableProjects = TableProjects.OrderBy(a => a.fechaFirma).ThenBy(a => a.pronumero).ToList();
            return TableProjects;
        }
        private bool CheckEditable(LineasDetalle lineasDetalle, bool Editable, int periodoActivo)
        {
            bool editLine = false;
            if (Editable)
            {
                editLine = lineasDetalle.pdeStatus == 0 || (lineasDetalle.pdeStatus == 2 && lineasDetalle.prpCodigo == periodoActivo) || (lineasDetalle.pdeStatus == 3 && lineasDetalle.prpCodigo == periodoActivo && lineasDetalle.pdelineaIDRechazo == 0);
            }
            return editLine;
        }
    }
}
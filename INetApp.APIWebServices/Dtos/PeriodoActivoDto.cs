using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;

namespace INetApp.APIWebServices.Dtos
{
    public class PeriodoActivoDto : BaseDto
    {
        public PeriodoActivoDto() : base() { }
        public PeriodoActivoDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public PeriodoActivoModel PeriodoActivoModel { get; set; }
    }
}

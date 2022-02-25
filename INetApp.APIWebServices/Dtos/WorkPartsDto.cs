using INetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Dtos
{
    public class WorkPartsDto : BaseDto
    {
        public WorkPartsDto() : base() { }
        public WorkPartsDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public WorkPartsModel WorkPartsModel { get; set; }
    }
}

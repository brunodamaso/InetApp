using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;

namespace INetApp.APIWebServices.Dtos
{
    public class InecoProjectsDto : BaseDto
    {
        public InecoProjectsDto() : base() { }
        public InecoProjectsDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public List<InecoProjectsModel> InecoProjectsModel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;

namespace INetApp.APIWebServices.Dtos
{    
    public class UserAccessDto : BaseDto
    {
        public UserAccessDto() : base() { }
        public UserAccessDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public string Resultado { get; set; }
    }
}

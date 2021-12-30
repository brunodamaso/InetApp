using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;
using Xamarin.Forms;

namespace INetApp.APIWebServices.Dtos
{    
    public class OptionsDto : BaseDto
    {
        public OptionsDto() : base() { }
        public OptionsDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public List<OptionsModel> OptionsModel { get; set; }
    }
}

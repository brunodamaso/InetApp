using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;
using Xamarin.Forms;

namespace INetApp.APIWebServices.Dtos
{
    public class MessageDto : BaseDto
    {
        public MessageDto() : base() { }
        public MessageDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public MessageModel MessageModel { get; set; } = new MessageModel();
    }
}

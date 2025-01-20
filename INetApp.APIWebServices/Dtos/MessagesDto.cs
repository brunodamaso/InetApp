using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;
using Xamarin.Forms;

namespace INetApp.APIWebServices.Dtos
{    
    public class MessagesDto : BaseDto
    {
        public MessagesDto() : base() { }
        public MessagesDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public List<MessageModel> MessagesModel { get; set; } = new List<MessageModel>();
    }
}

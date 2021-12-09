using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;
using Xamarin.Forms;

namespace INetApp.APIWebServices.Dtos
{    
    public class CategorysDto : BaseDto
    {
        public CategorysDto() : base() { }
        public CategorysDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public List<CategoryModel> CategorysModel { get; set; }
    }
}

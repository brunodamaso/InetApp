using System;
using System.Collections.Generic;
using System.Text;
using INetApp.Models;
using Xamarin.Forms;

namespace INetApp.APIWebServices.Dtos
{    
    public class CategoryDto : BaseDto
    {
        public CategoryDto() : base() { }
        public CategoryDto(bool isOk, string errorCode, string errorDescription, bool isConnected) : base(isOk, errorCode, errorDescription, isConnected) { }
        public List<CategoryModel> CategoryModels { get; set; }
    }
}

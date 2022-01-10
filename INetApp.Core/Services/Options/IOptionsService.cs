using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IOptionsService
    {
        Task<OptionsDto> GetOptionsAsync();
        Task<bool> MarkOptionsAsync(List<OptionsModel> optionsModels);

    }
}
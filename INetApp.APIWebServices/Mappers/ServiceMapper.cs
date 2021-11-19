using System.Collections.Generic;
using System.Collections.ObjectModel;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
using INetApp.APIWebServices.Responses;
using INetApp.Models;
using Mapster;
//using INetApp.Models;

namespace INetApp.APIWebServices.Mappers
{
    public static class ServiceMapper
    {
        public static TDto ConvertToBusiness<TDto,TEnti>(ServiceResponse<TEnti> serviceResponse) where TEnti : Response where TDto : BaseDto, new()
        {
            TDto dto = new TDto()
            {
                IsOk = serviceResponse.IsOk,
                ErrorCode = serviceResponse.ErrorCode,
                ErrorDescription = serviceResponse.Description,
                IsConnected = serviceResponse.IsConnected
            };

            if (serviceResponse.IsOk && serviceResponse.Resultado != null)
            {
                dto = serviceResponse.Resultado.Adapt<TDto>();
            }

            return dto;
        }

        public static CategoryDto ConvertToBusiness(ServiceResponse<CategoryEntitys> serviceResponse)
        {
            CategoryDto dto = new CategoryDto()
            {
                IsOk = serviceResponse.IsOk,
                ErrorCode = serviceResponse.ErrorCode,
                ErrorDescription = serviceResponse.Description,
                IsConnected = serviceResponse.IsConnected
            };

            if (serviceResponse.IsOk && serviceResponse.Resultado != null)
            {
                foreach (var item in serviceResponse.Resultado.CategoryEntity)
                {
                    CategoryModel aa = item.Adapt<CategoryModel>();
                    dto.CategoryModels.Add(aa);
                }
                //dto = serviceResponse.Resultado.Adapt<CategoryDto>();
            }

            return dto;
        }



        //public static CategoriasDto ConvertToBusiness(ServiceResponse<ChkLicenciaResponse> serviceResponse)
        //{
        //    CategoriasDto dto = new CategoriasDto(serviceResponse.IsOk, serviceResponse.ErrorCode, serviceResponse.Description, serviceResponse.IsConnected);

        //    if (serviceResponse.IsOk && serviceResponse.Content != null)
        //    {
        //        //dto.Result = serviceResponse.Content.Result;
        //        //dto.nombre = serviceResponse.Content.nombre;
        //        //dto.usuario = serviceResponse.Content.usuario;
        //        //dto.password = serviceResponse.Content.password;
        //        //dto.serverFTP = serviceResponse.Content.serverFTP;
        //        //dto.userFTP = serviceResponse.Content.userFTP;
        //        //dto.passwordFTP = serviceResponse.Content.passwordFTP;
        //        //dto.puertoFTP = serviceResponse.Content.puertoFTP;
        //        //dto.carpetaFTP = serviceResponse.Content.carpetaFTP;
        //        //dto.Email = serviceResponse.Content.Email;
        //    }

        //    return dto;
        //}
        //public static CategoriasDto ConvertToBusiness(ServiceResponse<RstPasswordResponse> serviceResponse)
        //{
        //    CategoriasDto dto = new CategoriasDto(serviceResponse.IsOk, serviceResponse.ErrorCode, serviceResponse.Description, serviceResponse.IsConnected);

        //    if (serviceResponse.IsOk && serviceResponse.Content != null)
        //    {
        //       // dto.Result = serviceResponse.Content.Result;
        //    }

        //    return dto;
        //}  
    }
}
    



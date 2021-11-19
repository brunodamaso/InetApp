using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
using INetApp.Models;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace INetApp.APIWebServices.Mappers
{
    public static class ServiceMapster
    {
        public static void CreateMapper()
        {
            TypeAdapterConfig<UserLoggedEntity, UserLoggedDto>
               .NewConfig()
               .EnableNonPublicMembers(true)
               .Map(dest => dest.UserLoggedModel, src => src)
               .IgnoreNullValues(true)
               .Map(dest => dest.UserLoggedModel.fullName, src => $"{src.name} {src.lastname}")
               .Map(dest => dest.UserLoggedModel.nameInitial, src => string.IsNullOrEmpty(src.name) ? "" : $"{src.name.Substring(0, 1)}")
               .Map(dest => dest.UserLoggedModel.lastNameInitial, src => string.IsNullOrEmpty(src.lastname) ? "" : $"{src.lastname.Substring(0, 1)}");


            TypeAdapterConfig<CategoryEntitys, CategoryDto>
               .NewConfig()
               .EnableNonPublicMembers(true)
               .Map(dest => dest.CategoryModels, src => src.CategoryEntity)
               .IgnoreNullValues(true);

            TypeAdapterConfig<CategoryEntity, CategoryModel>
                .NewConfig()
                .EnableNonPublicMembers(true)              
                .IgnoreNullValues(true);
        }
    }
}

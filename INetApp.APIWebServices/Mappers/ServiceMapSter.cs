using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
using INetApp.Models;
using Mapster;
using Newtonsoft.Json;
using System;
using System.Globalization;

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


            TypeAdapterConfig<CategorysEntity, CategorysDto>
               .NewConfig()
               .EnableNonPublicMembers(true)
               .Map(dest => dest.CategorysModel, src => src.CategorysEntities)
               .IgnoreNullValues(true);

            TypeAdapterConfig<CategoryEntity, CategoryModel>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true)
                 .Map(dest => dest.urIcon, src => src.UrlIcono);

            TypeAdapterConfig<MessagesEntity, MessagesDto>
               .NewConfig()
               .EnableNonPublicMembers(true)
               .Map(dest => dest.MessagesModel, src => src.MessagesEntities)
               .IgnoreNullValues(true);

            TypeAdapterConfig<MessageEntity, MessageDto>
               .NewConfig()
               .EnableNonPublicMembers(true)
               .Map(dest => dest.MessageModel, src => src)
               .IgnoreNullValues(true);


            TypeAdapterConfig<MessageEntity, MessageModel>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true)
                 .Map(dest => dest.date, src => DateTime.ParseExact(string.IsNullOrEmpty(src.date) ? DateTime.Now.ToString("yyyyMMdd") : src.date, "yyyyMMdd", CultureInfo.InvariantCulture))
                 .Map(dest => dest.fields, src => src.data == null ? null : JsonConvert.DeserializeObject<MessageDetails>(src.data));

        }
    }
}

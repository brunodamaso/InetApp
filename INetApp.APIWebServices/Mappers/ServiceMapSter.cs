using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
using INetApp.Models;
using Mapster;
using Newtonsoft.Json;
using System;
using System.Globalization;
using INetApp.APIWebServices.Helpers;

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


            TypeAdapterConfig<UserAccessEntity, UserAccessDto>
               .NewConfig()
               .EnableNonPublicMembers(true)
               .Map(dest => dest.UserAccessModel, src => src)
               .IgnoreNullValues(true);

            TypeAdapterConfig<CategorysEntity, CategorysDto>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true)
                .Map(dest => dest.CategorysModel, src => src.CategorysEntities);

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
                .Map(dest => dest.fields, src => src.data == null ? null : JsonConvert.DeserializeObject<MessageDetails>(src.data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .Ignore(dest => dest.checkeado);

            //.Map(dest => dest.fields, src => src.data == null ? null : JsonService.Deserialize<MessageDetails>(src.data, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault }));

            TypeAdapterConfig<OptionsEntity, OptionsDto>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .Map(dest => dest.OptionsModel, src => src.OptionsEntities)
                .IgnoreNullValues(true);

            TypeAdapterConfig<OptionsEntity, OptionsModel>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true);

            TypeAdapterConfig<WorkPartsEntity, WorkPartsDto>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .Map(dest => dest.WorkPartsModel, src => src)
                .Map(dest => dest.WorkPartsModel.lineasDetalle, src => src.lineasDetalleEntity)
                .Map(dest => dest.WorkPartsModel.lineasDetalleIneco, src => src.lineasDetalleInecoEntity)
                .IgnoreNullValues(true);

            TypeAdapterConfig<InecoProjectsEntity, InecoProjectsDto>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .Map(dest => dest.InecoProjectsModel, src => src.InecoProjectsEntities)
                .IgnoreNullValues(true);

            //TypeAdapterConfig<WorkPartsEntity, WorkPartsModel>
            //    .NewConfig()
            //    .EnableNonPublicMembers(true)
            //    .IgnoreNullValues(true);

            TypeAdapterConfig<LineasDetalleEntity, LineasDetalle>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true);

            TypeAdapterConfig<HoraDiaEntity, HoraDia>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true);

            TypeAdapterConfig<PeriodoActivoEntity, PeriodoActivoModel>
                .NewConfig()
                .EnableNonPublicMembers(true)
                .IgnoreNullValues(true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using INetApp.APIWebServices.Dtos;
using INetApp.APIWebServices.Entity;
using INetApp.APIWebServices.Responses;
using INetApp.Models;
using Mapster;
using Newtonsoft.Json;
using SQLite;

namespace INetApp.APIWebServices.Mappers
{
    public static class ServiceMapper
    {
        public static TDto ConvertToBusiness<TDto, TEnti>(ServiceResponse<TEnti> serviceResponse) where TEnti : Response where TDto : BaseDto, new()
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
                try
                {
                    dto = serviceResponse.Resultado.Adapt<TDto>();
                }
                catch (Exception es)
                {
                    Console.WriteLine(es.Message);
                }
            }
            return dto;
        }

        public static MessageDto ConvertToBusiness(ServiceResponse<MessageEntity> serviceResponse)
        {
            MessageDto dto = new MessageDto()
            {
                IsOk = serviceResponse.IsOk,
                ErrorCode = serviceResponse.ErrorCode,
                ErrorDescription = serviceResponse.Description,
                IsConnected = serviceResponse.IsConnected,
            };

            if (serviceResponse.IsOk && serviceResponse.Resultado != null)
            {
                try
                {
                    var item = serviceResponse.Resultado;
					if (!DateTime.TryParseExact(item.date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
					{
						parsedDate = DateTime.TryParseExact(item.date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) ? parsedDate : DateTime.Now;
					}
					dto.MessageModel = new MessageModel()
                    {
                        date = parsedDate,
                        fields = item.data == null ? null : JsonConvert.DeserializeObject<MessageDetails>(item.data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                        messageId = item.messageId,
                        name = item.name,
                        categoryId = item.categoryId,
                        favorite = item.favorite
                    };
                }
                catch (Exception es)
                {
                    Console.WriteLine(es.Message);
                }
            }
            return dto;
        }


        public static MessagesDto ConvertToBusiness(ServiceResponse<MessagesEntity> serviceResponse)
        {
            MessagesDto dto = new MessagesDto()
            {
                IsOk = serviceResponse.IsOk,
                ErrorCode = serviceResponse.ErrorCode,
                ErrorDescription = serviceResponse.Description,
                IsConnected = serviceResponse.IsConnected,
                MessagesModel = new List<MessageModel>()                
            };

            if (serviceResponse.IsOk && serviceResponse.Resultado != null)
            {
                try
                {
                    foreach (var item in serviceResponse.Resultado.MessagesEntities)
                    {
						if (!DateTime.TryParseExact(item.date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
						{
							parsedDate = DateTime.TryParseExact(item.date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) ? parsedDate : DateTime.Now;
						}
						dto.MessagesModel.Add(new MessageModel()
                        {

                            date = parsedDate,
                            fields = item.data == null ? null : JsonConvert.DeserializeObject<MessageDetails>(item.data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                            messageId = item.messageId,
                            name = item.name,
                            categoryId = item.categoryId,
                            favorite = item.favorite
                        });
                    }
                }
                catch (Exception es)
                {
                    Console.WriteLine(es.Message);
                }
            }
            return dto;
        }
    }
}



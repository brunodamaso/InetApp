using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INetApp.APIWebServices.Dtos;
using INetApp.Extensions;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.ViewModels.Base;
using Newtonsoft.Json;

namespace INetApp.Services
{
    public class PushService : IPushService
    {
        private ISettingsService settingsService;
        private IRepositoryWebService repositoryWebService;
        private readonly IDialogService DialogService;
        private INavigationService NavigationService;
        private protected readonly IIdentityService identityService;

        public PushService()
        {
        }

        public PushService(IRepositoryWebService _repositoryWebService)
        {
            settingsService = ViewModelLocator.Resolve<ISettingsService>();
            identityService = ViewModelLocator.Resolve<IIdentityService>();
            repositoryWebService = _repositoryWebService;
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public string GetToken()
        {
            return settingsService.PushToken;
        }
        public bool RegistrarToken(string token)
        {
            return true;
        }
        public virtual async Task OnPushAction(IDictionary<string, string> token)
        {
            try
            {
                if (token != null)
                {
                    string sTipo = "";
                    if (token.TryGetValue(Literales.keynoTipoMsg, out string notiExterna))
                    {
                        sTipo = notiExterna;
                    }
                    switch (sTipo)
                    {
                        case "1":
                            await OnPushActionUrl(token);
                            break;
                        default:
                            await OnPushActionTray(token);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task OnPushActionUrl(IDictionary<string, string> token)
        {
            try
            {
                NavigationService = ViewModelLocator.Resolve<INavigationService>();
                string sUrl = "";
                string sMessage = "";
                string sTitulo = "";
                if (token.TryGetValue(Literales.keynoUrlMsg, out string Url))
                {
                    sUrl = Url;
                }
                if (token.TryGetValue(Literales.keynotmessage, out string message))
                {
                    sMessage = message;
                }
                if (token.TryGetValue(Literales.keynottitle, out string title))
                {
                    sTitulo = title;
                }
                await NavigationService.NavigateToAsync("WebView?Ruta=" + sUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task OnPushActionTray(IDictionary<string, string> token)
        {
            try
            {
                settingsService = ViewModelLocator.Resolve<ISettingsService>();
                repositoryWebService = ViewModelLocator.Resolve<IRepositoryWebService>();
                NavigationService = ViewModelLocator.Resolve<INavigationService>();
                if (!settingsService.AuthAccessToken.IsNullOrEmpty())
                {

                    int idApplication = 0;
                    int idSolicitud = 0;
                    string sNombreCategoria = "Solicitud";
                    if (token.TryGetValue(Literales.keynotidapplication, out string idapplication))
                    {
                        idApplication = int.Parse(idapplication);
                    }
                    if (token.TryGetValue(Literales.keynotidsolicitud, out string idsolicitud))
                    {
                        idSolicitud = int.Parse(idsolicitud);
                    }
                    if (token.TryGetValue(Literales.keycategoryname, out string categoryname))
                    {
                        sNombreCategoria = categoryname;
                    }

                    if (idApplication > 0 && idSolicitud > 0)
                    {
                        MessagesDto messagesDto = await repositoryWebService.GetMessages(idApplication);
                        if (messagesDto.IsOk)
                        {
                            MessageModel message = messagesDto.MessagesModel.FirstOrDefault(a => a.messageId == idSolicitud);
                            MessageDto messageDto = await repositoryWebService.GetMessageDetails(idApplication, idSolicitud);
                            if (messageDto.IsOk)
                            {
                                messageDto.MessageModel.categoryId = idApplication;
                                messageDto.MessageModel.messageId = idSolicitud;
                                messageDto.MessageModel.name = message.name;
                                messageDto.MessageModel.date = message.date;
                                string StringParametro = JsonConvert.SerializeObject(messageDto.MessageModel);

                                Dictionary<string, string> Parametro = new Dictionary<string, string>
                            {
                                { "MessageModel", StringParametro }
                            };
                                await NavigationService.NavigateToAsync("MessageDetails", Parametro);
                            }
                        }
                    }
                    else if (idApplication > 0)
                    {
                        Dictionary<string, string> Parametro = new Dictionary<string, string>
                        {
                            { "Name", sNombreCategoria },
                            { "CategoryId", idApplication.ToString() }
                        };
                        await NavigationService.NavigateToAsync("Message", Parametro);
                    }
                    else
                    {
                        //todo ir a home (no hay q hacer nada)
                    }
                }
            }
            catch (Exception ex )
            {
                throw ex;
            }            
        }
    }
}

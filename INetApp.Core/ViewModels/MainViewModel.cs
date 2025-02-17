﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace INetApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool ispermissionApp;
        private readonly IUserService UserService;
        #region Properties
        public bool IspermissionApp
        {
            get => ispermissionApp;
            set
            {
                ispermissionApp = value;
                OnPropertyChanged(nameof(IspermissionApp));
            }
        }

        #endregion
        public ICommand ControlAccesoCommand => new Command(async () => await ControlAcceso());
        public ICommand BandejaEntradaCommand => new Command(async () => await BandejaEntrada());
        public ICommand PartesCommand => new Command(async () => await Partes());
        public ICommand FormacionCommand => new Command(async () => await Formacion());
        public ICommand InfoCommand => new Command(OnInfoCommand);

        public MainViewModel()
        {
            UserService = ViewModelLocator.Resolve<IUserService>();
        }
        public override async Task InitializeAsync(IDictionary<string, string> query)
        {
            IsBusy = true;
            Text_last_update = string.Format(Literales.view_text_last_updated, DateTime.Now);
            IspermissionApp = settingsService.Permission;

            //if (await UserService.CheckVersion())
            //{
            //    try
            //    {
            //        await Browser.OpenAsync(settingsService.Url, BrowserLaunchMode.External);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }                
            //}

            IsBusy = false;
            await base.InitializeAsync(query);
        }

        private async Task ControlAcceso()
        {
            await NavigationService.NavigateToAsync("LectorQR");
        }
        private async Task BandejaEntrada()
        {
            await NavigationService.NavigateToAsync("Category");
        }

        private async Task Partes()
        {
            await NavigationService.NavigateToAsync("WorkParts");
        }
        private async Task Formacion()
        {
            Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "Ruta", "http://inet.ineco.es/prod/prodEncuestas/paginaRanking.aspx" },
                    { "Titulo", Literales.action_formation }
                };
            await NavigationService.NavigateToAsync("WebView", Parametro);
        }
        private async void OnInfoCommand()
        {
            IsBusy = true;
            Dictionary<string, string> Parametro = new Dictionary<string, string>
                {
                    { "Formulario", "//MainView"}
                };

            await NavigationService.NavigateToAsync("InfoView", Parametro);

            IsBusy = false;
        }
    }
}
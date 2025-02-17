﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using INetApp.APIWebServices.Dtos;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.QrCode.Internal;

namespace INetApp.ViewModels
{
    public class LectorQRViewModel : ViewModelBase
    {
        private readonly ILectorQRService lectorQRService;

        private ZXing.Result _QR;
        private bool _isScanning = true;
        private bool _isAnalyzing = true;

        #region Properties
        public bool IsAnalyzing
        {
            get => _isAnalyzing;
            set
            {
                _isAnalyzing = value;
                OnPropertyChanged(nameof(_isAnalyzing));
            }
        }

        public bool IsScanning
        {
            get => _isScanning;
            set
            {
                _isScanning = value;
                OnPropertyChanged(nameof(IsScanning));
            }
        }

        public ZXing.Result QR
        {
            get => _QR;
            set
            {
                _QR = value;
                OnPropertyChanged(nameof(QR));
            }
        }
        #endregion

        //public ICommand SelectOptionsCommand => new Command<OptionsModel>(OnSelectMessage);
        public ICommand ScanCommand => new Command(OnScanExecute);

        public LectorQRViewModel()
        {
            //todo pedir permiso de camara
            lectorQRService = DependencyService.Get<ILectorQRService>();
            //borrar("9MQ==U2FsYUIkPTczJkVkaWZpY2IvSWQ");
        }
        
        private async void OnScanExecute()
        {
            IsAnalyzing = false;
            IsScanning = false;

            if (QR.Text.Contains("http") || QR.Text.Contains("www"))
            {
                await Browser.OpenAsync(QR.Text , BrowserLaunchMode.External);
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    UserAccessDto userAccessDto = await lectorQRService.GetAccesoAsync(QR.Text); //result.ToString()
                if (userAccessDto.IsOk)
                    {
                        await DialogService.ShowAlertAsync(userAccessDto.UserAccessModel.mensaje, "", Literales.btn_text_accept);
                    }
                    else
                    {
                        if (!userAccessDto.IsConnected)
                        {
                            await DialogService.ShowAlertAsync(Literales.exception_message_no_connection, "", Literales.btn_text_accept);
                        }
                        else if (userAccessDto.UserAccessModel == null)
                        {
                            await DialogService.ShowAlertAsync(Literales.notification_text, Literales.notification_title, Literales.btn_text_accept);
                        }
                        else
                        {
                            await DialogService.ShowAlertAsync(userAccessDto.ErrorDescription, Literales.notification_title, Literales.btn_text_accept);
                        }
                    }
                    IsAnalyzing = true;
                    IsScanning = true;

                    if (Device.RuntimePlatform != Device.UWP)
                    {
                    //    await NavigationService.NavigateToAsync("..");
                    }
                });
            }
            
        }
    }
}
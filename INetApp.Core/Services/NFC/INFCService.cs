using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using INetApp.NFC;
using System.ComponentModel;
using INetApp.APIWebServices.Dtos;

namespace INetApp.Services.NFC
{
    public interface INFCService
    {
        Task<UserAccessDto> GetAccesoAsync(string NFC);
        bool NfcIsEnabled { get; set; }
        Task ActivateNFC();

        /// <summary>
        /// Task to safely stop listening for NFC tags
        /// </summary>
        /// <returns>The task to be performed</returns>
        Task StopListening();

        /// <summary>
        /// Task to start listening for NFC tags if the user's device platform is not iOS
        /// </summary>
        /// <returns>The task to be performed</returns>
        Task StartListeningIfNotiOS();

        /// <summary>
        /// Task to safely start listening for NFC Tags
        /// </summary>
        /// <returns>The task to be performed</returns>
        Task BeginListening();

        /// <summary>
        /// Event raised when a NDEF message is received
        /// </summary>
        /// <param name="tagInfo">Received <see cref="ITagInfo"/></param>
        string Current_OnMessageReceived(ITagInfo tagInfo);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using INetApp.NFC;
using System.ComponentModel;

namespace INetApp.Services.NFC
{
    public class NFCService
    {
        #region NFC

        public const string ALERT_TITLE = "NFC";
        public const string MIME_TYPE = "application/com.Ineco.InetApp";
        //private readonly NFCNdefTypeFormat _type;

        //bool _makeReadOnly = false;
        private bool _eventsAlreadySubscribed = false;
        private bool _isDeviceiOS = false;

        public bool DeviceIsListening { get; set; }
        //public bool DeviceIsListening
        //{
        //    get => _deviceIsListening;
        //    set
        //    {
        //        _deviceIsListening = value;
        //        OnPropertyChanged(nameof(DeviceIsListening));
        //    }
        //}
        //private bool _deviceIsListening;

        //private bool _nfcIsEnabled;

        public bool NfcIsEnabled { get; set; }
        //public bool NfcIsEnabled
        //{
        //    get => _nfcIsEnabled;
        //    set
        //    {
        //        _nfcIsEnabled = value;
        //        OnPropertyChanged(nameof(NfcIsEnabled));
        //        OnPropertyChanged(nameof(NfcIsDisabled));
        //    }
        //}

        public bool NfcIsDisabled => !NfcIsEnabled;


        public async Task ActivateNFC()
        {
            if (CrossNFC.IsSupported)
            {
                if (!CrossNFC.Current.IsAvailable)
                {
                    Console.WriteLine("NFC is not available");
                }

                NfcIsEnabled = CrossNFC.Current.IsEnabled;
                if (!NfcIsEnabled)
                {
                    Console.WriteLine("NFC is disabled");
                }

                if (Device.RuntimePlatform == Device.iOS)
                {
                    _isDeviceiOS = true;
                }

                SubscribeEvents();

                //await StartListeningIfNotiOS();
            }
        }
        private void SubscribeEvents()
        {
            if (_eventsAlreadySubscribed)
            {
                return;
            }

            _eventsAlreadySubscribed = true;

            //CrossNFC.Current.OnMessageReceived += Current_OnMessageReceived;
            //CrossNFC.Current.OnMessagePublished += Current_OnMessagePublished;
            //CrossNFC.Current.OnTagDiscovered += Current_OnTagDiscovered;
            CrossNFC.Current.OnTagListeningStatusChanged += Current_OnTagListeningStatusChanged;
            //if (!_isDeviceiOS)
            //{
                CrossNFC.Current.OnNfcStatusChanged += Current_OnNfcStatusChanged;
            //}

            if (_isDeviceiOS)
            {
                CrossNFC.Current.OniOSReadingSessionCancelled += Current_OniOSReadingSessionCancelled;
            }
        }

        /// <summary>
        /// Unsubscribe from the NFC events
        /// </summary>
        private void UnsubscribeEvents()
        {
            //CrossNFC.Current.OnMessageReceived -= Current_OnMessageReceived;
            //CrossNFC.Current.OnMessagePublished -= Current_OnMessagePublished;
            //CrossNFC.Current.OnTagDiscovered -= Current_OnTagDiscovered;
            CrossNFC.Current.OnNfcStatusChanged -= Current_OnNfcStatusChanged;
            CrossNFC.Current.OnTagListeningStatusChanged += Current_OnTagListeningStatusChanged;

            if (_isDeviceiOS)
            {
                CrossNFC.Current.OniOSReadingSessionCancelled -= Current_OniOSReadingSessionCancelled;
            }
        }

        /// <summary>
        /// Event raised when Listener Status has changed
        /// </summary>
        /// <param name="isListening"></param>
        private void Current_OnTagListeningStatusChanged(bool isListening)
        {
            DeviceIsListening = isListening;
        }

        /// <summary>
        /// Event raised when NFC Status has changed
        /// </summary>
        /// <param name="isEnabled">NFC status</param>
        private async void Current_OnNfcStatusChanged(bool isEnabled)
        {
            NfcIsEnabled = isEnabled;
            Console.WriteLine($"NFC has been {(isEnabled ? "enabled" : "disabled")}");
        }

        /// <summary>
        /// Event raised when a NDEF message is received
        /// </summary>
        /// <param name="tagInfo">Received <see cref="ITagInfo"/></param>
        public string Current_OnMessageReceived(ITagInfo tagInfo)
        {
            if (tagInfo == null)
            {
                Console.WriteLine("No tag found");
                return "";
            }

            // Customized serial number
            byte[] identifier = tagInfo.Identifier;
            string serialNumber = NFCUtils.ByteArrayToHexString(identifier, ":");
            string title = !string.IsNullOrWhiteSpace(serialNumber) ? $"Tag [{serialNumber}]" : "Tag Info";

            if (!tagInfo.IsSupported)
            {
                Console.WriteLine("Unsupported tag (app)", title);
                //await ShowAlert("Unsupported tag (app)", title);
            }
            else if (tagInfo.IsEmpty)
            {
                Console.WriteLine("Empty tag", title);
            }
            else
            {
                NFCNdefRecord first = tagInfo.Records[0];
                ShowAlert(GetMessage(first), title);
            }
            return title;
        }

        /// <summary>
        /// Event raised when user cancelled NFC session on iOS 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Current_OniOSReadingSessionCancelled(object sender, EventArgs e)
        {
            Debug("User has cancelled NFC Session");
        }

        /// <summary>
        /// Event raised when a NFC Tag is discovered
        /// </summary>
        /// <param name="tagInfo"><see cref="ITagInfo"/> to be published</param>
        /// <param name="format">Format the tag</param>
        //async void Current_OnTagDiscovered(ITagInfo tagInfo, bool format)
        //{
        //    if (!CrossNFC.Current.IsWritingTagSupported)
        //    {
        //        Console.WriteLine("Writing tag is not supported on this device");
        //        return;
        //    }

        //    try
        //    {
        //        NFCNdefRecord record = null;
        //        switch (_type)
        //        {
        //            case NFCNdefTypeFormat.WellKnown:
        //                record = new NFCNdefRecord
        //                {
        //                    TypeFormat = NFCNdefTypeFormat.WellKnown,
        //                    MimeType = MIME_TYPE,
        //                    Payload = NFCUtils.EncodeToByteArray("Plugin.NFC is awesome!"),
        //                    LanguageCode = "en"
        //                };
        //                break;
        //            case NFCNdefTypeFormat.Uri:
        //                record = new NFCNdefRecord
        //                {
        //                    TypeFormat = NFCNdefTypeFormat.Uri,
        //                    Payload = NFCUtils.EncodeToByteArray("https://github.com/franckbour/Plugin.NFC")
        //                };
        //                break;
        //            case NFCNdefTypeFormat.Mime:
        //                record = new NFCNdefRecord
        //                {
        //                    TypeFormat = NFCNdefTypeFormat.Mime,
        //                    MimeType = MIME_TYPE,
        //                    Payload = NFCUtils.EncodeToByteArray("Plugin.NFC is awesome!")
        //                };
        //                break;
        //            default:
        //                break;
        //        }

        //        if (!format && record == null)
        //            throw new Exception("Record can't be null.");

        //        tagInfo.Records = new[] { record };

        //        if (format)
        //            CrossNFC.Current.ClearMessage(tagInfo);
        //        else
        //        {
        //            //CrossNFC.Current.PublishMessage(tagInfo, _makeReadOnly);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        /// <summary>
        /// Start listening for NFC Tags when "READ TAG" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked_StartListening(object sender, System.EventArgs e)
        {
            await BeginListening();
        }

        /// <summary>
        /// Stop listening for NFC tags
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked_StopListening(object sender, System.EventArgs e)
        {
            await StopListening();
        }

        ///// <summary>
        ///// Task to publish data to the tag
        ///// </summary>
        ///// <param name="type"><see cref="NFCNdefTypeFormat"/></param>
        ///// <returns>The task to be performed</returns>
        //async Task Publish(NFCNdefTypeFormat? type = null)
        //{
        //	await StartListeningIfNotiOS();
        //	try
        //	{
        //		if (ChkReadOnly.IsChecked)
        //		{
        //			if (!await DisplayAlert("Warning", "Make a Tag read-only operation is permanent and can't be undone. Are you sure you wish to continue?", "Yes", "No"))
        //			{
        //				ChkReadOnly.IsChecked = false;
        //				return;
        //			}
        //			_makeReadOnly = true;
        //		}
        //		else
        //			_makeReadOnly = false;

        //		if (type.HasValue) _type = type.Value;
        //		CrossNFC.Current.StartPublishing(!type.HasValue);
        //	}
        //	catch (Exception ex)
        //	{
        //		await ShowAlert(ex.Message);
        //	}
        //}

        /// <summary>
        /// Returns the tag information from NDEF record
        /// </summary>
        /// <param name="record"><see cref="NFCNdefRecord"/></param>
        /// <returns>The tag information</returns>
        private string GetMessage(NFCNdefRecord record)
        {
            string message = $"Message: {record.Message}";
            message += Environment.NewLine;
            message += $"RawMessage: {Encoding.UTF8.GetString(record.Payload)}";
            message += Environment.NewLine;
            message += $"Type: {record.TypeFormat}";

            if (!string.IsNullOrWhiteSpace(record.MimeType))
            {
                message += Environment.NewLine;
                message += $"MimeType: {record.MimeType}";
            }

            return message;
        }

        /// <summary>
        /// Write a debug message in the debug console
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        private void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Display an alert
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Alert title</param>
        /// <returns>The task to be performed</returns>
        private Task ShowAlert(string message, string title = null)
        {
            return null;
            //return DisplayAlert(string.IsNullOrWhiteSpace(title) ? ALERT_TITLE : title, message, "Cancel");
        }

        /// <summary>
        /// Task to start listening for NFC tags if the user's device platform is not iOS
        /// </summary>
        /// <returns>The task to be performed</returns>
        public async Task StartListeningIfNotiOS()
        {
            if (_isDeviceiOS)
            {
                return;
            }

            await BeginListening();
        }

        /// <summary>
        /// Task to safely start listening for NFC Tags
        /// </summary>
        /// <returns>The task to be performed</returns>
        public async Task BeginListening()
        {
            try
            {
                CrossNFC.Current.StartListening();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Task to safely stop listening for NFC tags
        /// </summary>
        /// <returns>The task to be performed</returns>
        public async Task StopListening()
        {
            try
            {
                CrossNFC.Current.StopListening();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}

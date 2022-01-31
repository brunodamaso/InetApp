using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;

namespace INetApp.NFC
{
    /// <summary>
    /// Android implementation of <see cref="INFC"/>
    /// </summary>
    public class NFCImplementation : INFC
    {
        public event EventHandler OnTagConnected;
        public event EventHandler OnTagDisconnected;
        public event NdefMessageReceivedEventHandler OnMessageReceived;
        public event NdefMessagePublishedEventHandler OnMessagePublished;
        public event TagDiscoveredEventHandler OnTagDiscovered;
        public event EventHandler OniOSReadingSessionCancelled;
        public event TagListeningStatusChangedEventHandler OnTagListeningStatusChanged;

        private readonly NfcAdapter _nfcAdapter;
        private bool _isListening;
        private bool _isWriting;
        private bool _isFormatting;
        private Tag _currentTag;

        /// <summary>
        /// Current Android <see cref="Context"/>
        /// </summary>
        private Context CurrentContext => CrossNFC.AppContext;

        /// <summary>
        /// Current Android <see cref="Activity"/>
        /// </summary>
        private Activity CurrentActivity => CrossNFC.GetCurrentActivity(true);

        /// <summary>
        /// Checks if NFC Feature is available
        /// </summary>
        public bool IsAvailable
        {
            get
            {
                if (CurrentContext.CheckCallingOrSelfPermission(Manifest.Permission.Nfc) != Android.Content.PM.Permission.Granted)
                {
                    return false;
                }

                return _nfcAdapter != null;
            }
        }

        /// <summary>
        /// Checks if NFC Feature is enabled
        /// </summary>
        public bool IsEnabled => IsAvailable && _nfcAdapter.IsEnabled;

        /// <summary>
        /// Checks if writing mode is supported
        /// </summary>
        public bool IsWritingTagSupported => NFCUtils.IsWritingSupported();

        /// <summary>
        /// NFC configuration
        /// </summary>
        public NfcConfiguration Configuration { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NFCImplementation()
        {
            _nfcAdapter = NfcAdapter.GetDefaultAdapter(CurrentContext);
            Configuration = NfcConfiguration.GetDefaultConfiguration();
        }

        /// <summary>
        /// Update NFC configuration
        /// </summary>
        /// <param name="configuration"><see cref="NfcConfiguration"/></param>
        public void SetConfiguration(NfcConfiguration configuration)
        {
            Configuration.Update(configuration);
        }

        /// <summary>
        /// Starts tags detection
        /// </summary>
        public void StartListening()
        {
            if (_nfcAdapter == null)
            {
                return;
            }

            Intent intent = new Intent(CurrentActivity, CurrentActivity.GetType()).AddFlags(ActivityFlags.SingleTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(CurrentActivity, 0, intent, 0);

            IntentFilter ndefFilter = new IntentFilter(NfcAdapter.ActionNdefDiscovered);
            ndefFilter.AddDataType("*/*");

            IntentFilter tagFilter = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            tagFilter.AddCategory(Intent.CategoryDefault);

            IntentFilter[] filters = new IntentFilter[] { ndefFilter, tagFilter };

            _nfcAdapter.EnableForegroundDispatch(CurrentActivity, pendingIntent, filters, null);

            _isListening = true;
            OnTagListeningStatusChanged?.Invoke(_isListening);
        }

        /// <summary>
        /// Stops tags detection
        /// </summary>
        public void StopListening()
        {
            DisablePublishing();
            if (_nfcAdapter != null)
            {
                _nfcAdapter.DisableForegroundDispatch(CurrentActivity);
            }

            _isListening = false;
            OnTagListeningStatusChanged?.Invoke(_isListening);
        }

        /// <summary>
        /// Starts tag publishing (writing or formatting)
        /// </summary>
        /// <param name="clearMessage">Format tag</param>
        public void StartPublishing(bool clearMessage = false)
        {
            if (!IsWritingTagSupported)
            {
                return;
            }

            _isWriting = true;
            _isFormatting = clearMessage;
        }

        /// <summary>
        /// Stops tag publishing
        /// </summary>
        public void StopPublishing()
        {
            DisablePublishing();
        }

        /// <summary>
        /// Publish or write a message on a tag
        /// </summary>
        /// <param name="tagInfo">see <see cref="ITagInfo"/></param>
        /// <param name="makeReadOnly">make tag read-only</param>
        public void PublishMessage(ITagInfo tagInfo, bool makeReadOnly = false)
        {
            WriteOrClearMessage(tagInfo, false, makeReadOnly);
        }

        /// <summary>
        /// Format tag
        /// </summary>
        /// <param name="tagInfo">see <see cref="ITagInfo"/></param>
        public void ClearMessage(ITagInfo tagInfo)
        {
            WriteOrClearMessage(tagInfo, true);
        }

        /// <summary>
        /// Write or Clear a NDEF message
        /// </summary>
        /// <param name="tagInfo"><see cref="ITagInfo"/></param>
        /// <param name="clearMessage">Clear Message</param>
        /// <param name="makeReadOnly">Make tag read-only</param>
        internal void WriteOrClearMessage(ITagInfo tagInfo, bool clearMessage = false, bool makeReadOnly = false)
        {
            try
            {
                if (_currentTag == null)
                {
                    throw new Exception(Configuration.Messages.NFCErrorMissingTag);
                }

                if (tagInfo == null)
                {
                    throw new Exception(Configuration.Messages.NFCErrorMissingTagInfo);
                }

                Ndef ndef = Ndef.Get(_currentTag);
                if (ndef != null)
                {
                    try
                    {
                        if (!ndef.IsWritable)
                        {
                            throw new Exception(Configuration.Messages.NFCErrorReadOnlyTag);
                        }

                        if (ndef.MaxSize < NFCUtils.GetSize(tagInfo.Records))
                        {
                            throw new Exception(Configuration.Messages.NFCErrorCapacityTag);
                        }

                        ndef.Connect();
                        OnTagConnected?.Invoke(null, EventArgs.Empty);

                        NdefMessage message = null;
                        if (clearMessage)
                        {
                            message = GetEmptyNdefMessage();
                        }
                        else
                        {
                            List<NdefRecord> records = new List<NdefRecord>();
                            for (int i = 0; i < tagInfo.Records.Length; i++)
                            {
                                NFCNdefRecord record = tagInfo.Records[i];
                                if (GetAndroidNdefRecord(record) is NdefRecord ndefRecord)
                                {
                                    records.Add(ndefRecord);
                                }
                            }

                            if (records.Any())
                            {
                                message = new NdefMessage(records.ToArray());
                            }
                        }

                        if (message != null)
                        {
                            ndef.WriteNdefMessage(message);

                            if (!clearMessage && makeReadOnly)
                            {
                                if (!MakeReadOnly(ndef))
                                {
                                    Console.WriteLine("Cannot lock tag");
                                }
                            }

                            ITagInfo nTag = GetTagInfo(_currentTag, ndef.NdefMessage);
                            OnMessagePublished?.Invoke(nTag);
                        }
                        else
                        {
                            throw new Exception(Configuration.Messages.NFCErrorWrite);
                        }
                    }
                    catch (Android.Nfc.TagLostException tlex)
                    {
                        throw new Exception("Tag Lost Error: " + tlex.Message);
                    }
                    catch (Java.IO.IOException ioex)
                    {
                        throw new Exception("Tag IO Error: " + ioex.Message);
                    }
                    catch (Android.Nfc.FormatException fe)
                    {
                        throw new Exception("Tag Format Error: " + fe.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Tag Error:" + ex.Message);
                    }
                    finally
                    {
                        if (ndef.IsConnected)
                        {
                            ndef.Close();
                        }

                        _currentTag = null;
                        OnTagDisconnected?.Invoke(null, EventArgs.Empty);
                    }
                }
                else
                {
                    throw new Exception(Configuration.Messages.NFCErrorNotCompliantTag);
                }
            }
            catch (Exception ex)
            {
                StopPublishingAndThrowError(ex.Message);
            }
        }

        /// <summary>
        /// Handle Android OnNewIntent
        /// </summary>
        /// <param name="intent">Android <see cref="Intent"/></param>
        internal void HandleNewIntent(Intent intent)
        {
            if (intent == null)
            {
                return;
            }

            if (intent.Action == NfcAdapter.ActionTagDiscovered || intent.Action == NfcAdapter.ActionNdefDiscovered)
            {
                _currentTag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
                if (_currentTag != null)
                {
                    ITagInfo nTag = GetTagInfo(_currentTag);
                    if (_isWriting)
                    {
                        // Write mode
                        OnTagDiscovered?.Invoke(nTag, _isFormatting);
                    }
                    else
                    {
                        // Read mode
                        OnMessageReceived?.Invoke(nTag);
                    }
                }
            }
        }

        /// <summary>
        /// Handle Android OnResume
        /// </summary>
        internal void HandleOnResume()
        {
            // Android 10 fix:
            // If listening mode is already enable, we restart listening when activity is resumed
            if (_isListening)
            {
                StartListening();
            }
        }

        #region Private

        /// <summary>
        /// Stops publishing and throws error
        /// </summary>
        /// <param name="message">message</param>
        private void StopPublishingAndThrowError(string message)
        {
            StopPublishing();
            throw new Exception(message);
        }

        /// <summary>
        /// Deactivate publishing
        /// </summary>
        private void DisablePublishing()
        {
            _isWriting = false;
            _isFormatting = false;
        }

        /// <summary>
        /// Transforms an array of <see cref="NdefRecord"/> into an array of <see cref="NFCNdefRecord"/>
        /// </summary>
        /// <param name="records">Array of <see cref="NdefRecord"/></param>
        /// <returns>Array of <see cref="NFCNdefRecord"/></returns>
        private NFCNdefRecord[] GetRecords(NdefRecord[] records)
        {
            NFCNdefRecord[] results = new NFCNdefRecord[records.Length];
            for (int i = 0; i < records.Length; i++)
            {
                NFCNdefRecord ndefRecord = new NFCNdefRecord
                {
                    TypeFormat = (NFCNdefTypeFormat)records[i].Tnf,
                    Uri = records[i].ToUri()?.ToString(),
                    MimeType = records[i].ToMimeType(),
                    Payload = records[i].GetPayload()
                };
                results.SetValue(ndefRecord, i);
            }
            return results;
        }

        /// <summary>
        /// Returns informations contains in NFC Tag
        /// </summary>
        /// <param name="tag">Android <see cref="Tag"/></param>
        /// <param name="ndefMessage">Android <see cref="NdefMessage"/></param>
        /// <returns><see cref="ITagInfo"/></returns>
        private ITagInfo GetTagInfo(Tag tag, NdefMessage ndefMessage = null)
        {
            if (tag == null)
            {
                return null;
            }

            Ndef ndef = Ndef.Get(tag);
            TagInfo nTag = new TagInfo(tag.GetId(), ndef != null);

            if (ndef != null)
            {
                nTag.Capacity = ndef.MaxSize;
                nTag.IsWritable = ndef.IsWritable;

                if (ndefMessage == null)
                {
                    ndefMessage = ndef.CachedNdefMessage;
                }

                if (ndefMessage != null)
                {
                    NdefRecord[] records = ndefMessage.GetRecords();
                    nTag.Records = GetRecords(records);
                }
            }

            return nTag;
        }

        /// <summary>
        /// Transforms a <see cref="NFCNdefRecord"/> into an Android <see cref="NdefRecord"/>
        /// </summary>
        /// <param name="record">Object <see cref="NFCNdefRecord"/></param>
        /// <returns>Android <see cref="NdefRecord"/></returns>
        private NdefRecord GetAndroidNdefRecord(NFCNdefRecord record)
        {
            if (record == null)
            {
                return null;
            }

            NdefRecord ndefRecord = null;
            switch (record.TypeFormat)
            {
                case NFCNdefTypeFormat.WellKnown:
                    string languageCode = record.LanguageCode;
                    if (string.IsNullOrWhiteSpace(languageCode))
                    {
                        languageCode = Configuration.DefaultLanguageCode;
                    }

                    ndefRecord = NdefRecord.CreateTextRecord(languageCode.Substring(0, 2), Encoding.UTF8.GetString(record.Payload));
                    break;
                case NFCNdefTypeFormat.Mime:
                    ndefRecord = NdefRecord.CreateMime(record.MimeType, record.Payload);
                    break;
                case NFCNdefTypeFormat.Uri:
                    ndefRecord = NdefRecord.CreateUri(Encoding.UTF8.GetString(record.Payload));
                    break;
                case NFCNdefTypeFormat.External:
                    ndefRecord = NdefRecord.CreateExternal(record.ExternalDomain, record.ExternalType, record.Payload);
                    break;
                case NFCNdefTypeFormat.Empty:
                    ndefRecord = GetEmptyNdefRecord();
                    break;
                case NFCNdefTypeFormat.Unknown:
                case NFCNdefTypeFormat.Unchanged:
                case NFCNdefTypeFormat.Reserved:
                default:
                    break;

            }
            return ndefRecord;
        }

        /// <summary>
        /// Returns an empty Android <see cref="NdefRecord"/>
        /// </summary>
        /// <returns>Android <see cref="NdefRecord"/></returns>
        private NdefRecord GetEmptyNdefRecord()
        {
            byte[] empty = Array.Empty<byte>();
            return new NdefRecord(NdefRecord.TnfEmpty, empty, empty, empty);
        }

        /// <summary>
        /// Returns an empty Android <see cref="NdefMessage"/>
        /// </summary>
        /// <returns>Android <see cref="NdefMessage"/></returns>
        private NdefMessage GetEmptyNdefMessage()
        {
            NdefRecord[] records = new NdefRecord[1];
            records[0] = GetEmptyNdefRecord();
            return new NdefMessage(records);
        }

        /// <summary>
        /// Make a tag read-only
        /// WARNING: This operation is permanent
        /// </summary>
        /// <param name="ndef"><see cref="Ndef"/></param>
        /// <returns>boolean</returns>
        private bool MakeReadOnly(Ndef ndef)
        {
            if (ndef == null)
            {
                return false;
            }

            bool result = false;
            bool newConnection = false;

            if (!ndef.IsConnected)
            {
                newConnection = true;
                ndef.Connect();
            }

            if (ndef.CanMakeReadOnly())
            {
                result = ndef.MakeReadOnly();
            }

            if (newConnection && ndef.IsConnected)
            {
                ndef.Close();
            }

            return result;
        }

        #endregion

        #region NFC Status Event Listener

        private NfcBroadcastReceiver _nfcBroadcastReceiver;

        private event OnNfcStatusChangedEventHandler _onNfcStatusChangedInternal;

        public event OnNfcStatusChangedEventHandler OnNfcStatusChanged
        {
            add
            {
                bool wasRunning = _onNfcStatusChangedInternal != null;
                _onNfcStatusChangedInternal += value;
                if (!wasRunning && _onNfcStatusChangedInternal != null)
                {
                    RegisterListener();
                }
            }
            remove
            {
                bool wasRunning = _onNfcStatusChangedInternal != null;
                _onNfcStatusChangedInternal -= value;
                if (wasRunning && _onNfcStatusChangedInternal == null)
                {
                    UnRegisterListener();
                }
            }
        }

        /// <summary>
        /// Register NFC Broadcast Receiver
        /// </summary>
        private void RegisterListener()
        {
            _nfcBroadcastReceiver = new NfcBroadcastReceiver(OnNfcStatusChange);
            CurrentContext?.RegisterReceiver(_nfcBroadcastReceiver, new IntentFilter(NfcAdapter.ActionAdapterStateChanged));
        }

        /// <summary>
        /// Unregister NFC Broadcast Receiver
        /// </summary>
        private void UnRegisterListener()
        {
            if (_nfcBroadcastReceiver == null)
            {
                return;
            }

            try
            {
                CurrentContext?.UnregisterReceiver(_nfcBroadcastReceiver);
            }
            catch (Java.Lang.IllegalArgumentException ex)
            {
                throw new Exception("NFC Broadcast Receiver Error: " + ex.Message);
            }

            _nfcBroadcastReceiver.Dispose();
            _nfcBroadcastReceiver = null;
        }

        /// <summary>
        /// Called when NFC status has changed
        /// </summary>
        /// <param name="value">NFC Availability</param>
        private void OnNfcStatusChange()
        {
            _onNfcStatusChangedInternal?.Invoke(IsEnabled);
        }

        /// <summary>
        /// Broadcast Receiver to check NFC feature availability
        /// </summary>
        [BroadcastReceiver(Enabled = true, Exported = false, Label = "NFC Status Broadcast Receiver")]
        private class NfcBroadcastReceiver : BroadcastReceiver
        {
            private readonly Action _onChanged;

            public NfcBroadcastReceiver() { }
            public NfcBroadcastReceiver(Action onChanged)
            {
                _onChanged = onChanged;
            }

            public override async void OnReceive(Context context, Intent intent)
            {
                if (intent.Action == NfcAdapter.ActionAdapterStateChanged)
                {
                    int state = intent.GetIntExtra(NfcAdapter.ExtraAdapterState, default);
                    if (state == NfcAdapter.StateOff || state == NfcAdapter.StateOn)
                    {
                        // await 1500ms to ensure that the status updates
                        await Task.Delay(1500);
                        _onChanged?.Invoke();
                    }
                }
            }
        }

        #endregion

    }
}

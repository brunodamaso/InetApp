using System;
using System.Collections.Generic;
using System.Text;
using IdentityModel;
using INetApp.APIWebServices.Dtos;
using INetApp.Extensions;
using INetApp.Services.Settings;
using PCLCrypto;
using Xamarin.Essentials;
using static PCLCrypto.WinRTCrypto;

namespace INetApp.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private const string APP_KEY = "com.ineco.android.master";
        private const string KEY_PREFIX = "8RYmvH71oMqnofee0Be9";

        private readonly ISettingsService settingsService;
        private readonly IDeviceService deviceService;

        public IdentityService(ISettingsService _settingsService, IDeviceService _deviceService)
        {
            settingsService = _settingsService;
            deviceService = _deviceService;
        }


        private static byte[] getRawKey(byte[] seed)
        {
            try
            {
                int keyLength = 128;
                byte[] keyBytes = new byte[keyLength / 8];
                for (int i = 0; i < keyLength / 8; i++)
                {
                    keyBytes[i] = 0x0;
                }
                byte[] passwordBytes = seed;
                int length = passwordBytes.Length < keyBytes.Length ? passwordBytes.Length
                        : keyBytes.Length;
                Array.Copy(passwordBytes, 0, keyBytes, 0, length);
                return keyBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void PutCredentialsFromPrefs(string user, string pass, Models.UserLoggedModel userLoggedModel)
        {

            //ISharedPreferences prefs = context.GetSharedPreferences(APP_KEY, FileCreationMode.Private); // Context.MODE_PRIVATE
            //prefs.Edit().PutString(USER_PREF, user).Apply();
            //string keyString = KEY_PREFIX + Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
            try
            {
                settingsService.UserName = user;

                string keyString = KEY_PREFIX + deviceService.DispositivoID;
                byte[] key = getRawKey(Encoding.UTF8.GetBytes(keyString)); // .GetBytes("UTF8")
                ISymmetricKeyAlgorithmProvider provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
                ICryptographicKey sKeySpec = provider.CreateSymmetricKey(key);
                byte[] passEncrypted = CryptographicEngine.Encrypt(sKeySpec, Encoding.UTF8.GetBytes(pass));
                settingsService.UserPass = Base64Url.Encode(passEncrypted);

                settingsService.Version = userLoggedModel.version.IsNullOrEmpty() ? VersionTracking.CurrentVersion : userLoggedModel.version;
                settingsService.Requerido = userLoggedModel.requerido;
				settingsService.Url = userLoggedModel.url.IsNullOrEmpty() ? "https://inet-pre.ineco.es/dti02/conecta_t/IndexAndroid.html" : userLoggedModel.url;

				settingsService.AuthAccessToken = user;
                settingsService.NameInitial = userLoggedModel.nameInitial + userLoggedModel.lastNameInitial;
                settingsService.NameFull = userLoggedModel.fullName;
                settingsService.Permission = userLoggedModel.permission;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            //SecretKeySpec sKeySpec = new SecretKeySpec(key, "AES");
            //Cipher cipher = null;
            //    cipher = Cipher.GetInstance("AES");
            //cipher.Init(CipherMode.EncryptMode, sKeySpec); // Cipher.DecryptMode
            //    //passEncrypted = cipher.doFinal(pass.getBytes("UTF8"));

            //prefs.Edit().PutString(PASS_PREF, Base64.EncodeToString(passEncrypted, Base64Flags.Default)).Apply();
        }
        public KeyValuePair<string, object> GetCredentialsFromPrefs()
        {
            KeyValuePair<string, object> retorno = new KeyValuePair<string, object>();
            string username = settingsService.UserName;
            string password = settingsService.UserPass;

            string keyString = KEY_PREFIX + deviceService.DispositivoID;//.Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);

            try
            {
                byte[] key = getRawKey(Encoding.UTF8.GetBytes(keyString)); // .GetBytes("UTF8")
                ISymmetricKeyAlgorithmProvider provider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
                ICryptographicKey sKeySpec = provider.CreateSymmetricKey(key);
                //SecretKeySpec sKeySpec = new SecretKeySpec(key, "AES");

                //Cipher cipher = null;
                //    cipher = Cipher.GetInstance("AES");
                //    //cipher.Init(CipherMode.DecryptMode, sKeySpec); // Cipher.DecryptMode
                byte[] passDecrypted = CryptographicEngine.Decrypt(sKeySpec, Base64Url.Decode(password));
                //passDecrypted = cipher.DoFinal(Base64.Decode(password, Base64Flags.Default));
                retorno = new KeyValuePair<string, object>(username, Encoding.UTF8.GetString(passDecrypted));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);//Console.WriteLine(e.StackTrace);
            }
            return retorno;
        }
    }
}

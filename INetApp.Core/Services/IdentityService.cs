using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using INetApp.Services;
using INetApp.Services.RequestProvider;
using INetApp.Models.Token;
using INetApp.Helpers;
using IdentityModel;
using PCLCrypto;
using static PCLCrypto.WinRTCrypto;
using INetApp.Services.Settings;
using INetApp;

namespace INetApp.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private const string APP_KEY = "com.ineco.android.master";
        private const string KEY_PREFIX = "8RYmvH71oMqnofee0Be9";
        
        private readonly IRequestProvider requestProvider;
        private readonly ISettingsService settingsService;
        private readonly IDeviceService deviceService;
        private string _codeVerifier;

        public IdentityService(IRequestProvider _requestProvider, ISettingsService _settingsService, IDeviceService _deviceService)
        {
            requestProvider = _requestProvider;
            settingsService = _settingsService;
            deviceService = _deviceService;
        }

        public string CreateAuthorizationRequest()
        {
            // Create URI to authorization endpoint
            var authorizeRequest = new AuthorizeRequest(GlobalSetting.Instance.AuthorizeEndpoint);

            // Dictionary with values for the authorize request
            var dic = new Dictionary<string, string>();
            dic.Add("client_id", GlobalSetting.Instance.ClientId);
            dic.Add("client_secret", GlobalSetting.Instance.ClientSecret);
            dic.Add("response_type", "code id_token");
            dic.Add("scope", "openid profile basket orders offline_access");
            dic.Add("redirect_uri", GlobalSetting.Instance.Callback);
            dic.Add("nonce", Guid.NewGuid().ToString("N"));
            dic.Add("code_challenge", CreateCodeChallenge());
            dic.Add("code_challenge_method", "S256");

            // Add CSRF token to protect against cross-site request forgery attacks.
            var currentCSRFToken = Guid.NewGuid().ToString("N");
            dic.Add("state", currentCSRFToken);

            var authorizeUri = authorizeRequest.Create(dic);
            return authorizeUri;
        }

        public string CreateLogoutRequest(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return string.Empty;
            }

            return string.Format("{0}?id_token_hint={1}&post_logout_redirect_uri={2}",
                GlobalSetting.Instance.LogoutEndpoint,
                token,
                GlobalSetting.Instance.LogoutCallback);
        }

        public async Task<UserToken> GetTokenAsync(string code)
        {
            string data = string.Format("grant_type=authorization_code&code={0}&redirect_uri={1}&code_verifier={2}", code, WebUtility.UrlEncode(GlobalSetting.Instance.Callback), _codeVerifier);
            var token = await requestProvider.PostAsync<UserToken>(GlobalSetting.Instance.TokenEndpoint, data, GlobalSetting.Instance.ClientId, GlobalSetting.Instance.ClientSecret);
            return token;
        }

        private string CreateCodeChallenge()
        {
            _codeVerifier = RandomNumberGenerator.CreateUniqueId();
            var sha256 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);
            var challengeBuffer = sha256.HashData(CryptographicBuffer.CreateFromByteArray(Encoding.UTF8.GetBytes(_codeVerifier)));
            byte[] challengeBytes;
            CryptographicBuffer.CopyToByteArray(challengeBuffer, out challengeBytes);
            return Base64Url.Encode(challengeBytes);
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

        public void PutCredentialsFromPrefs(string user, string pass)
        {
            settingsService.UserName = user;

            string keyString = KEY_PREFIX + this.deviceService.DispositivoID;

            //ISharedPreferences prefs = context.GetSharedPreferences(APP_KEY, FileCreationMode.Private); // Context.MODE_PRIVATE
            //prefs.Edit().PutString(USER_PREF, user).Apply();
            //string keyString = KEY_PREFIX + Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
            byte[] key = new byte[0];
            try
            {
                key = getRawKey(Encoding.UTF8.GetBytes(keyString)); // .GetBytes("UTF8")
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesGcm);
            var sKeySpec = provider.CreateSymmetricKey(key);

            //SecretKeySpec sKeySpec = new SecretKeySpec(key, "AES");
            //Cipher cipher = null;
            //try
            //{
            //    cipher = Cipher.GetInstance("AES");
            //}
            //catch (NoSuchAlgorithmException e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            //catch (NoSuchPaddingException e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            byte[] passEncrypted = new byte[0];
            try
            {
                passEncrypted = WinRTCrypto.CryptographicEngine.Encrypt(sKeySpec, Encoding.UTF8.GetBytes(pass));
                //cipher.Init(CipherMode.EncryptMode, sKeySpec); // Cipher.DecryptMode
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            //try
            //{                
            //    //passEncrypted = cipher.doFinal(pass.getBytes("UTF8"));
            //}
            //catch (IllegalBlockSizeException e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            //catch (BadPaddingException e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            //catch (Java.IO.UnsupportedEncodingException e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            settingsService.UserPass= Base64Url.Encode(passEncrypted);

            //prefs.Edit().PutString(PASS_PREF, Base64.EncodeToString(passEncrypted, Base64Flags.Default)).Apply();
        }
        public KeyValuePair<string, object> GetCredentialsFromPrefs()
        {
            string username = settingsService.UserName;
            string password = settingsService.UserPass;

            string keyString = KEY_PREFIX + this.deviceService.DispositivoID;//.Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);

            byte[] key = new byte[0];
            try
            {
                key = getRawKey(Encoding.UTF8.GetBytes(keyString)); // .GetBytes("UTF8")
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);//Console.WriteLine(e.StackTrace);
            }
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesGcm);
            var sKeySpec = provider.CreateSymmetricKey(key);

            //SecretKeySpec sKeySpec = new SecretKeySpec(key, "AES");

            //Cipher cipher = null;
            //try
            //{
            //    cipher = Cipher.GetInstance("AES");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            //try
            //{
            //    //cipher.Init(CipherMode.DecryptMode, sKeySpec); // Cipher.DecryptMode
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}
            byte[] passDecrypted = new byte[0];
            try
            {
                passDecrypted = WinRTCrypto.CryptographicEngine.Decrypt(sKeySpec, Base64Url.Decode(password));
                //passDecrypted = cipher.DoFinal(Base64.Decode(password, Base64Flags.Default));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            //catch (BadPaddingException e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //}  

            return new KeyValuePair<string, object>(username, Encoding.UTF8.GetString(passDecrypted));

        }
    }
}

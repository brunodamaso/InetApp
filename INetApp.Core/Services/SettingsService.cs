using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace INetApp.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        #region Setting Constants

        private const string USER_PREF = "i6KvvUNxz7";
        private const string PASS_PREF = "IzSkiR0nDz";
        private const string AccessToken = "access_token";
        private const string nameInitial = "Iniciales";
        private const string nameUser = "FullName";
        private const string keyPermission = "isPermission";

        //private const string IdToken = "id_token";        
        //private const string IdIdentityBase = "url_base";
        private readonly string AccessTokenDefault = string.Empty;
        private readonly string IdTokenDefault = string.Empty;
        
        #endregion

        #region Settings Properties

        public string UserName
        {
            get => Preferences.Get(USER_PREF, "");
            set => Preferences.Set(USER_PREF, value);
        }

        public string UserPass
        {
            get => Preferences.Get(PASS_PREF, "");
            set => Preferences.Set(PASS_PREF, value);
        }

        public string AuthAccessToken
        {
            get => Preferences.Get(AccessToken, AccessTokenDefault);
            set => Preferences.Set(AccessToken, value);
        }
        public string NameInitial
        {
            get => Preferences.Get(nameInitial, "");
            set => Preferences.Set(nameInitial, value);
        }
        public string NameFull
        {
            get => Preferences.Get(nameUser, "");
            set => Preferences.Set(nameUser, value);
        }

        public bool Permission
        {
            get => Preferences.Get(keyPermission, false);
            set => Preferences.Set(keyPermission, value);
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IdentityModel.Client;
using INetApp.Extensions;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.Validations;
using INetApp.ViewModels.Base;
using Xamarin.Forms;

namespace INetApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        private bool _isValid;
        private bool _isLogin;
        private string _authUrl;

        private readonly ISettingsService _settingsService;
        private readonly IIdentityService _identityService;

        #region Properties
        public ValidatableObject<string> UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => this.UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => this.Password);
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => this.IsValid);
            }
        }

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => this.IsLogin);
            }
        }

        public string LoginUrl
        {
            get => _authUrl;
            set
            {
                _authUrl = value;
                RaisePropertyChanged(() => this.LoginUrl);
            }
        }

        #endregion

        #region ICommand
        public ICommand LoginCommand => new Command(OnLoginClicked);
   
        #endregion

        public LoginViewModel()
        {
            _settingsService = DependencyService.Get<ISettingsService>();
            _identityService = DependencyService.Get<IIdentityService>();

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            
            AddValidations();
        }


        public override Task InitializeAsync(IDictionary<string, string> query)
        {
            (bool ContainsKeyAndValue, bool Value) logout = query.GetValueAsBool("Logout");

            if (logout.ContainsKeyAndValue && logout.Value == true)
            {
                Logout();
            }

            return Task.CompletedTask;
        }

        private void OnLoginClicked(object obj)
        {
            this.IsBusy = true;
            if (Validate())
            {
                //todo preguntar por idioma
                //todo color boton login
                //todo quitar titulo en login
                //

                //UserLoggedDto userLoggedDto = await repositoryWebService.GetUserLogged(this.Et_username, this.Et_password);
                //if (userLoggedDto.IsOk && userLoggedDto.UserLoggedModel.permission)
                //{
                //    this.UserLoggedModel = userLoggedDto.UserLoggedModel;

                //}
            }
            this.IsBusy = false;
        }

        private async Task SignInAsync()
        {
            this.IsBusy = true;

            await Task.Delay(10);

            this.LoginUrl = _identityService.CreateAuthorizationRequest();

            this.IsValid = true;
            this.IsLogin = true;
            this.IsBusy = false;
        }


        private void Logout()
        {
            string authIdToken = _settingsService.AuthIdToken;
            string logoutRequest = _identityService.CreateLogoutRequest(authIdToken);

            if (!string.IsNullOrEmpty(logoutRequest))
            {
                // Logout
                this.LoginUrl = logoutRequest;
            }
        }

        private async Task NavigateAsync(string url)
        {
            string unescapedUrl = System.Net.WebUtility.UrlDecode(url);

            if (unescapedUrl.Equals(GlobalSetting.Instance.LogoutCallback))
            {
                _settingsService.AuthAccessToken = string.Empty;
                _settingsService.AuthIdToken = string.Empty;
                this.IsLogin = false;
                this.LoginUrl = _identityService.CreateAuthorizationRequest();
            }
            else if (unescapedUrl.Contains(GlobalSetting.Instance.Callback))
            {
                AuthorizeResponse authResponse = new AuthorizeResponse(url);
                if (!string.IsNullOrWhiteSpace(authResponse.Code))
                {
                    Models.Token.UserToken userToken = await _identityService.GetTokenAsync(authResponse.Code);
                    string accessToken = userToken.AccessToken;

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        _settingsService.AuthAccessToken = accessToken;
                        _settingsService.AuthIdToken = authResponse.IdentityToken;
                        await NavigationService.NavigateToAsync("//Main/Catalog");
                    }
                }
            }
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }
    }
}
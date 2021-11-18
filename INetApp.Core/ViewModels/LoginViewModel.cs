using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IdentityModel.Client;
using INetApp.APIWebServices.Dtos;
using INetApp.Extensions;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.Services.Identity;
using INetApp.Services.Settings;
using INetApp.Services.User;
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

        private readonly IIdentityService identityService;
        private IUserService userService;
        private UserLoggedModel _UserLoggedModel;

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

        public UserLoggedModel UserLoggedModel
        {
            get => _UserLoggedModel;
            set
            {
                _UserLoggedModel = value;
                RaisePropertyChanged(() => _UserLoggedModel);
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
            identityService = DependencyService.Get<IIdentityService>();
            userService = DependencyService.Get<IUserService>();

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            GetCredenciales();

            AddValidations();
        }

        private void GetCredenciales()
        {
            KeyValuePair<string, object> credenciales = identityService.GetCredentialsFromPrefs();
            this.UserName.Value = credenciales.Key.ToString();
        }


        public override Task InitializeAsync(IDictionary<string, string> query)
        {
            (bool ContainsKeyAndValue, bool Value) logout = query.GetValueAsBool("Logout");

            if (logout.ContainsKeyAndValue && logout.Value)
            {
                Logout();
            }

            return Task.CompletedTask;
        }

        private async void OnLoginClicked(object obj)
        {
            this.IsBusy = true;
            if (Validate())
            {
                //todo progressbar en login
                //boton reintentar login

                UserLoggedDto userLoggedDto = await userService.GetUserLoggedDto(this.UserName.Value, this.Password.Value);
                if (userLoggedDto.IsOk) // && userLoggedDto.UserLoggedModel.permission)
                {
                    this.UserLoggedModel = userLoggedDto.UserLoggedModel;
                    await NavigationService.NavigateToAsync("//MainView");
                }
                else
                {
                    if (!userLoggedDto.IsConnected)
                    {
                        await DialogService.ShowAlertAsync(Literales.exception_message_no_connection, "", Literales.btn_text_accept);
                    }
                    else if (userLoggedDto.UserLoggedModel == null)
                    {
                        await DialogService.ShowAlertAsync(Literales.exception_message_login, "", Literales.btn_text_accept);
                    }
                    else
                    {
                        await DialogService.ShowAlertAsync(Literales.no_permissions, "", Literales.btn_text_accept);
                    }
                }
            }
            this.IsBusy = false;
        }

        private async Task SignInAsync()
        {
            this.IsBusy = true;

            await Task.Delay(10);

            this.LoginUrl = identityService.CreateAuthorizationRequest();

            this.IsValid = true;
            this.IsLogin = true;
            this.IsBusy = false;
        }


        private void Logout()
        {
            settingsService.AuthAccessToken = "";

            //string authIdToken = settingsService.AuthIdToken;
            //string logoutRequest = identityService.CreateLogoutRequest(authIdToken);

            //if (!string.IsNullOrEmpty(logoutRequest))
            //{
            //    // Logout
            //    this.LoginUrl = logoutRequest;
            //}
        }

        private async Task NavigateAsync(string url)
        {
            string unescapedUrl = System.Net.WebUtility.UrlDecode(url);

            if (unescapedUrl.Equals(GlobalSetting.Instance.LogoutCallback))
            {
                settingsService.AuthAccessToken = string.Empty;
                settingsService.AuthIdToken = string.Empty;
                this.IsLogin = false;
                this.LoginUrl = identityService.CreateAuthorizationRequest();
            }
            else if (unescapedUrl.Contains(GlobalSetting.Instance.Callback))
            {
                AuthorizeResponse authResponse = new AuthorizeResponse(url);
                if (!string.IsNullOrWhiteSpace(authResponse.Code))
                {
                    Models.Token.UserToken userToken = await identityService.GetTokenAsync(authResponse.Code);
                    string accessToken = userToken.AccessToken;

                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        settingsService.AuthAccessToken = accessToken;
                        settingsService.AuthIdToken = authResponse.IdentityToken;
                        await NavigationService.NavigateToAsync("//Main/Catalog");
                    }
                }
            }
        }

        private bool Validate()
        {
            //todo buscar string
            bool isValidPassword = false;
            bool isValidUser = ValidateUserName();
            if (!isValidUser)
            {
                DialogService.ShowAlertAsync(_userName.MsgErrors, "", Literales.btn_text_accept);
            }
            else
            {
                isValidPassword = ValidatePassword();
                if (!isValidPassword)
                {
                    DialogService.ShowAlertAsync(_password.MsgErrors, "", Literales.btn_text_accept);
                }
            }
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
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Literales.username_empty_error });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = Literales.password_empty_error });
        }
    }
}
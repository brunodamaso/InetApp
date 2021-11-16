using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IdentityModel.Client;
using INetApp.APIWebServices.Dtos;
using INetApp.Extensions;
using INetApp.Models;
using INetApp.Services;
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

        private readonly ISettingsService settingsService;
        private readonly IIdentityService identityService;
        private readonly IRepositoryWebService repositoryWebService;

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
            settingsService = DependencyService.Get<ISettingsService>();
            identityService = DependencyService.Get<IIdentityService>();
            repositoryWebService = DependencyService.Get<IRepositoryWebService>();

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


                UserLoggedDto userLoggedDto = await repositoryWebService.GetUserLogged(this.UserName.Value, this.Password.Value);
                if (userLoggedDto.IsOk && userLoggedDto.UserLoggedModel.permission)
                {
                    this.UserLoggedModel = userLoggedDto.UserLoggedModel;
                    settingsService.AuthAccessToken = this.UserName.Value;
                    settingsService.NameInitial = this.UserLoggedModel.nameInitial + this.UserLoggedModel.lastNameInitial;
                    settingsService.NameUser = this.UserLoggedModel.fullName;
                    //Application.Current.MainPage = new AppShell();
                    await NavigationService.NavigateToAsync("//MainPage");
                }
                else
                {
                    if (!userLoggedDto.IsConnected)
                    {
                        //todo buscar string
                        await DialogService.ShowAlertAsync("Order sent successfully!", "Checkout", "Ok");
                        //todo no hya conexion
                    }
                    else if (userLoggedDto.UserLoggedModel == null)
                    {
                        //todo error usuario no existe
                    }
                    else
                    {
                        //todo error usuario no tiene permisos
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
            bool isValidUser = ValidateUserName();
            if (!isValidUser)
            {
                DialogService.ShowAlertAsync(_userName.MsgErrors, "Revise", "Ok");
            }

            bool isValidPassword = ValidatePassword();
            if (!isValidPassword)
            {
                DialogService.ShowAlertAsync(_password.MsgErrors, "Revise", "Ok");
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
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A username is required." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
        }
    }
}
using INetApp.APIWebServices.Dtos;
using INetApp.Extensions;
using INetApp.Models;
using INetApp.Resources;
using INetApp.Services;
using INetApp.Services.Identity;
using INetApp.Validations;
using INetApp.ViewModels.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private readonly IUserService userService;
        private UserLoggedModel _UserLoggedModel;

        #region Properties
        public ValidatableObject<string> UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
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
                RaisePropertyChanged(() => IsValid);
            }
        }

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                RaisePropertyChanged(() => IsLogin);
            }
        }

        public string LoginUrl
        {
            get => _authUrl;
            set
            {
                _authUrl = value;
                RaisePropertyChanged(() => LoginUrl);
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
            UserName.Value = credenciales.Key.ToString();
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
            IsBusy = true;
            if (Validate())
            {
                //todo progressbar en login
                //todo progressbar en category sale gris
                //boton reintentar login

                UserLoggedDto userLoggedDto = await userService.GetUserLoggedDto(UserName.Value, Password.Value);
                if (userLoggedDto.IsOk) // && userLoggedDto.UserLoggedModel.permission)
                {
                    UserLoggedModel = userLoggedDto.UserLoggedModel;
                    if (UserLoggedModel.version != settingsService.Version)
                    {

                    }
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
            IsBusy = false;
        }

        private void Logout()
        {
            settingsService.AuthAccessToken = "";
            settingsService.UserName = "";
            settingsService.NameFull = "";
            settingsService.NameInitial = "";
            settingsService.UserPass = "";            
        }

        private bool Validate()
        {
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
using System.Threading.Tasks;

namespace INetApp.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return App.Current.MainPage.DisplayAlert(title, message, buttonLabel);
        }
        public Task<bool> ShowAlertAsync(string message, string title, string buttonAccept, string ButtonCancel)
        {
            return App.Current.MainPage.DisplayAlert(title, message, buttonAccept, ButtonCancel);
        }
        public Task<string> ShowPromptAsync(string message, string title, string buttonAccept, string ButtonCancel = null)
        {
            return App.Current.MainPage.DisplayPromptAsync(title, message, buttonAccept, ButtonCancel);
        }
    }
}

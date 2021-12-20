using System.Threading.Tasks;

namespace INetApp.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<bool> ShowAlertAsync(string message, string title, string buttonAccept, string ButtonCancel);
        Task<string> ShowPromptAsync(string message, string title, string buttonAccept, string ButtonCancel);
    }
}

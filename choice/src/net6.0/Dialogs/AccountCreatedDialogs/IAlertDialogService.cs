using System;
using System.Threading.Tasks;

namespace Choice.Dialogs.AccountCreatedDialogs
{
    public interface IAlertDialogService
    {
        Task ShowDialogAsync(string title, string message, string buttonText, Action action);
    }
}

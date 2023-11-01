using System;
using System.Threading.Tasks;

namespace Choice.Dialogs
{
    public interface IAlertDialogService
    {
        Task ShowDialogAsync(string title, string message, string buttonText, Action action);
    }
}

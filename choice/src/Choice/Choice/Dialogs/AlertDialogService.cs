using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Choice.Dialogs
{
    public class AlertDialogService : IAlertDialogService
    {
        private TaskCompletionSource<bool> taskCompletionSource;
        private Task<bool> task;

        public async Task ShowDialogAsync(string title, string message, string buttonText)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            AccountCreatedDialog alertDialog = new AccountCreatedDialog(title, message, buttonText, Callback);
            await Application.Current.MainPage.Navigation.PushPopupAsync(alertDialog);
            await task;
        }

        private async Task Callback(bool result)
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync();
            if (!taskCompletionSource.Task.IsCanceled &&
                !taskCompletionSource.Task.IsCompleted &&
                !taskCompletionSource.Task.IsFaulted)
            {
                taskCompletionSource.SetResult(result);
            }
        }
    }
}

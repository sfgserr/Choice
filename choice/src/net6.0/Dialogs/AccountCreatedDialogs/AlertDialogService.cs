using Mopups.Services;

namespace Choice.Dialogs.AccountCreatedDialogs
{
    public class AlertDialogService : IAlertDialogService
    {
        private TaskCompletionSource<bool> taskCompletionSource;
        private Task<bool> task;

        public async Task ShowDialogAsync(string title, string message, string buttonText, Action action)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            AccountCreatedDialog alertDialog = new AccountCreatedDialog(title, message, buttonText, async r =>
            {
                await Callback(r);
                action();
            });
            await MopupService.Instance.PushAsync(alertDialog);
            await task;
        }

        private async Task Callback(bool result)
        {
            await MopupService.Instance.PopAsync();
            if (!taskCompletionSource.Task.IsCanceled &&
                !taskCompletionSource.Task.IsCompleted &&
                !taskCompletionSource.Task.IsFaulted)
            {
                taskCompletionSource.SetResult(result);
            }
        }
    }
}

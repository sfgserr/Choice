using Mopups.Services;

namespace Choice.Dialogs.LinkSocialMediaDialogs
{
    public class LinkSocialMediaDialogService : ILinkSocialMediaDialogService
    {
        private TaskCompletionSource<bool> taskCompletionSource;
        private Task<bool> task;

        public async Task ShowDialogAsync(string text, Action<string> action)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            LinkSocialMediaDialog alertDialog = new LinkSocialMediaDialog(text, async (r, uri) =>
            {
                await Callback(r);
                action(uri);
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

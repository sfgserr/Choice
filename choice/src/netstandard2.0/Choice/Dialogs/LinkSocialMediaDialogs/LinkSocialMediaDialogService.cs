
using Choice.Dialogs.AccountCreatedDialogs;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

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

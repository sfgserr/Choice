using Choice.ViewModels;
using Mopups.Services;

namespace Choice.Dialogs.CategoriesDialogs
{
    public class CategoriesDialogService : ICategoriesDialogService
    {
        private TaskCompletionSource<bool> taskCompletionSource;
        private Task<bool> task;

        public async Task ShowDialog(Func<CategoryDialogViewModel, int> select, List<CategoryDialogViewModel> categories, int count)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            CategoriesDialog dialog = new CategoriesDialog(Callback, select, categories, count);

            await MopupService.Instance.PushAsync(dialog);
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

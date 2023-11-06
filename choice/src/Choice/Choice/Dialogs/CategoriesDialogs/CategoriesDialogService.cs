using Choice.Domain.Models;
using Choice.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Choice.Dialogs.CategoriesDialogs
{
    public class CategoriesDialogService : ICategoriesDialogService
    {
        private TaskCompletionSource<bool> taskCompletionSource;
        private Task<bool> task;

        public async Task ShowDialog(Action<CategoryViewModel> select, List<CategoryViewModel> categories)
        {
            taskCompletionSource = new TaskCompletionSource<bool>();
            task = taskCompletionSource.Task;

            CategoriesDialog dialog = new CategoriesDialog(Callback, select, categories);

            await Application.Current.MainPage.Navigation.PushPopupAsync(dialog);
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

using Choice.Domain.Models;
using Choice.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Dialogs.CategoriesDialogs
{
    public interface ICategoriesDialogService
    {
        Task ShowDialog(Func<CategoryViewModel, int> select, List<CategoryViewModel> categories, int count);
    }
}

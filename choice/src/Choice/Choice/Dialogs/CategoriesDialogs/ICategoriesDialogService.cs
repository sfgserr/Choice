using Choice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Dialogs.CategoriesDialogs
{
    public interface ICategoriesDialogService
    {
        Task ShowDialog(Action<Category> select, List<Category> categories);
    }
}

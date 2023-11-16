﻿using Choice.Domain.Models;
using Choice.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Choice.Dialogs.CategoriesDialogs
{
    public interface ICategoriesDialogService
    {
        Task ShowDialog(Func<CategoryDialogViewModel, int> select, List<CategoryDialogViewModel> categories, int count);
    }
}

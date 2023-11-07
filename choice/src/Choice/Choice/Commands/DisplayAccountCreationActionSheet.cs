using Choice.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Choice.Commands
{
    public class DisplayAccountCreationActionSheet : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            string action = await Application.Current.MainPage.DisplayActionSheet("Создать аккаунт", "Отмена", 
                                                                                  null, "Создать аккаунт клиента",
                                                                                  "Создать аккаунт компании");
            switch (action)
            {
                case "Создать аккаунт клиента":
                    await Shell.Current.GoToAsync(nameof(RegisterClientPage));
                    break;
                case "Создать аккаунт компании":
                    await Shell.Current.GoToAsync(nameof(RegisterCompanyPage));
                    break;
                case "Отмена":
                    return;
            }
        }
    }
}

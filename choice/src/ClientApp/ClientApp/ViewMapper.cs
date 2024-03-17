using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ClientApp.ViewModels;
using ClientApp.Views;
using System;

namespace ClientApp
{
    public class ViewMapper : IDataTemplate
    {
        private readonly LoginView _login;

        public ViewMapper(LoginView login)
        {
            _login = login;
        }

        public Control? Build(object? param)
        {
            string viewName = param!.GetType().Name.Replace("ViewModel", "View");

            return viewName switch
            {
                "LoginView" => _login,
                _ => new TextBlock() { Text = $"Not Found Control {viewName}" }
            };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}

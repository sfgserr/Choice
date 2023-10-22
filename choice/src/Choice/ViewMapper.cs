using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Choice.ViewModels;
using Choice.Views;

namespace Choice;

public class ViewMapper : IDataTemplate
{
    private readonly LoginView _login;

    public ViewMapper(LoginView login)
    {
        _login = login;
    }

    public Control? Build(object param)
    {
        string viewName = param.GetType().FullName!.Replace("ViewModel", "View");
        Type type = Type.GetType(viewName);

        if (type != null)
        {
            return type.Name switch
            {
                "LoginView" => (Control)_login
            };
        }

        return new TextBlock { Text = $"NotFound: {viewName}" };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
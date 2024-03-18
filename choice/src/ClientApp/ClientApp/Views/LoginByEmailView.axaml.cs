using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ClientApp.ViewModels;

namespace ClientApp.Views;

public partial class LoginByEmailView : UserControl
{
    public LoginByEmailView(LoginByEmailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
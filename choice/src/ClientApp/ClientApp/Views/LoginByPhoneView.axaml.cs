using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ClientApp.ViewModels;

namespace ClientApp.Views;

public partial class LoginByPhoneView : UserControl
{
    public LoginByPhoneView(LoginByPhoneViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
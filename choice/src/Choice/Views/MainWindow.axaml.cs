using Avalonia.Controls;
using Choice.ViewModels;

namespace Choice.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
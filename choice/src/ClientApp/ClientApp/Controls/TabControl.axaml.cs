using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ClientApp.Controls;

public partial class TabControl : UserControl
{
    public static readonly StyledProperty<object?> EmailTabViewProperty = 
        UserControl.ContentProperty.AddOwner<TabControl>();

    public static readonly StyledProperty<object?> PhoneTabViewProperty =
       UserControl.ContentProperty.AddOwner<TabControl>();

    public TabControl()
    {
        InitializeComponent();
        DataContext = this;

        emailButton.Click += EmailButton_Click;
        phoneButton.Click += PhoneButton_Click;
    }

    public object? EmailTabView
    {
        get => GetValue(EmailTabViewProperty);
        set => SetValue(EmailTabViewProperty, value);
    }

    public object? PhoneTabView
    {
        get => GetValue(PhoneTabViewProperty);
        set => SetValue(PhoneTabViewProperty, value);
    }

    private void EmailButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //((Control)EmailTabView!).IsVisible = true;
        //((Control)PhoneTabView!).IsVisible = false;

        emailButton.BorderBrush = Brush.Parse("#3F8AE0");
        phoneButton.BorderBrush = Brush.Parse("Transparent");
    }

    private void PhoneButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //((Control)EmailTabView!).IsVisible = false;
        //(Control)PhoneTabView!).IsVisible = true;

        emailButton.BorderBrush = Brush.Parse("Transparent");
        phoneButton.BorderBrush = Brush.Parse("#3F8AE0");
    }
}
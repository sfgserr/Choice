using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using ClientApp.ViewModels;
using System;

namespace ClientApp.Controls;

public partial class TabControl : UserControl
{
    public static readonly StyledProperty<object?> EmailTabProperty = 
        ContentControl.ContentProperty.AddOwner<TabControl>();

    public static readonly StyledProperty<object?> PhoneTabProperty =
       ContentControl.ContentProperty.AddOwner<TabControl>();

    public TabControl()
    {
        InitializeComponent();
        DataContext = this;

        emailButton.Click += EmailButton_Click;
        phoneButton.Click += PhoneButton_Click;

        CurrentTab = EmailTab!;
    }

    private object _currentTab;

    public object CurrentTab
    {
        get => _currentTab;
        set
        {
            _currentTab = value;
        }
    }

    public object? EmailTab
    {
        get => GetValue(EmailTabProperty);
        set => SetValue(EmailTabProperty, value);
    }

    public object? PhoneTab
    {
        get => GetValue(PhoneTabProperty);
        set => SetValue(PhoneTabProperty, value);
    }

    private void EmailButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        emailButton.BorderBrush = Brush.Parse("#3F8AE0");
        phoneButton.BorderBrush = Brush.Parse("Transparent");

        CurrentTab = EmailTab!;
    }

    private void PhoneButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        emailButton.BorderBrush = Brush.Parse("Transparent");
        phoneButton.BorderBrush = Brush.Parse("#3F8AE0");
    }
}
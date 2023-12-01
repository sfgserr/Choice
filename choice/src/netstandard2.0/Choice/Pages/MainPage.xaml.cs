using Choice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace Choice.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<MainViewModel>();
        }
    }
}

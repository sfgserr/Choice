using Choice.Pages;
using Choice.Stores.Authenticators;
using Choice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;

namespace Choice
{
    public partial class App : Application
    {
        public App()
        {
            ServicesContainer.SetUpContainer();

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}

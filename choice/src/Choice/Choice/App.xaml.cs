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

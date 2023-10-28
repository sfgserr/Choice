using Choice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCompanyPage : ContentPage
    {
        public RegisterCompanyPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<RegisterCompanyViewModel>();
        }
    }
}
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
    public partial class CompanyCardPage : ContentPage
    {
        public CompanyCardPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<CompanyCardViewModel>();
        }
    }
}
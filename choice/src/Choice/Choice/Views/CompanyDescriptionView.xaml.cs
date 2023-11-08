using Choice.Domain.Models;
using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyDescriptionView : ContentView
    {
        public CompanyDescriptionView()
        {
            InitializeComponent();
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CompanyDescriptionViewModel viewModel = (CompanyDescriptionViewModel)BindingContext;

            if (unavailable.IsChecked == true)
                viewModel.Input.PrepaymentAvailability = PrepaymentAvailability.Without;
            else
                viewModel.Input.PrepaymentAvailability = PrepaymentAvailability.With;
        }
    }
}
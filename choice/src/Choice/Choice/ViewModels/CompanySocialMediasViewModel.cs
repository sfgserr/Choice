using Choice.Services.AuthenticationServices;
using Choice.Stores.IndexStores;

namespace Choice.ViewModels
{
    public class CompanySocialMediasViewModel : ViewModelBase
    {
        private RegisterCompanyInput _input;

        private readonly IIndexStore _indexStore;

        public CompanySocialMediasViewModel(RegisterCompanyInput input, IIndexStore indexStore)
        {
            _input = input;
            _indexStore = indexStore;
        }


    }
}

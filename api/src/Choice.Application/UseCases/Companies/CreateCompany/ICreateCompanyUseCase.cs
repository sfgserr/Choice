using Choice.Domain.Models;

namespace Choice.Application.UseCases.Companies.CreateCompany
{
    public interface ICreateCompanyUseCase
    {
        Task Execute(string email, string password, string title, string phoneNumber, string address, string siteUri, List<SocialMedia> socialMedias, List<string> photoUris, PrepaymentAvailability prepaymentAvailability);

        void SetOutputPort(IOutputPort outputPort);
    }
}

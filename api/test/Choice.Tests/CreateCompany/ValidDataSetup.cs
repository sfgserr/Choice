using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.CreateCompany
{
    internal class ValidDataSetup : TheoryData<string, string, string, string, string, string, 
                                    List<SocialMedia>, List<string>, List<Category>, PrepaymentAvailability> 
    {
        public ValidDataSetup() 
        {
            Add("choice@gmail.com", "123456", "Apple", "+7232192", "Moscow, Russia", "https://apple.com",
                new List<SocialMedia>(), new List<string>() { "/photoUri" }, new List<Category>() { new Category() }, PrepaymentAvailability.With);
        }
    }
}

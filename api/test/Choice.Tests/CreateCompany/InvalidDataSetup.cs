using Choice.Domain.Models;
using Xunit;

namespace Choice.UnitTests.CreateCompany
{
    internal sealed class InvalidDataSetup : TheoryData<string, string, string, string, string, string,
                                    List<SocialMedia>, List<string>, List<Category>, PrepaymentAvailability>
    {
        public InvalidDataSetup()
        {
            Add("", "", "", "", ", ", "", new List<SocialMedia>(), new List<string>(), new List<Category>(),
                PrepaymentAvailability.With);
        }
    }
}

using Choice.ClientService.Application.Services;
using Choice.Common.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Choice.ClientService.Infrastructure.Authentication
{
    public class CompanyService : ICompanyService
    {
        private readonly IHttpContextAccessor _context;

        public CompanyService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Address GetAddress()
        {
            string[] address = _context.HttpContext.User.FindFirst("address")?.Value.Split(',')!;

            return new(address[0], address[1]);
        }
    }
}

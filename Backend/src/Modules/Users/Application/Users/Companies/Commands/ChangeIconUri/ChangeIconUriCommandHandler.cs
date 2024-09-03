using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.ChangeIconUri
{
    internal class ChangeIconUriCommandHandler : ICommandHandler<ChangeIconUriCommand>
    {
        private readonly ICompanyRepository _repository;
        private readonly IUserContext _userContext;
        
        internal ChangeIconUriCommandHandler(ICompanyRepository repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task Execute(ChangeIconUriCommand command)
        {
            var company = await _repository.Get(new (_userContext.Id.Value));

            company.ChangeIconUri(command.IconUri);
        }
    }
}
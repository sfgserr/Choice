using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.ChangeData
{
    internal class ChangeDataCommandHandler : ICommandHandler<ChangeDataCommand>
    {
        private readonly ICompanyRepository _repository;
        private readonly IUserContext _userContext;
        private readonly IUsersCounter _usersCounter;

        internal ChangeDataCommandHandler(
            ICompanyRepository repository, 
            IUserContext userContext, 
            IUsersCounter usersCounter)
        {
            _repository = repository;
            _userContext = userContext;
            _usersCounter = usersCounter;
        }

        public async Task Execute(ChangeDataCommand command)
        {
            var company = await _repository.Get(new(_userContext.Id.Value));
            
            company.ChangeData(
                command.Name,
                command.Email,
                command.PhoneNumber,
                command.Address,
                _usersCounter,
                command.Description,
                command.Categories,
                command.PhotoUris,
                command.SocialMediaUris,
                command.IsPrepaymentAvailable);
        }
    }
}
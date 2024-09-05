using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Users;
using Users.Domain.Users.Companies;

namespace Users.Application.Users.Companies.Commands.FillData
{
    internal class FillDataCommandHandler : ICommandHandler<FillDataCommand>
    {
        private readonly ICompanyRepository _repository;
        private readonly IUserContext _usersContext;

        internal FillDataCommandHandler(ICompanyRepository repository, IUserContext usersContext)
        {
            _repository = repository;
            _usersContext = usersContext;
        }

        public async Task Execute(FillDataCommand command)
        {
            var company = await _repository.Get(new(_usersContext.Id.Value));
            
            company.FillData(
                command.Description,
                command.CategoryIds,
                command.PhotoUris,
                command.SocialMediaUris,
                command.IsPrepaymentAvailable);
        }
    }
}
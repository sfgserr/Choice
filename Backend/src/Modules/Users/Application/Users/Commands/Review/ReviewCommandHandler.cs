using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Users;

namespace Users.Application.Users.Commands.Review
{
    internal class ReviewCommandHandler : ICommandHandler<ReviewCommand>
    {
        private readonly IUserRepository _repository;

        internal ReviewCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(ReviewCommand command)
        {
            var user = await _repository.Get(new(command.ToUserId));
            
            user.Review(command.Grade);
        }
    }
}
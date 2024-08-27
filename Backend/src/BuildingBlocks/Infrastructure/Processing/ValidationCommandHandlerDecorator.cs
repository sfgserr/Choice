using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using FluentValidation;

namespace BuildingBlocks.Infrastructure.Processing
{
    public class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly ICommandHandler<T> _decorated;

        public ValidationCommandHandlerDecorator(IList<IValidator<T>> validators, ICommandHandler<T> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public async Task Execute(T command)
        {
            var errors = _validators
                .SelectMany(v => v.Validate(command).Errors)
                .Where(e => e != null)
                .ToList();

            if (errors.Count > 0)
            {
                throw new InvalidCommandException(errors.Select(e => e.ErrorMessage).ToList());
            }

            await _decorated.Execute(command);
        }
    }
}

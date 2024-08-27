using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using FluentValidation;

namespace BuildingBlocks.Infrastructure.Processing
{
    public class ValidationCommandHandlerWithResultDecorator<T, TResult> : ICommandHandlerWithResult<T, TResult> where T : ICommandWithResult<TResult>
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly ICommandHandlerWithResult<T, TResult> _decorated;

        public ValidationCommandHandlerWithResultDecorator(
            IList<IValidator<T>> validators, 
            ICommandHandlerWithResult<T, TResult> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public async Task<TResult> Execute(T command)
        {
            var errors = _validators
                .SelectMany(v => v.Validate(command).Errors)
                .Where(e => e != null)
                .ToList();

            if (errors.Count > 0)
            {
                throw new InvalidCommandException(errors.Select(e => e.ErrorMessage).ToList());
            }

            return await _decorated.Execute(command);
        }
    }
}

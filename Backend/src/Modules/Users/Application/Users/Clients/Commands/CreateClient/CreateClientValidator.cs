using Application.Users.Clients.Commands.Create;
using FluentValidation;

namespace Application.Users.Clients.Commands.CreateClient
{
    internal class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");

            RuleFor(c => c.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");

            RuleFor(c => c.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");

            RuleFor(c => c.Street)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");
        }
    }
}

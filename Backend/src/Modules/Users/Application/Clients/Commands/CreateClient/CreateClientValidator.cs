using FluentValidation;

namespace Users.Application.Clients.Commands.CreateClient
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
                .WithMessage("Name must be provided")
                .MinimumLength(8)
                .MaximumLength(16)
                .WithMessage("Password length should in range from 8 to 16");

            RuleFor(c => c.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");

            RuleFor(c => c.Street)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");

            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email must be provided")
                .EmailAddress()
                .WithMessage("Invalid email");
        }
    }
}

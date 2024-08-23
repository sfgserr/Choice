using FluentValidation;

namespace Application.Users.Clients.Commands.ChangeData
{
    internal class ChangeDataValidator : AbstractValidator<ChangeDataCommand>
    {
        public ChangeDataValidator()
        {
            RuleFor(c => c.Name)
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

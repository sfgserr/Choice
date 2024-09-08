using FluentValidation;

namespace Users.Application.Companies.Commands.CreateCompany
{
    internal class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
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
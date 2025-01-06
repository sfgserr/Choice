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
                .WithMessage("Имя обязательное поле");

            RuleFor(c => c.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Пароль обязательное поле");

            RuleFor(c => c.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Город обязательное поле");

            RuleFor(c => c.Street)
                .NotEmpty()
                .NotNull()
                .WithMessage("Улица обязательное поле");
        }
    }
}
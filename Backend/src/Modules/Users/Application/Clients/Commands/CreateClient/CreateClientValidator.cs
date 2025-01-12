using FluentValidation;
using BuildingBlocks.Application.Extensions;

namespace Users.Application.Clients.Commands.CreateClient
{
    internal class CreateClientValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Имя обязательное поле");

            RuleFor(c => c.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Пароль обязательное поле")
                .MinimumLength(8)
                .MaximumLength(16)
                .WithMessage("Мин. длина 8. М16акс. длина ");

            RuleFor(c => c.City)
                .NotEmpty()
                .NotNull()
                .WithMessage("Город обязательное поле");

            RuleFor(c => c.Street)
                .NotEmpty()
                .NotNull()
                .WithMessage("Улица обязательное поле");

            RuleFor(c => c.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("E-mail обязательное поле")
                .EmailAddress()
                .WithMessage("Неправильный формат e-mail'а");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Номер телефона обязательное поле")
                .PhoneNumber();
        }
    }
}

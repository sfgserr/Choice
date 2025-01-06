using BuildingBlocks.Application.Extensions;
using FluentValidation;

namespace Users.Application.Clients.Commands.ChangeData
{
    internal class ChangeDataValidator : AbstractValidator<ChangeDataCommand>
    {
        public ChangeDataValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Имя обязательное поле");

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

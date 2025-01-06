using BuildingBlocks.Application.Extensions;
using FluentValidation;

namespace Users.Application.Companies.Commands.ChangeData
{
    internal class ChangeDataValidator : AbstractValidator<ChangeDataCommand>
    {
        public ChangeDataValidator()
        {
            RuleFor(x => x.Categories)
                .NotEmpty()
                .NotNull()
                .WithMessage("Выберите хотя бы одну категорию");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Имя обязательное поле");
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Номер телефона обязательное поле");
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Описание обязательное поле");
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("E-mail обязательное поле");
            
            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Номер телефона обязательное поле")
                .PhoneNumber();
        }
    }
}
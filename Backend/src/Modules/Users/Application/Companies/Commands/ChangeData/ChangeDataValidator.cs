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
                .WithMessage("Company must have at least 1 category");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .WithMessage("Phone number must be provided");
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Description must be provided");
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name must be provided");
        }
    }
}
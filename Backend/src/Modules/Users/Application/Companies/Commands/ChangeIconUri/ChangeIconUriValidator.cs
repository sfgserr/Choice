using FluentValidation;

namespace Users.Application.Companies.Commands.ChangeIconUri
{
    internal class ChangeIconUriValidator : AbstractValidator<ChangeIconUriCommand>
    {
        public ChangeIconUriValidator()
        {
            RuleFor(x => x.IconUri)
                .NotEmpty()
                .NotNull()
                .WithMessage("Нельзя установить пустой uri");
        }
    }
}
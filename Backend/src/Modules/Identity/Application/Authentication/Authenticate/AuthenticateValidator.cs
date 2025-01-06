using FluentValidation;

namespace Identity.Application.Authentication.Authenticate
{
    internal class AuthenticateValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateValidator()
        {
            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("E-mail обязательное поле");
            
            RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Пароль обязательное поле");
        }
    }
}
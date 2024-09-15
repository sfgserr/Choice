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
                .WithMessage("Email must be provided");
            
            RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password must be provided");
        }
    }
}
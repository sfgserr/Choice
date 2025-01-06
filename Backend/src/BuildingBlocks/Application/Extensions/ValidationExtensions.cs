using System.Text.RegularExpressions;
using BuildingBlocks.Application.Cqrs.Commands;
using FluentValidation;

namespace BuildingBlocks.Application.Extensions
{
    public static class ValidationExceptions
    {
        private const string PhoneNumberRegex = "^[0-9]{10}$";

        public static IRuleBuilderOptionsConditions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> builder) where T : ICommand
        {
            return builder.Custom((c, context) =>
            {
                if (Regex.Match(c, PhoneNumberRegex).Success)
                {
                    context.AddFailure("Неправильный формат телефона");
                }
            });
        }
    }
}
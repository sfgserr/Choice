using System;

namespace Choice.Exceptions
{
    public class PasswordDoesNotEqualToConfirmtionException : Exception
    {
        public override string Message => "Пароли не совпадают";
    }
}

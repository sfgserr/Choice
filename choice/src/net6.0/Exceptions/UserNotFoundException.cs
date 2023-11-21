using System;

namespace Choice.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public override string Message => "Логин или пароль неверны";
    }
}

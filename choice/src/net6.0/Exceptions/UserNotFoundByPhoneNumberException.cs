using System;

namespace Choice.Exceptions
{
    public class UserNotFoundByPhoneNumberException : Exception
    {
        public override string Message => "Нет пользователя с таким номером телефона";
    }
}

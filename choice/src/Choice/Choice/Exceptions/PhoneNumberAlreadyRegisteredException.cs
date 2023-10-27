using System;

namespace Choice.Exceptions
{
    public class PhoneNumberAlreadyRegisteredException : Exception
    {
        public override string Message => "Этот номер телефона уже зарегестрирован";
    }
}

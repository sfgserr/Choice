using System;

namespace Choice.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public override string Message => "Этот e-mail уже зарегестрирован";
    }
}

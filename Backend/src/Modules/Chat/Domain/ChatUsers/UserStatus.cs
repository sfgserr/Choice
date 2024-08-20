using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers
{
    public class UserStatus : ValueObject
    {
        private UserStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static UserStatus Online { get; } = new("Online");

        public static UserStatus Offline { get; } = new("Offline");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

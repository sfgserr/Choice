namespace Identity.Application.Authentication.Phone
{
    public class Code
    {
        private Code(DateTime expirationDate, string value, Guid userId)
        {
            ExpirationDate = expirationDate;
            Value = value;
            UserId = userId;
        }

        public static Code Generate(Guid userId)
        {
            var random = new Random();
            var value = random.Next(1000, 9999).ToString();

            var code = new Code(DateTime.UtcNow.AddMinutes(1), value, userId);
            
            return code;
        }
        
        public DateTime ExpirationDate { get; }
        
        public string Value { get; }

        public Guid UserId { get; }
    }
}
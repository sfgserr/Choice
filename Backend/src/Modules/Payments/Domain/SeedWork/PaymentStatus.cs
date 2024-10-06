using BuildingBlocks.Domain;

namespace Payments.Domain.SeedWork
{
    public class PaymentStatus : ValueObject
    {
        private PaymentStatus(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static PaymentStatus WaitingForPayment { get; } = new("WaitingForPayment");

        public static PaymentStatus Expired { get; } = new("Expired");

        public static PaymentStatus Paid { get; } = new("Paid");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static PaymentStatus Parse(string s) => s switch
        {
            "WaitingForPayment" => WaitingForPayment,
            "Expired" => Expired,
            "Paid" => Paid,
            _ => throw new ArgumentException("No such status")
        };
    }
}

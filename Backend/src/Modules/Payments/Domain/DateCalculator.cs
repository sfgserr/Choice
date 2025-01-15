namespace Payments.Domain
{
    internal class DateCalculator
    {
        internal static DateTime CalculateExpirationDateForSubscription(string period)
        {
            return period switch
            {
                "Month" => DateTime.UtcNow.AddMonths(1),
                "HalfYear" => DateTime.UtcNow.AddMonths(6),
                "Year" => DateTime.UtcNow.AddYears(1),
                _ => throw new ArgumentException("No such period")
            };
        }

        internal static DateTime CalculateExpirationDateForPayment()
        {
            return DateTime.UtcNow.AddMinutes(10);
        }
    }
}

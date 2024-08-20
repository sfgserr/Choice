namespace Payments.Domain
{
    internal class DateCalculator
    {
        internal static DateTime CalculateExpirationDateForSubscription(string period)
        {
            return period switch
            {
                "Month" => DateTime.Now.AddMonths(1),
                "HalfYear" => DateTime.Now.AddMonths(6),
                "Year" => DateTime.Now.AddYears(1),
                _ => throw new ArgumentException("No such period")
            };
        }

        internal static DateTime CalculateExpirationDateForPayment()
        {
            return DateTime.Now.AddMinutes(10);
        }
    }
}

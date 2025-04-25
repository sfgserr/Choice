namespace Payments.Application.Payments.CreatePayment
{
    public class CreatePaymentResult
    {
        public CreatePaymentResult(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}
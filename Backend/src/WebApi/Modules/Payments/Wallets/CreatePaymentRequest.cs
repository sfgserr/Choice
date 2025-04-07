namespace WebApi.Modules.Payments.Wallets
{
    public class CreatePaymentRequest
    {
        public CreatePaymentRequest(int copecks)
        {
            Copecks = copecks;
        }

        public int Copecks { get; }
    }
}
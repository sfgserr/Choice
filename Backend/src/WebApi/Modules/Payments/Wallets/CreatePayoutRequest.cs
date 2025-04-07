namespace WebApi.Modules.Payments.Wallets
{
    public class CreatePayoutRequest
    {
        public CreatePayoutRequest(int copecks, string bankCardNumber)
        {
            Copecks = copecks;
            BankCardNumber = bankCardNumber;
        }

        public int Copecks { get; }
        
        public string BankCardNumber { get; }
    }
}
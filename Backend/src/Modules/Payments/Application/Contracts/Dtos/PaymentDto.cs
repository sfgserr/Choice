namespace Payments.Application.Contracts.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        
        public AmountDto Amount { get; set; }
        
        public ConfirmationDto Confirmation { get; set; }
        
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class PayoutDto
    {
        public Guid Id { get; set; }
        
        public AmountDto Amount { get; set; }

        public string Status { get; set; }

        public Dictionary<string, string> Metadata { get; set; }
    }
    
    public class AmountDto
    {
        public double Value { get; set; }
        
        public string Currency { get; set; }
    }

    public class ConfirmationDto
    {
        public string Type { get; set; }

        public string ConfirmationUrl { get; set; }
    }
}
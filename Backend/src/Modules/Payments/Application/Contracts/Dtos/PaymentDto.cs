using System.Text.Json.Serialization;

namespace Payments.Application.Contracts.Dtos
{
    public class PaymentDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("amount")]
        public AmountDto Amount { get; set; }
        
        [JsonPropertyName("confirmation")]
        public ConfirmationDto Confirmation { get; set; }
        
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }

    public class PayoutDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("amount")]
        public AmountDto Amount { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }
    }
    
    public class AmountDto
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }
        
        [JsonPropertyName("currency")]
        
        public string Currency { get; set; }
    }

    public class ConfirmationDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("confirmation_url")]
        public string ConfirmationUrl { get; set; }
    }
}
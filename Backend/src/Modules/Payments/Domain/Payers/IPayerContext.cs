
namespace Payments.Domain.Payers
{
    public interface IPayerContext
    {
        PayerId Id { get; }
        
        bool Subscribed { get; }
    }
}

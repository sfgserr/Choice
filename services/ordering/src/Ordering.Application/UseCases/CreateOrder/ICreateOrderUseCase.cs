
namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task Execute(string receiverId, int orderRequestId, double price, double prepayment, int deadline,
            DateTime enrollmentTime);

        void SetOutputPort(IOutputPort outputPort);
    }
}

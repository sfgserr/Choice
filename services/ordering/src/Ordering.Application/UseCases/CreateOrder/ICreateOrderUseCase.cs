
namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task Execute(string receiverId, int orderRequestId, int price, int prepayment, int deadline,
            DateTime enrollmentDate);

        void SetOutputPort(IOutputPort outputPort);
    }
}


namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public interface ISendOrderRequestUseCase
    {
        Task Execute(string description, List<int> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate);

        void SetOutputPort(IOutputPort outputPort);
    }
}

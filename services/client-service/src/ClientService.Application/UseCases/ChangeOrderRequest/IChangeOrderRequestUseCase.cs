
namespace Choice.ClientService.Application.UseCases.ChangeOrderRequest
{
    public interface IChangeOrderRequestUseCase
    {
        Task Execute(int requestId, string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate);

        void SetOutputPort(IOutputPort outputPort);
    }
}


namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public interface ISendOrderRequestUseCase
    {
        Task Execute(string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate);

        void SetOutputPort(IOutputPort outputPort);
    }
}

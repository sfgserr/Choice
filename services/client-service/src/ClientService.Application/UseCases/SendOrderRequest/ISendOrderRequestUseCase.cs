﻿
namespace Choice.ClientService.Application.UseCases.SendOrderRequest
{
    public interface ISendOrderRequestUseCase
    {
        Task Execute(int clientId, string description, List<string> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate);

        void SetOutputPort(IOutputPort outputPort);
    }
}

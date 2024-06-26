﻿using Choice.ClientService.Domain.OrderRequests;

namespace Choice.ClientService.Application.UseCases.GetRequest
{
    public interface IOutputPort
    {
        void Ok(OrderRequest request, bool isUserCompany);

        void NotFound();
    }
}

﻿using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.FinishOrder
{
    public interface IOutputPort
    {
        void Ok(Order order, string receiverId);

        void NotFound();

        void Invalid();
    }
}

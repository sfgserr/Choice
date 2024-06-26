﻿using Choice.Ordering.Domain.OrderEntity;

namespace Choice.Ordering.Application.UseCases.CreateOrder
{
    public interface IOutputPort
    {
        void Ok(Order order);

        void Invalid();
    }
}

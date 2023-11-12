﻿using Choice.Domain;
using Choice.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice.Infrastructure.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ChoiceContext _context;

        public OrderRepository(ChoiceContext context)
        {
            _context = context;
        }

        public async Task<Order> Create(Order entity)
        {
            await _context.Orders.AddAsync(entity);

            return entity;
        }

        public async Task Delete(Order entity)
        {
            await _context
                .Database
                .ExecuteSqlRawAsync($"DELETE FROM Orders WHERE Id={entity.Id}");
        }

        public async Task<IList<Order>> Get()
        {
            return await _context.Orders
                                 .Include(e => e.Categories)
                                 .ToListAsync();
        }

        public async Task<Order> GetBy(Func<Order, bool> func)
        {
            List<Order> orders = await _context.Orders.ToListAsync();

            return orders.FirstOrDefault(c => func(c));
        }

        public async Task<Order> Update(Order entity)
        {
            await Task.Run(() =>
            {
                _context.Orders.Update(entity);
            });

            return entity;
        }
    }
}

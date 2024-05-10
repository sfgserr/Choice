using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using Dapper;
using Npgsql;

namespace Choice.Chat.Api.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Add(Order order)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            await connection.ExecuteAsync(
                @"INSERT INTO Orders (OrderId, Prepayment, Deadline, Price, SenderId, GroupId, 
                                        CreationTime, EnrollmentTime, IsEnrolled, DateChanged, Status) 
                VALUES (@OrderId, @Prepayment, @Deadline, @Price, @SenderId, @GroupId, 
                        @CreationTime, @EnrollmentDate, @IsEnrolled, @DateChanged, @Status)",
                new
                {
                    order.OrderId,
                    order.Prepayment,
                    order.Deadline,
                    order.Price,
                    order.SenderId,
                    order.GroupId,
                    order.CreationTime,
                    order.EnrollmentDate,
                    order.IsEnrolled,
                    order.Status,
                    order.DateChanged
                });

        }

        public async Task<Order> Get(int id)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            return await connection.QueryFirstAsync
                    ("SELECT * FROM Orders WHERE OrderId = @Id",
                        new { Id = id });
        }

        public async Task<IList<Order>> GetAll(int groupId)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            var messages = await connection.QueryAsync<Order>("SELECT * FROM Orders");

            return messages.Where(m => m.GroupId == groupId).ToList();
        }

        public async Task<bool> Update(Order order)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            int affections = await connection.ExecuteAsync(
                @"UPDATE Orders 
                    SET Prepayment = @Prepayment, Deadline = @Deadline, Price = @Price, 
                    SenderId = @SenderId, GroupId = @GroupId, CreationTime = @CreationTime, 
                    EnrollmentDate = @EnrollmentDate, IsEnrolled = @IsEnrolled, Status = @Status,
                    DateChanged = @DateChanged
                    WHERE OrderId = @OrderId",
                new
                {
                    order.OrderId,
                    order.Prepayment,
                    order.Deadline,
                    order.Price,
                    order.SenderId,
                    order.GroupId,
                    order.CreationTime,
                    order.EnrollmentDate,
                    order.IsEnrolled,
                    order.Status,
                    order.DateChanged
                });

            return affections > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);
            int affections = await connection.ExecuteAsync("DELETE FROM Orders WHERE Id = @Id", new { Id = id });

            return affections > 0;
        }
    }
}

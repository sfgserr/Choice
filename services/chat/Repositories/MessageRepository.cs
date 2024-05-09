using Choice.Chat.Api.Entities;
using Choice.Chat.Api.Repositories.Interfaces;
using Dapper;
using Npgsql;

namespace Choice.Chat.Api.Repositories
{
    public sealed class MessageRepository : IRepository<Message>
    {
        private readonly IConfiguration _configuration;

        public MessageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Add(Message message)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            await connection.ExecuteAsync
                ("INSERT INTO Messages (Text, SenderId, ReceiverId) VALUES (@Text, @SenderId, @ReceiverId)",
                    new { message.Text, message.SenderId, message.ReceiverId });
        }

        public async Task<Message> Get(int id)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            return await connection.QueryFirstAsync
                    ("SELECT * FROM Messages WHERE Id = @Id",
                        new { Id = id });
        }

        public async Task<IList<Message>> GetAll(string senderId, string receiverId)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            var messages = await connection.QueryAsync<Message>("SELECT * FROM Messages");

            return messages.Where(m => (m.SenderId == senderId || m.SenderId == receiverId) &&
                                 (m.ReceiverId == senderId || m.ReceiverId == receiverId)).ToList();
        }

        public async Task<bool> Update(Message message)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);

            int affections = await connection.ExecuteAsync
                ("UPDATE Messages SET Text = @Text, SenderId = @SenderId, ReceiverId = @ReceiverId WHERE Id = @Id",
                    new { message.Id, message.Text, message.SenderId, message.ReceiverId });

            return affections > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new NpgsqlConnection(_configuration["PostgreSqlSettings:ConnectionString"]);
            int affections = await connection.ExecuteAsync("DELETE FROM Messages WHERE Id = @Id", new { Id = id });

            return affections > 0;
        }
    }
}

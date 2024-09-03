using BuildingBlocks.Application.Data;
using Dapper;
using Users.Domain.Users;

namespace Users.Application.Users
{
    public class UsersCounter : IUsersCounter
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public UsersCounter(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int CountUsersByEmail(string email)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql = 
                $"""
                SELECT COUNT(*)
                FROM users."Users"
                WHERE users."Users"."Email" = @Email
                """;

            return connection.QuerySingle<int>(sql, new { Email = email });
        }

        public int CountUsersByPhoneNumber(string phoneNumber)
        {
            using var connection = _connectionFactory.GetConnection();

            const string sql =
                $"""
                SELECT COUNT(*)
                FROM users."Users"
                WHERE users."Users"."PhoneNumber" = @PhoneNumber
                """;

            return connection.QuerySingle<int>(sql, new { PhoneNumber = phoneNumber });
        }
    }
}

using BuildingBlocks.Application.Data;
using Dapper;
using FirebaseAdmin.Messaging;
using Notification = Chat.Application.RealTimeMessaging.Notification;

namespace Chat.Infrastructure.Firebase
{
    internal class FirebaseNotificationService
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public FirebaseNotificationService(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Notify(Notification notification)
        {
            var tokens = await GetTokens(notification.UserId);
            
            foreach (var token in tokens)
            {
                await FirebaseMessaging.DefaultInstance.SendAsync(new Message
                {
                    Token = token,
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = notification.Title,
                        Body = notification.Body
                    },
                    Android = new AndroidConfig
                    {
                        Priority = Priority.High, 
                        Notification = new AndroidNotification
                        {
                            ChannelId = "high-priority"
                        }
                    }
                });
            }
        }
        
        //srp violated
        private async Task<IEnumerable<string>> GetTokens(Guid userId)
        {
            try
            {
                using var connection = _connectionFactory.GetConnection();

                const string sql =
                    $"""
                     SELECT 
                         chat."Devices"."Token"
                         FROM chat."Devices"
                         WHERE chat."Devices"."UserId" = @Id AND chat."Devices"."ExpirationDate" > NOW()                   
                     """;

                return await connection.QueryAsync<string>(
                    sql,
                    new
                    {
                        Id = userId
                    });
            }
            catch(InvalidOperationException ex)
            {
                return [];
            }
        }
    }
}
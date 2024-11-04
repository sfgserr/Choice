using Autofac;
using Chat.Application.Contracts;
using Chat.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Configuration.SignalR
{
    internal class SignalRModule<T> : Module where T : Hub
    {
        private readonly IHubContext<T> _hubContext;

        internal SignalRModule(IHubContext<T> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ChatService<T>(
                    c.Resolve<IChatUsersStore>(), 
                    _hubContext))
                .As<IChatService>()
                .InstancePerDependency();

            builder.RegisterType<ChatUsersStore>()
                .As<IChatUsersStore>()
                .SingleInstance();
        }
    }
}
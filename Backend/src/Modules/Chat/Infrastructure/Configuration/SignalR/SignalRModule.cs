using Autofac;
using Chat.Application.Contracts;
using Chat.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.Configuration.SignalR
{
    internal class SignalRModule<T> : Module where T : Hub
    {
        private readonly IHubContext<T> _hubContext;
        private readonly IChatUsersStore _usersStore;
        
        internal SignalRModule(IHubContext<T> hubContext, IChatUsersStore usersStore)
        {
            _hubContext = hubContext;
            _usersStore = usersStore;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ChatService<T>(
                    c.Resolve<IChatUsersStore>(), 
                    _hubContext))
                .As<IChatService>()
                .InstancePerDependency();

            builder.RegisterInstance(_usersStore)
                .As<IChatUsersStore>()
                .SingleInstance();
        }
    }
}
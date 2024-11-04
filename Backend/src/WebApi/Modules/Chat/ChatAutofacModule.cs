using Autofac;
using Chat.Application.Contracts;
using Chat.Infrastructure;

namespace WebApi.Modules.Chat
{
    public class ChatAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChatModule>()
                .As<IChatModule>()
                .InstancePerLifetimeScope();
        }
    }
}
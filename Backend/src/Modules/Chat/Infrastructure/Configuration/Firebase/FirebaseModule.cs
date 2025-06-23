using Autofac;
using Chat.Infrastructure.Firebase;

namespace Chat.Infrastructure.Configuration.Firebase
{
    internal class FirebaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FirebaseNotificationService>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
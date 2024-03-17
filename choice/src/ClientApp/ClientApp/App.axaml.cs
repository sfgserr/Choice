using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ClientApp.Extensions;
using ClientApp.ViewModels;
using ClientApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientApp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            BindingPlugins.DataValidators.RemoveAt(0);

            ServiceCollection services = new();
            services.AddApplicationServices();

            IServiceProvider provider = services.BuildServiceProvider();

            DataTemplates.Add(provider.GetRequiredService<ViewMapper>());

            if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
            {
                singleView.MainView = provider.GetRequiredService<LoginView>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
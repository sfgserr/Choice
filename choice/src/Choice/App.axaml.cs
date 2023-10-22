using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Choice.Stores.Navigators;
using Choice.ViewModels;
using Choice.ViewModels.Factories;
using Choice.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Choice;

public partial class App : Application
{
    private IHost _host;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _host = CreateDefaultBuilder().Build();

            desktop.MainWindow = _host.Services.GetService<MainWindow>();

            DataTemplates.Add(_host.Services.GetRequiredService<ViewMapper>());
        }

        base.OnFrameworkInitializationCompleted();
    }

    private IHostBuilder CreateDefaultBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ViewMapper>();

                services.AddSingleton<INavigator, Navigator>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<LoginViewModel>();

                services.AddScoped<CreateViewModel<LoginViewModel>>(services => () => 
                    services.GetRequiredService<LoginViewModel>());

                services.AddScoped<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<LoginView>();
            });
}
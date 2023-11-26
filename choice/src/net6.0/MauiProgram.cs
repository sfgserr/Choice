using Choice.DependencyInjectionExtensions;
using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;

namespace Choice;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureSyncfusionCore()
			.UseMauiMaps()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddPages()
						.AddViewModels()
						.AddServices();

		return builder.Build();
	}
}

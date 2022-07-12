using CommunityToolkit.Maui;
using PocSocketMaui.Services;
using PocSocketMaui.Services.Interfaces;
using PocSocketMaui.ViewModels;
using PocSocketMaui.Views.Pages;

namespace PocSocketMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder().UsePrismApp<App>(prismAppBuilder => {
			prismAppBuilder.RegisterTypes(containerRegistery => {
				RegisterNavigation(containerRegistery);
				RegisterServices(containerRegistery);
				RegisterHelpers(containerRegistery);
			})
			.OnAppStart(async navigationService => await navigationService.NavigateAsync($"/{ nameof(MainPage)}"));
		})
		.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })
		.UseMauiCommunityToolkit();

        return builder.Build();
    }

	private static void RegisterNavigation(IContainerRegistry containerRegistry)
	{
		containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();

		containerRegistry.RegisterForNavigation<NavigationPage>();
	}

	private static void RegisterServices(IContainerRegistry containerRegistry)
	{
		containerRegistry.RegisterSingleton<IAlertDialogService, CommunityToolkitAlertDialogService>();
		//containerRegistry.RegisterSingleton<ISocketService, PusherService>();
		containerRegistry.RegisterSingleton<ISocketService, SocketService>();
	}

	private static void RegisterHelpers(IContainerRegistry containerRegistry)
	{
	}
}
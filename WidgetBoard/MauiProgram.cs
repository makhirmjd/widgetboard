using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;
using WidgetBoard.Helpers;
using WidgetBoard.Helpers.Extentions;
using WidgetBoard.Pages;
using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            ConfigureServices(builder.Services);

            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddPage<BoardDetailsPage, BoardDetailsPageViewModel>(RouteNames.BoardDetails);
            services.AddPage<BoardListPage, BoardListPageViewModel>(RouteNames.BoardList);
            services.AddPage<FixedBoardPage, FixedBoardPageViewModel>(RouteNames.FixedBoard);
            services.AddPage<SettingsPage, SettingsPageViewModel>(RouteNames.Settings);
            services.AddPage<AppShell, AppShellViewModel>("default");

            services.AddSingleton<WidgetFactory>();
            WidgetFactory.ReisterWidget<ClockWidgetView, ClockWidgetViewModel>("Clock");
            services.AddTransient<ClockWidgetView>();
            services.AddTransient<ClockWidgetViewModel>();
            services.AddSingleton<WidgetTemplateSelector>();
        }
    }
}

using WidgetBoard.ViewModels;

namespace WidgetBoard.Helpers.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddPage<TPage, TViewModel>(
            this IServiceCollection services,
            string route)
            where TPage : Page
            where TViewModel : BaseViewModel
    {
        services
            .AddTransient<TPage>()
            .AddTransient<TViewModel>();
        Routing.RegisterRoute(route, typeof(TPage));
    }
}

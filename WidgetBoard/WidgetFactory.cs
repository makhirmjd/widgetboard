using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard;

public class WidgetFactory(IServiceProvider serviceProvider)
{
    private static IDictionary<Type, Type> widgetRegistrations = new Dictionary<Type, Type>();
    private static IDictionary<string, Type> widgetNameRegistrations = new Dictionary<string, Type>();
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public static void ReisterWidget<TWidgetView, TWidgetViewModel>(string displayName)
        where TWidgetView : IWidgetView where TWidgetViewModel : IWidgetViewModel
    {
        widgetRegistrations.Add(typeof(TWidgetViewModel), typeof(TWidgetView));
        widgetNameRegistrations.Add(displayName, typeof(TWidgetViewModel));
    }

    public IList<string> AvailableWidgets => widgetNameRegistrations.Keys.ToList();

    public IWidgetView? CreateWidget(IWidgetViewModel widgetViewModel)
    {
        if (widgetRegistrations.TryGetValue(widgetViewModel.GetType(), out Type? widgetViewType))
        {
            IWidgetView widgetView = (IWidgetView)serviceProvider.GetRequiredService(widgetViewType);
            widgetView.WidgetViewModel = widgetViewModel;
            return widgetView;
        }
        return default;
    }

    public IWidgetViewModel? CreateWidgetViewModel(string displayName)
    {
        if (widgetNameRegistrations.TryGetValue(displayName, out Type? widgetViewModelType))
        {
            return (IWidgetViewModel)serviceProvider.GetRequiredService(widgetViewModelType);
        }
        return default;
    }
}

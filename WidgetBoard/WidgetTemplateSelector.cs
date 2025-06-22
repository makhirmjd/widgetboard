using WidgetBoard.ViewModels;

namespace WidgetBoard;

public class WidgetTemplateSelector(WidgetFactory widgetFactory) : DataTemplateSelector
{
    private readonly WidgetFactory widgetFactory = widgetFactory;

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (item is IWidgetViewModel widgetViewModel)
        {
            return new(() => widgetFactory.CreateWidget(widgetViewModel));
        }
        return default;
    }
}

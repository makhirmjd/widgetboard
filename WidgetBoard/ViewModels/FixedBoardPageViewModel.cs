using System.Collections.ObjectModel;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public partial class FixedBoardPageViewModel : BaseViewModel, IQueryAttributable
{
    private string boardName = string.Empty;
    private int numberOfColumns;
    private int numberOfRows;

    public string BoardName
    {
        get => boardName;
        set => SetProperty(ref boardName, value);
    }

    public int NumberOfColumns
    {
        get => numberOfColumns;
        set => SetProperty(ref numberOfColumns, value);
    }

    public int NumberOfRows
    {
        get => numberOfRows;
        set => SetProperty(ref numberOfRows, value);
    }

    public ObservableCollection<IWidgetViewModel> Widgets { get; }
    public WidgetTemplateSelector WidgetTemplateSelector { get; }

    public FixedBoardPageViewModel(WidgetTemplateSelector widgetTemplateSelector)
    {
        WidgetTemplateSelector = widgetTemplateSelector;
        Widgets = [];
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Board? board = (Board)query["Board"];
        BoardName = board.Name;
        NumberOfColumns = board.NumberOfColumns;
        NumberOfRows = board.NumberOfRows;
    }
}

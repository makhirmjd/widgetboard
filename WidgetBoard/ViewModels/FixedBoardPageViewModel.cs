using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public partial class FixedBoardPageViewModel : BaseViewModel, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Board? board = query["Board"] as Board;
    }
}

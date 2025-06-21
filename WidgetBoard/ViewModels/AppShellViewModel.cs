using System.Collections.ObjectModel;
using WidgetBoard.Helpers;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public partial class AppShellViewModel : BaseViewModel
{
    public ObservableCollection<Board> Boards { get; } = [];

    private Board? currentBoard;

    public Board? CurrentBoard
    {
        get => currentBoard;
        set
        {
            if (SetProperty(ref currentBoard, value))
            {
                _ = BoardSelected(currentBoard);
            }
        }
    }


    public AppShellViewModel()
    {
        Boards.Add(new()
        {
            Name = "My first board",
            NumberOfColumns = 3,
            NumberOfRows = 2
        });
    }

    private async Task BoardSelected(Board? board)
    {
        if (board is not null)
        {
            await Shell.Current.GoToAsync(
            RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", board }
            });
        }
    }
}

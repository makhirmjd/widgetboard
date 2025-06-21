using System.Collections.ObjectModel;
using WidgetBoard.Helpers;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

public partial class BoardListPageViewModel : BaseViewModel
{
    public ObservableCollection<Board> Boards { get; } = [];

    public Command AddBoardCommand { get; }

    public BoardListPageViewModel()
    {
        Boards.Add(new()
        {
            Name = "My first board",
            NumberOfColumns = 3,
            NumberOfRows = 2
        });

        AddBoardCommand = new(OnAddBoard);
    }

    private Board? currentBoard;

    public Board? CurrentBoard
    {
        get => currentBoard;
        set
        {
            if (SetProperty(ref currentBoard, value))
            {
                _ = BoardSelected(value);
            }
        }
    }

    private async Task BoardSelected(Board? board)
    {
        if (board is not null)
        {
            await Shell.Current.GoToAsync(RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", board }
            });
        }
    }

    private async void OnAddBoard()
    {
        TaskCompletionSource<Board?> boardCreated = new();
        await Shell.Current.GoToAsync(RouteNames.BoardDetails,
            new Dictionary<string, object>
            {
                { "Created", boardCreated }
            });

        Board? newBoard = await boardCreated.Task;

        if (newBoard is not null)
        {
            Boards.Add(newBoard);
        }
    }

}

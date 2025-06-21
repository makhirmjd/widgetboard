using WidgetBoard.Helpers;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

[QueryProperty(nameof(BoardCreatedCompletionSource), "Created")]
public partial class BoardDetailsPageViewModel : BaseViewModel
{
	private string boardName = string.Empty;

	public string BoardName
	{
		get => boardName;
		set
		{
			SetProperty(ref boardName, value);
			SaveCommand.ChangeCanExecute();
        }
	}

	private bool isFixed;

	public bool IsFixed
	{
		get => isFixed;
		set => SetProperty(ref isFixed, value);
	}

	private int numberOfColumns = 3;

	public int NumberOfColumns
	{
		get => numberOfColumns;
		set => SetProperty(ref numberOfColumns, value);
	}

	private int numberOfRows = 2;

	public int NumberOfRows
	{
		get => numberOfRows;
		set => SetProperty(ref numberOfRows, value);
	}

	public Command SaveCommand { get; }
	public Command CancelCommand { get; }
	public TaskCompletionSource<Board?>? BoardCreatedCompletionSource { get; set; }

    public BoardDetailsPageViewModel()
    {
		SaveCommand = new(async () => await Save(), () => !string.IsNullOrWhiteSpace(BoardName));
		CancelCommand = new(async () =>
		{
			await Shell.Current.GoToAsync("..");
			BoardCreatedCompletionSource?.SetResult(default);
        });
    }

    private async Task Save()
	{
		Board board = new()
		{
			Name = BoardName,
			NumberOfColumns = NumberOfColumns,
			NumberOfRows = NumberOfRows
		};
        await Shell.Current.GoToAsync("..");
		BoardCreatedCompletionSource?.SetResult(board);
    }
}

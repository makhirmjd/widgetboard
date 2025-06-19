using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

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

    public BoardDetailsPageViewModel()
    {
		SaveCommand = new(Save, () => !string.IsNullOrWhiteSpace(BoardName));
    }

    private void Save()
	{
		Board board = new()
		{
			Name = BoardName,
			NumberOfColumns = NumberOfColumns,
			NumberOfRows = NumberOfRows
		};
	}
}

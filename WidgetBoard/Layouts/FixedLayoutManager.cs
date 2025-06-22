using System.Windows.Input;
using WidgetBoard.Controls;

namespace WidgetBoard.Layouts;

public partial class FixedLayoutManager : BindableObject, ILayoutManager
{
    public static readonly BindableProperty NumberOfColumnsProperty =
        BindableProperty.Create(
            nameof(NumberOfColumns),
            typeof(int),
            typeof(FixedLayoutManager),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: OnNumberOfColumnsChanged);

    public int NumberOfColumns
    {
        get => (int)GetValue(NumberOfColumnsProperty);
        set => SetValue(NumberOfColumnsProperty, value);
    }

    private static void OnNumberOfColumnsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        FixedLayoutManager manager = (FixedLayoutManager)bindable;
        manager.InitializeGrid();
    }

    public static readonly BindableProperty NumberOfRowsProperty =
        BindableProperty.Create(
            nameof(NumberOfRows),
            typeof(int),
            typeof(FixedLayoutManager),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: OnNumberOfRowsChanged);

    public int NumberOfRows
    {
        get => (int)GetValue(NumberOfRowsProperty);
        set => SetValue(NumberOfRowsProperty, value);
    }

    private static void OnNumberOfRowsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        FixedLayoutManager manager = (FixedLayoutManager)bindable;
        manager.InitializeGrid();
    }

    public static readonly BindableProperty PlaceholderTappedCommandProperty =
        BindableProperty.Create(
            nameof(PlaceholderTappedCommand),
            typeof(ICommand),
            typeof(FixedLayoutManager));

    public ICommand PlaceholderTappedCommand
    {
        get => (ICommand)GetValue(PlaceholderTappedCommandProperty);
        set => SetValue(PlaceholderTappedCommandProperty, value);
    }

    private void OnTapGestureRecognizerTapped(object? sender, EventArgs e)
    {
        if (sender is Placeholder placeholder)
        {
            if (PlaceholderTappedCommand?.CanExecute(placeholder.Position) == true)
            {
                PlaceholderTappedCommand.Execute(placeholder.Position);
            }
        }
    }

    private BoardLayout? board;
    private bool isInitialized;

    public BoardLayout? Board
    {
        get => board;
        set
        {
            board = value;
            InitializeGrid();
        }
    }

    private void InitializeGrid()
    {
        if (Board is null || NumberOfColumns == 0 || NumberOfRows == 0 || isInitialized)
        {
            return;
        }
        isInitialized = true;
        for (int i = 0; i < NumberOfColumns; i++)
        {
            Board.AddColumn(new ColumnDefinition(new GridLength(1, GridUnitType.Star)));
        }
        for (int i = 0; i < NumberOfRows; i++)
        {
            Board.AddRow(new RowDefinition(new GridLength(1, GridUnitType.Star)));
        }
        for (int column = 0; column < NumberOfColumns; column++)
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                Placeholder placeholder = new()
                {
                    Position = row * NumberOfColumns + column,
                };
                TapGestureRecognizer tapGestureRecognizer = new();
                tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;
                placeholder.GestureRecognizers.Add(tapGestureRecognizer);
                Board.AddPlaceholder(placeholder);
                Grid.SetColumn(placeholder, column);
                Grid.SetRow(placeholder, row);
            }
        }
    }

    public void SetPosition(BindableObject bindableObject, int position)
    {
        if (NumberOfColumns == 0 || Board is null)
        {
            return;
        }
        int column = position % NumberOfColumns;
        int row = position / NumberOfColumns;

        Grid.SetColumn(bindableObject, column);
        Grid.SetRow(bindableObject, row);

        Placeholder? placeholder = Board.Placeholders.FirstOrDefault(p => p.Position == position);
        if (placeholder is not null)
        {
            Board.RemovePlaceholder(placeholder);
        }
    }
}

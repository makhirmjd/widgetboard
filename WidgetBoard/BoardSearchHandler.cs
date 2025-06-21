using System.Threading.Tasks;
using WidgetBoard.Helpers;
using WidgetBoard.Models;

namespace WidgetBoard;

public partial class BoardSearchHandler : SearchHandler
{
    private readonly IList<Board> boards = 
    [
        new()
        {
            Name = "My first board"
        },
        new()
        {
            Name = "My second board"
        },
        new()
        {
            Name = "My third board"
        }
    ];

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);
        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = default;
        }
        else
        {
            ItemsSource = boards.Where(b => b.Name.Contains(newValue, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

        // Let the animation complete
        await Task.Delay(1000);
        await Shell.Current.GoToAsync(RouteNames.FixedBoard,
            new Dictionary<string, object>
            {
                { "Board", (Board)item }
            });
    }
}

using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.Services.Interfaces;

namespace TicTacToe.BLL.Services;

public class GameProcessService : IGameProcessService
{
    public OutcomeDto? TryGetOutcome(Guid?[] field)
    {
        short[]? CalculateWinCells(short index1, short index2, short index3)
        {
            var cells = new[] { field[index1], field[index2], field[index3] };

            var grouped = cells.Where(x => x is not null)
                .GroupBy(x => x).FirstOrDefault();
            return grouped?.Count() == 3 ? new []{index1, index2, index3} : null;
        }

        var winCellsIndexes = CalculateWinCells(0, 1, 2)
                  ?? CalculateWinCells(3, 4, 5)
                  ?? CalculateWinCells(6, 7, 8)
                  ?? CalculateWinCells(0, 3, 6)
                  ?? CalculateWinCells(1, 4, 7)
                  ?? CalculateWinCells(2, 5, 8)
                  ?? CalculateWinCells(0, 4, 8)
                  ?? CalculateWinCells(2, 4, 6);

        if (winCellsIndexes != null)
        {
            return new OutcomeDto
            {
                IsDraw = false,
                CellWinIndexes = winCellsIndexes
            };
        }

        if (field.All(x => x != null))
        {
            return new OutcomeDto
            {
                IsDraw = true
            };
        }

        return null;
    }
}
using TicTacToe.BLL.Dto.Game;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Infrastructure.Mappers;

public static class GameMappingExtensions
{
    public static Outcome? ToOutcome(this OutcomeDto? dto, Guid dtoUserId)
    {
        if (dto == null)
        {
            return null;
        }

        return new Outcome
        {
            Winner = !dto.IsDraw && dto.CellWinIndexes != null
                ? new WinnerDetail
                {
                    UserId = dtoUserId,
                    CellIndex1 = dto.CellWinIndexes[0],
                    CellIndex2 = dto.CellWinIndexes[1],
                    CellIndex3 = dto.CellWinIndexes[2]
                }
                : null
        };
    }
}
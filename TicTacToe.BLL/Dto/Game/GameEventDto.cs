using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Dto.Game;

public class GameEventDto
{
    public Guid GameId { get; set; }
    public Guid TurnUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public OutcomeDto? Outcome { get; set; }
    public CellEventDto?[] CellEvents { get; set; }
}

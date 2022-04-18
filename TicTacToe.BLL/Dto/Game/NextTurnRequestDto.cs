namespace TicTacToe.BLL.Dto.Game;

public class NextTurnRequestDto
{
    public Guid GameId { get; set; }
    public short CellIndex { get; set; }
    public Guid UserId { get; set; }
}
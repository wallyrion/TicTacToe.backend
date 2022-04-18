namespace TicTacToe.API.ViewModels.Game;

public class NextTurnRequestVM
{
    public Guid GameId { get; set; }
    public short CellIndex { get; set; }
}
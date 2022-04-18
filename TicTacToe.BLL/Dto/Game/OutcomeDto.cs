namespace TicTacToe.BLL.Dto.Game;

public class OutcomeDto
{
    public short[]? CellWinIndexes { get; set; }
    public bool IsDraw { get; set; }
}
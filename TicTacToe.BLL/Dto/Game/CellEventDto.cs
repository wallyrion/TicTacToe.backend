namespace TicTacToe.BLL.Dto.Game;

public class CellEventDto
{
    public Guid? UserId { get; set; }
    public DateTime? TurnDate { get; }
}

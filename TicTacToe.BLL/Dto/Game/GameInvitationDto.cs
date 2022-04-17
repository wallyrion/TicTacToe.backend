namespace TicTacToe.BLL.Dto.Game;

public class GameInvitationDto
{
    public Guid GameId { get; set; }
    public Guid OpponentId { get; set; }
    public string OpponentEmail { get; set; }
    public DateTime? InvitationDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public Guid FirstTurnPlayerId { get; set; }
}
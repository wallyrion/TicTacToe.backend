namespace TicTacToe.BLL.Dto.Game;

public class GameInvitationDto
{
    public Guid GameId { get; set; }
    public string User1Email { get; set; }
    public Guid User1Id { get; set; }
    public string User2Email { get; set; }
    public Guid User2Id { get; set; }
    public DateTime? InvitationDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public Guid FirstTurnPlayerId { get; set; }
}
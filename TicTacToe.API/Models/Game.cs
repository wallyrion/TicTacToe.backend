
namespace TicTacToe.API.Models;

public class Game
{
    public Guid Id { get; set; }
    public Guid User1Id { get; set; }
    public Guid User2Id { get; set; }
    public DateTime? InvitationDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public Guid FirstTurnPlayerId { get; set; }
}
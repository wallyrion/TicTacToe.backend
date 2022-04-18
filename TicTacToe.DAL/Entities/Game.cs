
using System.Collections;

namespace TicTacToe.DAL.Entities;

public record Game
{
    public Guid Id { get; set; }
    public Guid User1Id { get; set; }
    public Guid User2Id { get; set; }
    public DateTime? InvitationDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public Guid FirstTurnPlayerId { get; init; }
    public ICollection<CellEvent> Field { get; set; } = null!;
    public Outcome? Outcome { get; set; }
}

public record CellEvent
{
    public Guid? UserId { get; set; }
    public DateTime? TurnDate { get; set; }
}

public record Outcome
{
    public WinnerDetail? Winner { get; set; }

    public bool IsDraw => Winner != null;
}

public record WinnerDetail
{
    public Guid UserId { get; set; }
    public short CellIndex1 { get; set; }
    public short CellIndex2 { get; set; }
    public short CellIndex3 { get; set; }
}

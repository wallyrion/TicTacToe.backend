using TicTacToe.BLL.Dto.Game;

namespace TicTacToe.BLL.Services.Interfaces;

public interface IGameService
{
    Task Accept(Guid gameId);
    Task<GameInvitationDto> Invite(Guid currentUserId, Guid opponentId);
}
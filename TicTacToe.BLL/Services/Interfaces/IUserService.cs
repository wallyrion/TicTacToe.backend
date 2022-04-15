using TicTacToe.BLL.Dto.Game;

namespace TicTacToe.BLL.Services.Interfaces;

public interface IGameService
{
    Task<GameInvitationDto> Invite(GameInviteRequestDto gameInviteRequestDto);
    Task Accept(Guid gameId);
}
using TicTacToe.BLL.Dto;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.SignalR;

namespace TicTacToe.BLL.Services.Interfaces;

public interface IGameService
{
    Task<GameInvitationDto> Invite(GameInviteRequestDto gameInviteRequestDto);
    Task Accept(Guid gameId);
}
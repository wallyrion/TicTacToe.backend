using TicTacToe.API.Dto;
using TicTacToe.API.Models;
using TicTacToe.API.SignalR;

namespace TicTacToe.API;

public interface IGameService
{
    Task<GameInvitation> Invite(GameInviteDto gameInviteDto);
    Task Accept(Guid gameId);
}
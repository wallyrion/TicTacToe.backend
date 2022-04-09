using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.SignalR;

namespace TicTacToe.BLL.Services.Interfaces;

public interface INotificationService
{
    Task SendInvitationAsync(GameInvitationDto gameInvitation);
    Task AcceptInvitationAsync(Guid gameId);
}
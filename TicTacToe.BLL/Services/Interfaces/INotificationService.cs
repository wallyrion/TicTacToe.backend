using TicTacToe.BLL.Dto.Game;

namespace TicTacToe.BLL.Services.Interfaces;

public interface INotificationService
{
    Task SendInvitationAsync(GameInvitationDto gameInvitation, Guid userId);
    Task AcceptInvitationAsync(Guid gameId, Guid userId);
    Task HandleOpponentTurn(GameEventDto gameEvent, Guid userIdToNotify);
}
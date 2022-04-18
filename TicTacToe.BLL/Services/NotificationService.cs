using Microsoft.AspNetCore.SignalR;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.BLL.SignalR;

namespace TicTacToe.BLL.Services;

public class NotificationService: INotificationService
{
    private readonly IHubContext<GameHub> _hubContext;

    public NotificationService(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendInvitationAsync(GameInvitationDto gameInvitation, Guid userIdToNotify)
    {
        await SendToSpecificUser(userIdToNotify, "invite", gameInvitation);
    }

    public async Task AcceptInvitationAsync(Guid gameId, Guid userIdToNotify)
    {
        await SendToSpecificUser(userIdToNotify, "accepted", gameId);
    }

    public async Task HandleOpponentTurn(GameEventDto gameEvent, Guid userIdToNotify)
    {
        await SendToSpecificUser(userIdToNotify, "opponentTurn", gameEvent);
    }

    private async Task SendToSpecificUser(Guid userId, string methodName, object data)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync(methodName, data);
    }
}
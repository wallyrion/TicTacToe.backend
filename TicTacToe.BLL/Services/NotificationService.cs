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

    public async Task SendInvitationAsync(GameInvitationDto gameInvitation, Guid userId)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync("invite", gameInvitation);
    }

    public async Task AcceptInvitationAsync(Guid gameId, Guid userId)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync("accepted", gameId);
    }
}
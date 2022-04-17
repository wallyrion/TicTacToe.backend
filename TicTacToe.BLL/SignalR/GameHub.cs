using Microsoft.AspNetCore.SignalR;
using TicTacToe.BLL.Extensions;
using TicTacToe.BLL.Services;

namespace TicTacToe.BLL.SignalR;

/*
public interface IHubClient
{
    Task BroadcastMessage(string message);
    Task InviteToGameMessage();
}

public class BroadcastHub : Hub<IHubClient>
{
    public async Task InviteToGame()
    {
        await Clients.All.BroadcastMessage("some message");
    }
}
*/

public class GameHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public Task Send(string data)
    {
        return Clients.All.SendAsync("Send", data);
    }
}
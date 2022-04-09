using Microsoft.AspNetCore.SignalR;

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
    public Task Send(string data)
    {
        return Clients.All.SendAsync("Send", data);
    }
}
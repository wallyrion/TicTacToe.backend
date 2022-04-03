using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.API.SignalR;

//public interface IHubClient
//{
//    Task BroadcastMessage();
//}

//public class BroadcastHub : Hub<IHubClient>
//{
//}

public class GameHub : Hub
{
    public Task Send(string data)
    {
        return Clients.All.SendAsync("Send", data);
    }
}
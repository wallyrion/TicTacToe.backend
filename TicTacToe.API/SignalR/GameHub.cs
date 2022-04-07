using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.API.SignalR;

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

public class GameInvitation
{
    public Guid GameId { get; set; }
    public string User1Email { get; set; }
    public Guid User1Id { get; set; }
    public string User2Email { get; set; }
    public Guid User2Id { get; set; }
    public DateTime? InvitationDate { get; set; }
    public DateTime? AcceptedDate { get; set; }
    public Guid FirstTurnPlayerId { get; set; }
}

public class GameHub : Hub
{
    public Task Send(string data)
    {
        //Clients.Client("sWwWP6krPWPe0ktWH06dnQ").SendAsync("Send", "some test data");
        return Clients.All.SendAsync("Send", data);
    }

    public Task SendInvitation(GameInvitation data)
    {
        //Clients.Client("sWwWP6krPWPe0ktWH06dnQ").SendAsync("Send", "some test data");
        return Clients.All.SendAsync("Invite", data);
    }
}
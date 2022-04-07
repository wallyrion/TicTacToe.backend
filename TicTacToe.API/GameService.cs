using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Dto;
using TicTacToe.API.Exceptions;
using TicTacToe.API.Models;
using TicTacToe.API.SignalR;

namespace TicTacToe.API;

public class GameService : IGameService
{
   // private readonly GameHub _gameHub;
    private readonly IHubContext<GameHub> _hubContext;

    public GameService(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task<GameInvitation> Invite(GameInviteDto gameInviteDto)
    {
        if (gameInviteDto.CurrentUserEmail == gameInviteDto.SecondUserEmail)
        {
            throw new Exception("Can not create game with the same user");
        }

        using var context = new TicTacToeContext();

        var user1 = await context.Users.FirstOrDefaultAsync(x => x.Email == gameInviteDto.CurrentUserEmail);
        if (user1 == null)
        {
            throw new EntityNotFoundException($"User with email {gameInviteDto.CurrentUserEmail} not found");
        }

        var user2 = await context.Users.FirstOrDefaultAsync(x => x.Email == gameInviteDto.SecondUserEmail);
        if (user2 == null)
        {
            throw new EntityNotFoundException($"User with email {gameInviteDto.SecondUserEmail} not found");
        }


        var firstUser = new Random().Next(0, 2);
        var game = new Game
        {
            Id = Guid.NewGuid(),
            User1Id = user1.Id,
            User2Id = user2.Id,
            InvitationDate = DateTime.UtcNow,
            FirstTurnPlayerId = firstUser == 0 ? user1.Id : user2.Id
        };

        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();

        var gameInvitationWs = new GameInvitation
        {
            User1Id = user1.Id,
            User2Id = user2.Id,
            User1Email = user1.Email,
            User2Email = user2.Email,
            GameId = game.Id,
            FirstTurnPlayerId = game.FirstTurnPlayerId,
            InvitationDate = game.InvitationDate,
            AcceptedDate = game.AcceptedDate
        };

        //await _gameHub.SendInvitation(gameInvitationWs);
        await _hubContext.Clients.All.SendAsync("invite", gameInvitationWs);
        return gameInvitationWs;
    }

    public async Task Accept(Guid gameId)
    {

        using var context = new TicTacToeContext();

        var game = await context.Games.FirstOrDefaultAsync(x =>  x.Id == gameId);

        if (game == null)
        {
            throw new EntityNotFoundException($"Game {gameId} not found");
        }

        game.AcceptedDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        await _hubContext.Clients.All.SendAsync("accepted", gameId);
    }

}
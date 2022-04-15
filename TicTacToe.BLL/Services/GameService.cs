using Microsoft.EntityFrameworkCore;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.Exceptions;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.DAL;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Services;

public class GameService : IGameService
{
    private readonly INotificationService _notificationService;

    public GameService(
        INotificationService notificationService
        )
    {
        _notificationService = notificationService;
    }

    public async Task<GameInvitationDto> Invite(GameInviteRequestDto gameInviteRequestDto)
    {
        if (gameInviteRequestDto.CurrentUserEmail == gameInviteRequestDto.SecondUserEmail)
        {
            throw new Exception("Can not create game with the same user");
        }

        await using var context = new TicTacToeContext();

        var user1 = await context.Users.FirstOrDefaultAsync(x => x.Email == gameInviteRequestDto.CurrentUserEmail);
        if (user1 == null)
        {
            throw new EntityNotFoundException($"User with email {gameInviteRequestDto.CurrentUserEmail} not found");
        }

        var user2 = await context.Users.FirstOrDefaultAsync(x => x.Email == gameInviteRequestDto.SecondUserEmail);
        if (user2 == null)
        {
            throw new EntityNotFoundException($"User with email {gameInviteRequestDto.SecondUserEmail} not found");
        }

        var firstUser = new Random().Next(0, 2);
        var game = new Game
        {
            User1Id = user1.Id,
            User2Id = user2.Id,
            InvitationDate = DateTime.UtcNow,
            FirstTurnPlayerId = firstUser == 0 ? user1.Id : user2.Id
        };

        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();

        var gameInvitationWs = new GameInvitationDto
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

        await _notificationService.SendInvitationAsync(gameInvitationWs);

        return gameInvitationWs;
    }

    public async Task Accept(Guid gameId)
    {
        await using var context = new TicTacToeContext();

        var game = await context.Games.FirstOrDefaultAsync(x =>  x.Id == gameId);

        if (game == null)
        {
            throw new EntityNotFoundException($"Game {gameId} not found");
        }

        game.AcceptedDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
        await _notificationService.AcceptInvitationAsync(gameId);
    }

}
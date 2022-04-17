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

    public async Task<GameInvitationDto> Invite(Guid currentUserId, Guid opponentId)
    {
        await using var context = new TicTacToeContext();

        var currentUser = await context.Users.FindAsync(currentUserId);
        if (currentUser == null)
        {
            throw new InvalidRequestException($"User {currentUserId} not found");
        }

        var opponent = await context.Users.FindAsync(opponentId);
        if (opponent == null)
        {
            throw new InvalidRequestException($"Opponent user {opponent} not found");
        }

        var firstUser = new Random().Next(0, 2);
        var game = new Game
        {
            User1Id = currentUser.Id,
            User2Id = opponent.Id,
            InvitationDate = DateTime.UtcNow,
            FirstTurnPlayerId = firstUser == 0 ? currentUser.Id : opponent.Id
        };

        await context.Games.AddAsync(game);
        await context.SaveChangesAsync();

        var gameInvitationWs = new GameInvitationDto
        {
            User1Id = currentUser.Id,
            User1Email = currentUser.Email,
            User2Id = opponent.Id,
            User2Email = opponent.Email,
            GameId = game.Id,
            FirstTurnPlayerId = game.FirstTurnPlayerId,
            InvitationDate = game.InvitationDate,
            AcceptedDate = game.AcceptedDate,
        };

        await _notificationService.SendInvitationAsync(gameInvitationWs, opponent.Id);

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
        await _notificationService.AcceptInvitationAsync(gameId, game.User1Id);
    }
}
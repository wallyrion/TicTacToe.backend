using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicTacToe.BLL.BusinessValidators;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.Exceptions;
using TicTacToe.BLL.Infrastructure.Mappers;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.Common;
using TicTacToe.DAL;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Services;

public class GameService : IGameService
{
    private readonly INotificationService _notificationService;
    private readonly IGameProcessService _gameProcessService;
    private readonly TicTacToeContext _context;
    private readonly IMapper _mapper;
    public GameService(
        INotificationService notificationService,
        IGameProcessService gameProcessService,
        IMapper mapper, TicTacToeContext context)
    {
        _notificationService = notificationService;
        _gameProcessService = gameProcessService;
        _mapper = mapper;
        _context = context;
    }

    public async Task<GameInvitationDto> Invite(Guid currentUserId, Guid opponentId)
    {
        var currentUser = await _context.Users.FindAsync(currentUserId);
        if (currentUser == null)
        {
            throw new InvalidRequestException($"User {currentUserId} not found");
        }

        var opponent = await _context.Users.FindAsync(opponentId);
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
            FirstTurnPlayerId = firstUser == 0 ? currentUser.Id : opponent.Id,
            Field = new CellEvent[9].Select(_ => new CellEvent()).ToArray()
        };

        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();

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

    public async Task<GameEventDto> HandleNextTurn(NextTurnRequestDto dto)
    {
        Game? game = await _context.Games.FindAsync(dto.GameId);

        GameValidator.ValidateNextTurnRequest(game, dto.GameId);

        var currentDate = DateTimeProvider.UtcNow;

        var field = game!.Field.ToArray();
        field[dto.CellIndex] = new CellEvent
        {
            UserId = dto.UserId,
            TurnDate = currentDate
        };

        game.Field = field.ToList();
        var cells = field.Select(x => x?.UserId).ToArray();
        var outcome = _gameProcessService.TryGetOutcome(cells);

        game.Outcome = outcome.ToOutcome(dto.UserId);

        var gameEvent = new GameEventDto
        {
            Outcome = outcome,
            CreatedDate = currentDate,
            GameId = dto.GameId,
            TurnUserId = dto.UserId,
            CellEvents = _mapper.Map<CellEventDto[]>(field)
        };
        await _context.SaveChangesAsync();
        var userIdToNotify = game.User1Id == dto.UserId ? game.User2Id : game.User1Id;
        await _notificationService.HandleOpponentTurn(gameEvent, userIdToNotify);
        return gameEvent;
    }

    public async Task Accept(Guid gameId)
    {
        var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == gameId);

        if (game == null)
        {
            throw new EntityNotFoundException($"Game {gameId} not found");
        }

        game.AcceptedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        await _notificationService.AcceptInvitationAsync(gameId, game.User1Id);
    }
}
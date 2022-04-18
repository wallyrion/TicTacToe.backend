using System.Diagnostics.CodeAnalysis;
using TicTacToe.BLL.Exceptions;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.BusinessValidators;

public class GameValidator
{
    public static void ValidateNextTurnRequest(Game? game, Guid gameId)
    {
        if (game is null)
        {
            throw new InvalidRequestException($"Invalid game request. {gameId} Not found");
        }

        if (game.AcceptedDate == null)
        {
            throw new InvalidRequestException($"Invalid game request. {gameId} has nob been started");
        }
    }
}
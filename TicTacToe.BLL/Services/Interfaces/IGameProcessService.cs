using TicTacToe.BLL.Dto.Game;

namespace TicTacToe.BLL.Services.Interfaces;

public interface IGameProcessService
{
    OutcomeDto? TryGetOutcome(Guid?[] field);
}
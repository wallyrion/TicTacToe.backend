namespace TicTacToe.BLL.Services.Interfaces;

public interface ITokenService
{
    public Task<Tuple<string, string>> GenerateTokensAsync(Guid userId);
}
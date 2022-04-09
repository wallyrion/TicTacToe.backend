using Microsoft.EntityFrameworkCore;
using TicTacToe.BLL.Exceptions;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.DAL;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Services;

public class TokenService : ITokenService
{
    public async Task<Tuple<string, string>> GenerateTokensAsync(Guid userId)
    {
        await using var context = new TicTacToeContext();
        var accessToken = await TokenHelper.GenerateAccessToken(userId);
        var refreshToken = await TokenHelper.GenerateRefreshToken();

        var userRecord = await context.Users
            .FirstOrDefaultAsync(e => e.Id == userId);

        if (userRecord == null)
        {
            throw new EntityNotFoundException($"User {userId} not found");
        }

        var salt = PasswordHelper.GetSecureSalt();
        var refreshTokenHashed = PasswordHelper.HashUsingPbkdf2(refreshToken, salt);

        if (userRecord.RefreshTokens.Any())
        {
            userRecord.RefreshTokens = userRecord.RefreshTokens.Where(item => item.ExpiryDate < DateTime.UtcNow).ToList();

        }

        userRecord.RefreshTokens?.Add(new RefreshToken
        {
            ExpiryDate = DateTime.Now.AddDays(14),
            UserId = userId,
            TokenHash = refreshTokenHashed,
            TokenSalt = Convert.ToBase64String(salt),
            CreatedDate = DateTime.UtcNow
        });


        await context.SaveChangesAsync();

        var token = new Tuple<string, string>(accessToken, refreshToken);

        return token;
    }
}
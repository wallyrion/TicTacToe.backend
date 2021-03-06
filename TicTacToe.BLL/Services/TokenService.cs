using Microsoft.EntityFrameworkCore;
using TicTacToe.BLL.Dto.User;
using TicTacToe.BLL.Exceptions;
using TicTacToe.BLL.Helpers;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.Common;
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

        var passwordSalt = Convert.FromBase64String(userRecord.PasswordSalt);
        var refreshTokenHashed = PasswordHelper.HashUsingPbkdf2(refreshToken, passwordSalt);

        if (userRecord.RefreshTokens.Any())
        {
            userRecord.RefreshTokens = userRecord.RefreshTokens.Where(item => item.ExpiryDate > DateTime.UtcNow).ToList();
        }

        userRecord.RefreshTokens?.Add(new RefreshToken
        {
            ExpiryDate = DateTime.Now.AddDays(14),
            UserId = userId,
            TokenHash = refreshTokenHashed,
            CreatedDate = DateTime.UtcNow
        });


        await context.SaveChangesAsync();

        var token = new Tuple<string, string>(accessToken, refreshToken);

        return token;
    }

    public async Task ValidateRefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequest)
    {
        await using var context = new TicTacToeContext();

        var user = await context.Users.FirstOrDefaultAsync(o => o.Id == refreshTokenRequest.UserId);

        if (user == null)
        {
            throw new EntityNotFoundException($"User {refreshTokenRequest.UserId} not found");
        }
        var refreshTokenToValidateHash = PasswordHelper.HashUsingPbkdf2(refreshTokenRequest.RefreshToken, Convert.FromBase64String(user.PasswordSalt));

        var token = user.RefreshTokens.FirstOrDefault(token => token.TokenHash == refreshTokenToValidateHash);

        if (token == null)
        {
            throw new InvalidOperationException("invalid refresh token.");
        }

        if (token.ExpiryDate < DateTimeProvider.UtcNow)
        {
            throw new InvalidOperationException("Refresh token expired");
        }

    }
}
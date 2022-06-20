
namespace TicTacToe.DAL.Entities;

public class User
{
    public Guid Id { get; set; }
    public string? Sex { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string PasswordSalt { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public IList<RefreshToken> RefreshTokens { get; set; } = Array.Empty<RefreshToken>();
    public IReadOnlyList<string> InvitedUsersIds { get; set; } = Array.Empty<string>();
}
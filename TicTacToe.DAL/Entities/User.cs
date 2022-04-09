
namespace TicTacToe.DAL.Entities;

public partial record User
{
    public Guid Id { get; set; }
    public string? Sex { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string PasswordSalt { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
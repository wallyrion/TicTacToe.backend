using System.ComponentModel.DataAnnotations;

namespace TicTacToe.API.ViewModels.User;

public class RefreshTokenRequestVM
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string RefreshToken { get; set; } = null!;
}
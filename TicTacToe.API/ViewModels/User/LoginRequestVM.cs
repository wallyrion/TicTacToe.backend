using System.ComponentModel.DataAnnotations;

namespace TicTacToe.API.ViewModels.User
{
    public class LoginRequestVM
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}

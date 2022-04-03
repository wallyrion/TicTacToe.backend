using System.ComponentModel.DataAnnotations;

namespace TicTacToe.API.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}

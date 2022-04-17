using System.ComponentModel.DataAnnotations;

namespace TicTacToe.API.ViewModels.Game;

public class GameInviteRequestVM
{
    [Required]
    public Guid OpponentId {get; set; }
}


﻿using System.ComponentModel.DataAnnotations;

namespace TicTacToe.API.ViewModels.Game;

public class GameInviteViewModel
{
    [Required]
    public string CurrentUserEmail {get; set; } = null!;

    [Required]
    public string SecondUserEmail { get; set; } = null!;
}


namespace TicTacToe.API.ViewModels.Game;

public class GameInviteViewModel
{
    public string CurrentUserEmail {get; set; }

    public string SecondUserEmail { get; set; }
}

public class GameAcceptInvitationViewModel
{
    public string InviterEmail { get; set; }

    public string InvitedEmail { get; set; }
}
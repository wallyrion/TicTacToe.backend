namespace TicTacToe.API.Dto;

public class GameInviteDto
{
    public string CurrentUserEmail { get; set; }

    public string SecondUserEmail { get; set; }
}

public class GameAcceptInvitationDto
{
    public Guid GameId { get; set; }

    public string User1Email { get; set; }

    public string User2Email { get; set; }
}
namespace TicTacToe.API.ViewModels.User;

public class UserVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Sex { get; set; }
}

public record UserSearchVM
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid Id { get; set; }
    public bool IsAlreadyInvited { get; set; }
}
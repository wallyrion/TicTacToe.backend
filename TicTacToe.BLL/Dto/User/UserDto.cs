namespace TicTacToe.BLL.Dto.User;

public record UserDto
{
    public string? Sex { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
}

namespace TicTacToe.API.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Sex { get; set; }
    public string Password { get; set; }
}
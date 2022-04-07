using TicTacToe.API.Models;

namespace TicTacToe.API;

public interface IUserService 
{
    Task Register(User user);
    Task<User?> Login(string email, string password);
}
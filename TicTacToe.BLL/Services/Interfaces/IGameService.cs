using TicTacToe.BLL.Dto.User;

namespace TicTacToe.BLL.Services.Interfaces;

public interface IUserService 
{
    Task<UserDto> Register(LoginDto user);
    Task<UserDto?> Login(string email, string password);
}
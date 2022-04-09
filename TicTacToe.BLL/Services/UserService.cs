using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicTacToe.BLL.Dto.User;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.DAL;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    public UserService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<UserDto> Register(LoginDto loginDto)
    {
        await using var context = new TicTacToeContext();
        var emailExist = await context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        if (emailExist != null)
        {
            throw new Exception($"Email {emailExist.Email} already exists.");
        }

        var userEntity = _mapper.Map<User>(loginDto);
        var userCreated = await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();

        return _mapper.Map<UserDto>(userCreated.Entity);
    }

    public async Task<UserDto?> Login(string email, string password)
    {
        await using var context = new TicTacToeContext();
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        return _mapper.Map<UserDto>(user);
    }
}
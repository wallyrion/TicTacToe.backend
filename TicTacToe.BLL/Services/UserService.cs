using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicTacToe.BLL.Dto.User;
using TicTacToe.BLL.Exceptions;
using TicTacToe.BLL.Helpers;
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

        var salt = PasswordHelper.GetSecureSalt();
        var passwordHash = PasswordHelper.HashUsingPbkdf2(loginDto.Password, salt);
        userEntity.PasswordSalt = Convert.ToBase64String(salt);
        userEntity.PasswordHash = passwordHash;

        var userCreated = await context.Users.AddAsync(userEntity);

        await context.SaveChangesAsync();

        return _mapper.Map<UserDto>(userCreated.Entity);
    }

    public async Task<UserDto?> Login(string email, string password)
    {
        await using var context = new TicTacToeContext();
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            throw new ValidationException($"User {email} not found");
        }
        var passwordHash = PasswordHelper.HashUsingPbkdf2(password, Convert.FromBase64String(user.PasswordSalt));

        if (passwordHash != user.PasswordHash)
        {
            return null;
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUser(Guid id)
    {
        await using var context = new TicTacToeContext();
        var user = await context.Users.FindAsync(id);

        return _mapper.Map<UserDto>(user);
    }
}
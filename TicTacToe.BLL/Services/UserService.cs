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
    private readonly TicTacToeContext _context;
    public UserService(IMapper mapper, TicTacToeContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<UserDto> Register(LoginDto loginDto)
    {
        var emailExist = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        if (emailExist != null)
        {
            throw new Exception($"Email {emailExist.Email} already exists.");
        }

        var userEntity = _mapper.Map<User>(loginDto);

        var salt = PasswordHelper.GetSecureSalt();
        var passwordHash = PasswordHelper.HashUsingPbkdf2(loginDto.Password, salt);
        userEntity.PasswordSalt = Convert.ToBase64String(salt);
        userEntity.PasswordHash = passwordHash;

        var userCreated = await _context.Users.AddAsync(userEntity);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(userCreated.Entity);
    }

    public async Task<UserDto?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            throw new InvalidRequestException($"User email {email} or password are incorrect");
        }

        var passwordHash = PasswordHelper.HashUsingPbkdf2(password, Convert.FromBase64String(user.PasswordSalt));

        if (passwordHash != user.PasswordHash)
        {
            throw new InvalidRequestException($"User email {email} or password are incorrect");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserSearchDto>> Search(string part, Guid userId, CancellationToken cancellationToken)
    {
        var partLower = part.ToLowerInvariant();
        List<User> users = await _context.Users
            .Where(x => x.Id != userId && (
                x.Email.ToLower().Contains(partLower) || x.Name.ToLower().Contains(partLower))
            )
            .Take(30)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<UserSearchDto>>(users);
    }
}
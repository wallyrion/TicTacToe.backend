using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Models;

namespace TicTacToe.API;

public class UserService : IUserService
{
    public async Task Register(User user)
    {
        await using var context = new TicTacToeContext();
        var emailExist = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
        if (emailExist != null)
        {
            throw new Exception($"Email {emailExist.Email} already exists.");
        }
        
        var res = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> Login(string email, string password)
    {
        await using var context = new TicTacToeContext();
        var res = await context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

        return res;
    }
}
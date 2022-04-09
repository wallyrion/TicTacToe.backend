using Microsoft.Extensions.DependencyInjection;
using TicTacToe.BLL.Services;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.DAL;

namespace TicTacToe.BLL.Infrastructure;

public static class ServiceDependencies
{
    public static IServiceCollection RegisterBllDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<TicTacToeContext>();
        return services;
    }
}
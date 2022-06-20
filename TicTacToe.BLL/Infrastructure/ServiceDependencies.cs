using Microsoft.Extensions.DependencyInjection;
using TicTacToe.BLL.Services;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.DAL;

namespace TicTacToe.BLL.Infrastructure;

public static class ServiceDependencies
{
    public static IServiceCollection RegisterBllDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGameService, GameService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<IGameProcessService, GameProcessService>();

        services.AddScoped<TicTacToeContext>();
        return services;
    }
}
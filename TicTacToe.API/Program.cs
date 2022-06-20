using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TicTacToe.API.Middleware;
using TicTacToe.BLL.Helpers;
using TicTacToe.BLL.Infrastructure;
using TicTacToe.BLL.SignalR;
using TicTacToe.Common.Options;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.RegisterBllDependencies();
    builder.Services.AddSignalR();
    builder.Services.Configure<TicTacToeDbOptions>(builder.Configuration.GetRequiredSection(TicTacToeDbOptions.SectionName));
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = TokenHelper.Issuer,
                ValidAudience = TokenHelper.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret)),
                ClockSkew = TimeSpan.Zero
            };

        });

    builder.Services.AddAuthorization();

    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseCors(x =>
        x.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200", "http://192.168.68.120:4200", "https://wallyrion.github.io"));

    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseMiddleware<WebSocketsMiddleware>();


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<GameHub>("hubs/game");
    });

    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Error during app initialization");

}



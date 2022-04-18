using Microsoft.EntityFrameworkCore;
using TicTacToe.DAL.Entities;

namespace TicTacToe.DAL;

public sealed class TicTacToeContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // todo move to the config
        optionsBuilder.UseCosmos(
            "https://tictactoe-wallyrion.documents.azure.com:443/",
            "BQOkP3R4zgJbdHwAUxr2EjrTRNuoQovIC8Fi4rRhpFS1KtMi6tDGmPKTrs47e5vKsanHZQLykInYK8bPvq6Yyw==",
            databaseName: "TicTacToeDB");

        optionsBuilder
            .LogTo(Console.WriteLine)
            .EnableDetailedErrors();
    }

    public TicTacToeContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToContainer("Users")
                .HasPartitionKey(x => x.Email)
                .HasKey(x => x.Id)
                ;
            entity.OwnsMany(x => x.RefreshTokens);

        });
            

        modelBuilder.Entity<Game>()
            .ToContainer("Games")
            .HasPartitionKey(x => x.Id)
            .HasKey(x => x.Id);
        modelBuilder.Entity<Game>()
            .OwnsMany(x => x.Field);
        modelBuilder.Entity<Game>()
            .OwnsOne(x => x.Outcome);

    }
}
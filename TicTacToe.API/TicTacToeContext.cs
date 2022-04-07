using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Models;

namespace TicTacToe.API;

public sealed class TicTacToeContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
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

        #region PartitionKey

        modelBuilder.Entity<User>()
            .ToContainer("Users")
            .HasPartitionKey(x => x.Email)
            ;

        modelBuilder.Entity<Game>()
            .ToContainer("Games")
            .HasPartitionKey(x => x.Id)
            ;


        #endregion


        //#region DefaultContainer
        //modelBuilder.HasDefaultContainer("Store");
        //#endregion

        //#region Container
        //modelBuilder.Entity<Users>()
        //    .ToContainer("Orders");
        //#endregion

        //#region NoDiscriminator
        //modelBuilder.Entity<Order>()
        //    .HasNoDiscriminator();
        //#endregion

        //#region PartitionKey
        //modelBuilder.Entity<Order>()
        //    .HasPartitionKey(o => o.PartitionKey);
        //#endregion

        //#region ETag
        //modelBuilder.Entity<Order>()
        //    .UseETagConcurrency();
        //#endregion

        //#region PropertyNames
        //modelBuilder.Entity<Order>().OwnsOne(
        //    o => o.ShippingAddress,
        //    sa =>
        //    {
        //        sa.ToJsonProperty("Address");
        //        sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
        //        sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
        //    });
        //#endregion

        //#region OwnsMany
        //modelBuilder.Entity<Distributor>().OwnsMany(p => p.ShippingCenters);
        //#endregion

        //#region ETagProperty
        //modelBuilder.Entity<Distributor>()
        //    .Property(d => d.ETag)
        //    .IsETagConcurrency();
        //#endregion
    }
}
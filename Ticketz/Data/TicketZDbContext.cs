using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticketz.Models;

namespace Ticketz.Data;

public class TicketZDbContext : IdentityDbContext<ApplicationUser>
{
    public TicketZDbContext(DbContextOptions<TicketZDbContext> options):base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Actor_Movie>()
            .HasKey(e => new { e.ActorId, e.MovieId });
        modelBuilder.Entity<Actor_Movie>()
            .HasOne(e => e.Movie)
            .WithMany(e => e.Actor_Movie)
            .HasForeignKey(e => e.MovieId);
        modelBuilder.Entity<Actor_Movie>()
          .HasOne(e => e.Actor)
          .WithMany(e => e.Actor_Movie)
          .HasForeignKey(e => e.ActorId);
    }

    public DbSet<Actor_Movie> Actors_Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

}

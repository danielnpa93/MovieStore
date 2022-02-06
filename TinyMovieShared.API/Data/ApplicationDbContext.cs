using Microsoft.EntityFrameworkCore;
using TinyMovieShared.API.Models.Entities;

namespace TinyMovieShared.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<UserMovie> UsersMovies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.Role)
                .HasConversion(
                r => r.ToString(),
                r => (Role)Enum.Parse(typeof(Role), r));

            modelBuilder.Entity<User>()
              .Property(user => user.Status)
              .HasConversion(
              s => s.ToString(),
              s => (Status)Enum.Parse(typeof(Status), s));

            modelBuilder.Entity<User>()
               .HasIndex(user => user.Username)
               .IsUnique();

            modelBuilder.Entity<UserMovie>()
                .HasKey(um => new { um.UserId, um.MovieId });
        }
    }
}

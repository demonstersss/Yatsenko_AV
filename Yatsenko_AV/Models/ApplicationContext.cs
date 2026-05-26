using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
namespace Yatsenko_AV.Models
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Game> Games { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Developer> Developers { get; set; }
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Game>()
				.HasOne(g => g.Genre)
				.WithMany()
				.HasForeignKey(g => g.GenreId);
			modelBuilder.Entity<Game>()
				.HasOne(g => g.Developer)
				.WithMany()
				.HasForeignKey(g => g.DeveloperId);
			modelBuilder.Entity<Order>()
				.HasOne(o => o.Game)
				.WithMany()
				.HasForeignKey(o => o.GameId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>().HasData(new User
			{
				Id = 1,
				Username = "admin",
				PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"), 
				Role = "Admin"
			});
		}
	}
}

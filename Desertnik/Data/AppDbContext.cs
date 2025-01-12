using Microsoft.EntityFrameworkCore;

namespace Desertnik.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) :
		base(options)
		{ }
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var demoUsers = new User[]
			{
				new User{ Id = "8867b3f8-a8ff-40b1-bafd-f103d66b2fe8", Username="admin", Password="AQAAAAIAAYagAAAAEHn3H/jVqPfpAfdw00S/kzKmiS7qSKGh1/IAOtWol/384+fqt/PNaF+AaRBxN8A9Hw==", Role="Admin" },
				new User{ Id = "d762dfdd-47bd-4942-b389-12495a89d6b0", Username="test1", Password="AQAAAAIAAYagAAAAEGM8412cv8MJUrmhnabNXyVGKdCREDfM4stRrB6pIo4ujeaEuuC4NtXE0zRTzI6wNA==", Role="User" }
			};

			modelBuilder.Entity<User>().HasData(demoUsers);

		}
	}
}
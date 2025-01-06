namespace Desertnik.Data
{
	public class Review
	{
		public int Id { get; set; }
		public Recipe Recipe { get; set; }
		public User User { get; set; } // Korisnik koji je ocijenio recept
		public double Score { get; set; }
	}
}
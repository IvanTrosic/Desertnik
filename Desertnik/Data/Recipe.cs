namespace Desertnik.Data
{
	public class Recipe
	{
		public string Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<Ingredient> Ingredients { get; set; } = [];
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
		public User User { get; set; } // Korisnik koji je napisao recept
		public string ImageUrl { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/9/95/Nema_slike.png";
	}
}
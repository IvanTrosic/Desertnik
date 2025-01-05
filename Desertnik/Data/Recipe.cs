namespace Desertnik.Data
{
	public class Recipe
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Ingredient> Ingredients { get; set; } = [];
	}
}

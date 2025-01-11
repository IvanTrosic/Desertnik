namespace Desertnik.Data
{
	public class Ingredient
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public List<Recipe> Recipes { get; set; } = [];
	}
}
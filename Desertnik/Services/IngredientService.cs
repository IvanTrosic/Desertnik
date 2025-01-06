using Desertnik.Data;
using Microsoft.EntityFrameworkCore;

namespace Desertnik.Services
{
	public class IngredientService
	{
		AppDbContext _context;
		public IngredientService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Ingredient>> GetIngredientsAsync()
		{
			var result = _context.Ingredients;
			return await Task.FromResult(result.ToList());
		}
		public async Task<Ingredient> GetIngredientByIdAsync(string id)
		{
			return await _context.Ingredients.FindAsync(id);
		}
		public async Task<Ingredient> InsertIngredientAsync(Ingredient ingredient)
		{
			_context.Ingredients.Add(ingredient);
			await _context.SaveChangesAsync();
			return ingredient;
		}
		public async Task<Ingredient> UpdateIngredientAsync(string id, Ingredient i)
		{
			_context.Ingredients.Update(i);
			await _context.SaveChangesAsync();
			return i;
		}
		public async Task<Ingredient> DeleteIngredientAsync(string id)
		{
			var ingredient = await _context.Ingredients.FindAsync(id);
			if (ingredient == null)
				return null;
			_context.Ingredients.Remove(ingredient);
			await _context.SaveChangesAsync();
			return ingredient;
		}
		private bool IngredientExists(string id)
		{
			return _context.Ingredients.Any(e => e.Id == id);
		}
	}
}

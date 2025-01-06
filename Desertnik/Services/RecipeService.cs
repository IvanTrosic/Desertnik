using Desertnik.Data;
using Microsoft.EntityFrameworkCore;

namespace Desertnik.Services
{
	public class RecipeService
	{
		AppDbContext _context;
		public RecipeService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Recipe>> GetRecipesAsync()
		{
			var result = _context.Recipes.Include(recipe => recipe.Ingredients);
			return await Task.FromResult(result.ToList());
		}
		public async Task<Recipe> GetRecipeByIdAsync(string id)
		{
			return await _context.Recipes.FindAsync(id);
		}
		public async Task<Recipe> InsertRecipeAsync(Recipe recipe)
		{
			_context.Recipes.Add(recipe);
			await _context.SaveChangesAsync();
			return recipe;
		}
		public async Task<Recipe> UpdateRecipeAsync(string id, Recipe r)
		{
			var recipe = await _context.Recipes.FindAsync(id);
			if (recipe == null)
				return null;
			recipe.Name = r.Name;
			recipe.Description = r.Description;
			_context.Recipes.Update(recipe);
			await _context.SaveChangesAsync();
			return recipe;
		}
		public async Task<Recipe> DeleteRecipeAsync(string id)
		{
			var recipe = await _context.Recipes.FindAsync(id);
			if (recipe == null)
				return null;
			_context.Recipes.Remove(recipe);
			await _context.SaveChangesAsync();
			return recipe;
		}
		private bool RecipeExists(string id)
		{
			return _context.Recipes.Any(e => e.Id == id);
		}

		public async Task<List<Recipe>> SearchRecipesAsync(string? name, DateTime? fromDate, DateTime? toDate, List<string>? ingredientIds, double? minScore)
		{
			var query = _context.Recipes.AsQueryable();

			// Po nazivu
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(r => r.Name.Contains(name));
			}

			// Po datumu
			if (fromDate.HasValue)
			{ 
				query = query.Where(r => r.CreatedDate >= fromDate.Value);
			}
			if (toDate.HasValue)
			{ 
				query = query.Where(r => r.CreatedDate <= toDate.Value);
			}

			// Po sastojcima
			if (ingredientIds != null && ingredientIds.Any())
			{
				query = query.Where(r => r.Ingredients.Any(i => ingredientIds.Contains(i.Id)));
			}

			// Po ocjenama
			if (minScore.HasValue)
			{
				query = query.Where(r => r.AverageScore >= minScore.Value);
			}

			return await query.Include(r => r.Ingredients).ToListAsync();
		}
	}
}

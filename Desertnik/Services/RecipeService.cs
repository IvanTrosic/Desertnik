using Desertnik.Data;
using Desertnik.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
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
			var result = _context.Recipes.Include(recipe => recipe.Ingredients).Include(recipe => recipe.User);
			return await Task.FromResult(result.ToList());
		}
		public async Task<Recipe> GetRecipeByIdAsync(string id)
		{
			return await _context.Recipes.Include(recipe => recipe.User).FirstOrDefaultAsync(r => r.Id == id);
		}
		public async Task<Recipe> InsertRecipeAsync(RecipeModel recipe, List<Ingredient> ingredients, AuthenticationStateProvider authenticationStateProvider)
		{

			var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
			var claimsPrincipal = authState.User;
			if (claimsPrincipal.Identity is not null && claimsPrincipal.Identity.IsAuthenticated)
			{
				var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == claimsPrincipal.Identity.Name);

				var newRecipe = new Recipe
				{
					Id = Guid.NewGuid().ToString(),
					Name = recipe.Name,
					Description = recipe.Description,
					Ingredients = ingredients,
					User = user,
					ImageUrl = string.IsNullOrEmpty(recipe.ImageUrl) ? "https://upload.wikimedia.org/wikipedia/commons/9/95/Nema_slike.png" : recipe.ImageUrl
				};
				_context.Recipes.Add(newRecipe);
				await _context.SaveChangesAsync();
				return newRecipe;
			}
			else
			{
				return null;
			}
		}
		public async Task<Recipe> UpdateRecipeAsync(string id, Recipe r)
		{
			var recipe = await _context.Recipes.FindAsync(id);
			if (recipe == null)
				return null;
			recipe.Name = r.Name;
			recipe.Description = r.Description;
			recipe.Ingredients = r.Ingredients;
			recipe.ImageUrl = r.ImageUrl = string.IsNullOrEmpty(r.ImageUrl) ? "https://upload.wikimedia.org/wikipedia/commons/9/95/Nema_slike.png" : r.ImageUrl;
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

		public async Task<List<Recipe>> SearchRecipesAsync(string? name, DateTime? fromDate, DateTime? toDate, List<string>? ingredientIds)
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

			return await query.Include(r => r.Ingredients).Include(recipe => recipe.User).ToListAsync();
		}
	}
}
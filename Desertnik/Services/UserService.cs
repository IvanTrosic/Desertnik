using Desertnik.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Desertnik.Services
{
	public class UserService
	{
		AppDbContext _context;
		public UserService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ClaimsPrincipal> RegisterAsync(string username, string password)
		{
			if (_context.Users.Any(u => u.Username == username))
				return null; // Korisnik već postoji

			_context.Users.Add(new User
			{
				Id = Guid.NewGuid().ToString(),
				Username = username,
				Password = new PasswordHasher<string>().HashPassword(username, password),
				Role = "User"
			});

			await _context.SaveChangesAsync();

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.Role, "User")
			};

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			return principal;
		}

		public async Task<ClaimsPrincipal?> LoginAsync(string username, string password)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
			if (user == null || new PasswordHasher<string>().VerifyHashedPassword(username, user.Password, password) == PasswordVerificationResult.Failed)
			{
				return null;
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			return principal;
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task UpdateUserAsync(string userId, string username, string role)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
				user.Username = username;
				user.Role = role;
				_context.Users.Update(user);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DeleteUserAsync(string userId)
		{
			var userRecipes = _context.Recipes.Where(r => r.User.Id == userId);

			_context.Recipes.RemoveRange(userRecipes);

			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
				return;

			_context.Users.Remove(user);

			await _context.SaveChangesAsync();

			return;
		}

	}
}
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

		public async Task<bool> RegisterAsync(string username, string password)
		{
			if (_context.Users.Any(u => u.Username == username))
				return false; // Korisnik već postoji

			_context.Users.Add(new User
			{
				Id = Guid.NewGuid().ToString(),
				Username = username,
				Password = new PasswordHasher<string>().HashPassword(username, password),
				Role = "User"
			});

			await _context.SaveChangesAsync();
			return true;
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
			var user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
				_context.Users.Remove(user);
				await _context.SaveChangesAsync();
			}
		}
	}
}
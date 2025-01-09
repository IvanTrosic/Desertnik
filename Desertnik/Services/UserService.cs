using Desertnik.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

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
			if (user == null || new PasswordHasher<string>().VerifyHashedPassword(username, user.Password, password) == PasswordVerificationResult.Failed) { 
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
	}


}

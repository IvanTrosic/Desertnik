﻿namespace Desertnik.Data
{
	public class User
	{
		public string Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Role { get; set; } = "User"; // User ili Admin
		public List<Review> Reviews { get; set; }
	}
}
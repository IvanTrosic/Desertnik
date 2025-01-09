using System.ComponentModel.DataAnnotations;

namespace Desertnik.Data
{
	public class AccessModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Korisničko ime je obavezno")]
		public string? Username { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Lozinka je obavezna")]
		public string? Password { get; set; }
	}
}

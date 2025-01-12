using System.ComponentModel.DataAnnotations;

namespace Desertnik.Data.Models
{
	public class AccessModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Korisničko ime je obavezno")]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "Korisničko ime mora imati između 5 i 20 znakova")]
		public string? Username { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Lozinka je obavezna")]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$",
		ErrorMessage = "Lozinka mora imati najmanje 8 znakova, sadržavati velika i mala slova, brojeve i posebne znakove")] // https://stackoverflow.com/a/48636105
		public string? Password { get; set; }
	}
}
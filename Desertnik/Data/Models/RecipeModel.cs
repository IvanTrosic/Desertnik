using System.ComponentModel.DataAnnotations;

namespace Desertnik.Data.Models
{
	public class RecipeModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Naziv recepta je obavezan")]
		public string? Name { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Opis recepta je obavezan")]
		public string? Description { get; set; }
	}
}
using System.ComponentModel.DataAnnotations;

namespace Yatsenko_AV.Models
{
	public class Genre
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Жанр")]
		[Required]
		public string Name { get; set; } = null!;

	}
}

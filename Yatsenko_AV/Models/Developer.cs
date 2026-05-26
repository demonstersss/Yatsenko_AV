using System.ComponentModel.DataAnnotations;

namespace Yatsenko_AV.Models
{
	public class Developer
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Разработчик")]
		[Required]
		public string Name { get; set; } = null!;

	}
}

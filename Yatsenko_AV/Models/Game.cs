using System.ComponentModel.DataAnnotations;

namespace Yatsenko_AV.Models
{
	public class Game
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Название игры")]
		[Required(ErrorMessage = "Название обязательно")]
		public string Name { get; set; } = null!;

		[Display(Name = "Год выхода")]
		[Range(1950, 2026, ErrorMessage = "Некорректный год")]
		public int Year { get; set; }

		[Display(Name = "Жанр")]
		public int GenreId { get; set; }
		public Genre? Genre { get; set; }

		[Display(Name = "Разработчик")]
		public int DeveloperId { get; set; }
		public Developer? Developer { get; set; }
	}
}

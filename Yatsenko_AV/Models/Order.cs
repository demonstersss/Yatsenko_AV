using System;
using System.ComponentModel.DataAnnotations;

namespace Yatsenko_AV.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Игра")]
		[Required(ErrorMessage = "Выбор игры обязателен")]
		public int GameId { get; set; }
		public Game? Game { get; set; }

		[Display(Name = "Имя покупателя")]
		[Required(ErrorMessage = "Имя покупателя обязательно")]
		[StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
		public string CustomerName { get; set; } = string.Empty;

		[Display(Name = "Дата заказа")]
		public DateTime OrderDate { get; set; } = DateTime.UtcNow;

		[Display(Name = "Количество")]
		[Range(1, 100, ErrorMessage = "Количество должно быть от 1 до 100")]
		public int Quantity { get; set; } = 1;
	}
}
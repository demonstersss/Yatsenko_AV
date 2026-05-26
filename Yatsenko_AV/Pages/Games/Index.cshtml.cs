using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Games
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationContext _context;
		public List<Game> Games { get; set; } = new();

		public string? StatusMessage { get; set; }

		public IndexModel(ApplicationContext context) => _context = context;

		public async Task OnGetAsync()
		{
			Games = await _context.Games
				.Include(g => g.Genre)
				.Include(g => g.Developer)
				.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			if (!User.IsInRole("Admin")) return Forbid();

			var game = await _context.Games.FindAsync(id);
			if (game != null)
			{
				_context.Games.Remove(game);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostMakeOrderAsync(int gameId)
		{
			var game = await _context.Games.FindAsync(gameId);
			if (game == null) return NotFound();

			var username = User.Identity?.Name ?? "Гость";

			var order = new Order
			{
				GameId = gameId,
				CustomerName = username,
				OrderDate = DateTime.UtcNow,
				Quantity = 1
			};

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			StatusMessage = $"Успешно! Оформлен предзаказ на игру \"{game.Name}\".";
			return RedirectToPage();
		}
	}
}
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
			var game = await _context.Games.FindAsync(id);
			if (game != null)
			{
				_context.Games.Remove(game);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage();
		}
	}
}
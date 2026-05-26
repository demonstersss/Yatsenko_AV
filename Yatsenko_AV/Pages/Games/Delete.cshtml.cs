using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Games
{
	[Authorize]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationContext _context;

		public DeleteModel(ApplicationContext context) => _context = context;

		[BindProperty]
		public Game Game { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Game = await _context.Games
				.Include(g => g.Genre)
				.Include(g => g.Developer)
				.FirstOrDefaultAsync(g => g.Id == id);

			if (Game == null) return NotFound();

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int id)
		{
			var game = await _context.Games.FindAsync(id);
			if (game != null)
			{
				_context.Games.Remove(game);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage("Index");
		}
	}
}
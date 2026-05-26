using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Games
{
	[Authorize]
	public class EditModel : PageModel
	{
		private readonly ApplicationContext _context;

		public EditModel(ApplicationContext context) => _context = context;

		[BindProperty]
		public Game Game { get; set; } = new();

		public SelectList Genres { get; set; } = default!;
		public SelectList Developers { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Game = await _context.Games.FindAsync(id) ?? new Game();
			if (Game.Id == 0) return NotFound();

			Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name", Game.GenreId);
			Developers = new SelectList(await _context.Developers.ToListAsync(), "Id", "Name", Game.DeveloperId);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				await OnGetAsync(Game.Id);
				return Page();
			}

			_context.Games.Update(Game);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
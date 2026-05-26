using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Games
{
	[Authorize]
	public class CreateModel : PageModel
	{
		private readonly ApplicationContext _context;

		public CreateModel(ApplicationContext context) => _context = context;

		[BindProperty]
		public Game Game { get; set; } = new();

		public SelectList Genres { get; set; } = default!;
		public SelectList Developers { get; set; } = default!;

		public async Task OnGetAsync()
		{
			Genres = new SelectList(await _context.Genres.ToListAsync(), "Id", "Name");
			Developers = new SelectList(await _context.Developers.ToListAsync(), "Id", "Name");
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				await OnGetAsync();
				return Page();
			}

			_context.Games.Add(Game);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
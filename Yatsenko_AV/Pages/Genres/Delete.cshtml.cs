using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Genres
{
	[Authorize(Roles = "Admin")]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationContext _context;

		public DeleteModel(ApplicationContext context) => _context = context;

		[BindProperty]
		public Genre Genre { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Genre = await _context.Genres.FindAsync(id);
			if (Genre == null) return NotFound();
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int id)
		{
			var genre = await _context.Genres.FindAsync(id);
			if (genre != null)
			{
				_context.Genres.Remove(genre);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage("Index");
		}
	}
}
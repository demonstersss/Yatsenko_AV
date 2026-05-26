using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Genres
{
	[Authorize(Roles = "Admin")]
	public class EditModel : PageModel
	{
		private readonly ApplicationContext _context;

		public EditModel(ApplicationContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Genre Genre { get; set; } = new();

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Genre = await _context.Genres.FindAsync(id);

			if (Genre == null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Genres.Update(Genre);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
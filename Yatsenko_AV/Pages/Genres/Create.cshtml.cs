using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Genres
{
	[Authorize(Roles = "Admin")]
	public class CreateModel : PageModel
	{
		private readonly ApplicationContext _context;

		public CreateModel(ApplicationContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Genre Genre { get; set; } = new();

		public void OnGet() { }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Genres.Add(Genre);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
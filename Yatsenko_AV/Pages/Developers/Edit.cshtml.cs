using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Developers
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
		public Developer Developer { get; set; } = new();

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Developer = await _context.Developers.FindAsync(id);

			if (Developer == null)
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

			_context.Developers.Update(Developer);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
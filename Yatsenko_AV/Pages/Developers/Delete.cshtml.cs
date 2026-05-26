using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Developers
{
	[Authorize(Roles = "Admin")]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationContext _context;

		public DeleteModel(ApplicationContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Developer Developer { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Developer = await _context.Developers.FindAsync(id);

			if (Developer == null)
			{
				return NotFound();
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int id)
		{
			var developer = await _context.Developers.FindAsync(id);

			if (developer != null)
			{
				_context.Developers.Remove(developer);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("Index");
		}
	}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Developers
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
		public Developer Developer { get; set; } = new();

		public void OnGet() { }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Developers.Add(Developer);
			await _context.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Orders
{
	[Authorize]
	public class CreateModel : PageModel
	{
		private readonly ApplicationContext _context;

		public CreateModel(ApplicationContext context) => _context = context;

		[BindProperty]
		public Order Order { get; set; } = new();

		public SelectList GamesList { get; set; } = null!;

		public async Task<IActionResult> OnGetAsync(int? gameId)
		{
			GamesList = new SelectList(await _context.Games.ToListAsync(), "Id", "Name");

			if (gameId.HasValue)
			{
				Order.GameId = gameId.Value;
			}

			if (User.IsInRole("User"))
			{
				Order.CustomerName = User.Identity?.Name ?? "User";
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (User.IsInRole("User"))
			{
				Order.CustomerName = User.Identity?.Name ?? "User";
				ModelState.Remove("Order.CustomerName");
			}

			if (!ModelState.IsValid)
			{
				GamesList = new SelectList(await _context.Games.ToListAsync(), "Id", "Name");
				return Page();
			}

			_context.Orders.Add(Order);
			await _context.SaveChangesAsync();

			if (User.IsInRole("Admin"))
			{
				return RedirectToPage("./Index");
			}

			return RedirectToPage("/Games/Index");
		}
	}
}
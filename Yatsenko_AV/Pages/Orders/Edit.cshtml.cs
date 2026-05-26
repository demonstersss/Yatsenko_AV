using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Orders
{
	[Authorize(Roles = "Admin")]
	public class EditModel : PageModel
	{
		private readonly ApplicationContext _context;

		[BindProperty]
		public Order Order { get; set; } = null!;

		public SelectList GamesList { get; set; } = null!;

		public EditModel(ApplicationContext context) => _context = context;

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Order = await _context.Orders.FindAsync(id);
			if (Order == null) return NotFound();

			// Формируем выпадающий список игр для возможности изменения привязки
			GamesList = new SelectList(await _context.Games.ToListAsync(), "Id", "Name");
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				GamesList = new SelectList(await _context.Games.ToListAsync(), "Id", "Name");
				return Page();
			}

			_context.Attach(Order).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Orders.Any(o => o.Id == Order.Id)) return NotFound();
				throw;
			}

			return RedirectToPage("./Index");
		}
	}
}
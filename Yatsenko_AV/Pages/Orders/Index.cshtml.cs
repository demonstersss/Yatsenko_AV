using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Orders
{
	[Authorize(Roles = "Admin")] 
	public class IndexModel : PageModel
	{
		private readonly ApplicationContext _context;
		public List<Order> Orders { get; set; } = new();

		public IndexModel(ApplicationContext context) => _context = context;

		public async Task OnGetAsync()
		{
			Orders = await _context.Orders
				.Include(o => o.Game)
				.ToListAsync();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			var order = await _context.Orders.FindAsync(id);
			if (order != null)
			{
				_context.Orders.Remove(order);
				await _context.SaveChangesAsync();
			}
			return RedirectToPage();
		}
	}
}
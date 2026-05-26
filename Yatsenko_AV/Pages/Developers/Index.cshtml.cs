using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Developers
{
	[Authorize(Roles = "Admin")]
	public class IndexModel : PageModel
	{
		private readonly ApplicationContext _context;

		public IndexModel(ApplicationContext context)
		{
			_context = context;
		}

		public List<Developer> Developers { get; set; } = new();

		public async Task OnGetAsync()
		{
			Developers = await _context.Developers.ToListAsync();
		}
	}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Genres
{
	[Authorize(Roles = "Admin")]
	public class IndexModel : PageModel
	{
		private readonly ApplicationContext _context;

		public IndexModel(ApplicationContext context)
		{
			_context = context;
		}

		public List<Genre> Genres { get; set; } = new();

		public async Task OnGetAsync()
		{
			Genres = await _context.Genres.ToListAsync();
		}
	}
}
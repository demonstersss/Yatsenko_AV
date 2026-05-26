using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Genres
{
	[Authorize(Roles = "Admin")]
	public class DetailsModel : PageModel
    {
        private readonly Yatsenko_AV.Models.ApplicationContext _context;

        public DetailsModel(Yatsenko_AV.Models.ApplicationContext context)
        {
            _context = context;
        }

        public Genre Genre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(m => m.Id == id);

            if (genre is not null)
            {
                Genre = genre;

                return Page();
            }

            return NotFound();
        }
    }
}

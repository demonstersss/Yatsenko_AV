using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Developers
{
	[Authorize(Roles = "Admin")]
	public class DetailsModel : PageModel
    {
        private readonly Yatsenko_AV.Models.ApplicationContext _context;

        public DetailsModel(Yatsenko_AV.Models.ApplicationContext context)
        {
            _context = context;
        }

        public Developer Developer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developer = await _context.Developers.FirstOrDefaultAsync(m => m.Id == id);

            if (developer is not null)
            {
                Developer = developer;

                return Page();
            }

            return NotFound();
        }
    }
}

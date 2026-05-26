using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Games
{
	[Authorize]
	public class DetailsModel : PageModel
    {
        private readonly Yatsenko_AV.Models.ApplicationContext _context;

        public DetailsModel(Yatsenko_AV.Models.ApplicationContext context)
        {
            _context = context;
        }

        public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);

            if (game is not null)
            {
                Game = game;

                return Page();
            }

            return NotFound();
        }
    }
}

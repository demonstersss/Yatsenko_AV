using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly ApplicationContext _context;
    public DetailsModel(ApplicationContext context)
    {
        _context = context;
    }

    public Order Order { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
        if (order is null)
        {
            return NotFound();
        }
        else
        {
            Order = order;
        }

        return Page();
    }
}

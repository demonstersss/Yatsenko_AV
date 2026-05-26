using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Controllers
{
	[ApiController]
	[Route("api/[controller]")] 
	[Authorize] 
	public class OrdersRESTController : ControllerBase
	{
		private readonly ApplicationContext _context;

		public OrdersRESTController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		{
			return await _context.Orders
				.Include(o => o.Game)
				.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrder(int id)
		{
			var order = await _context.Orders
				.Include(o => o.Game)
				.FirstOrDefaultAsync(o => o.Id == id);

			if (order == null)
			{
				return NotFound(new { message = $"Заказ с ID {id} не найден." });
			}

			return order;
		}

		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var gameExists = await _context.Games.AnyAsync(g => g.Id == order.GameId);
			if (!gameExists)
			{
				return BadRequest(new { message = "Указанная игра не существует." });
			}

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
		{
			if (id != order.Id)
			{
				return BadRequest(new { message = "ID в запросе не совпадает с ID объекта." });
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Entry(order).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(id))
				{
					return NotFound(new { message = $"Заказ с ID {id} не найден для обновления." });
				}
				throw;
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var order = await _context.Orders.FindAsync(id);
			if (order == null)
			{
				return NotFound(new { message = $"Заказ с ID {id} не найден для удаления." });
			}

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool OrderExists(int id)
		{
			return _context.Orders.Any(e => e.Id == id);
		}
	}
}
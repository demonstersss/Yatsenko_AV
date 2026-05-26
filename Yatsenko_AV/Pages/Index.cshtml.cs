using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationContext _context;
		private readonly ILogger<IndexModel> _logger;
		public int GameCount { get; set; }

		public IndexModel(ApplicationContext context, ILogger<IndexModel> logger)
		{
			_context = context;
			_logger = logger;
		}
		public void OnGet()
		{
			_logger.LogInformation("Загрузка главной страницы. Текущее кол-во игр: {GameCount}",
				_context.Games.Count());
			// throw new Exception();
			GameCount = _context.Games.Count();
			if (!_context.Games.Any())
			{
				var faker = new Faker("ru");

				var genres = new List<Genre>
				{
					new Genre { Name = "RPG" }, new Genre { Name = "Шутер" },
					new Genre { Name = "Стратегия" }, new Genre { Name = "Приключения" }
				};
						_context.Genres.AddRange(genres);
						_context.SaveChanges();

						var developers = new List<Developer>
				{
					new Developer { Name = "CD Projekt RED" }, new Developer { Name = "Valve" },
					new Developer { Name = "Ubisoft" }, new Developer { Name = "Rockstar Games" }
				};
				_context.Developers.AddRange(developers);
				_context.SaveChanges();

				var games = new Faker<Game>("ru")
					.RuleFor(g => g.Name, f => f.Commerce.ProductName())
					.RuleFor(g => g.Year, f => f.Random.Number(1995, 2026))
					.RuleFor(g => g.GenreId, f => f.PickRandom(genres).Id)
					.RuleFor(g => g.DeveloperId, f => f.PickRandom(developers).Id)
					.Generate(15);

				_context.Games.AddRange(games);
				_context.SaveChanges();
			}
			_logger.LogInformation("Seeding завершён успешно.");
		}
	}
}

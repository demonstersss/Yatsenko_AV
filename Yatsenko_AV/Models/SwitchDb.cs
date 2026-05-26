using Microsoft.EntityFrameworkCore;

namespace Yatsenko_AV.Models
{
	public class SwitchDb
	{
		public SwitchDb(WebApplicationBuilder app)
		{
			string? DefaultDb = app.Configuration["DefaultDb"];
			string? connection = app.Configuration.GetConnectionString(DefaultDb!);

			switch (DefaultDb)
			{
				case "SQlite":
					app.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
					break;
				case "Postgres":
					app.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
					break;
				case "MSSQL":
					app.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
					break;
				case "MySQL":
					app.Services.AddDbContext<ApplicationContext>(options => options.UseMySql(connection, ServerVersion.Parse("8.0.36"))); break;		
				default:
					app.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
					break;
			}
		}
	}
}

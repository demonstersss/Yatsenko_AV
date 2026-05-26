using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Yatsenko_AV.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.");

// builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connectionString));

var db = new SwitchDb(builder);

builder.Services.AddControllers();
builder.Services.AddRazorPages();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)  
	.Enrich.FromLogContext()
	.CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Account/Login";
		options.LogoutPath = "/Account/Logout";
		options.AccessDeniedPath = "/Account/AccessDenied";
		options.Cookie.Name = "YatsenkoAuth";
		options.ExpireTimeSpan = TimeSpan.FromHours(8);
		options.SlidingExpiration = true;
		options.Events = new CookieAuthenticationEvents
		{
			OnRedirectToLogin = context =>
			{
				context.Response.Redirect(context.RedirectUri);
				return Task.CompletedTask;
			},
			OnRedirectToAccessDenied = context =>
			{
				context.Response.Redirect(context.RedirectUri);
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
	app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");

	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
app.MapRazorPages()
   .WithStaticAssets();

Log.Information("Приложение Yatsenko_AV запущено.",
 app.Environment.EnvironmentName);

app.Run();


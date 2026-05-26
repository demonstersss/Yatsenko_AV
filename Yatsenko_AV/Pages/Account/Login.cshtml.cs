using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Account
{
	public class LoginModel : PageModel
	{
		private readonly ApplicationContext _context;

		public LoginModel(ApplicationContext context)
		{
			_context = context;
		}

		[BindProperty]
		public string Username { get; set; } = "";

		[BindProperty]
		public string Password { get; set; } = "";

		public string? ErrorMessage { get; set; }

		public void OnGet() { }

		public async Task<IActionResult> OnPostAsync()
		{
			var user = _context.Users.FirstOrDefault(u => u.Username == Username);

			if (user == null || !BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
			{
				ErrorMessage = "Неверный логин или пароль";
				return Page();
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, user.Role ?? "User")
			};

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

			return RedirectToPage("/Index");
		}
	}
}
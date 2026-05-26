using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yatsenko_AV.Models;

namespace Yatsenko_AV.Pages.Account
{
	public class RegisterModel : PageModel
	{
		private readonly ApplicationContext _context;

		public RegisterModel(ApplicationContext context)
		{
			_context = context;
		}

		[BindProperty]
		public string Username { get; set; } = "";

		[BindProperty]
		public string Password { get; set; } = "";

		[BindProperty]
		public string ConfirmPassword { get; set; } = "";

		public string? ErrorMessage { get; set; }
		public string? SuccessMessage { get; set; }

		public void OnGet() { }

		public async Task<IActionResult> OnPostAsync()
		{
			if (Password != ConfirmPassword)
			{
				ErrorMessage = "Пароли не совпадают";
				return Page();
			}

			if (_context.Users.Any(u => u.Username == Username))
			{
				ErrorMessage = "Пользователь с таким именем уже существует";
				return Page();
			}

			var user = new User
			{
				Username = Username,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
				Role = "User"
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			SuccessMessage = "Регистрация прошла успешно! Теперь вы можете войти.";
			return RedirectToPage("/Account/Login");
		}
	}
}
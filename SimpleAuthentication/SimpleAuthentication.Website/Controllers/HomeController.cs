#region References

using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleAuthentication.Website.Models;
using SimpleAuthentication.Website.Services;

#endregion

namespace SimpleAuthentication.Website.Controllers
{
	public class HomeController : BaseController
	{
		#region Fields

		private readonly ILogger<HomeController> _logger;

		#endregion

		#region Constructors

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		#endregion

		#region Methods

		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult LogIn(string returnUrl)
		{
			if (IsAuthenticated)
			{
				return Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
			}

			return View(new Credentials());
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> LogIn(Credentials model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("userName", Constants.LoginInvalidError);
				return View(model);
			}

			var accountService = new AccountService();
			var user = accountService.AuthenticateUser(model);

			if (user == null)
			{
				ModelState.AddModelError("userName", Constants.LoginInvalidError);
				return View(model);
			}

			var claims = new List<Claim> { new Claim(ClaimTypes.Name, user) };
			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				new AuthenticationProperties { IsPersistent = model.RememberMe });

			return Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
		}

		public async Task<ActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("/");
		}
		
		#endregion
	}
}
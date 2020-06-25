#region References

using System.Diagnostics;
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

			_logger.LogCritical("HERE");
		}

		#endregion

		#region Methods

		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult LogIn(string returnUrl)
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
		public async Task<IActionResult> LogIn(Credentials model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("userName", Constants.LoginInvalidError);
				return View(model);
			}

			var account = BasicAuthenticationHandler.Validate(model);

			if (account == null)
			{
				ModelState.AddModelError("userName", Constants.LoginInvalidError);
				return View(model);
			}

			var ticket = BasicAuthenticationHandler.CreateTicket(account, model.RememberMe, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(ticket.Principal, ticket.Properties);

			return Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
		}

		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect("/");
		}
		
		#endregion
	}
}
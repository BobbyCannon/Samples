#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleAuthentication.Website.Models;
using SimpleAuthentication.Website.ViewModels;

#endregion

namespace SimpleAuthentication.Website.Services
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		#region Constants

		public const string AuthenticationScheme = "BasicAuthentication";

		#endregion

		#region Constructors

		public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		#endregion

		#region Methods

		public static AuthenticationTicket CreateTicket(Account account, in bool rememberMe, string authenticationType)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, account.Id),
				new Claim(ClaimTypes.Name, account.UserName)
			};

			claims.AddRange(account.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var identity = new ClaimsIdentity(claims, authenticationType);
			var principal = new ClaimsPrincipal(identity);
			var ticket = new AuthenticationTicket(principal, authenticationType);
			ticket.Properties.IsPersistent = rememberMe;

			return ticket;
		}

		public static Account Validate(Credentials credentials)
		{
			switch (credentials.EmailAddress)
			{
				case "admin@domain.com" when credentials.Password == "123456":
					return new Account { Id = "1", Roles = new[] { "administrator" }, UserName = credentials.EmailAddress };
				
				case "editor@domain.com" when credentials.Password == "123456":
					return new Account { Id = "2", Roles = new[] { "editor" }, UserName = credentials.EmailAddress };

				case "user@domain.com" when credentials.Password == "123456":
					return new Account { Id = "3", Roles = new string[0], UserName = credentials.EmailAddress };

				default:
					return null;
			}
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (Request.HttpContext.User.Identity.IsAuthenticated || !Request.Headers.ContainsKey("Authorization"))
			{
				return Task.FromResult(AuthenticateResult.NoResult());
			}

			try
			{
				var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
				var username = credentials[0];
				var password = credentials[1];

				var account = Validate(new Credentials { EmailAddress = username, Password = password });
				var ticket = CreateTicket(account, true, AuthenticationScheme);
				return Task.FromResult(AuthenticateResult.Success(ticket));
			}
			catch
			{
				return Task.FromResult(AuthenticateResult.NoResult());
			}
		}

		#endregion
	}
}
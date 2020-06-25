#region References

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

#endregion

namespace SimpleAuthentication.Website.Services
{
	[ExcludeFromCodeCoverage]
	public class RoleAuthorizeAttribute : AuthorizeAttribute
	{
		#region Constructors

		public RoleAuthorizeAttribute(params UserRoles[] roles)
		{
			Roles = string.Join(",", roles).ToLower();
			AuthenticationSchemes = $"{BasicAuthenticationHandler.AuthenticationScheme},{CookieAuthenticationDefaults.AuthenticationScheme}";
		}

		public RoleAuthorizeAttribute(params string[] roles)
		{
			Roles = string.Join(",", roles).ToLower();
			AuthenticationSchemes = $"{BasicAuthenticationHandler.AuthenticationScheme},{CookieAuthenticationDefaults.AuthenticationScheme}";
		}

		#endregion
	}

	public enum UserRoles
	{
		Administrator,
		Editor
	}
}
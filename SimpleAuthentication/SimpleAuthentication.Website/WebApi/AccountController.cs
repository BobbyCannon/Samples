#region References

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuthentication.Website.Services;
using SimpleAuthentication.Website.ViewModels;

#endregion

namespace SimpleAuthentication.Website.WebApi
{
	[ApiController]
	public class AccountController : ControllerBase
	{
		#region Methods

		[HttpGet]
		[Route("api/Account")]
		[RoleAuthorize(UserRoles.Administrator,UserRoles.Editor)]
		//[Authorize(Roles = "administrator", AuthenticationSchemes = "Cookies")]
		public Account GetAccount()
		{
			return new Account
			{
				UserName = Request.HttpContext.User.Identity.Name
			};
		}

		#endregion
	}
}
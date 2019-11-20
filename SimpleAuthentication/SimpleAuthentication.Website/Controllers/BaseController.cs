#region References

using Microsoft.AspNetCore.Mvc;

#endregion

namespace SimpleAuthentication.Website.Controllers
{
	public abstract class BaseController : Controller
	{
		#region Properties

		protected bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

		#endregion
	}
}
#region References

using System.Web.Mvc;

#endregion

namespace SimpleWebApi.Website.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

		#endregion
	}
}
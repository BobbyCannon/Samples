#region References

using System.Web.Mvc;

#endregion

namespace SimpleWebApi.Website
{
	public class FilterConfig
	{
		#region Methods

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new AuthorizeAttribute());
		}

		#endregion
	}
}
#region References

using System.Configuration;
using System.Web.Http;
using SimpleWebApi.Website.Attributes;

#endregion

namespace SimpleWebApi.Website
{
	public static class WebApiConfig
	{
		#region Methods

		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
			config.Filters.Add(new AuthorizeAttribute());
			config.Filters.Add(new AuthorizeAttribute());
			var test = int.Parse(ConfigurationManager.AppSettings["WebApiRequestPerSecond"]);
			config.Filters.Add(new ThrottleAttribute { RequestsPerSecond = test });

		}

		#endregion
	}
}
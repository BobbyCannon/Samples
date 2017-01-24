#region References

using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleWebApi.Data;
using SimpleWebApi.Data.Migrations;
using Speedy.EntityFramework;

#endregion

namespace SimpleWebApi.Website
{
	public class WebApiApplication : HttpApplication
	{
		#region Methods

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer(new MigrateDatabaseToLatestVersionByContext<EntityFrameworkContosoDatabase, Configuration>());

			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = Speedy.Extensions.GetSerializerSettings(true, false);
			GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
			GlobalConfiguration.Configuration.EnsureInitialized();

		}

		#endregion
	}
}
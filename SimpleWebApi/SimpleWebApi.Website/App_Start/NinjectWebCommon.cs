#region References

using System;
using System.Web;
using System.Web.Http;
using EpicCoders.Website;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.WebApi.DependencyResolver;
using SimpleWebApi.Data;
using WebActivatorEx;

#endregion

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace EpicCoders.Website
{
	public static class NinjectWebCommon
	{
		#region Fields

		private static readonly Bootstrapper _bootstrapper = new Bootstrapper();

		#endregion

		#region Methods

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			_bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			_bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns> The created kernel. </returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
				GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
				RegisterServices(kernel);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel"> The kernel. </param>
		private static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<IContosoDatabase>().To<EntityFrameworkContosoDatabase>().InRequestScope();
		}

		#endregion
	}
}
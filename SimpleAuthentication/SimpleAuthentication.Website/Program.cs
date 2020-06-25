#region References

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

#endregion

namespace SimpleAuthentication.Website
{
	public class Program
	{
		#region Methods

		/*
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE DATABASE [Sample]
GO
USE [Sample]
GO
CREATE TABLE [dbo].[NlogDBLog] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Application] [nvarchar](100) NOT NULL,
	[MachineName] [nvarchar](50) NOT NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](250) NULL,
	[Callsite] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
CONSTRAINT [PK_dbo.NlogDBLog] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
		 */

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			logger.Log(NLog.LogLevel.Info, "Startup");

			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.SetMinimumLevel(LogLevel.Trace);
				})
				.UseNLog();;
		}

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		#endregion
	}
}
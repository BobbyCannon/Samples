#region References

using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace SimpleAuthentication.Website
{
	public class Startup
	{
		#region Constructors

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		#endregion

		#region Properties

		public IConfiguration Configuration { get; }

		#endregion

		#region Methods

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.UseAuthentication();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

			services.AddMvc(options =>
			{
				options.Filters.Add(new AuthorizeFilter(policy));
				options.EnableEndpointRouting = false;
			});

			services.AddControllers();
			services.AddControllersWithViews();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					//options.Cookie.Name = ".ASPXAUTH";
					options.Cookie.Domain = "sample.local";
					options.Cookie.Path = "/";
					options.Cookie.SecurePolicy = CookieSecurePolicy.None;
					// you should be doing secure only but this is a sample
					//options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
					options.Cookie.SameSite = SameSiteMode.Strict;
					options.ExpireTimeSpan = TimeSpan.FromMinutes(16384);
					options.LoginPath = "/home/login";
					options.LogoutPath = "/home/logout";
					options.SlidingExpiration = true;
					// this is for backward compatibility
					//options.TicketDataFormat = new FormsAuthenticationDataFormat<AuthenticationTicket>(authenticationOptions,
					//	FormsAuthHelper.ConvertCookieToTicket, FormsAuthHelper.ConvertTicketToCookie);
				});
		}

		#endregion
	}
}
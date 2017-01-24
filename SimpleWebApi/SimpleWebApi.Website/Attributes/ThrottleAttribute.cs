#region References

using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

#endregion

namespace SimpleWebApi.Website.Attributes
{
	public class ThrottleAttribute : ActionFilterAttribute
	{
		/// <inheritdoc />
		public override bool AllowMultiple => false;

		#region Properties

		/// <summary>
		/// The number of request per second allowed.
		/// </summary>
		public int RequestsPerSecond { get; set; }

		#endregion

		#region Methods

		/// <inheritdoc />
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			var name = $"{actionContext.Request.Method}-{actionContext.Request.RequestUri}";
			var key = string.Concat(name, "-", GetClientIp(actionContext.Request));

			if (HttpRuntime.Cache[key] == null)
			{
				HttpRuntime.Cache.Add(key, true, null, DateTime.UtcNow.AddMilliseconds(1000d / RequestsPerSecond), Cache.NoSlidingExpiration, CacheItemPriority.Low, null);
				return;
			}

			var message = $"You may only perform this action {RequestsPerSecond} per second.";
			actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Conflict, message);
		}

		private static string GetClientIp(HttpRequestMessage request)
		{
			if (request.Properties.ContainsKey("MS_HttpContext"))
			{
				return ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress;
			}

			if (!request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
			{
				return null;
			}

			var prop = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];
			return prop.Address;
		}

		#endregion
	}
}
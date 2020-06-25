#region References

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace SimpleAuthentication.Tests
{
	[TestClass]
	public class WebApiTests
	{
		#region Methods

		[TestMethod]
		public void RolesCheck()
		{
			var client = new HttpClient { BaseAddress = new Uri("http://sample.local", UriKind.Absolute) };
			var headerValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("admin@domain.com:123456")));
			client.DefaultRequestHeaders.Authorization = headerValue;
			var result = client.GetAsync("api/Account").Result;
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			Console.WriteLine(result.Content.ReadAsStringAsync().Result);
		}
		
		[TestMethod]
		public void RolesCheckAsEditor()
		{
			var client = new HttpClient { BaseAddress = new Uri("http://sample.local", UriKind.Absolute) };
			var headerValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("editor@domain.com:123456")));
			client.DefaultRequestHeaders.Authorization = headerValue;
			var result = client.GetAsync("api/Account").Result;
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			Console.WriteLine(result.Content.ReadAsStringAsync().Result);
		}
		
		[TestMethod]
		public void RolesCheckShouldFail()
		{
			var client = new HttpClient { BaseAddress = new Uri("http://sample.local", UriKind.Absolute) };
			var headerValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("user@domain.com:123456")));
			client.DefaultRequestHeaders.Authorization = headerValue;
			var result = client.GetAsync("api/Account").Result;
			Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
		}

		#endregion
	}
}
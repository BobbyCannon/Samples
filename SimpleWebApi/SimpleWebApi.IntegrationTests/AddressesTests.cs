#region References

using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebApi.Web;
using SimpleWebApi.Website.UnitTests;

#endregion

namespace SimpleWebApi.IntegrationTests
{
	[TestClass]
	public class AddressesTests : BaseTests
	{
		#region Methods

		[TestMethod]
		public void AddressesShouldBeAnonymous()
		{
			TestHelper.CreateDatabase().Dispose();

			var helper = new HttpHelper(BaseAddress);

			using (var actual = helper.Get("api/Addresses"))
			{
				Assert.AreEqual(true, actual.IsSuccessStatusCode);
				Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
			}
		}

		#endregion
	}
}
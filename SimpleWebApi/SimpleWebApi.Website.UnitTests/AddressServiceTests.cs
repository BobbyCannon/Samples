#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebApi.Website.Services;
using AddressEntity = SimpleWebApi.Data.Entities.Address;

#endregion

namespace SimpleWebApi.Website.UnitTests
{
	[TestClass]
	public class AddressServiceTests
	{
		#region Methods

		[TestMethod]
		public void TestMethod1()
		{
			using (var database = TestHelper.CreateDatabase())
			{
				var address = AddAddress("Line1");

				var expected = new[]
				{
					AddressService.ToModel(address)
				};

				var service = new AddressService(database);
				var actual = service.GetAddresses(null);

				TestHelper.AreEqual(expected, actual.Results);
			}
		}

		private AddressEntity AddAddress(string line1)
		{
			return new AddressEntity
			{
				Line1 = line1
			};
		}

		#endregion
	}
}
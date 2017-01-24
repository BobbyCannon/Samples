#region References

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebApi.Website.UnitTests;

#endregion

namespace SimpleWebApi.IntegrationTests
{
	public class BaseTests
	{
		#region Constructors

		protected BaseTests()
		{
			BaseAddress = "http://localhost/";
		}

		#endregion

		#region Properties

		public string BaseAddress { get; }

		#endregion

		#region Methods

		[TestInitialize]
		public void TestInitialize()
		{
			TestHelper.Initialize(true);
		}

		#endregion
	}
}
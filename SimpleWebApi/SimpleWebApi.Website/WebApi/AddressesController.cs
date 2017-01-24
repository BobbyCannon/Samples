#region References

using System.Web.Http;
using SimpleWebApi.Data;
using SimpleWebApi.Models;
using SimpleWebApi.Models.Data;
using SimpleWebApi.Website.Services;

#endregion

namespace SimpleWebApi.Website.WebApi
{
	public class AddressesController : ApiController
	{
		#region Fields

		private readonly IContosoDatabase _database;

		#endregion

		#region Constructors

		public AddressesController(IContosoDatabase database)
		{
			_database = database;
		}

		#endregion

		#region Methods

		[HttpPost]
		[AllowAnonymous]
		[Route("api/Addresses")]
		public Address CreateAddress([FromBody] Address address)
		{
			return new Address { Line1 = "test 1" };
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("api/Addresses/{id}")]
		public Address GetAddress([FromUri] int id)
		{
			return new Address { Line1 = "test 1" };
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("api/Addresses")]
		public PagedResults<Address> GetAddresses([FromUri] PagedRequest request)
		{
			var service = new AddressService(_database);
			return service.GetAddresses(request);
		}

		#endregion
	}
}
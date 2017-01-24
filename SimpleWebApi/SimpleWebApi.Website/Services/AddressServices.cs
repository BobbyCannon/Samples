#region References

using System.Linq;
using SimpleWebApi.Data;
using SimpleWebApi.Models;
using SimpleWebApi.Models.Data;
using AddressEntity = SimpleWebApi.Data.Entities.Address;

#endregion

namespace SimpleWebApi.Website.Services
{
	public class AddressService
	{
		#region Fields

		private readonly IContosoDatabase _database;

		#endregion

		#region Constructors

		public AddressService(IContosoDatabase database)
		{
			_database = database;
		}

		#endregion

		#region Methods

		public PagedResults<Address> GetAddresses(PagedRequest request)
		{
			request = request ?? new PagedRequest();
			request.Cleanup();

			return _database.Addresses.GetPagedResults(request, ToModel);
		}

		public static Address ToModel(AddressEntity entity)
		{
			return new Address
			{
				Id = entity.Id,
				Line1 = entity.Line1,
				Line2 = entity.Line2,
				City = entity.City,
				Postal = entity.Postal,
				State = entity.State
			};
		}

		#endregion
	}
}
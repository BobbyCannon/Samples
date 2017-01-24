#region References

using System;
using System.Text.RegularExpressions;
using SimpleWebApi.Data.Entities;
using Speedy;

#endregion

namespace SimpleWebApi.Data
{
	[Serializable]
	public class ContosoDatabase : Database, IContosoDatabase
	{
		#region Fields

		public static readonly string[] SyncOrder;

		#endregion

		#region Constructors

		public ContosoDatabase(string directory = null, DatabaseOptions options = null)
			: base(directory, options)
		{
			Addresses = GetSyncableRepository<Address>();
			People = GetSyncableRepository<Person>();

			// Setup options.
			Options.SyncOrder = SyncOrder;

			// Address Map
			Property<Address>(x => x.Line1).IsRequired().HasMaximumLength(256);
			Property<Address>(x => x.Line2).IsRequired().HasMaximumLength(256);
			Property<Address>(x => x.City).IsRequired().HasMaximumLength(256);
			Property<Address>(x => x.State).IsRequired().HasMaximumLength(128);
			Property<Address>(x => x.Postal).IsRequired().HasMaximumLength(128);
			Property<Address>(x => x.SyncId).IsRequired().IsUnique();

			// Person Map
			Property<Person>(x => x.Name).IsRequired().HasMaximumLength(256).IsUnique();
			Property<Person>(x => x.SyncId).IsRequired().IsUnique();
			HasRequired<Person, Address>(p => p.Address, p => p.AddressId, a => a.People);
			HasOptional<Person, Address>(p => p.BillingAddress, p => p.BillingAddressId);
		}

		static ContosoDatabase()
		{
			SyncOrder = new[]
			{
				typeof(Address).FullName,
				typeof(Person).FullName
			};
		}

		#endregion

		#region Properties

		public IRepository<Address> Addresses { get; }
		
		public IRepository<Person> People { get; }

		#endregion
	}
}
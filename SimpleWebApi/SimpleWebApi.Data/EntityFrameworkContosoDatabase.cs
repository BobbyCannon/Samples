#region References

using System.Diagnostics.CodeAnalysis;
using SimpleWebApi.Data.Entities;
using Speedy;
using Speedy.EntityFramework;

#endregion

namespace SimpleWebApi.Data
{
	[ExcludeFromCodeCoverage]
	public class EntityFrameworkContosoDatabase : EntityFrameworkDatabase, IContosoDatabase
	{
		#region Constructors

		public EntityFrameworkContosoDatabase()
			: this("name=DefaultConnection", new DatabaseOptions())
		{
		}

		public EntityFrameworkContosoDatabase(DatabaseOptions options)
			: this("name=DefaultConnection", options)
		{
		}

		public EntityFrameworkContosoDatabase(string nameOrConnectionString, DatabaseOptions options = null)
			: base(nameOrConnectionString, options)
		{
			Options.SyncOrder = ContosoDatabase.SyncOrder;
		}

		#endregion

		#region Properties

		public IRepository<Address> Addresses => GetSyncableRepository<Address>();

		public IRepository<Person> People => GetSyncableRepository<Person>();

		#endregion
	}
}
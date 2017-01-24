#region References

using System.Text.RegularExpressions;
using SimpleWebApi.Data.Entities;
using Speedy;

#endregion

namespace SimpleWebApi.Data
{
	public interface IContosoDatabase : ISyncableDatabase
	{
		#region Properties

		IRepository<Address> Addresses { get; }

		IRepository<Person> People { get; }

		#endregion
	}
}
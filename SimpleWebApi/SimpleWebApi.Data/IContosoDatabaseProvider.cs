using Speedy;

namespace SimpleWebApi.Data
{
	public interface IContosoDatabaseProvider : ISyncableDatabaseProvider
	{
		#region Methods

		new IContosoDatabase GetDatabase();

		#endregion
	}
}
﻿#region References

using System;
using Speedy;

#endregion

namespace SimpleWebApi.Data
{
	public class ContosoDatabaseProvider : IContosoDatabaseProvider
	{
		#region Fields

		private readonly string _directory;
		private readonly Lazy<ContosoDatabase> _memoryDatabase;

		#endregion

		#region Constructors

		public ContosoDatabaseProvider(string directory = null, DatabaseOptions options = null)
		{
			_directory = directory;
			Options = options ?? new DatabaseOptions();

			_memoryDatabase = new Lazy<ContosoDatabase>(() => new ContosoDatabase(null, Options.DeepClone()), true);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the options for this database.
		/// </summary>
		public DatabaseOptions Options { get; }

		#endregion

		#region Methods

		public IContosoDatabase GetDatabase()
		{
			if (!string.IsNullOrEmpty(_directory))
			{
				return new ContosoDatabase(_directory, Options.DeepClone());
			}

			// todo: do this better. Basically we don't want this to change.
			_memoryDatabase.Value.Options.MaintainDates = Options.MaintainDates;
			_memoryDatabase.Value.Options.MaintainSyncId = Options.MaintainSyncId;
			_memoryDatabase.Value.Options.UnmaintainEntities = Options.UnmaintainEntities;
			return _memoryDatabase.Value;
		}

		/// <summary>
		/// Gets an instance of the syncable database.
		/// </summary>
		/// <returns> The syncable database instance. </returns>
		ISyncableDatabase ISyncableDatabaseProvider.GetDatabase()
		{
			return GetDatabase();
		}

		/// <summary>
		/// Gets an instance of the database.
		/// </summary>
		/// <returns> The database instance. </returns>
		IDatabase IDatabaseProvider.GetDatabase()
		{
			return GetDatabase();
		}

		#endregion
	}
}
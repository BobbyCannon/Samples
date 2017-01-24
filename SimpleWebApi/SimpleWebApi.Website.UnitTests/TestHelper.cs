#region References

using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Http;
using KellermanSoftware.CompareNetObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebApi.Data;
using SimpleWebApi.Data.Migrations;
using SimpleWebApi.Website.UnitTests.Properties;
using static Speedy.Extensions;

#endregion

namespace SimpleWebApi.Website.UnitTests
{
	public static class TestHelper
	{
		#region Constructors

		static TestHelper()
		{
			// Flip this to turn all unit tests into integration test that run against a database.
			RunUnitTestAgainstDatabase = false;

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityFrameworkContosoDatabase, Configuration>());
		}

		#endregion

		#region Properties

		public static bool RunUnitTestAgainstDatabase { get; private set; }

		#endregion

		#region Methods

		public static void AreEqual<T>(T expected, T actual, params string[] membersToIgnore)
		{
			var compareObjects = new CompareLogic
			{
				Config =
				{
					IgnoreObjectTypes = true,
					MaxDifferences = int.MaxValue
				}
			};

			if (membersToIgnore.Any())
			{
				compareObjects.Config.MembersToIgnore = membersToIgnore.ToList();
			}

			var result = compareObjects.Compare(expected, actual);
			Assert.IsTrue(result.AreEqual, result.DifferencesString);
		}

		public static void AreEqual<T>(T expected, Func<T> actual, int timeout, int delay = 100)
		{
			Retry(() => AreEqual(expected, actual()), timeout, delay);
		}

		public static void AreNotEqual<T>(T expected, T actual, params string[] membersToIgnore)
		{
			var compareObjects = new CompareLogic
			{
				Config =
				{
					IgnoreObjectTypes = true,
					MaxDifferences = int.MaxValue
				}
			};

			if (membersToIgnore.Any())
			{
				compareObjects.Config.MembersToIgnore = membersToIgnore.ToList();
			}

			var result = compareObjects.Compare(expected, actual);
			Assert.IsFalse(result.AreEqual, "The values are exactly the same.");
		}

		public static void ClearDatabase()
		{
			CreateDatabase().Dispose();
		}

		public static void Contains(string expected, Func<string> actual, int timeout, int delay = 100)
		{
			Retry(() => actual().Contains(expected), timeout, delay);
		}

		public static IContosoDatabase CreateDatabase(bool clear = true)
		{
			if (!RunUnitTestAgainstDatabase)
			{
				return new ContosoDatabase();
			}

			var database = new EntityFrameworkContosoDatabase();

			if (clear)
			{
				database.Database.ExecuteSqlCommand(Resources.ClearDatabase);
			}

			return database;
		}

		public static void Dump(this object item)
		{
			Debug.WriteLine(item);
		}

		public static void Dump(this byte[] item)
		{
			foreach (var i in item)
			{
				Debug.Write($"{i:X2},");
			}

			Debug.WriteLine("");
		}

		public static void Dump<T>(this T item, Func<T, object> action)
		{
			Debug.WriteLine(action(item));
		}

		public static void ExpectedException<T>(Action work, string errorMessage) where T : Exception
		{
			try
			{
				work();
			}
			catch (HttpResponseException ex)
			{
				// todo: Can we make this better? blah...
				var exception = ex.Response.Content.ToJson();
				Assert.IsTrue(exception.Contains(errorMessage));
				return;
			}
			catch (T ex)
			{
				if (!ex.Message.Contains(errorMessage))
				{
					Assert.Fail("Expected <" + ex.Message + "> to contain <" + errorMessage + ">.");
				}
				return;
			}

			Assert.Fail("The expected exception was not thrown.");
		}

		public static IContosoDatabase GetDatabase()
		{
			return CreateDatabase(false);
		}

		public static void Initialize(bool runAgainstDatabase)
		{
			RunUnitTestAgainstDatabase = runAgainstDatabase;
		}

		public static void NotContains(string expected, Func<string> actual, int timeout, int delay = 100)
		{
			Retry(() => !actual().Contains(expected), timeout, delay);
		}

		#endregion
	}
}
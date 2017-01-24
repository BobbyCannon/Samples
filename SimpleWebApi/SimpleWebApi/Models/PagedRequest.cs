#region References

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace SimpleWebApi.Models
{
	/// <summary>
	/// Represents a paged request to a service.
	/// </summary>
	public class PagedRequest
	{
		#region Constructors

		/// <summary>
		/// Instantiates a paged request.
		/// </summary>
		public PagedRequest()
		{
			Cleanup();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The filter to limit the request to. Defaults to an empty filter.
		/// </summary>
		public string Filter { get; set; }

		/// <summary>
		/// The optional collection of filter values.
		/// </summary>
		public IEnumerable<string> FilterValues { get; set; }

		/// <summary>
		/// The values to be include in the results. Defaults to an empty collection.
		/// </summary>
		public IEnumerable<string> Including { get; set; }

		/// <summary>
		/// The value to order the request by.
		/// </summary>
		public string Order { get; set; }

		/// <summary>
		/// The page to start the request on.
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// The number of items per page.
		/// </summary>
		public int PerPage { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Cleanup the request. Set default values.
		/// </summary>
		public void Cleanup()
		{
			Cleanup(Filter, x => x == null, () => Filter = string.Empty);
			Cleanup(FilterValues, x => x == null, () => FilterValues = new string[0]);
			Cleanup(Including, x => x == null, () => Including = new string[0]);
			Cleanup(Order, string.IsNullOrWhiteSpace, () => Order = "Id");
			Cleanup(Page, x => x <= 0, () => Page = 1);
			Cleanup(PerPage, x => x <= 0, () => PerPage = 10);
			Cleanup(PerPage, x => x > 100, () => PerPage = 100);
		}

		/// <summary>
		/// Cleanup a single item based on the test.
		/// </summary>
		/// <typeparam name="T"> The item type to be cleaned up. </typeparam>
		/// <param name="item"> The item to test and clean up. </param>
		/// <param name="test"> The test for the time. </param>
		/// <param name="action"> The action to cleanup the item. </param>
		private static void Cleanup<T>(T item, Func<T, bool> test, Action action)
		{
			if (test(item))
			{
				action();
			}
		}

		#endregion
	}
}
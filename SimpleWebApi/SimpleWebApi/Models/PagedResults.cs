#region References

using System.Collections.Generic;

#endregion

namespace SimpleWebApi.Models
{
	/// <summary>
	/// Represents a page of results for a paged request to a service.
	/// </summary>
	/// <typeparam name="T"> The type of the items in the results collection. </typeparam>
	public class PagedResults<T> : PagedRequest
	{
		#region Properties

		/// <summary>
		/// The value to determine if the request has more pages.
		/// </summary>
		public bool HasMore => Page != TotalPages;

		/// <summary>
		/// The results for a paged request.
		/// </summary>
		public IEnumerable<T> Results { get; set; }

		/// <summary>
		/// The total count of items for the request.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// The total count of pages for the request.
		/// </summary>
		public int TotalPages => TotalCount > 0 ? TotalCount / PerPage + (TotalCount % PerPage > 0 ? 1 : 0) : 1;

		#endregion
	}
}
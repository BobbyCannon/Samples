#region References

using System;
using System.Linq;
using SimpleWebApi.Models;
using Speedy.Linq;

#endregion

namespace SimpleWebApi.Website
{
	public static class Extensions
	{
		#region Methods

		/// <summary>
		/// Gets paged results.
		/// </summary>
		/// <typeparam name="T1"> The type of item in the query. </typeparam>
		/// <typeparam name="T2"> The type of the item returned. </typeparam>
		/// <param name="query"> The queryable collection. </param>
		/// <param name="request"> The request values. </param>
		/// <param name="transform"> The function to transfer the results. </param>
		/// <returns> The paged results. </returns>
		public static PagedResults<T2> GetPagedResults<T1, T2>(this IQueryable<T1> query, PagedRequest request, Func<T1, T2> transform)
		{
			if (!string.IsNullOrWhiteSpace(request.Filter))
			{
				query = query.Where(request.Filter);
			}

			var orderedQuery = query.OrderBy(request.Order);
			var response = new PagedResults<T2>
			{
				Filter = request.Filter,
				FilterValues = request.FilterValues,
				TotalCount = query.Count(),
				Order = request.Order,
				PerPage = request.PerPage
			};

			response.Page = response.TotalPages < request.Page ? response.TotalPages : request.Page;
			response.Results = orderedQuery
				.Skip((response.Page - 1) * response.PerPage)
				.Take(response.PerPage)
				.ToList()
				.Select(transform)
				.ToList();

			return response;
		}

		#endregion
	}
}
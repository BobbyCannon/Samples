#region References

using System;

#endregion

namespace SimpleWebApi.Models.Data
{
	[Serializable]
	public class Person : DataModel
	{
		#region Properties

		public Address Address { get; set; }
		public Address BillingAddress { get; set; }
		public string Name { get; set; }

		#endregion

	}
}
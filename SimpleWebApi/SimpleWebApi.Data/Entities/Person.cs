#region References

using System;
using Speedy.Sync;

#endregion

namespace SimpleWebApi.Data.Entities
{
	[Serializable]
	public class Person : SyncEntity
	{
		#region Properties

		public virtual Address Address { get; set; }
		public int AddressId { get; set; }
		public virtual Address BillingAddress { get; set; }
		public int? BillingAddressId { get; set; }
		public string Name { get; set; }

		#endregion

	}
}
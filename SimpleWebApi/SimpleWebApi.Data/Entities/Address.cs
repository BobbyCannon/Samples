#region References

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Speedy;
using Speedy.Sync;

#endregion

namespace SimpleWebApi.Data.Entities
{
	[Serializable]
	public class Address : SyncEntity
	{
		#region Constructors

		[SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
		public Address()
		{
			People = new Collection<Person>();
		}

		#endregion

		#region Properties

		public string City { get; set; }

		/// <summary>
		/// Read only property
		/// </summary>
		public string FullAddress => $"{Line1}{Environment.NewLine}{City}, {State}  {Postal}";

		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public virtual ICollection<Person> People { get; set; }
		public string Postal { get; set; }
		public string State { get; set; }

		#endregion
	}
}
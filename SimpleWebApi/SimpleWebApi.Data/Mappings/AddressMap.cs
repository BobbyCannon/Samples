#region References

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics.CodeAnalysis;
using SimpleWebApi.Data.Entities;

#endregion

namespace SimpleWebApi.Data.Mappings
{
	[ExcludeFromCodeCoverage]
	public class AddressMap : EntityTypeConfiguration<Address>
	{
		#region Constructors

		public AddressMap()
		{
			// Primary Key
			HasKey(x => x.Id);

			// Table & Column Mappings
			ToTable("Addresses");
			Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(x => x.City).IsRequired().HasMaxLength(256);
			Property(x => x.CreatedOn).IsRequired().HasColumnType("datetime2").HasPrecision(7);
			Property(x => x.Line1).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute { IsUnique = false } }));
			Property(x => x.Line2).IsRequired().HasMaxLength(256);
			Property(x => x.ModifiedOn).IsRequired().HasColumnType("datetime2").HasPrecision(7).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute { IsUnique = false } }));
			Property(x => x.Postal).IsRequired().HasMaxLength(128);
			Property(x => x.State).IsRequired().HasMaxLength(128);
			Property(x => x.SyncId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute { IsUnique = true } }));
			Ignore(x => x.FullAddress);
		}

		#endregion
	}
}
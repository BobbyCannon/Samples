#region References

using System.Data.Entity.Migrations;

#endregion

namespace SimpleWebApi.Data.Migrations
{
	public partial class InitialMigration : DbMigration
	{
		#region Methods

		public override void Down()
		{
			DropForeignKey("dbo.People", "BillingAddressId", "dbo.Addresses");
			DropForeignKey("dbo.People", "AddressId", "dbo.Addresses");
			DropIndex("dbo.People", new[] { "ModifiedOn" });
			DropIndex("dbo.People", new[] { "Name" });
			DropIndex("dbo.People", new[] { "BillingAddressId" });
			DropIndex("dbo.People", new[] { "AddressId" });
			DropIndex("dbo.Addresses", new[] { "ModifiedOn" });
			DropIndex("dbo.Addresses", new[] { "SyncId" });
			DropIndex("dbo.Addresses", new[] { "Line1" });
			DropTable("dbo.People");
			DropTable("dbo.Addresses");
		}

		public override void Up()
		{
			CreateTable(
					"dbo.Addresses",
					c => new
					{
						Id = c.Int(false, true),
						City = c.String(false, 256),
						Line1 = c.String(false, 256),
						Line2 = c.String(false, 256),
						Postal = c.String(false, 128),
						State = c.String(false, 128),
						SyncId = c.Guid(false),
						ModifiedOn = c.DateTime(false, 7, storeType: "datetime2"),
						CreatedOn = c.DateTime(false, 7, storeType: "datetime2")
					})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Line1)
				.Index(t => t.SyncId, unique: true)
				.Index(t => t.ModifiedOn);

			CreateTable(
					"dbo.People",
					c => new
					{
						Id = c.Int(false, true),
						AddressId = c.Int(false),
						BillingAddressId = c.Int(),
						Name = c.String(false, 256),
						SyncId = c.Guid(false),
						ModifiedOn = c.DateTime(false, 7, storeType: "datetime2"),
						CreatedOn = c.DateTime(false, 7, storeType: "datetime2")
					})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Addresses", t => t.AddressId)
				.ForeignKey("dbo.Addresses", t => t.BillingAddressId)
				.Index(t => t.AddressId)
				.Index(t => t.BillingAddressId)
				.Index(t => t.Name, unique: true)
				.Index(t => t.ModifiedOn);
		}

		#endregion
	}
}
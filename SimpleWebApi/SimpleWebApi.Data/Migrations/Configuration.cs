#region References

using System.Data.Entity.Migrations;

#endregion

namespace SimpleWebApi.Data.Migrations
{
	public sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkContosoDatabase>
	{
		#region Constructors

		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "Contoso";
		}

		#endregion

		#region Methods

		protected override void Seed(EntityFrameworkContosoDatabase context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}

		#endregion
	}
}
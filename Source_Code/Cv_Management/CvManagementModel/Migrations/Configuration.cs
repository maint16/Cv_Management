using System.Data.Entity.Migrations;
using CvManagementModel.Models.Context;

namespace CvManagementModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CvManagementDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CvManagementDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
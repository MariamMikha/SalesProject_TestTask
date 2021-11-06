namespace SalesProject.Migrations
{
    using SalesProject.Models.DBClasses;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesProject.Models.DBClasses.MWCoreEntity>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SalesProject.Models.DBClasses.MWCoreEntity context)
        {
            //  This method will be called after migrating to the latest version.
            if (!context.MW_OrderStatus.Any())
            {
                List<MW_OrderStatus> StatusList = new List<MW_OrderStatus>();
                StatusList.Add(new MW_OrderStatus { OrderStatusID = 1, OrderStatusName = "Placed" });
                StatusList.Add(new MW_OrderStatus { OrderStatusID = 2, OrderStatusName = "Delivered" });
                StatusList.Add(new MW_OrderStatus { OrderStatusID = 3, OrderStatusName = "Cancelled" });

                context.MW_OrderStatus.AddRange(StatusList);
            }



            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

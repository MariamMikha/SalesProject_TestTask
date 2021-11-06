using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SalesProject.Models.DBClasses
{
    public class MWCoreEntity : DbContext
    {
        public MWCoreEntity() : base("MWCoreDB")
        {

        }

        public DbSet<MW_WebAPILogger> MW_WebAPILogger { get; set; }
        public DbSet<MW_Customers> MW_Customers { get; set; }
        public DbSet<MW_CustomerDevices> MW_CustomerDevices { get; set; }
        public DbSet<MW_Products> MW_Products { get; set; }
        public DbSet<MW_Orders> MW_Orders { get; set; }
        public DbSet<MW_OrderStatus> MW_OrderStatus { get; set; }
        public DbSet<MW_OrdersDetails> MW_OrdersDetails { get; set; }

    }
}
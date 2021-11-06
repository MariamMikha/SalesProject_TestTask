namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMW_OrdersTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MW_Orders", "PaymentMethodID");
            DropColumn("dbo.MW_Orders", "PayoutStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MW_Orders", "PayoutStatus", c => c.Int(nullable: false));
            AddColumn("dbo.MW_Orders", "PaymentMethodID", c => c.Int(nullable: false));
        }
    }
}

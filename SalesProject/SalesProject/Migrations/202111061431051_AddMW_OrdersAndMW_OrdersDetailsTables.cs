namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMW_OrdersAndMW_OrdersDetailsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MW_Orders",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CustomerID = c.Long(nullable: false),
                        SubAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderNotes = c.String(),
                        OrderStatusID = c.Int(nullable: false,defaultValue:1),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MW_OrderStatus", t => t.OrderStatusID, cascadeDelete: true)
                .Index(t => t.OrderStatusID);
            
            CreateTable(
                "dbo.MW_OrdersDetails",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OrderID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MW_Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MW_Orders", "OrderStatusID", "dbo.MW_OrderStatus");
            DropForeignKey("dbo.MW_OrdersDetails", "OrderID", "dbo.MW_Orders");
            DropIndex("dbo.MW_OrdersDetails", new[] { "OrderID" });
            DropIndex("dbo.MW_Orders", new[] { "OrderStatusID" });
            DropTable("dbo.MW_OrdersDetails");
            DropTable("dbo.MW_Orders");
        }
    }
}

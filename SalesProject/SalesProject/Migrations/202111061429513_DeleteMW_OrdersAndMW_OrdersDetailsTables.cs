namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMW_OrdersAndMW_OrdersDetailsTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MW_OrdersDetails", "OrderID", "dbo.MW_Orders");
            DropForeignKey("dbo.MW_Orders", "OrderStatusID", "dbo.MW_OrderStatus");
            DropIndex("dbo.MW_Orders", new[] { "OrderStatusID" });
            DropIndex("dbo.MW_OrdersDetails", new[] { "OrderID" });
            DropTable("dbo.MW_Orders");
            DropTable("dbo.MW_OrdersDetails");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
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
                        OrderStatusID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.MW_OrdersDetails", "OrderID");
            CreateIndex("dbo.MW_Orders", "OrderStatusID");
            AddForeignKey("dbo.MW_Orders", "OrderStatusID", "dbo.MW_OrderStatus", "OrderStatusID", cascadeDelete: true);
            AddForeignKey("dbo.MW_OrdersDetails", "OrderID", "dbo.MW_Orders", "ID", cascadeDelete: true);
        }
    }
}

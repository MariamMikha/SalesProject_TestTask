namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMW_ProductsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MW_Products",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductDescription = c.String(),
                        MainImage = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        SoldOut = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                        NumberOfViews = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MW_Products");
        }
    }
}

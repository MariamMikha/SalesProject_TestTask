namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MW_CustomerDevices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ApiToken = c.String(),
                        DeviceID = c.String(),
                        DeviceToken = c.String(),
                        DevicePlatform = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MW_Customers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        Username = c.String(),
                        Email = c.String(),
                        Image = c.String(),
                        Password = c.String(),
                        PhoneCode = c.String(),
                        PhoneNumber = c.String(),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MW_Customers");
            DropTable("dbo.MW_CustomerDevices");
        }
    }
}

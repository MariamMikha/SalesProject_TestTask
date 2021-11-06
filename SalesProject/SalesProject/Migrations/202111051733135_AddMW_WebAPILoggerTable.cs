namespace SalesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMW_WebAPILoggerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MW_WebAPILogger",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RequestDateTime = c.DateTime(nullable: false),
                        ServiceLink = c.String(),
                        RequestString = c.String(),
                        ResponseString = c.String(),
                        ElapsedTime = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MW_WebAPILogger");
        }
    }
}

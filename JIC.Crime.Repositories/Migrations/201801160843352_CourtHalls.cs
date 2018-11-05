namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourtHalls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtConfigurations_CourtHalls",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        CourtID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID, cascadeDelete: true)
                .Index(t => t.CourtID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourtConfigurations_CourtHalls", "CourtID", "dbo.Configurations_Courts");
            DropIndex("dbo.CourtConfigurations_CourtHalls", new[] { "CourtID" });
            DropTable("dbo.CourtConfigurations_CourtHalls");
        }
    }
}

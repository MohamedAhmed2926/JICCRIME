namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_CourtID_overall_number : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configurations_OverallNumbers", "CourtID", c => c.Int(nullable: false));
            CreateIndex("dbo.Configurations_OverallNumbers", "CourtID");
            AddForeignKey("dbo.Configurations_OverallNumbers", "CourtID", "dbo.Configurations_Courts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Configurations_OverallNumbers", "CourtID", "dbo.Configurations_Courts");
            DropIndex("dbo.Configurations_OverallNumbers", new[] { "CourtID" });
            DropColumn("dbo.Configurations_OverallNumbers", "CourtID");
        }
    }
}

namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hallidcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseSessions", "HallID", c => c.Int());
            AddColumn("dbo.Cases_CaseSessions", "CourtConfigurations_CourtHalls_ID", c => c.Long());
            CreateIndex("dbo.Cases_CaseSessions", "CourtConfigurations_CourtHalls_ID");
            AddForeignKey("dbo.Cases_CaseSessions", "CourtConfigurations_CourtHalls_ID", "dbo.CourtConfigurations_CourtHalls", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cases_CaseSessions", "CourtConfigurations_CourtHalls_ID", "dbo.CourtConfigurations_CourtHalls");
            DropIndex("dbo.Cases_CaseSessions", new[] { "CourtConfigurations_CourtHalls_ID" });
            DropColumn("dbo.Cases_CaseSessions", "CourtConfigurations_CourtHalls_ID");
            DropColumn("dbo.Cases_CaseSessions", "HallID");
        }
    }
}

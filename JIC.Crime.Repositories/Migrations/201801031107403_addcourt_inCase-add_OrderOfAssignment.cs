namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcourt_inCaseadd_OrderOfAssignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_MasterCase", "OrderOfAssignment", c => c.String());
            AddColumn("dbo.Cases_Cases", "CourtID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cases_Cases", "CourtID");
            AddForeignKey("dbo.Cases_Cases", "CourtID", "dbo.Configurations_Courts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cases_Cases", "CourtID", "dbo.Configurations_Courts");
            DropIndex("dbo.Cases_Cases", new[] { "CourtID" });
            DropColumn("dbo.Cases_Cases", "CourtID");
            DropColumn("dbo.Cases_MasterCase", "OrderOfAssignment");
        }
    }
}

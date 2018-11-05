namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverAlllNullAple2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cases_MasterCase", new[] { "OverallID" });
            AlterColumn("dbo.Cases_MasterCase", "OverallID", c => c.Int());
            CreateIndex("dbo.Cases_MasterCase", "OverallID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cases_MasterCase", new[] { "OverallID" });
            AlterColumn("dbo.Cases_MasterCase", "OverallID", c => c.Int(nullable: false));
            CreateIndex("dbo.Cases_MasterCase", "OverallID");
        }
    }
}

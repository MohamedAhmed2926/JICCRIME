namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqCycleDate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CourtConfigurations_CycleRolls", new[] { "CourtID", "RollDate" }, unique: true, name: "CycleRolls_Uniq_Index");
        }
        
        public override void Down()
        {
            DropIndex(table: "CourtConfigurations_CycleRolls",
          name: "CycleRolls_Uniq_Index");
        }
    }
}

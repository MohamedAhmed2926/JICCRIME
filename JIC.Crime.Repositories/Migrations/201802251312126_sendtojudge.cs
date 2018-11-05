namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sendtojudge : DbMigration
    {
        public override void Up()
        {
           
            AddColumn("dbo.CourtConfigurations_CircuitRolls", "HallID", c => c.Long(nullable: true));
            AddColumn("dbo.CourtConfigurations_CircuitRolls", "ProsecuterID", c => c.Int(nullable: true));
            AddColumn("dbo.Cases_Cases", "IsSentToJudge", c => c.Boolean(nullable: true));
            CreateIndex("dbo.Cases_Cases", "IsSentToJudge");
            CreateIndex("dbo.CourtConfigurations_CircuitRolls", "HallID");
            CreateIndex("dbo.CourtConfigurations_CircuitRolls", "ProsecuterID");
            AddForeignKey("dbo.CourtConfigurations_CircuitRolls", "HallID", "dbo.CourtConfigurations_CourtHalls", "ID");
            AddForeignKey("dbo.CourtConfigurations_CircuitRolls", "ProsecuterID", "dbo.Configurations_Prosecuters", "ID");
          
        }
        
        public override void Down()
        {
    
        }
    }
}

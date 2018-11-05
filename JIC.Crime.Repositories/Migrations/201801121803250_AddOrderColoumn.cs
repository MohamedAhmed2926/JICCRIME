namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderColoumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseDefendants", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.Cases_CaseVictims", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases_CaseVictims", "Order");
            DropColumn("dbo.Cases_CaseDefendants", "Order");
        }
    }
}

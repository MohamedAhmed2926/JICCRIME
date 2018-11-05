namespace JIC.Fault.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProsecutionServiceIds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Service_CaseVictimProsecution",
                c => new
                    {
                        CaseVictimID = c.Long(nullable: false),
                        ProsecutionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaseVictimID)
                .ForeignKey("dbo.Cases_CaseVictims", t => t.CaseVictimID)
                .Index(t => t.CaseVictimID);
            
            CreateTable(
                "dbo.Service_CaseProsecution",
                c => new
                    {
                        CaseID = c.Int(nullable: false),
                        ProsecutionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaseID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.Service_CaseDefendantProsecution",
                c => new
                    {
                        CaseDefendantID = c.Long(nullable: false),
                        ProsecutionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaseDefendantID)
                .ForeignKey("dbo.Cases_CaseDefendants", t => t.CaseDefendantID)
                .Index(t => t.CaseDefendantID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Service_CaseDefendantProsecution", "CaseDefendantID", "dbo.Cases_CaseDefendants");
            DropForeignKey("dbo.Service_CaseProsecution", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Service_CaseVictimProsecution", "CaseVictimID", "dbo.Cases_CaseVictims");
            DropIndex("dbo.Service_CaseDefendantProsecution", new[] { "CaseDefendantID" });
            DropIndex("dbo.Service_CaseProsecution", new[] { "CaseID" });
            DropIndex("dbo.Service_CaseVictimProsecution", new[] { "CaseVictimID" });
            DropTable("dbo.Service_CaseDefendantProsecution");
            DropTable("dbo.Service_CaseProsecution");
            DropTable("dbo.Service_CaseVictimProsecution");
        }
    }
}

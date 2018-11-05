namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCrimesRelation : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE [dbo].[Cases_DefendantsCharges] DROP CONSTRAINT [FK_dbo.Cases_DefendantsCharges_dbo.Cases_DefendatnsCaseLog_DefendantCaseLogID]");
            DropForeignKey("dbo.Cases_DefendantsCharges", "DefendantCaseLogID", "dbo.Configurations_Lookups");
            DropIndex("dbo.Cases_DefendantsCharges", new[] { "DefendantCaseLogID" });
            DropColumn("dbo.Cases_DefendantsCharges", "DefendantCaseLogID");
            AddColumn("dbo.Cases_DefendantsCharges", "DefendantID", c => c.Long(nullable: false));
            CreateIndex("dbo.Cases_DefendantsCharges", "DefendantID");
            AddForeignKey("dbo.Cases_DefendantsCharges", "DefendantID", "dbo.Cases_CaseDefendants", "ID");

        }
        
        public override void Down()
        {
        }
    }
}

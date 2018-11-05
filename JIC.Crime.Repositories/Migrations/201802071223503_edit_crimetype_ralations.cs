namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_crimetype_ralations : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Configurations_Prosecuters", "Configurations_Persons_ID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "CrimeType", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Configurations_Lookups");
            //DropIndex("dbo.Configurations_Prosecuters", new[] { "Configurations_Persons_ID" });
            //AddColumn("dbo.CourtConfigurations_TextPredictions", "Configurations_Lookups_ID", c => c.Int());
            //CreateIndex("dbo.CourtConfigurations_TextPredictions", "Configurations_Lookups_ID");
            AddForeignKey("dbo.CourtConfigurations_Circuits", "CrimeType", "dbo.Cases_CrimeTypes", "ID");
            AddForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Cases_CrimeTypes", "ID");
            //AddForeignKey("dbo.CourtConfigurations_TextPredictions", "Configurations_Lookups_ID", "dbo.Configurations_Lookups", "ID");
            //DropColumn("dbo.Configurations_Prosecuters", "Configurations_Persons_ID");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Configurations_Prosecuters", "Configurations_Persons_ID", c => c.Long());
            //DropForeignKey("dbo.CourtConfigurations_TextPredictions", "Configurations_Lookups_ID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Cases_CrimeTypes");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "CrimeType", "dbo.Cases_CrimeTypes");
            //DropIndex("dbo.CourtConfigurations_TextPredictions", new[] { "Configurations_Lookups_ID" });
            //DropColumn("dbo.CourtConfigurations_TextPredictions", "Configurations_Lookups_ID");
            //CreateIndex("dbo.Configurations_Prosecuters", "Configurations_Persons_ID");
            AddForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Configurations_Lookups", "ID");
            AddForeignKey("dbo.CourtConfigurations_Circuits", "CrimeType", "dbo.Configurations_Lookups", "ID");
            //AddForeignKey("dbo.Configurations_Prosecuters", "Configurations_Persons_ID", "dbo.Configurations_Persons", "ID");
        }
    }
}

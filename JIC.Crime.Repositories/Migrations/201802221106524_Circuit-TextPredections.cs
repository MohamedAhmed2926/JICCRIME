namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CircuitTextPredections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Cases_CrimeTypes");
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "UserID", "dbo.Security_Users");
            DropIndex("dbo.CourtConfigurations_TextPredictions", new[] { "UserID" });
            DropIndex("dbo.CourtConfigurations_TextPredictions", new[] { "CrimeTypeID" });
       //     AddColumn("dbo.Cases_CaseSessions", "SecretaryID", c => c.Int());
            AddColumn("dbo.CourtConfigurations_TextPredictions", "CircuitID", c => c.Int(nullable: false));
      //      CreateIndex("dbo.Cases_CaseSessions", "SecretaryID");
            CreateIndex("dbo.CourtConfigurations_TextPredictions", "CircuitID");
        //    AddForeignKey("dbo.Cases_CaseSessions", "SecretaryID", "dbo.Security_Users", "Id");
            AddForeignKey("dbo.CourtConfigurations_TextPredictions", "CircuitID", "dbo.CourtConfigurations_Circuits", "ID");
            DropColumn("dbo.CourtConfigurations_TextPredictions", "UserID");
            DropColumn("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.CourtConfigurations_TextPredictions", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "CircuitID", "dbo.CourtConfigurations_Circuits");
       //     DropForeignKey("dbo.Cases_CaseSessions", "SecretaryID", "dbo.Security_Users");
            DropIndex("dbo.CourtConfigurations_TextPredictions", new[] { "CircuitID" });
      //      DropIndex("dbo.Cases_CaseSessions", new[] { "SecretaryID" });
            DropColumn("dbo.CourtConfigurations_TextPredictions", "CircuitID");
         //   DropColumn("dbo.Cases_CaseSessions", "SecretaryID");
            CreateIndex("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID");
            CreateIndex("dbo.CourtConfigurations_TextPredictions", "UserID");
            AddForeignKey("dbo.CourtConfigurations_TextPredictions", "UserID", "dbo.Security_Users", "Id");
            AddForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Cases_CrimeTypes", "ID");
        }
    }
}

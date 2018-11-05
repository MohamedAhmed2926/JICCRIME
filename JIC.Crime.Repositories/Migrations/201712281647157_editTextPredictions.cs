namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTextPredictions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtConfigurations_TextPredictions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Phrase = c.String(nullable: false),
                        UserID = c.Int(nullable: false),
                        CrimeTypeID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Users", t => t.UserID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CrimeTypeID)
                .Index(t => t.UserID)
                .Index(t => t.CrimeTypeID);
            
            DropColumn("dbo.Configurations_Prosecuters", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Configurations_Prosecuters", "Name", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "CrimeTypeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_TextPredictions", "UserID", "dbo.Security_Users");
            DropIndex("dbo.CourtConfigurations_TextPredictions", new[] { "CrimeTypeID" });
            DropIndex("dbo.CourtConfigurations_TextPredictions", new[] { "UserID" });
            DropTable("dbo.CourtConfigurations_TextPredictions");
        }
    }
}

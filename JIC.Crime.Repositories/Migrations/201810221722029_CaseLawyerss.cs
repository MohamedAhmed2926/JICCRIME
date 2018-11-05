namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseLawyerss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseLawyers",
                c => new
                    {
                        CardNumber = c.String(nullable: false, maxLength: 128),
                        ID = c.Int(nullable: false),
                        LevelID = c.Int(nullable: false),
                        PersonID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CardNumber)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.LevelID)
                .Index(t => t.LevelID)
               
                .Index(t => t.PersonID, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseLawyers", "LevelID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CaseLawyers", "PersonID", "dbo.Configurations_Persons");
            DropIndex("dbo.CaseLawyers", new[] { "PersonID" });
            DropIndex("dbo.CaseLawyers", new[] { "LevelID" });
            DropTable("dbo.CaseLawyers");
        }
    }
}

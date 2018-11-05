namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Case_Lawyer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Case_Lawyer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LevelID = c.Int(nullable: false),
                        PersonID = c.Long(nullable: false),
                        CardNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.LevelID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .Index(t => t.LevelID)
                .Index(t => t.PersonID, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Case_Lawyer", "PersonID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Case_Lawyer", "LevelID", "dbo.Configurations_Lookups");
            DropIndex("dbo.Case_Lawyer", new[] { "PersonID" });
            DropIndex("dbo.Case_Lawyer", new[] { "LevelID" });
            DropTable("dbo.Case_Lawyer");
        }
    }
}

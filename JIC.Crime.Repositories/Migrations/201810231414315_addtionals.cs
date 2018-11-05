namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtionals : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Lawyers", "PersonID", "dbo.Configurations_Persons");
            //DropForeignKey("dbo.Lawyers", "LevelID", "dbo.Configurations_Lookups");
            //DropIndex("dbo.Lawyers", new[] { "LevelID" });
            //DropIndex("dbo.Lawyers", new[] { "PersonID" });
            //DropIndex("dbo.Lawyers", new[] { "CardNumber" });
            ////DropTable("dbo.Lawyers");
        }
        
        public override void Down()
        {
        //    CreateTable(
        //        "dbo.Lawyers",
        //        c => new
        //            {
        //                ID = c.Int(nullable: false, identity: true),
        //                LevelID = c.Int(nullable: false),
        //                PersonID = c.Long(nullable: false),
        //                CardNumber = c.String(nullable: false),
        //            })
        //        .PrimaryKey(t => t.ID);
            
        //    CreateIndex("dbo.Lawyers", "CardNumber", unique: true);
        //    CreateIndex("dbo.Lawyers", "PersonID", unique: true);
        //    CreateIndex("dbo.Lawyers", "LevelID");
        //    AddForeignKey("dbo.Lawyers", "LevelID", "dbo.Configurations_Lookups", "ID");
        //    AddForeignKey("dbo.Lawyers", "PersonID", "dbo.Configurations_Persons", "ID");
        }
    }
}

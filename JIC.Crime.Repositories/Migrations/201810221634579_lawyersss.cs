namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lawyersss : DbMigration
    {
        public override void Up()
        {
           // DropTable("dbo.Lawyers");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.Lawyers",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            PersonID = c.Long(nullable: false),
            //            LevelID = c.Int(nullable: false),
            //            CardNumber = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
    }
}

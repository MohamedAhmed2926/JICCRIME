namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sent : DbMigration
    {
        public override void Up()
        {
         
            DropIndex("dbo.Cases_Cases", new[] { "IsSentToJudge" });
            DropColumn("dbo.Cases_Cases", "IsSentToJudge");
        }
        
        public override void Down()
        {
         
        }
    }
}

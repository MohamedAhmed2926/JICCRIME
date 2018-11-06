namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isSent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseSessions", "IsSentToJudge", c => c.Boolean());


        }

        public override void Down()
        {
            DropColumn("dbo.Cases_CaseSessions", "IsSentToJudge");
        }
    }
}

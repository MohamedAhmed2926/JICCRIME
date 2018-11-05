namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secretarycolinsessions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseSessions", "SecretaryID", c => c.Int());
            CreateIndex("dbo.Cases_CaseSessions", "SecretaryID");
            AddForeignKey("dbo.Cases_CaseSessions", "SecretaryID", "dbo.Security_Users", "ID");

        }

        public override void Down()
        {
        }
    }
}

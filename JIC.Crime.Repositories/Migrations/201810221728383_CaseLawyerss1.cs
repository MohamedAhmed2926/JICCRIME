namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseLawyerss1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CaseLawyers");
            AlterColumn("dbo.CaseLawyers", "CardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.CaseLawyers", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CaseLawyers", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CaseLawyers");
            AlterColumn("dbo.CaseLawyers", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.CaseLawyers", "CardNumber", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CaseLawyers", "CardNumber");
        }
    }
}

namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseDocuments", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cases_CaseDocuments", "CreatedBy", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Cases_CaseDocuments", "LastModifiedAt", c => c.DateTime());
            AddColumn("dbo.Cases_CaseDocuments", "LastModifiedBy", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases_CaseDocuments", "LastModifiedBy");
            DropColumn("dbo.Cases_CaseDocuments", "LastModifiedAt");
            DropColumn("dbo.Cases_CaseDocuments", "CreatedBy");
            DropColumn("dbo.Cases_CaseDocuments", "CreatedAt");
        }
    }
}

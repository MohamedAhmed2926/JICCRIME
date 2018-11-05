namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class folderCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseDocumentFolders", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cases_CaseDocumentFolders", "CreatedBy", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Cases_CaseDocumentFolders", "LastModifiedAt", c => c.DateTime());
            AddColumn("dbo.Cases_CaseDocumentFolders", "LastModifiedBy", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases_CaseDocumentFolders", "LastModifiedBy");
            DropColumn("dbo.Cases_CaseDocumentFolders", "LastModifiedAt");
            DropColumn("dbo.Cases_CaseDocumentFolders", "CreatedBy");
            DropColumn("dbo.Cases_CaseDocumentFolders", "CreatedAt");
        }
    }
}

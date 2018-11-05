namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCaseNoteStatusIDNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cases_Cases", new[] { "NoteStatusID" });
            AlterColumn("dbo.Cases_Cases", "NoteStatusID", c => c.Int(nullable: true));
            CreateIndex("dbo.Cases_Cases", "NoteStatusID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cases_Cases", new[] { "NoteStatusID" });
            AlterColumn("dbo.Cases_Cases", "NoteStatusID", c => c.Int());
            CreateIndex("dbo.Cases_Cases", "NoteStatusID");
        }
    }
}

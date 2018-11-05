namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditWitnessestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseWitnesses", "TestimonyFileData", c => c.Binary());
            AddColumn("dbo.Cases_CaseWitnesses", "UserID", c => c.Int(nullable: false));
          //  AddColumn("dbo.Cases_WitnessSessionLog", "TestimonyFileData", c => c.Binary());


           // CreateIndex("dbo.Cases_WitnessSessionLog", "WitnessID");
            CreateIndex("dbo.Cases_CaseWitnesses", "UserID");
         //   AddForeignKey("dbo.Cases_WitnessSessionLog", "WitnessID", "dbo.Cases_CaseWitnesses", "ID");
            AddForeignKey("dbo.Cases_CaseWitnesses", "UserID", "dbo.Security_Users", "ID");
         }
        
        public override void Down()
        {
            DropColumn("dbo.Cases_CaseWitnesses", "UserID");
            DropColumn("dbo.Cases_CaseWitnesses", "TestimonyFileData");
        }
    }
}

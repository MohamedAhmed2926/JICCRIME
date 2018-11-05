namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class witness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases_CaseWitnesses", "WitnessDocument", c => c.String());
            AddColumn("dbo.Cases_CaseWitnesses", "FileDataDocument", c => c.Binary());
          //  DropColumn("dbo.Security_Users", "MadeOperations");
        }
        
        public override void Down()
        {
          //  AddColumn("dbo.Security_Users", "MadeOperations", c => c.Boolean());
            DropColumn("dbo.Cases_CaseWitnesses", "FileDataDocument");
            DropColumn("dbo.Cases_CaseWitnesses", "WitnessDocument");
        }
    }
}

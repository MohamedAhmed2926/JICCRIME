namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WitnessSessionLogs : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cases_WitnessSessionLog", new[] { "Cases_CaseWitnesses_ID" });
            //DropColumn("dbo.Cases_WitnessSessionLog", "WitnessID");
           // RenameColumn(table: "dbo.Cases_WitnessSessionLog", name: "Cases_CaseWitnesses_ID", newName: "WitnessID");
            DropPrimaryKey("dbo.Cases_WitnessSessionLog");
            AddColumn("dbo.Cases_WitnessSessionLog", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Cases_WitnessSessionLog", "WitnessID", c => c.Long(nullable: false));
           // AlterColumn("dbo.Cases_WitnessSessionLog", "WitnessID", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Cases_WitnessSessionLog", "ID");
            //  CreateIndex("dbo.Cases_WitnessSessionLog", "WitnessID");
          //  DropIndex("dbo.Cases_WitnessSessionLog", new[] { "Cases_WitnessSessionLog_WitnessID" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cases_WitnessSessionLog", new[] { "WitnessID" });
            DropPrimaryKey("dbo.Cases_WitnessSessionLog");
            AlterColumn("dbo.Cases_WitnessSessionLog", "WitnessID", c => c.Long());
            AlterColumn("dbo.Cases_WitnessSessionLog", "WitnessID", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Cases_WitnessSessionLog", "ID");
            AddPrimaryKey("dbo.Cases_WitnessSessionLog", "WitnessID");
            RenameColumn(table: "dbo.Cases_WitnessSessionLog", name: "WitnessID", newName: "Cases_CaseWitnesses_ID");
            AddColumn("dbo.Cases_WitnessSessionLog", "WitnessID", c => c.Long(nullable: false, identity: true));
            CreateIndex("dbo.Cases_WitnessSessionLog", "Cases_CaseWitnesses_ID");
        }
    }
}

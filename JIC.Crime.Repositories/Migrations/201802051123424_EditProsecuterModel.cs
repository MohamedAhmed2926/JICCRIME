namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditProsecuterModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Configurations_Prosecuters", new[] { "PersonID" });

            DropForeignKey("dbo.Configurations_Prosecuters", "PersonID", "dbo.Configurations_Persons");
            //   RenameColumn(table: "dbo.Configurations_Prosecuters", name: "PersonID", newName: "Configurations_Persons_ID");
            AddColumn("dbo.Configurations_Prosecuters", "Name", c => c.String(maxLength: 100));
            AddColumn("dbo.Configurations_Prosecuters", "NationalID", c => c.String(maxLength: 14));
            DropColumn("dbo.Configurations_Prosecuters", "PersonID");
            //CreateIndex("dbo.Configurations_Prosecuters", "Configurations_Persons_ID");
        }
        
        public override void Down()
        {
            //DropIndex("dbo.Configurations_Prosecuters", new[] { "Configurations_Persons_ID" });
            //AlterColumn("dbo.Configurations_Prosecuters", "Configurations_Persons_ID", c => c.Long(nullable: false));
            //DropColumn("dbo.Configurations_Prosecuters", "NationalID");
            //DropColumn("dbo.Configurations_Prosecuters", "Name");
            //RenameColumn(table: "dbo.Configurations_Prosecuters", name: "Configurations_Persons_ID", newName: "PersonID");
            //CreateIndex("dbo.Configurations_Prosecuters", "PersonID");
        }
    }
}

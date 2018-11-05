namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditWitness : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cases_WitnessSessionLog", "WitnessTestimony");
            AddColumn("dbo.Cases_WitnessSessionLog", "WitnessTestimony", c => c.String(nullable: true));

        }
        
        public override void Down()
        {
        }
    }
}

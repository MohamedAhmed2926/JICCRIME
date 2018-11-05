namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCaseCircuitIDNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cases_Cases", new[] { "CircuitID" });
            AlterColumn("dbo.Cases_Cases", "CircuitID", c => c.Int(nullable:true));
            CreateIndex("dbo.Cases_Cases", "CircuitID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Cases_Cases", new[] { "CircuitID" });
            AlterColumn("dbo.Cases_Cases", "CircuitID", c => c.Int());
            CreateIndex("dbo.Cases_Cases", "CircuitID");
        }
    }
}

namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtt : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.Cases_Cases", "IsDeleted", c => c.Boolean());
        }

        public override void Down()
        {
          //  DropColumn("dbo.Cases_Cases", "IsDeleted");

        }
    }
}

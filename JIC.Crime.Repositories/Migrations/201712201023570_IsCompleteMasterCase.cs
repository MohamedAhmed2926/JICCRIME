namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsCompleteMasterCase : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.Cases_MasterCase", "IsComplete", c => c.Boolean(nullable: false));
         
        }
        
        public override void Down()
        {
           
            DropColumn("dbo.Cases_MasterCase", "IsComplete");
           
        }
    }
}

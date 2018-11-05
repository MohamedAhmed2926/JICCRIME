namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newcolumninCase_Lawyer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Case_Lawyer", "LawyerFileData", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Case_Lawyer", "LawyerFileData");
        }
    }
}

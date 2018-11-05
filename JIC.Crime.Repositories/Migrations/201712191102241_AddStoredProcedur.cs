namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoredProcedur : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
   "SP_SetViewAll",
   p => new
   {
       UserID=p.Int()
   },
   @"UPDATE Notifications SET BeenRead = 'True' WHERE NotifierID = @UserID"
 );
        }
        
        public override void Down()
        {
            DropStoredProcedure("SP_SetViewAll");
        }
    }
}

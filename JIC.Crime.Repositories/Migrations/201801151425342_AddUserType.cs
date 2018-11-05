namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (10, N'مدير الإدارة الجنائية', N'2018-01-15 00:00:00', N'System', NULL, NULL)");
        }
        
        public override void Down()
        {
        }
    }
}

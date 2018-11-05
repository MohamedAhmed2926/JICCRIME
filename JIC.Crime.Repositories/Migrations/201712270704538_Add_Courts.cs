namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Courts : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Configurations_Courts] ON
            INSERT INTO [dbo].[Configurations_Courts] ([ID], [Name], [ParentID], [CourtLevelID]) VALUES (1, N'محكمة القاهرة جنايات', NULL, 11)
            INSERT INTO [dbo].[Configurations_Courts] ([ID], [Name], [ParentID], [CourtLevelID]) VALUES (2, N'محكمة إسكندرية جنايات', NULL, 11)
            SET IDENTITY_INSERT [dbo].[Configurations_Courts] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}

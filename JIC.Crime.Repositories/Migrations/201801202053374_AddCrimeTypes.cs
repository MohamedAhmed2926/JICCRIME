namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCrimeTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Cases_CrimeTypes] ON
            Delete dbo.Cases_CrimeTypes;
            INSERT INTO [dbo].[Cases_CrimeTypes] ([ID], [Name], [Code]) VALUES (1, N'جنح صحفية', N'1 ');
            INSERT INTO [dbo].[Cases_CrimeTypes] ([ID], [Name], [Code]) VALUES (2, N'جنايات عادية', N'2 ');
            INSERT INTO [dbo].[Cases_CrimeTypes] ([ID], [Name], [Code]) VALUES (3, N'جنايات إرهاب', N'3 ');
            SET IDENTITY_INSERT [dbo].[Cases_CrimeTypes] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}

namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lookup : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Configurations_Lookups] ON
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (94, N'حضر', 8, N'2018-05-30 00:00:00', N'Ahmed Khalfallah', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (95, N'لم يحضر', 8, N'2018-05-30 00:00:00', N'Ahmed Khalfallah', NULL, NULL)

SET IDENTITY_INSERT [dbo].[Configurations_Lookups] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}

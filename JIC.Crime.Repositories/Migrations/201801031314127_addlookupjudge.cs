namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlookupjudge : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Configurations_Lookups] ON
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (67, N'قاضى يسار اليسار', 18, N'2018-01-03 00:00:00', N'Heba Basyony', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (68, N'قاضى يسار يسار اليسار', 18, N'2018-01-03 00:00:00', N'Heba Basyony', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (69, N'قاضى يسار يسار يسار اليسار', 18, N'2018-01-03 00:00:00', N'Heba Basyony', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (70, N'قاضى يسار يسار يسار يسار اليسار', 18, N'2018-01-03 00:00:00', N'Heba Basyony', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Configurations_Lookups] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}

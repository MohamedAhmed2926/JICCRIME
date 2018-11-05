namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlookupforlawyers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
              INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (20, N'œ—Ã…«·„Õ«„Ì', 1)

                        ");



            Sql(@"
            SET IDENTITY_INSERT [dbo].[Configurations_Lookups] ON
            INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (98, N'ÃœÊ· ⁄«„', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)
            INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (99, N'«» œ«∆Ï', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)
            INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (100, N'«” ∆‰«›', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)
            INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (101, N'‰ﬁ÷', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)

            SET IDENTITY_INSERT [dbo].[Configurations_Lookups] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}

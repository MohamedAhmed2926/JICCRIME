namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addlookupDocumentsTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Configurations_Lookups] ON

INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (80 , N'Õ—“', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (81 , N'√„— «Õ«·…', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (82 , N'√œ·… À»Ê ', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (83 , N'„–ﬂ—… √”»«» «·Õﬂ„', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (84 , N'‰”Œ… «·Õﬂ„ «·‰Â«∆Ì', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (85 , N'„Õ÷— Ã„⁄ «·«” œ·«·« ', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (86 , N'  ÕﬁÌﬁ«  «·‰Ì«»…', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (87 , N'«⁄·«‰« ', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID],[Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (88 , N' ﬁ—Ì— «·ÃÂ«  «·„⁄«Ê‰…', 10, N'2018-01-04 00:00:00', N'heba adel', NULL, NULL)

SET IDENTITY_INSERT [dbo].[Configurations_Lookups] OFF
");
        }

        public override void Down()
        {
        }
    }
}

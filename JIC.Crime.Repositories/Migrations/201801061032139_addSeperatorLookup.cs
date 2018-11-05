namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSeperatorLookup : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            SET IDENTITY_INSERT [dbo].[CourtConfigurations_Cycles] ON
            INSERT INTO [dbo].[CourtConfigurations_Cycles] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (5, N'فاصل الإنعقاد الأول', N'2017-01-05 00:00:00', N'System', NULL, NULL)
            INSERT INTO [dbo].[CourtConfigurations_Cycles] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (6, N'فاصل الإنعقاد الثانى', N'2018-01-05 00:00:00', N'System', NULL, NULL)
            SET IDENTITY_INSERT [dbo].[CourtConfigurations_Cycles] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}

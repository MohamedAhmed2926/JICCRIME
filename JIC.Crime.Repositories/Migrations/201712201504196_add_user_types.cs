namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_user_types : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (1, N'مدير نظام محكمة إبتدائية', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (2, N'قاضى', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (3, N'أمين السر', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (4, N'موظف الجدول', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (5, N'مدير نظام محكمة إبتدائية', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (6, N'رئيس المحكمة', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (7, N'موظف إستعلام', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (8, N'مدير نظام مركز المعلومات', N'2017-12-20 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (9, N'موظف التنفيذ', N'2017-12-20 00:00:00', N'System', NULL, NULL)
");
        }
        
        public override void Down()
        {
            Sql(@"Delete From Security_UserTypes");
        }
    }
}

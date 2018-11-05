namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Security_UserTypes] ([ID], [Name], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (11, N'„ÊŸ› ﬁÌœ »Ì«‰«  ', N'2017-12-20 00:00:00', N'System', NULL, NULL)
");
            //            Sql(String.Format(@"
            //                SET IDENTITY_INSERT CustomRoles ON 
            //                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
            //                SET IDENTITY_INSERT CustomRoles OFF ",
            //               ((int)Base.SystemUserTypes.DataEntry), Base.SystemUserTypes.DataEntry.ToString()));

            //            Sql(@"
            //                SET IDENTITY_INSERT [dbo].[Security_Users] ON
            //                INSERT INTO [dbo].[Security_Users] ([Id], [FullName], [UserTypeID], [CourtID], [TitleID], [Active], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy], [FailedPWCount], [MobileNo], [PersonsId], [Locked], [ActiveDateFrom], [ActiveDateTo], [ProsecutionID], [LevelID], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (11, N'„Õ„œÌ‰'
            //,10, 1, NULL, 1, N'2017-12-26 20:33:42', N'System', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'JICAdmin@jic.com', 0, N'ACL+DpW6TAQfEAzjKLSJIkMs7ONnNPC5/iD+B5Xwqox0zrpgedV6iv8gDpCUqykl+A==', N'53687ca3-7cb5-4266-8aea-847da839f94b', N'', 0, 0, NULL, 1, 0, N'JICAdmin')
            //                INSERT INTO [dbo].[Security_UserRole] ([RoleId], [UserId], [Security_Users_Id], [Security_Role_Id]) VALUES (8, 1, NULL, NULL)
            //                SET IDENTITY_INSERT [dbo].[Security_Users] OFF
            //            ");
        }
        
        public override void Down()
        {
        }
    }
}

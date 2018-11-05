namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Pros : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Configurations_Prosecutions] ON
INSERT INTO [dbo].[Configurations_Prosecutions] ([ID], [Name], [ParentID], [CourtID], [Code]) VALUES (2, N'نيابة القاهرة الكلية جنايات', NULL, 1, N'1 ')
INSERT INTO [dbo].[Configurations_Prosecutions] ([ID], [Name], [ParentID], [CourtID], [Code]) VALUES (3, N'نيابة الإسكندرية الكلية جنايات', NULL, 2, N'2 ')
INSERT INTO [dbo].[Configurations_Prosecutions] ([ID], [Name], [ParentID], [CourtID], [Code]) VALUES (4, N'نيابة القاهرة الجديدة الجزئية', 2, 1, N'3 ')
INSERT INTO [dbo].[Configurations_Prosecutions] ([ID], [Name], [ParentID], [CourtID], [Code]) VALUES (5, N'نيابة الإسكندرية الجزئية', 3, 2, N'4 ')
SET IDENTITY_INSERT [dbo].[Configurations_Prosecutions] OFF
");
            Sql(@"SET IDENTITY_INSERT [dbo].[Configurations_PoliceStations] ON
INSERT INTO [dbo].[Configurations_PoliceStations] ([ID], [Name], [ProsecutionID]) VALUES (1, N'قسم شرطة التجمع الاول', 4)
INSERT INTO [dbo].[Configurations_PoliceStations] ([ID], [Name], [ProsecutionID]) VALUES (2, N'قسم شرطة التجمع الاول', 4)
INSERT INTO [dbo].[Configurations_PoliceStations] ([ID], [Name], [ProsecutionID]) VALUES (3, N'قسم شرطة سموحه', 5)
SET IDENTITY_INSERT [dbo].[Configurations_PoliceStations] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}

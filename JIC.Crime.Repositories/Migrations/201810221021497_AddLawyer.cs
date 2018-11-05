namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLawyer : DbMigration
    {
        public override void Up()
        {
//            CreateTable(
//                 "dbo.Lawyer",
//             c => new
//             {

//                 ID = c.Long(nullable: false, identity: true),
//                 PersonID = c.Long(nullable: false),
//                 CardNumber = c.Long(nullable: false),
//                 LevelID = c.Int(nullable: false),
//                 //file data



//             })
//                    .PrimaryKey(t => t.ID)
//             .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
//             .ForeignKey("dbo.Configurations_Lookups", t => t.LevelID)
//          ;
//            CreateIndex("dbo.Lawyer", "CardNumber", unique: true);
           


//            Sql(@"
//                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (20, N'ÏÑÌÉÇáãÍÇãí', 1)
       
//            ");



//            Sql(@"
//SET IDENTITY_INSERT [dbo].[Configurations_Lookups] ON
//INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (98, N'ÌÏæá ÚÇã', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)
//INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (99, N'ÇÈÊÏÇÆì', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)
//INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (100, N'ÇÓÊÆäÇÝ', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)
//INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (101, N'äÞÖ', 20, N'2017-01-01 00:00:00', N'System', NULL, NULL)

//SET IDENTITY_INSERT [dbo].[Configurations_Lookups] OFF
//");




            //    DropColumn("dbo.Cases_CaseWitnesses", "WitnessDocument");
            //  DropColumn("dbo.Cases_CaseWitnesses", "FileDataDocument");
        }

        public override void Down()
        {
         //   AddColumn("dbo.Cases_CaseWitnesses", "FileDataDocument", c => c.Binary());
         //   AddColumn("dbo.Cases_CaseWitnesses", "WitnessDocument", c => c.String());
        }
    }
}

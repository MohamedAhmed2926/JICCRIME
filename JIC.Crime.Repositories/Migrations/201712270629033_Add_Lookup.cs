namespace JIC.Crime.Repositories.Migrations
{
    using JIC.Crime.Repositories.Repositories;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Lookup : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (2, N'أنواع المنشآت', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (3, N'تصنيف المنشآت التجارية', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (4, N'درجات التقاضي', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (5, N'أنواع الإجراءات', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (6, N'حالات المتهمين تبعا للمحكمة', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (7, N'حالات المتهمين تبعا لقسم الشرطة', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (8, N'أنواع الحضور', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (9, N'حالات القضية', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (10, N'أنواع المرفقات', 1)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (11, N'درجات القضاة', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (12, N'التهم', 1)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (13, N'نوع النقل', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (14, N'أنواع العقوبات', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (15, N'نوع الأخفاق فى الدخول', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (16, N'صفة المستأنف', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (17, N'سبب إستئناف النيابة', 0)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (18, N'أنواع قضاء المنصة', 0)
                ");
            Sql(@"
SET IDENTITY_INSERT [dbo].[Configurations_Lookups] ON
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (1, N'مصري', 1, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (2, N'سعودي', 1, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (3, N'ألماني', 1, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (4, N'وزارة', 2, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (5, N'جهة حكومية', 2, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (6, N'شركة', 2, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (7, N'شركة صناعية', 3, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (8, N'شركة تجارية', 3, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (9, N'شركة مقاولات', 3, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (10, N'جزئي', 4, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (11, N'كلي', 4, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (12, N'نقض', 4, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (13, N'قضية', 5, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (14, N'معارضة', 5, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (15, N'أمر جنائي', 5, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (16, N'محبوس', 6, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (17, N'هارب', 6, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (18, N'غير مطلوب', 6, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (19, N'محبوس', 7, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (20, N'هارب', 7, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (21, N'غير مطلوب', 7, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (22, N'حضور شخصي', 8, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (23, N'حضور اعتباري', 8, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (24, N'حضور بتوكيل', 8, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (25, N'غيابي', 8, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (26, N'جديدة', 9, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (27, N'متداولة', 9, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (28, N'حكم قطعي', 9, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (29, N'حكم تمهيدي', 9, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (30, N'محجوزة للحكم', 9, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (31, N'متوقفة علي النظام', 9, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (32, N'شيك', 10, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (33, N'عقد', 10, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (34, N'مذكرة', 10, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (35, N'عريضة', 10, N'2017-01-01 00:00:00', N'Systm', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (37, N'قاضي', 11, N'2017-01-01 00:00:00', N'system', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (38, N'رئيس ب', 11, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (39, N'مستشار بمحاكم الأستئنناف', 11, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (40, N'نائب رئيس الإستئناف', 11, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (41, N'رئيس إستئناف', 11, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (42, N'مستشار محكمة النقض', 11, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (43, N'نائب رئيس النقض', 11, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (45, N'ضرب', 12, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (46, N'سرقة', 12, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (47, N'نقل ادارى', 13, N'2017-05-28 00:00:00', N'system', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (48, N'حبس', 14, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (49, N'غرامة', 14, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (50, N'حبس وغرامة', 14, N'2017-01-01 00:00:00', N'System', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (51, N'BadPassword', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (52, N'SuccessLogin', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (53, N'AccountLocked', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (54, N'AccountExpired', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (55, N'NotAllowedAccess', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (56, N'HackTry', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (57, N' UnknownUsername', 15, N'2017-05-06 00:00:00', N'hassanshaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (58, N'LastFallAndLocked', 15, N'2017-05-06 00:00:00', N'hassan shaddad', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (59, N'النيابة', 16, N'2017-06-11 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (60, N'المتهم', 16, N'2017-06-11 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (61, N'مدعى بالحق المدنى', 16, N'2017-06-11 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (62, N'ثبوت', 17, N'2017-06-12 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (63, N'خطأ', 17, N'2017-06-12 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (64, N'قاضى المنصة', 18, N'2017-06-24 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (65, N'قاضى اليمين', 18, N'2017-06-24 00:00:00', N'Ahmed Ghorab', NULL, NULL)
INSERT INTO [dbo].[Configurations_Lookups] ([ID], [Name], [CategoryID], [CreatedAt], [CreatedBy], [LastModifiedAt], [LastModifiedBy]) VALUES (66, N'قاضى اليسار', 18, N'2017-06-24 00:00:00', N'Ahmed Ghorab', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Configurations_Lookups] OFF
");


        }
        
        public override void Down()
        {
        }
    }
}

namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_document_type_encode : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'حرز' WHERE ID = 80;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'أمر احالة' WHERE ID = 81;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'أدلة ثبوت' WHERE ID = 82;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'مذكرة أسباب الحكم' WHERE ID = 83;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'نسخة الحكم النهائي' WHERE ID = 84;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'محضر جمع الاستدلالات' WHERE ID = 85;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N' تحقيقات النيابة' WHERE ID = 86;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'اعلانات' WHERE ID = 87;
            UPDATE [dbo].[Configurations_Lookups] SET Name = N'تقرير الجهات المعاونة' WHERE ID = 88;"
            );
        }
        
        public override void Down()
        {
        }
    }
}

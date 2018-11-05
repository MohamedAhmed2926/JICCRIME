namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_lookup_categories : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (1, N'الجنسيات', 1)
                INSERT INTO [dbo].[Configurations_LookupCategories] ([ID], [Name], [ManagedByUser]) VALUES (19, N'البلاد', 1)
            ");
        }
        
        public override void Down()
        {
        }
    }
}

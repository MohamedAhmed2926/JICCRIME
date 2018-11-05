namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourtCrimes : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Configurations_Courts_Crimes] ([ID], [CourtID], [CrimeTypeID]) VALUES (1, 1, 1)
INSERT INTO [dbo].[Configurations_Courts_Crimes] ([ID], [CourtID], [CrimeTypeID]) VALUES (2, 1, 2)
INSERT INTO [dbo].[Configurations_Courts_Crimes] ([ID], [CourtID], [CrimeTypeID]) VALUES (3, 1, 3)
INSERT INTO [dbo].[Configurations_Courts_Crimes] ([ID], [CourtID], [CrimeTypeID]) VALUES (4, 2, 2)
INSERT INTO [dbo].[Configurations_Courts_Crimes] ([ID], [CourtID], [CrimeTypeID]) VALUES (5, 2, 3)
");
        }
        
        public override void Down()
        {
        }
    }
}

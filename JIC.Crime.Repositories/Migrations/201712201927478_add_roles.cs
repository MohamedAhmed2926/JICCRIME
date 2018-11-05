namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_roles : DbMigration
    {
        public override void Up()
        {
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.ElementaryCourtAdministrator), Base.SystemUserTypes.ElementaryCourtAdministrator.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.Judge), Base.SystemUserTypes.Judge.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.Secretary), Base.SystemUserTypes.Secretary.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.schedualEmployee), Base.SystemUserTypes.schedualEmployee.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.InitialCourtAdministrator), Base.SystemUserTypes.InitialCourtAdministrator.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.CourtHead), Base.SystemUserTypes.CourtHead.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.InquiriesEmployee), Base.SystemUserTypes.InquiriesEmployee.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.JICAdmin), Base.SystemUserTypes.JICAdmin.ToString()));
            Sql(String.Format(@"
                SET IDENTITY_INSERT CustomRoles ON 
                INSERT INTO CustomRoles  ([ID], [Name]) VALUES ({0},N'{1}') 
                SET IDENTITY_INSERT CustomRoles OFF ", 
                ((int)Base.SystemUserTypes.ImplementationEmployee), Base.SystemUserTypes.ImplementationEmployee.ToString()));
        }
        
        public override void Down()
        {
        }
    }
}

namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomUserClaims", newName: "Security_UserClaim");
            RenameTable(name: "dbo.CustomUserLogins", newName: "Security_UserLogin");
            RenameTable(name: "dbo.CustomUserRoles", newName: "Security_UserRole");
            RenameTable(name: "dbo.CustomRoles", newName: "Security_Role");
            RenameColumn(table: "dbo.Security_UserRole", name: "CustomRole_Id", newName: "Security_Role_Id");
            RenameIndex(table: "dbo.Security_UserRole", name: "IX_CustomRole_Id", newName: "IX_Security_Role_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Security_UserRole", name: "IX_Security_Role_Id", newName: "IX_CustomRole_Id");
            RenameColumn(table: "dbo.Security_UserRole", name: "Security_Role_Id", newName: "CustomRole_Id");
            RenameTable(name: "dbo.Security_Role", newName: "CustomRoles");
            RenameTable(name: "dbo.Security_UserRole", newName: "CustomUserRoles");
            RenameTable(name: "dbo.Security_UserLogin", newName: "CustomUserLogins");
            RenameTable(name: "dbo.Security_UserClaim", newName: "CustomUserClaims");
        }
    }
}

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using JIC.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JIC.Base.Entities.Models
{
    public class Security_UserRole : IdentityUserRole<int> { }
    public class Security_UserClaim : IdentityUserClaim<int> { }
    public class Security_UserLogin : IdentityUserLogin<int> { }

    public class Security_Role : IdentityRole<int, Security_UserRole>
    {
        public Security_Role() { }
        public Security_Role(string name) { Name = name; }
    }


    public class Security_RoleStore : RoleStore<Security_Role, int, Security_UserRole>
    {
        public Security_RoleStore(DbContext context)
            : base(context)
        {
        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User :  IdentityUser<int, Security_UserLogin, Security_UserRole, Security_UserClaim>, IUser<int>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User,int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class DBContext<TUser> : IdentityDbContext<TUser, Security_Role, int, Security_UserLogin, Security_UserRole, Security_UserClaim> where TUser : IdentityUser<int, Security_UserLogin, Security_UserRole, Security_UserClaim>
    {
        static DBContext()
        {
            // Mohamed EL-Saeed
            //This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // Fixing "Provider not loaded" error.
            // for more info refer to this url http://robsneuron.blogspot.com/2013/11/entity-framework-upgrade-to-6.html
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DBContext()
            : base(SystemConfigurations.Settings_ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            //builder.Entity<User>().HasKey<string>(l => l.Id);
            builder.Entity<Security_UserLogin>().HasKey<int>(l => l.UserId);
            builder.Entity<Security_Role>().HasKey<int>(r => r.Id);
            builder.Entity<Security_UserRole>().HasKey(r => new
            {
                r.RoleId,
                r.UserId
            });
        }
    }
}
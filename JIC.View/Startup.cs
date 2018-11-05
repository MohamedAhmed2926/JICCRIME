using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JIC.Crime.View.Startup))]
namespace JIC.Crime.View
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
